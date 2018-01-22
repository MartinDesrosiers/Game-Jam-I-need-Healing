using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    Material m_material;
    float m_scrollSpeed;
	// Use this for initialization
	void Start () {
        m_material = GetComponent<MeshRenderer>().material;
        m_scrollSpeed = 0.1f;
    }
	
	// Update is called once per frame
	void Update () {
        m_material.mainTextureOffset = new Vector2(m_scrollSpeed * Time.time, m_scrollSpeed * Time.time);
    }
}
