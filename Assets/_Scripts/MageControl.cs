using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageControl : MonoBehaviour {
    public float m_speed;
	public float m_reach;
    Transform m_sphere;
    bool m_called;
    bool m_shieldIsExpending;
    int m_playerWhoCalled;
    float m_inMiddleZ;
    float m_inMiddleX;
    private void Start()
    {
        m_sphere = transform.GetChild(0).transform;
        m_sphere.localScale = new Vector3(0f, 0f, 0f);
        m_called = false;
    }
    // Update is called once per frame
    void Update () {
        if(Input.GetKey("space")){
            StartCoroutine(ExpendShield());
        }
	}
    private void FixedUpdate()
    {
        //Don't forget to set the mage's speed in the inspector
        //VERSION 1
        Vector3 playerOne;
        Vector3 playerTwo;
        Vector3 playerThree;
        playerOne = GameManager.Instance.m_players[0].transform.position;
        playerTwo = GameManager.Instance.m_players[1].transform.position;
        playerThree = GameManager.Instance.m_players[2].transform.position;
        
        //VERSION 1. Avance constament et ajuste sa position en z dependant d'ou les joueur ce trouve
        m_inMiddleZ = (playerOne.z + playerTwo.z + playerThree.z) / 3;
        //transform.position = new Vector3(transform.position.x, transform.position.y, m_inMiddleZ);
        //VERSION 2. Ne fait qu'avancer. that's it
        //VERSION 3. Avance avec les joueurs tout en restant au milieu;
        m_inMiddleZ = (playerOne.z + playerTwo.z + playerThree.z) / 3;
        m_inMiddleX = (playerOne.x + playerTwo.x + playerThree.x) / 3;
        //CONSTANT MOVEMENT
        if (!m_called)
        {
            Vector3 in_middle = new Vector3(m_inMiddleX, transform.position.y, m_inMiddleZ);
            Vector3 newPos = Vector3.MoveTowards(transform.GetComponent<Rigidbody>().position, in_middle, 10 * Time.deltaTime);
            transform.GetComponent<Rigidbody>().MovePosition(newPos);
        }
        else
        {
            MoveTowardWarriors();
        }
        //END OF CONSTANT MOVEMENT
    }
    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
    void MoveTowardWarriors()
    {
        Vector3 endPos = GameManager.Instance.m_players[m_playerWhoCalled].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.GetComponent<Rigidbody>().position, endPos, 10 * Time.deltaTime);
        transform.GetComponent<Rigidbody>().MovePosition(newPos);
    }
    public void CallTheMage(int Num)
    {
        m_called = true;
        m_playerWhoCalled = Num;
        StartCoroutine(MageTimer());
    }
    IEnumerator MageTimer()
    {
        float timer = 0;
        while(timer < 5)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        m_called = false;
    }
    IEnumerator ExpendShield()
    {
        int speed = 20;
        Vector3 expend = new Vector3(0f,0f,0f);
        m_shieldIsExpending = true;
        while(m_sphere.localScale.x < 10)
        {
            expend.x += speed * Time.deltaTime;
            expend.y += speed * Time.deltaTime;
            expend.z += speed * Time.deltaTime;
            m_sphere.localScale = expend;
            yield return null;
        }
    }
}
