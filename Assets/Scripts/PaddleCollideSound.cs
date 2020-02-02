﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCollideSound : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<BallControl>())
		{
			FindObjectOfType<AudioManager>().PlayPaddle();
		}
	}
}
