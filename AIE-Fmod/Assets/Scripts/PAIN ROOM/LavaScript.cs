using UnityEngine;
using System.Collections;

public class LavaScript : MonoBehaviour
{
	public bool scroll = false;

	public float horizontalScrollSpeed = -0.025f;
	public float verticalScrollSpeed = -0.025f;
	private Material mat;
	
	void Start ()
	{
		mat = GetComponent<Renderer>().material;
	}
	
	void Update ()
	{
		if (scroll)
		{
			float horizontalOffset = horizontalScrollSpeed * Time.time;
			float verticalOffset = verticalScrollSpeed * Time.time;
			mat.mainTextureOffset = new Vector2(horizontalOffset, verticalOffset);
		}		
	}
}
