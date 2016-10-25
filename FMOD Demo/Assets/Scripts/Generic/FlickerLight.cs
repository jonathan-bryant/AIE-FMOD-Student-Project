using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour
{
    int m_count;
    Color m_emission;
    // Use this for initialization
    void Start()
    {
        m_emission = GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_count == 5)
        {
            int rng = Random.Range(1, 100);
            if (rng <= 20)
            {
                GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(m_emission.r, m_emission.g, m_emission.b, m_emission.a * 0.5f));
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(m_emission.r, m_emission.g, m_emission.b, m_emission.a));
            }
            m_count = 0;
        }
        m_count++;
    }
}
