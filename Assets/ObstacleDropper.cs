using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDropper : MonoBehaviour {

	public GameObject[] obstaclesPrefabs;
	public float dropInterval = 5f;
	public float lifetime = 10f;

	private Transform m_ObstacleContainer;
	private List<GameObject> m_Obsacles;
	private float m_ElapsedTime = 0;
	// Use this for initialization
	void Start () {
		m_Obsacles = new List<GameObject>();
		m_ObstacleContainer = new GameObject("ObstacleContainer").transform;
	}
	
	// Update is called once per frame
	void Update () {
		m_ElapsedTime += Time.deltaTime;
		if (m_ElapsedTime >= dropInterval)
		{
			InstantiateObstacle();
			m_ElapsedTime = 0;
		}
	}
	void InstantiateObstacle()
	{
		int randomObstacleIndex = Random.Range(0, obstaclesPrefabs.Length -1);
		GameObject newObstacle = Instantiate(obstaclesPrefabs[randomObstacleIndex], transform);
		newObstacle.transform.parent = m_ObstacleContainer;
		m_Obsacles.Add(newObstacle);
		StartCoroutine(KillCountdown(newObstacle));
	}

	IEnumerator KillCountdown(GameObject goner)
	{
		yield return new WaitForSeconds(lifetime);
		Destroy(goner);
	}
}
