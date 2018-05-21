using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTunnel : MonoBehaviour {
    public float maxSpeed = 5;
    public float acceleration = .1f;

    private float m_Speed = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Speed < maxSpeed)
        {
            m_Speed += acceleration;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (m_Speed * Time.deltaTime));
	}
}
