using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBehavior : MonoBehaviour
{
	public bool movingTowardsVacuum = false;
	public float moveTowardsSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (movingTowardsVacuum)
		{
			moveTowardsVacuum();
		}
    }

	private void moveTowardsVacuum()
	{
		Vector2 target = FindObjectOfType<VacuumBehavior>().transform.position;
		transform.position = Vector2.MoveTowards(transform.position, target, moveTowardsSpeed);
		if (Vector2.Distance(this.transform.position, target) < 0.2f)
		{
			Destroy(this.gameObject);
		}
	}
}
