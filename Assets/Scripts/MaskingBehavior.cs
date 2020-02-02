using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskingBehavior : MonoBehaviour
{
	SpriteMask Mask;
	Color[] Colors;
	int Width;
	int Height;

	void Start()
	{
		//Get objects
		Mask = GameObject.Find("Mask").GetComponent<SpriteMask>();

		//Extract color data once
		Colors = Mask.sprite.texture.GetPixels();

		//Store mask dimensionns
		Width = Mask.sprite.texture.width;
		Height = Mask.sprite.texture.height;

		ClearMask();
	}

	void ClearMask()
	{
		//set all color data to transparent
		for (int i = 0; i < Colors.Length; ++i)
		{
			Colors[i] = new Color(1, 1, 1, 0);
		}

		Mask.sprite.texture.SetPixels(Colors);
		Mask.sprite.texture.Apply(false);
	}

	//This function will draw a circle onto the texture at position cx, cy with radius r
	public void DrawOnMask(int cx, int cy, int r)
	{
		int px, nx, py, ny, d;

		for (int x = 0; x <= r; x++)
		{
			d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));

			for (int y = 0; y <= d; y++)
			{
				px = cx + x;
				nx = cx - x;
				py = cy + y;
				ny = cy - y;
				print("HERE");
				print(Colors.Length);
				Colors[py * Width + px] = new Color(1, 1, 1, 1);
				Colors[py * Width + nx] = new Color(1, 1, 1, 1);
				Colors[ny * Height + px] = new Color(1, 1, 1, 1);
				Colors[ny * Height + nx] = new Color(1, 1, 1, 1);
			}
		}

		Mask.sprite.texture.SetPixels(Colors);
		Mask.sprite.texture.Apply(false);
	}


	void Update()
	{

		//Get mouse coordinates
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//Check if mouse button is held down
		if (Input.GetMouseButton(0))
		{
			//Check if we hit the collider
			RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
			if (hit.collider != null)
			{
				//Normalize to the texture coodinates
				int y = (int)((0.5 - (Mask.transform.position - mousePosition).y) * Height);
				int x = (int)((0.5 - (Mask.transform.position - mousePosition).x) * Width);
				print("DRWING");
				//Draw onto the mask
				DrawOnMask(x, y, 5);
			}
		}
	}
}
