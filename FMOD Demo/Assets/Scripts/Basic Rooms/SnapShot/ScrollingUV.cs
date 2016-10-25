using UnityEngine;

public class ScrollingUV : MonoBehaviour
{
    public int m_materialIndex = 0;
    public float scrollSpeed = 0;
    public bool m_randSpeed = false;
	float m_elapsed = 0.0f;
    Renderer rend;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        if (m_randSpeed)
            scrollSpeed = Random.Range(1.0f, 2.2f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		m_elapsed += Time.deltaTime * scrollSpeed;
        if (rend.materials.Length > 1)
            rend.materials[m_materialIndex].mainTextureOffset = new Vector2(0, m_elapsed);
        else
            rend.material.mainTextureOffset = new Vector2(0, m_elapsed);
    }
}
