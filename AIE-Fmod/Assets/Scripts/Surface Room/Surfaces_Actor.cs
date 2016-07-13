using UnityEngine;
using System.Collections;

public class Surfaces_Actor : Actor {
    
    new void Start()
    {
        base.Start();
    }
    
    new void Update()
    {
        base.Update();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void OnTriggerEnter(Collider a_collider)
    {
        //Fmod: Depending on the type of ground that is triggered, set surface parameter to change the sound of the footstep.
        string tag = a_collider.gameObject.tag;
        if (tag == "Grass")
        {
            m_footstepSurfaceParamter.setValue(2.0f);
        }
        else if (tag == "Carpet")
        {
            m_footstepSurfaceParamter.setValue(1.0f);
        }
        else if (tag == "Wood")
        {
            m_footstepSurfaceParamter.setValue(3.0f);
        }
    }
    void OnTriggerExit(Collider a_collider)
    {
        //Fmod: When trigger exit happens on grass, carpet or wood, set surface parameter to 0(which is silent).
        string tag = a_collider.gameObject.tag;
        if (tag == "Grass" || tag == "Carpet" || tag == "Wood")
        {
            m_footstepSurfaceParamter.setValue(0.0f);
        }
    }
}
