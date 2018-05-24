using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform posTarget;

    private Vector3 m_Difference;

    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        m_Difference = posTarget.position - transform.position;
    }
    // Update is called once per frame
    void LateUpdate () {
        transform.position = posTarget.position - m_Difference;
	}
}
