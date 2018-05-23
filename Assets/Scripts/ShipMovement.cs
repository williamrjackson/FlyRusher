using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {
    public Transform ship;
    [Range (0.01f, .5f)]
    public float snapSpeed = .1f;
    public TouchAxisCtrl touchAxisControl;
    public float xRange = 4f;
    public float yRange = 4f;
    public float maxSpeed = 5;
    public float acceleration = .1f;

    private float m_Speed = 0;
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
                m_InitialXY = new Vector2(ship.position.x, ship.position.y);
            }
            m_TargetXY = new Vector3(m_InitialXY.x + touchAxisControl.GetAxis("Horizontal") * xRange,
                                     m_InitialXY.y + touchAxisControl.GetAxis("Vertical") * yRange);
        }
        else
        {
            m_bHasBeenTouching = false;
        }
        Vector2 lerpPos = Vector2.Lerp(new Vector2(ship.position.x, ship.position.y), m_TargetXY, snapSpeed);
        ship.eulerAngles = new Vector3(Remap(ship.position.y - m_TargetXY.y, -yRange, yRange, -60, 60), 
                                            ship.eulerAngles.y, 
                                            Remap(ship.position.x - m_TargetXY.x, -xRange, xRange, -60, 60));
        ship.position = new Vector3(lerpPos.x, lerpPos.y, ship.position.z);
        if (m_Speed < maxSpeed)
        {
            m_Speed += acceleration;
        }
        transform.position += transform.forward * (m_Speed * Time.deltaTime);
    }

    float Remap(float val, float srcMin, float srcMax, float destMin, float destMax)
    {
        return Mathf.Lerp(destMin, destMax, Mathf.InverseLerp(srcMin, srcMax, val));
    }
}
