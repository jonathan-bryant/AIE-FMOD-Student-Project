using UnityEngine;

public class ScrollingUV : MonoBehaviour
{
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
        rend.material.mainTextureOffset = new Vector2(0, m_elapsed);
    }
}
