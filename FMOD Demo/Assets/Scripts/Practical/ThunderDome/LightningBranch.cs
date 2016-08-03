using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningBranch : MonoBehaviour
{
    public int m_branchNum;
    public Lightning m_lightning;
    public Vector3 m_startPosition;
    public Vector3 m_startDirection;
    public float m_startWidth;
    public int m_numOfZags;
    protected List<LightningZag> m_zags;

    public virtual void Destroy()
    {
        for (int i = 0; i < m_zags.Count; ++i)
        {
            GameObject obj = m_zags[i].gameObject;
            m_zags[i].Destroy();
            Destroy(obj);
        }
        m_zags.Clear();
    }

    void Start()
    {
        
    }
    void Update()
    {

    }
}
