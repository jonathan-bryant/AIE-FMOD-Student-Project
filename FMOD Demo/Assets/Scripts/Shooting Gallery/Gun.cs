using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject m_bullet;
    public Transform m_gunHole;
    // Use this for initialization
    public float m_power = 2.0f;
    public float m_fireRate = 0.1f;
    float m_elapsed;
	void Start () {
        m_elapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Shoot();
	}
    void Shoot()
    {
        m_elapsed = Mathf.Min(m_elapsed + Time.deltaTime, m_fireRate);
        if(Input.GetMouseButton(0) && m_elapsed >= m_fireRate)
        {
            GameObject obj = Instantiate(m_bullet) as GameObject;
            obj.transform.position = m_gunHole.position;
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * m_power);
            m_elapsed = 0.0f;
        }
    }
}
