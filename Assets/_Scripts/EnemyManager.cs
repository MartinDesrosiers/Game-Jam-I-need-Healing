using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int NumberOfEnemyPrefabs = 3;

    public Enemy[] enemyPrefabs;
    List<Enemy> enemyList;

    public PlayerControler[] players;
    public MageControl healer;

    Camera mainCamera;

    float timeSinceLastSpawn; //time since an enemy was last spawned
    public int maxNumberOfEnemies; //max number of enemies that you can have at a single time
    public float spawnRate; //rate at which enemies are spawned

    // Use this for initialization
    void Start()
    {
        loadPrefabs();

        enemyList = new List<Enemy>();
        players = GameObject.FindObjectsOfType<PlayerControler>();
        healer = GameObject.FindObjectOfType<MageControl>(); 
        mainCamera = Camera.main;
        timeSinceLastSpawn = 0.0f;
        spawnRate = 5.0f;
        maxNumberOfEnemies = 1;
    }

    
    // Update is called once per frame
    void Update()
    {
        spawnsUpdate();
        movementsUpdate();
    }

    void loadPrefabs()
    {
        enemyPrefabs = new Enemy[NumberOfEnemyPrefabs];

        //load all existing enemy prefabs
        for (int i = 0; i < NumberOfEnemyPrefabs; i++)
        {
            enemyPrefabs[i] = Resources.Load<Enemy>("_Prefabs/Enemies/Enemy" + i);
        }
    }

    
    void spawnsUpdate()
    {
        //if an enemy hasn't been spawned 
        if (timeSinceLastSpawn >= spawnRate && enemyList.Count < maxNumberOfEnemies)
        {
            createEnemy();
            timeSinceLastSpawn = 0;
            Debug.Log("EnemyList capacity: " + enemyList.Count);
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }

    void movementsUpdate()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            Debug.Log("Enemy Health: " + (enemyList[i]).health);
            
            //if isDead remove the enemy from the list, apply movement of specific enemy in enemyList
            if (((Enemy)enemyList[i]).isDead())
            {
                Debug.Log("Enemy at index " + i + " is dead");
                Destroy(enemyList[i].gameObject, 0);
                enemyList.RemoveAt(i);
                i--;

                Debug.Log("EnemyList size: " + enemyList.Count);
            }
            else
            {
                move(enemyList[i]);
            }
        }
    }

    //Create enemy with random type and random position
    void createEnemy()
    {
        createEnemy(Random.Range(0, NumberOfEnemyPrefabs));
    }

    //Create an anemy with a random position 
    void createEnemy(int enemyType)
    {
        Vector3 position;

        //Generate a random value between 0 & 1. If > 0.5 spawn on the right side of the camera,
        //otherwise spawn on the left side of the camera
        if (Random.value > 0.5f)
        {
            //spawn enemy on the right side of the camera
            position = new Vector3((mainCamera.transform.position.x + 15), Enemy.height, Random.Range(-3.0f, 6.0f));
        }
        else
        {
            //spawn enemy on the left side of the camera
            position = new Vector3((mainCamera.transform.position.x - 15), Enemy.height, Random.Range(-3.0f, 6.0f));
        }

        createEnemy(enemyType, position);
    }

    //Create an enemy of the specified type at the specified position and add it to the enemyList
    void createEnemy(int enemyType, Vector3 position)
    {
        //create and instance of the specified enemy at the specified position
        Enemy enemyInstance = Instantiate<Enemy>(enemyPrefabs[enemyType], position, Quaternion.identity);

        //initialize the enemy
        enemyInstance.Init(enemyType);

        //add the enemy to the enemy list
        enemyList.Add(enemyInstance);
        
    }

    //If the enemy is not dead, move it towards the closest player or make it attack the closest player, otherwise return true to indicate that the enemy is dead 
    void move(Enemy enemy)
    {
        //if the enemy has no target player or if the player is dead, find a new target
        if (enemy.target == null || enemy.target.isDead())
        {
            Debug.Log("Finding new target for enemy#" + enemy.ID);
            findTargetForEnemy(enemy);
        }

        //if the target player is in attack range, attack the player otherwise closer to the player
        if (enemy.GetComponent<Enemy>().canMove)
        {
            if (enemy.isInAttackRange())
            {
                enemy.attack(players);
            }
            else
            {
                enemy.moveTowardsTarget();
            }
        }
        else
        {
            if (!enemy.ApplyForce())
                enemy.GetComponent<Enemy>().canMove = true;
        }
    }

    //Find the closest player and set that player as the enemy's target
    public void findTargetForEnemy(Enemy enemy)
    {
        int closestPlayerIndex = -1;
        float distanceToClosestPlayer = -1;

        //find the closest player by checking the absolute value distance between each player and the enemy 
        for (int i = 0; i < players.Length; i++)
        {
            //set the closest player to the first living player found in players list
            if (closestPlayerIndex == -1)
            {
                if (!players[i].isDead())
                {
                    closestPlayerIndex = i;
                    distanceToClosestPlayer = Vector3.Distance(enemy.transform.position, players[i].transform.position);
                }
            }
            else
            {
                if (!players[i].isDead())
                {
                    //check to see if the player i is closer to the enemy than any other players in the players list
                    if (Mathf.Abs(Vector3.Distance(enemy.transform.position, players[i].transform.position)) < Mathf.Abs(distanceToClosestPlayer))
                    {
                        closestPlayerIndex = i;
                        distanceToClosestPlayer = Vector3.Distance(enemy.transform.position, players[i].transform.position);
                    }
                }
            }
        }

        //set the enemy's target to the closest player, if no players were found set the target to null
        if (closestPlayerIndex != -1)
        {
            enemy.target = (players[closestPlayerIndex]);
        }
        else
        {
            enemy.target = null;
        }
    }
}
