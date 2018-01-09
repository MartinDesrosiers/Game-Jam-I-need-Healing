using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager m_instance;
    public GameObject m_player;
    public GameObject m_enemie;

    GameObject[] players;
    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new GameManager();
            return m_instance;
        }
    }
    private void Start()
    {
        players = new GameObject[3];
        players[0] = Instantiate(m_player, new Vector3(0f, 1f, 0f), Quaternion.identity) as GameObject;
        players[1] = Instantiate(m_player, new Vector3(2f, 1f, 0f), Quaternion.identity) as GameObject;
        players[2] = Instantiate(m_player, new Vector3(4f, 1f, 0f), Quaternion.identity) as GameObject;
        players[0].GetComponent<PlayerControler>().Init('1');
        players[1].GetComponent<PlayerControler>().Init('2');
        players[2].GetComponent<PlayerControler>().Init('3');
    }
}
