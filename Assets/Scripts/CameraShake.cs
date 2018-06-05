using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public Properties testProperties;
    private IEnumerator currentCoroutine;
    // Use this for initialization

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartShake();
        }
    }
    public void StartShake()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = Shake(testProperties);
        StartCoroutine(currentCoroutine);
    }

    float DampingCurve(float x, float dampingPercent)
    {
        x = Mathf.Clamp01(x);
        float a = Mathf.Lerp(2, .25f, dampingPercent);
        float b = 1 - Mathf.Pow(x, a);
        return b * b * b;
    }

    IEnumerator Shake(Properties props)
    {
        float completionPercent = 0;
        float movePercent = 0;

        float angle_rad = props.angle * Mathf.Deg2Rad - Mathf.PI;
        Vector3 previousWaypoint = Vector3.zero;
        Vector3 currentWaypoint = Vector3.zero;
        float moveDistance = 0;

        while (true)
        {
            if (movePercent >= 1 || completionPercent == 0)
            {
                float dampingFactor = DampingCurve(completionPercent, props.dampingPercent);
                float noiseAngle = (Random.value - .5f) * Mathf.PI;
                angle_rad += Mathf.PI + noiseAngle * props.noisePercent;
                currentWaypoint = new Vector3(transform.localPosition.x, Mathf.Cos(angle_rad), Mathf.Sin(angle_rad)) * props.strength * dampingFactor;
                previousWaypoint = transform.localPosition;

                moveDistance = Vector3.Distance(currentWaypoint, previousWaypoint);
                movePercent = 0;
            }
            completionPercent += Time.deltaTime / props.duration;
            movePercent += Time.deltaTime / moveDistance * props.speed;
            transform.localPosition = Vector3.Lerp(previousWaypoint, currentWaypoint, movePercent);


            yield return null;
        }
    }
    [System.Serializable]
    public class Properties
    {
        public float angle;
        public float strength;
        public float speed;
        public float duration;
        [Range(0, 1)]
        public float dampingPercent;
        [Range(0, 1)]
        public float noisePercent;
    }
}
