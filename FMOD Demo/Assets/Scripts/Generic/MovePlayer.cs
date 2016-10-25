using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public float m_speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerStay(Collider a_col)
    {
        if (a_col.gameObject.tag == "Player")
        {
            a_col.gameObject.GetComponent<ActorControls>().MoveActor(-transform.up * m_speed * Time.deltaTime);
        }
    }
}
