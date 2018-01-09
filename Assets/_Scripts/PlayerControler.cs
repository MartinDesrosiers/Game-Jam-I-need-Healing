using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    public float speed;
    public char playerNumber;
    // Use this for initialization
    public void Init(char number)
    {
        playerNumber = number;
    }

    // Update is called once per frame
    void Update () {
        switch (playerNumber)
        {
            case '1':
                if (Input.GetKey("a") && !Input.GetKey("d"))
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                if (Input.GetKey("d") && !Input.GetKey("a"))
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                if (Input.GetKey("s") && !Input.GetKey("w"))
                {
                    transform.Translate(Vector3.back * speed * Time.deltaTime);
                }
                if (Input.GetKey("w") && !Input.GetKey("s"))
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                break;
            case '2':
                if (Input.GetKey("left") && !Input.GetKey("right"))
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                if (Input.GetKey("right") && !Input.GetKey("left"))
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                if (Input.GetKey("down") && !Input.GetKey("up"))
                {
                    transform.Translate(Vector3.back * speed * Time.deltaTime);
                }
                if (Input.GetKey("up") && !Input.GetKey("down"))
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                break;
            case '3':
                if (Input.GetKey("h") && !Input.GetKey("k"))
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                if (Input.GetKey("k") && !Input.GetKey("h"))
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                if (Input.GetKey("j") && !Input.GetKey("u"))
                {
                    transform.Translate(Vector3.back * speed * Time.deltaTime);
                }
                if (Input.GetKey("u") && !Input.GetKey("j"))
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                break;
        }
    }
}
