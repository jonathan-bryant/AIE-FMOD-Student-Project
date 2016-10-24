using UnityEngine;
using System.Collections;

public class ScrollingUV : MonoBehaviour
{
	public int m_materialIndex = 0;
	float m_elapsed = 0.0f;
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
		m_elapsed += Time.deltaTime * scrollSpeed;
        float offset = Time.time * scrollSpeed;
		rend.materials[m_materialIndex].SetTextureOffset("_MainTex", new Vector2(0, m_elapsed));

    }
}
