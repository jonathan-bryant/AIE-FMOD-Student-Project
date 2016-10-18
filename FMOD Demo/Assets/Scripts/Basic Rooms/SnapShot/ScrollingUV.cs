using UnityEngine;
using System.Collections;

public class ScrollingUV : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    Renderer rend;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float offset = Time.time * scrollSpeed;
        rend.materials[0].SetTextureOffset("_MainTex", new Vector2(0, offset));

    }
}
