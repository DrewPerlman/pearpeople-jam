using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBehavior : MonoBehaviour
{
	public float vicinityRadius = 1f;
	public float moveTowardsSpeed = 0.05f;
	//public GameObject spriteMaskPrefab;
	//public GameObject spriteMaskParent;

	// Start is called before the first frame update
	void Start()
    {
		//InvokeRepeating("CreateDirtLayerSpriteMask", 0f, .05f);
    }

    // Update is called once per frame
    void Update()
    {

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Garbage")
		{
			collision.GetComponent<GarbageBehavior>().movingTowardsVacuum = true;
		}
	}

	//private void CreateDirtLayerSpriteMask()
	//{
	//	GameObject spriteInstance = Instantiate(spriteMaskPrefab, this.transform.position, Quaternion.identity);
	//	spriteInstance.transform.parent = spriteMaskParent.transform;
	//}
}
