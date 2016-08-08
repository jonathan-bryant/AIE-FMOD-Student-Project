﻿using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    public float m_timer;
    float m_elapsed;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_elapsed += Time.deltaTime;
        if (m_elapsed >= m_timer)
            Destroy(gameObject);
	}
}