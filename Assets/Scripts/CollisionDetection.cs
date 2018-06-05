using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    public CameraShake cameraShake;
	void OnCollisionEnter(Collision col)
    {
        cameraShake.StartShake();
    }
}
