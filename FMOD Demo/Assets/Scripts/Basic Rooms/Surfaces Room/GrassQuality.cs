using UnityEngine;
using System.Collections;

public class GrassQuality : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int q = QualitySettings.GetQualityLevel();
        
        for (int i = 0; i < transform.childCount; ++i)
        {
            bool active = true;
            if (q < 3)
                if (i % 3 == 0)
                    active = false;
            if (q < 2)
                if (i % 4 == 0)
                    active = false;
            transform.GetChild(i).gameObject.SetActive(active);
        }

        //Animator anim = GetComponentInParent<Animator>();
        //float rng = Random.Range(0.0f, 1.0f);

        //anim.SetFloat("Start", rng);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
