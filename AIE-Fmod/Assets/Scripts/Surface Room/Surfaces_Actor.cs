using UnityEngine;
using System.Collections;

public class Surfaces_Actor : Actor {



    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
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
        string tag = a_collider.gameObject.tag;
        if (tag == "Grass")
        {
            m_footstepSurfaceParamter.setValue(2.0f);
            Debug.Log("Grass footsteps");
        }
        else if (tag == "Carpet")
        {
            m_footstepSurfaceParamter.setValue(1.0f);
            Debug.Log("Carpet footsteps");
        }
        else if (tag == "Wood")
        {
            m_footstepSurfaceParamter.setValue(3.0f);
            Debug.Log("Wood footsteps");
        }
    }
    void OnTriggerExit(Collider a_collider)
    {
        string tag = a_collider.gameObject.tag;
        if (tag == "Grass" || tag == "Carpet" || tag == "Wood")
        {
            m_footstepSurfaceParamter.setValue(0.0f);
        }
    }
}
