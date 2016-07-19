using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [FMODUnity.EventRef]
    public string m_HitSoundPath;
    FMOD.Studio.EventInstance m_HitSoundEvent;
    // Use this for initialization
    void Start () {
        m_HitSoundEvent = FMODUnity.RuntimeManager.CreateInstance(m_HitSoundPath);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision a_col)
    {
        m_HitSoundEvent.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_HitSoundEvent, transform, null);
    }
}
