using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    /*===============================================Fmod====================================================
    |   This piece of code will allow the string m_footstepSurfaceName to use the event browser to select   |
    |   the event, in the inspector.                                                                        |
    =======================================================================================================*/
    [FMODUnity.EventRef]

    /*===============================================Fmod====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_footstepSurfaceName;
    Vector3 m_position;

    void Start()
    {

    }
    void Update()
    {
    }

    public void PlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_footstepSurfaceName, m_position);
    }
    public void SetPosition(Vector3 a_position)
    {
        m_position = a_position;
    }
}