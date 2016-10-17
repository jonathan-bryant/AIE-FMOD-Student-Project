/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Base Target class. Plays sound OnCollision.                     |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
    public float m_minRadius;
    public float m_maxRadius;
    public BaseTarget m_Parent;

    //---------------------------------Fmod-------------------------------
    //Call this to display it in Unity Inspector.
    //--------------------------------------------------------------------
    [FMODUnity.EventRef]
    //---------------------------------Fmod-------------------------------
    //Name of Event. Used in conjunction with EventInstance.
    //--------------------------------------------------------------------
    public string m_hitSoundPath;
    //---------------------------------Fmod-------------------------------
    //EventInstance. Used to play or stop the sound, etc.
    //--------------------------------------------------------------------
    FMOD.Studio.EventInstance m_hitSound;
    //---------------------------------Fmod-------------------------------
    //ParameterInstance. Used to reference a parameter stored in 
    //EventInstance. Example use case: changing 
    //from wood to carpet floor.
    //--------------------------------------------------------------------
    FMOD.Studio.ParameterInstance m_hitMaterial;
    //---------------------------------Fmod-------------------------------
    //This int will be used on start to change the parameter value of the 
    //hit.
    //--------------------------------------------------------------------
    [UnityEngine.Range(0, 2)]
    public int m_material;

    void Start () {
        float rngSize = Random.Range(m_minRadius, m_maxRadius);
        transform.localScale = new Vector3(rngSize, rngSize, rngSize);
        //---------------------------------Fmod-------------------------------
        //Calling this function will create an EventInstance. The return value
        //is the created instance.
        //--------------------------------------------------------------------
        m_hitSound = FMODUnity.RuntimeManager.CreateInstance(m_hitSoundPath);
        //---------------------------------Fmod-------------------------------
        //Calling this function will return a reference to a parameter inside
        //EventInstance and store it in ParameterInstance.
        //--------------------------------------------------------------------
        m_hitSound.getParameter("Material", out m_hitMaterial);
        //---------------------------------Fmod-------------------------------
        //This function is used to set the ParameterInstance value.
        //--------------------------------------------------------------------
        m_hitMaterial.setValue(m_material);

    }
	
	void Update () {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

    void OnCollisionEnter(Collision a_col)
    {
        if (a_col.gameObject.name.Contains("Bullet"))
        {
            //---------------------------------Fmod-------------------------------
            //Calling EventInstance.start() will start the event.
            //--------------------------------------------------------------------
            m_hitSound.start();
            //---------------------------------Fmod-------------------------------
            //A gameobject needs to be attached to the instance, so the sound can 
            //follow the gameobject. Everytime the EventInstance.start() function 
            //is called, the gameobject needs to be reattached.
            //--------------------------------------------------------------------
            m_hitSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, null));

            if (m_Parent)
                m_Parent.Hit(this);
            GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
