using UnityEngine;
using System.Collections;

public class RoomCompleted : MonoBehaviour {

    public Sprite m_completed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void CompleteRoom()
    {
        GetComponent<SpriteRenderer>().sprite = m_completed;
    }
}
