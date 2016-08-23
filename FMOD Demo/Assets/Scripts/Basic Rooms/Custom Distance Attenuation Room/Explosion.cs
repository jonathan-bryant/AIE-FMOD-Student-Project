using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public ParticleSystem m_particle;

    /*===============================================Fmod====================================================
    |   This piece of code will allow the string m_footstepSurfaceName to use the event browser to select   |
    |   the event, in the inspector.                                                                        |
    =======================================================================================================*/
    [FMODUnity.EventRef]

    /*===============================================Fmod====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_footstepSurfaceName;

    float m_elapsed;

	void Start () {

	}
	void Update () {
        m_elapsed += Time.deltaTime;
        if(m_elapsed >= 6.0f)
        {
            FMODUnity.RuntimeManager.PlayOneShot(m_footstepSurfaceName, transform.position);
            m_elapsed = 0.0f;
            m_particle.Play();
        }
	}
}
