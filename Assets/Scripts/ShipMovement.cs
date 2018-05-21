using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {
    [Range (0.01f, .5f)]
    public float snapSpeed = .1f;
    public TouchAxisCtrl touchAxisControl;
    public float xRange = 4f;
    public float yRange = 4f;

    private bool m_bHasBeenTouching;
    private Vector2 m_InitialXY;
    Vector2 m_TargetXY;
    
	// Update is called once per frame
	void Update () {
        if (touchAxisControl.IsTouching())
        {
            if (!m_bHasBeenTouching)
            {
                m_bHasBeenTouching = true;
                m_InitialXY = new Vector2(transform.position.x, transform.position.y);
            }
            m_TargetXY = new Vector3(m_InitialXY.x + touchAxisControl.GetAxis("Horizontal") * xRange,
                                   m_InitialXY.y + touchAxisControl.GetAxis("Vertical") * yRange);
        }
        else
        {
            m_bHasBeenTouching = false;
        }
        Vector2 lerpPos = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), m_TargetXY, snapSpeed);
        transform.eulerAngles = new Vector3(Remap(transform.position.y - m_TargetXY.y, -yRange, yRange, -55, 45), 
                                            transform.eulerAngles.y, 
                                            Remap(transform.position.x - m_TargetXY.x, -xRange, xRange, -45, 45));
        transform.position = new Vector3(lerpPos.x, lerpPos.y, transform.position.z);
	}

    float Remap(float val, float srcMin, float srcMax, float destMin, float destMax)
    {
        return Mathf.Lerp(destMin, destMax, Mathf.InverseLerp(srcMin, srcMax, val));
    }
}
