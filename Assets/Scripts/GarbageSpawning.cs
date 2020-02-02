using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawning : MonoBehaviour
{
	public GameObject garbagePrefab;
	public int numGarbage;
	private BallControl theBall;

	private void Start()
	{
		theBall = FindObjectOfType<BallControl>();
		for (int i = 0; i < numGarbage; i++)
		{
			spawnGarbage();
		}
	}

	void spawnGarbage()
	{
		Vector2 someObjectPosition = new Vector2(0, -0.54f);
		Vector2 newPosition = new Vector2(0, 0);
		//while 
		//(
		//	((newPosition.x > -0.5f || newPosition.x < 0.5f) && newPosition.y < 0.5f) || 
		//	((newPosition.x > -0.5f || newPosition.x < 0.5f) && newPosition.y > -0.5f)
		//)
		//{
		while (Vector2.Distance(newPosition, theBall.transform.position) < 0.8f)
		{
			newPosition = someObjectPosition + Random.insideUnitCircle * 3.7f;
		}
		//}
		GameObject garbageInstance = Instantiate(garbagePrefab, newPosition, GetRandomRotation());
	}

	private Quaternion GetRandomRotation()
	{
		Quaternion rot = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
		return rot;
	}
}
