using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static float height = 0.3f;

    private static int nextId = 0;
    public int ID;

    public float movementSpeed;
    
    public int enemyType;

    public int health;
    public int repulsiveForce;
    public float attackDelay;
    public float timeSinceLastAttack;
    public float attackRange;
    public float attackSpeed;
    public int attackDamage;
    public bool isAttacking;
    public bool canMove;
    public int weaponType;
    public GameObject weapon;
    public Vector3 oldPos;
    public Vector3 norm;

    public PlayerControler target { get; set; }

    // Use this for initialization
    public void Init(int enemyType)
    {
        ID = nextId++;
        repulsiveForce = 120;
        this.enemyType = enemyType;
        canMove = true;
        enemyTypeInitializer();
    }

    //Initialize enemyType values to presets
    private void enemyTypeInitializer()
    {
        switch (enemyType)
        {
            case 0:
                attackDelay = 1;
                timeSinceLastAttack = attackDelay;
                health = 1;
                movementSpeed = 1f;
                attackRange = 0.75f;
                attackSpeed = 5f;
                attackDamage = 1;
                break;
            default:
                attackDelay = 1;
                timeSinceLastAttack = attackDelay;
                health = 1;
                movementSpeed = 1f;
                attackRange = 0.75f;
                attackSpeed = 5f;
                attackDamage = 1;
                break;
        }

        weapon = gameObject.transform.Find("Weapon").gameObject;
        weapon.SetActive(false);
        isAttacking = false;
    }


    //Move towards target with respect to movement speed and stop moving when within attacking range for x
    public void moveTowardsTarget()
    {
        Vector3 newPosition;
        Vector3 newFacing;

        Debug.Log("Enemy #" + ID + ": Moving towards target");
        
        //move towards target player position minus attack range
        newPosition = Vector3.MoveTowards(gameObject.transform.position, target.transform.position - new Vector3(attackRange, 0, 0), movementSpeed * Time.deltaTime);

        //fix y position to default enemy height
        newPosition = new Vector3(newPosition.x, height, newPosition.z);

        //Find rotation to face target player
        newFacing = Vector3.RotateTowards(gameObject.transform.forward, target.transform.position - gameObject.transform.position, movementSpeed * Time.deltaTime, 0.0f);

        //prevent rotation on the y axis
        newFacing.y = 0;

        //rotate the enemy closer to facing its target and set the enemy position to new position closer to target and facing the
        gameObject.transform.forward = newFacing;
        gameObject.transform.position = newPosition;
    }

    //deal damage to enemy health
    public void dealDamage(int damage)
    {
        health = health - damage;
    }

    //return true if enemy health is less than equal to zero, else return false
    public bool isDead()
    {
        Debug.Log("Enemy #" + ID + " is Dead: " + (health <= 0));
        return (health <= 0);
    }

    //return true if the absolute value of the distance between the enemy and the player is less than equal to the enemy's attack range
    public bool isInAttackRange()
    {
        return isInAttackRange(target);
    }

    //return true if the absolute value of the distance between the enemy and the player is less than equal to the enemy's attack range
    public bool isInAttackRange(PlayerControler targetedPlayer)
    {
        float offset = 0.1f;

        return isInAttackRange(targetedPlayer, offset);
    }

    //return true if the absolute value of the distance between the enemy and the player is less than equal to the enemy's attack range + an
    //offset to account for player movements during attack frames
    public bool isInAttackRange(PlayerControler targetedPlayer, float offset)
    {
        //add an extra offset to account for y movement position being set back to the enemy height in moveTowardsTarget() 
        return (Mathf.Abs(Vector3.Distance(gameObject.transform.position, targetedPlayer.transform.position)) <= attackRange + offset);
    }

    //TODO
    //check all players to see if attack hits any of them
    public void attack(PlayerControler[] players)
    {
        Debug.Log("Enemy #" + ID + ": Attacking");

        if (!isAttacking)
        {
            StartCoroutine(swingWeapon(players));
        }
    }

    public IEnumerator swingWeapon(PlayerControler[] players)
    {
        if (!isAttacking)
        {
            if (timeSinceLastAttack <= attackDelay)
            {
                timeSinceLastAttack += Time.deltaTime;
            }
            else
            {
                weapon.transform.localEulerAngles = Vector3.zero;
                weapon.SetActive(true);
                isAttacking = true;
                timeSinceLastAttack = 0;
            }
        }

        while (weapon.transform.localEulerAngles.y < 90)
        {
            float yRot = 0;
            yRot += 90 * attackSpeed * Time.deltaTime;
            weapon.transform.Rotate(new Vector3(0f, yRot, 0f));
            yield return null;
        }

        for (int i = 0; i < players.Length; i++)
        {
            if (isInAttackRange(players[i], 0.2f) && isAttacking)
            {
                Debug.Log("Player hit by enemy");
                players[i].SendMessage("playerHitByEnemyWeapon", 1);
            }
        }

        isAttacking = false;
        weapon.SetActive(false);
    }

    public bool ApplyForce() {
        if (Vector3.SqrMagnitude(transform.position - oldPos) < 18)
        {
            transform.GetComponent<Rigidbody>().AddForce(norm * repulsiveForce * Time.deltaTime, ForceMode.Impulse);
            transform.GetComponent<Rigidbody>().isKinematic = false;
            return true;
        }
        else
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            return false;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Sword" && !collision.transform.parent.GetComponentInParent<PlayerControler>().GetSwordCollider)
        {
            dealDamage(1);
            Debug.Log("Enemy #" + ID + " hit by sword, health down to " + health);
        }
    }
}

