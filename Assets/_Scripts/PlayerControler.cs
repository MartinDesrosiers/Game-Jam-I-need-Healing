using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    Collider boxCol;
    public float m_speed;
    char m_playerNumber;
    int m_health;
    bool m_isAttacking;

    public bool GetSwordCollider { get { return boxCol.enabled; } }
    // Use this for initialization
    public void Init(char number, Material mat)
    {
        boxCol = transform.GetChild(1).GetChild(0).GetComponent<Collider>();
        boxCol.enabled = false;
        m_playerNumber = number;
        m_isAttacking = false;
        transform.GetChild(0).GetComponent<Renderer>().material = mat;
        m_health = 5;
    }

    // Update is called once per frame
    void Update () {
        if (!isDead())
        {
            switch (m_playerNumber)
            {
                case '1':
                    if (Input.GetKey("a") && !Input.GetKey("d"))
                    {
                        if(transform.localEulerAngles.y != 180)
                            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        if(transform.position.x + (Vector3.one.x * m_speed * Time.deltaTime + 0.01) > StageManager.x_MinBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("d") && !Input.GetKey("a"))
                    {
                        if (transform.localEulerAngles.y != 0)
                            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        if (transform.position.x + (Vector3.one.x * m_speed * Time.deltaTime + 0.01) < StageManager.x_MaxBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("s") && !Input.GetKey("w"))
                    {
                        if (transform.localEulerAngles.y != 90)
                            transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                        if (transform.position.z + (Vector3.one.z * m_speed * Time.deltaTime + 0.01) > StageManager.z_MinBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("w") && !Input.GetKey("s"))
                    {
                        if (transform.localEulerAngles.y != 270)
                            transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        if (transform.position.z + (Vector3.one.z * m_speed * Time.deltaTime + 0.01) < StageManager.z_MaxBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKeyDown("q"))
                    {
                        if (!m_isAttacking)
                        {
                            m_isAttacking = true;
                            Attack();
                        }
                    }
                    if (Input.GetKeyDown("e"))
                    {
                        GameManager.Instance.m_mage.GetComponent<MageControl>().CallTheMage(0);
                    }
                    break;
                case '2':
                    if (Input.GetKey("left") && !Input.GetKey("right"))
                    {
                        if (transform.localEulerAngles.y != 180)
                            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        if (transform.position.x + (Vector3.one.x * m_speed * Time.deltaTime + 0.01) > StageManager.x_MinBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("right") && !Input.GetKey("left"))
                    {
                        if (transform.localEulerAngles.y != 0)
                            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        if (transform.position.x + (Vector3.one.x * m_speed * Time.deltaTime + 0.01) < StageManager.x_MaxBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("down") && !Input.GetKey("up"))
                    {
                        if (transform.localEulerAngles.y != 90)
                            transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                        if (transform.position.z + (Vector3.one.z * m_speed * Time.deltaTime + 0.01) > StageManager.z_MinBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("up") && !Input.GetKey("down"))
                    {
                        if (transform.localEulerAngles.y != 270)
                            transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        if (transform.position.z + (Vector3.one.z * m_speed * Time.deltaTime + 0.01) < StageManager.z_MaxBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKeyDown("."))
                    {
                        if (!m_isAttacking)
                        {
                            m_isAttacking = true;
                            Attack();
                        }
                    }
                    if (Input.GetKeyDown("/"))
                    {
                        GameManager.Instance.m_mage.GetComponent<MageControl>().CallTheMage(1);
                    }
                    break;
                case '3':
                    if (Input.GetKey("h") && !Input.GetKey("k"))
                    {
                        if (transform.localEulerAngles.y != 180)
                            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        if (transform.position.x + (Vector3.one.x * m_speed * Time.deltaTime + 0.01) > StageManager.x_MinBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("k") && !Input.GetKey("h"))
                    {
                        if (transform.localEulerAngles.y != 0)
                            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        if (transform.position.x + (Vector3.one.x * m_speed * Time.deltaTime + 0.01) < StageManager.x_MaxBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("j") && !Input.GetKey("u"))
                    {
                        if (transform.localEulerAngles.y != 90)
                            transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                        if (transform.position.z + (Vector3.one.z * m_speed * Time.deltaTime + 0.01) > StageManager.z_MinBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKey("u") && !Input.GetKey("j"))
                    {
                        if (transform.localEulerAngles.y != 270)
                            transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        if (transform.position.z + (Vector3.one.z * m_speed * Time.deltaTime + 0.01) < StageManager.z_MaxBound)
                            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                    }
                    if (Input.GetKeyDown("y"))
                    {
                        if (!m_isAttacking)
                        {
                            m_isAttacking = true;
                            Attack();
                        }
                    }
                    if (Input.GetKeyDown("i"))
                    {
                        GameManager.Instance.m_mage.GetComponent<MageControl>().CallTheMage(2);
                    }
                    break;
            }
        }
    }

    //return true if player health is less than equal to zero, else return false
    public bool isDead()
    {
        return (m_health <= 0);
    }


    //temporary hit by enemy weapon function which also disables player mesh renderer if player's life falls to 0 or less
    public void playerHitByEnemyWeapon(int damage)
    {
        m_health -= damage;

        Debug.Log("Player #" + m_playerNumber + " health after hit: " + m_health);

        if (isDead())
        {
            gameObject.SetActive(false);
        }
    }

    void Attack()
    {
        boxCol.enabled = true;
        StartCoroutine(Slash());
    }

    IEnumerator Slash()
    {
        while(transform.GetChild(1).localEulerAngles.y < 90)
        {
            float yRot = 0;
            yRot += 90 * 3 * Time.deltaTime;
            transform.GetChild(1).Rotate(new Vector3(0f, yRot, 0f));
            yield return null;
        }
        while (transform.GetChild(1).localEulerAngles.y < 345)
        {
            float yRot = transform.localEulerAngles.y;
            yRot -= 90 * 3 * Time.deltaTime;
            transform.GetChild(1).Rotate(new Vector3(0f, yRot, 0f));
            yield return null;
        }
        transform.GetChild(1).localEulerAngles = Vector3.zero;
        boxCol.enabled = false;
        m_isAttacking = false;
    }

}
