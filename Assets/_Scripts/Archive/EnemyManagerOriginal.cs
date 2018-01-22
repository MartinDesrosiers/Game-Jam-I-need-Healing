using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerOriginal : MonoBehaviour
{
    public static int NumberOfEnemyPrefabs = 1;

    private static float x_MinBound = -15.0f, x_MaxBound = 80.0f, z_MinBound = -3.0f, z_MaxBound = 6.0f;

    public GameObject[] enemyPrefabs; 
    ArrayList enemyList;
    PlayerControler[] players;
    Camera mainCamera;

    float timeSinceLastSpawn; //time since an enemy was last spawned
    public int maxNumberOfEnemies; //max number of enemies that you can have at a single time
    public float spawnRate; //rate at which enemies are spawned

    // Use this for initialization
    void Start()
    {
        loadPrefabs();

        enemyList = new ArrayList();
        players = GameObject.FindObjectsOfType<PlayerControler>();
        mainCamera = Camera.main;

        timeSinceLastSpawn = 0;
        spawnRate = 5;
        maxNumberOfEnemies = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //if an enemy hasn't been spawned 
        if(timeSinceLastSpawn > spawnRate && enemyList.Capacity < maxNumberOfEnemies)
        {
            createEnemy();
            timeSinceLastSpawn = 0;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }

        

        for (int i = 0; i < enemyList.Capacity; i++)
        {
            //apply movement of specific enemy in enemyList, if isDead remove the enemy from the list
            if(((Enemy)enemyList[i]).move())
            {
                enemyList.RemoveAt(i);
                i--;
            }
        }
    }

    void loadPrefabs()
    {
        enemyPrefabs = new GameObject[NumberOfEnemyPrefabs];

        //load all existing enemy prefabs
        for (int i = 0; i < NumberOfEnemyPrefabs; i++)
        {
            enemyPrefabs[i] = Resources.Load<GameObject>("_Prefabs/Enemies/Enemy" + i);
        }
    }

    //Create enemy with random type and random position
    void createEnemy()
    {
        int type = 0;

        createEnemy(type);
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
            position = new Vector3((mainCamera.transform.position.x + 5), 0.15f, Random.Range(-3.0f, 6.0f));
        }
        else
        {
            //spawn enemy on the left side of the camera
            position = new Vector3((mainCamera.transform.position.x - 5), 0.15f, Random.Range(-3.0f, 6.0f));
        }

        createEnemy(enemyType, position);
    }

    //Create an enemy of the specified type at the specified position and add it to the enemyList
    void createEnemy(int enemyType, Vector3 position)
    {
        enemyList.Add(new Enemy(enemyType, Instantiate(enemyPrefabs[enemyType], position, Quaternion.identity), players));
    }

    private class Enemy
    {
        GameObject enemyPrefab;

        public int movementSpeed;

        //int ID;
        int enemyType;
        PlayerControler[] players;


        float health;
        public float attackRange;

        PlayerControler target;


        /*
         *  Constructor
         * 
         *  @param  enemyType The type of monster you want to create
         *  @param  ID  Position in the enemyList
         */
        public Enemy(int enemyType, GameObject enemyPrefab, PlayerControler[] players)
        {
            enemyPrefab = (GameObject)Instantiate(enemyPrefab);

            this.enemyType = enemyType;
            this.players = players;

            health = 100.0f;

            target = null;
        }


        //If the enemy is not dead, move it towards the closest player or make it attack the closest player, otherwise return true to indicate that the enemy is dead 
        public bool move()
        {
            bool enemyIsDead = isDead();

            //if enemy isn't dead move it closer to the target player
            if (!enemyIsDead)
            {
                //if the enemy has no target player or if the player is dead, find a new target
                if (target == null || target.isDead())
                {
                    findTarget();
                }

                //if the target player is in attack range, attack the player otherwise closer to the player
                if (isInAttackRange())
                {
                    attack();
                }
                else
                {
                    moveTowardsTarget();
                }
              
            }

            return enemyIsDead;
        }

        public void findTarget()
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
                        distanceToClosestPlayer = Vector3.Distance(enemyPrefab.transform.position, players[i].transform.position);
                    }
                }
                else
                {
                    //check to see if the player i is closer to the enemy than any other players in the players list
                    if(Mathf.Abs(Vector3.Distance(enemyPrefab.transform.position, players[i].transform.position)) < Mathf.Abs(distanceToClosestPlayer))
                    {
                        closestPlayerIndex = i;
                        distanceToClosestPlayer = Vector3.Distance(enemyPrefab.transform.position, players[i].transform.position);
                    }
                }
            }

            //set the enemy's target to the closest player, if no players were found set the target to null
            if (closestPlayerIndex != -1)
            {
                target = players[closestPlayerIndex];
            }
            else
            {
                target = null;
            }
        }

        //TODO
        //check all players to see if attack hits any of them
        public void attack()
        {

        }

        //TODO
        public void moveTowardsTarget()
        {

        }

        //deal damage to enemy health
        public void dealDamage(float damage)
        {
            health -= damage;
        }

        //return true if enemy health is less than equal to zero, else return false
        public bool isDead()
        {
            return (health <= 0);
        }

        //return true if the absolute value of the distance between the enemy and the player is less than equal to the enemy's attack range
        public bool isInAttackRange()
        {
            return (Mathf.Abs(Vector3.Distance(enemyPrefab.transform.position, target.transform.position)) <= attackRange);
        }
    }

    struct EnemyType
    {
        string name;
        float movementSpeed;
        float attackDelay;
    }
}
