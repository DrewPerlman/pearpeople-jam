using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBehavior : MonoBehaviour
{
	public bool movingTowardsVacuum = false;
	private VacuumBehavior theVacuum;
	private float movingTimer = 0.0f;
	private float destroyTimer = 0.0f;
	private AudioManager am;
	private bool destroyy;

	// Start is called before the first frame update
	void Start()
    {
		theVacuum = FindObjectOfType<VacuumBehavior>();
		am = FindObjectOfType<AudioManager>();
	}

    // Update is called once per frame
    void Update()
    {
		if (movingTowardsVacuum)
		{
			moveTowardsVacuum();
		}
		if (destroyy)
		{
			DestroySelf();
		}
    }

	private void moveTowardsVacuum()
	{
		movingTimer += Time.deltaTime;
		if (movingTimer > 1)
		{
			movingTimer = 0;
			movingTowardsVacuum = false;
		}
		Vector2 target = theVacuum.transform.position;
		transform.position = Vector2.MoveTowards(transform.position, target, theVacuum.moveTowardsSpeed);
		if (Vector2.Distance(this.transform.position, target) < 0.2f && destroyy == false)
		{
			if (ProgressBar.instance != null)
			{
				ProgressBar.instance.RemoveEntry(this.gameObject);
			}
			GetComponentInChildren<SpriteRenderer>().enabled = false;
			GetComponent<AudioSource>().Play();
			destroyy = true;
		}
	}

	private void DestroySelf()
	{
		destroyTimer += Time.deltaTime;
		if (destroyTimer > 1f) { Destroy(this.gameObject); }
	}
}
