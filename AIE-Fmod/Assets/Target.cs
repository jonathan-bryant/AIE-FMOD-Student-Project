using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject m_shatterObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision a_col)
    {
        if (a_col.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            int rand = Random.Range(4, 6);
            for (int i = 0; i < rand; ++i)
            {
                GameObject obj = Instantiate(m_shatterObj, transform.position + (Random.insideUnitSphere * Random.Range(transform.localScale.magnitude * 0.5f, transform.localScale.magnitude)), Quaternion.LookRotation(transform.forward, transform.up)) as GameObject;
                obj.transform.localScale = transform.localScale * (1.0f / rand);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.AddForce((obj.transform.position - a_col.gameObject.transform.position) * a_col.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 0.5f, ForceMode.Impulse);
            }
        }
    }
}
