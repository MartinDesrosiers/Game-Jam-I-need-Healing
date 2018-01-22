using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance = null;
    public GameObject enemyManager;
    public GameObject m_playerPrefab;
    public GameObject m_magePrefab;
    public Material[] material;

    public GameObject[] m_players;
    public GameObject m_mage;
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
    private void Awake() { 
        if (m_instance == null)
            m_instance = this;
        else if (m_instance != this)
            Destroy(gameObject);
        m_players = new GameObject[3];
        m_players[0] = Instantiate(m_playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        m_players[1] = Instantiate(m_playerPrefab, new Vector3(2f, 0f, 5f), Quaternion.identity) as GameObject;
        m_players[2] = Instantiate(m_playerPrefab, new Vector3(4f, 0f, 0f), Quaternion.identity) as GameObject;
        m_players[0].GetComponent<PlayerControler>().Init('1', material[0]);
        m_players[1].GetComponent<PlayerControler>().Init('2', material[1]);
        m_players[2].GetComponent<PlayerControler>().Init('3', material[2]);
        m_mage = Instantiate(m_magePrefab, new Vector3(0f, .15f, 0f), Quaternion.identity) as GameObject;

        //instantiate the enemy manager 
        enemyManager = GameObject.Instantiate(enemyManager);
        loadBGM();
    }

    private void loadBGM()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        AudioClip clip;

        switch (Random.Range(0, 9))
        {
            case 0:
                clip = Resources.Load<AudioClip>("Sound/BGM/Rolemusic_-_14_-_The_Will");
                break;
            case 1:
                clip = Resources.Load<AudioClip>("Sound/BGM/Pocketmaster_-_06_-_Just_A_Minute");
                break;
            case 2:
                clip = Resources.Load<AudioClip>("Sound/BGM/Pocketmaster_-_11_-_Vorwaerts_Tom_Woxom_Version_Tatkraft");
                break;
            case 3:
                clip = Resources.Load<AudioClip>("Sound/BGM/Quelle_Fromage_-_05_-_Noam_Chomskys_Corporate_Carbohydrate_Surprise");
                break;
            case 4:
                clip = Resources.Load<AudioClip>("Sound/BGM/RoccoW_-_01_-_Welcome");
                break;
            case 5:
                clip = Resources.Load<AudioClip>("Sound/BGM/rolem_-_01_-_The_White_Kitty");
                break;
            case 6:
                clip = Resources.Load<AudioClip>("Sound/BGM/rolemu_-_02_-_The_White_Frame");
                break;
            case 7:
                clip = Resources.Load<AudioClip>("Sound/BGM/rolemu_-_04_-_The_Black_Kitty");
                break;
            case 8:
                clip = Resources.Load<AudioClip>("Sound/BGM/rolemusi_-_03_-_The_White");
                break;
            case 9:
                clip = Resources.Load<AudioClip>("Sound/BGM/sawsquarenoise_-_03_-_Field_Force");
                break;
            default:
                clip = Resources.Load<AudioClip>("Sound/BGM/rolem_ - _01_ - _The_White_Kitty");
                break;
        }

        audioSource.clip = clip;
        audioSource.Play();
    
    }
}
