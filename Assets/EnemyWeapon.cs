using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    enum Weapons { Sword };

    private void Start()
    {
        Debug.Log("EnemyWeapon.Start");
    }

    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("EnemyWeapon.OnTriggerEnter");
        if(collider.tag == "Player")
        {
            Debug.Log("Dealing damage to player");

            collider.SendMessage("playerHitByEnemyWeapon", 1);
        }
    }
}