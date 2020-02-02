using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawning : MonoBehaviour
{
	public GameObject garbagePrefab;
	public int numGarbage;

	private void Start()
	{
		for (int i = 0; i < numGarbage; i++)
		{
			spawnGarbage();
		}
	}

	void spawnGarbage()
	{
		Vector2 someObjectPosition = new Vector2(0, -0.54f);
		Vector2 newPosition = someObjectPosition + Random.insideUnitCircle * 3.8f;
		GameObject garbageInstance = Instantiate(garbagePrefab, newPosition, Quaternion.identity);
	}
}
