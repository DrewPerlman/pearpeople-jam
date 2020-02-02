using UnityEngine;

public class DrawLines : MonoBehaviour
{
	private Texture2D m_Texture;
	private Color[] m_Colors;
	RaycastHit2D hit;
	SpriteRenderer spriteRend;
	Color zeroAlpha = Color.clear;
	public int erSize;
	public Vector2Int lastPos;
	public bool Drawing = false;
	private GameObject m_ball;

	void Start()
	{
		spriteRend = gameObject.GetComponent<SpriteRenderer>();
		var tex = spriteRend.sprite.texture;
		m_Texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
		m_Texture.filterMode = FilterMode.Bilinear;
		m_Texture.wrapMode = TextureWrapMode.Clamp;
		m_Colors = tex.GetPixels();
		m_Texture.SetPixels(m_Colors);
		m_Texture.Apply();
		spriteRend.sprite = Sprite.Create(m_Texture, spriteRend.sprite.rect, new Vector2(0.5f, 0.5f));
		m_ball = FindObjectOfType<BallControl>().gameObject;
	}

	void Update()
	{
		if (m_ball)
		{
			UpdateTexture();
			Drawing = true;
		}
		else
			Drawing = false;
	}

	public void UpdateTexture()
	{
		int w = m_Texture.width;
		int h = m_Texture.height;
		//var mousePos = hit.point- (Vector2)hit.collider.bounds.min;
		Vector2 mousePos = FindObjectOfType<BallControl>().transform.position;
		mousePos -= (Vector2)this.GetComponent<BoxCollider2D>().bounds.min;
		mousePos.x *= w / this.GetComponent<BoxCollider2D>().bounds.size.x;
		mousePos.y *= h / this.GetComponent<BoxCollider2D>().bounds.size.y;
		Vector2Int p = new Vector2Int((int)mousePos.x, (int)mousePos.y);
		Vector2Int start = new Vector2Int();
		Vector2Int end = new Vector2Int();
		if (!Drawing)
			lastPos = p;
		start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - erSize, 0, w);
		start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - erSize, 0, h);
		end.x = Mathf.Clamp(Mathf.Max(p.x, lastPos.x) + erSize, 0, w);
		end.y = Mathf.Clamp(Mathf.Max(p.y, lastPos.y) + erSize, 0, h);
		Vector2 dir = p - lastPos;
		for (int x = start.x; x < end.x; x++)
		{
			for (int y = start.y; y < end.y; y++)
			{
				Vector2 pixel = new Vector2(x, y);
				Vector2 linePos = p;
				if (Drawing)
				{
					float d = Vector2.Dot(pixel - lastPos, dir) / dir.sqrMagnitude;
					d = Mathf.Clamp01(d);
					linePos = Vector2.Lerp(lastPos, p, d);
				}
				if ((pixel - linePos).sqrMagnitude <= erSize * erSize)
				{
					m_Colors[x + y * w] = zeroAlpha;
				}
			}
		}
		lastPos = p;
		m_Texture.SetPixels(m_Colors);
		m_Texture.Apply();
		spriteRend.sprite = Sprite.Create(m_Texture, spriteRend.sprite.rect, new Vector2(0.5f, 0.5f));
	}
}