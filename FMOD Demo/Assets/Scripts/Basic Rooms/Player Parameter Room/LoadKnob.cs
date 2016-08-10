using UnityEngine;
using System.Collections;

public class LoadKnob : MonoBehaviour {

    public FMODUnity.StudioEventEmitter m_emitter;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        m_emitter.SetParameter("Load", transform.eulerAngles.x);
	}
}
