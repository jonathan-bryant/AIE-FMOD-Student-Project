using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightning : MonoBehaviour
{
    public Material m_material;

    public Vector3 m_direction;
    public float m_randDirectionAngle;

    public Vector2 m_randMainAngle;
    public Vector2 m_randSubAngle;

    [Range(0, 100)]
    public int m_mainBranchChance;
    [Range(0, 100)]
    public int m_subBranchChance;
    public int m_maxNumOfBranches;

    public Vector2 m_randNumOfMainSplits;
    public Vector2 m_randNumOfSubSplits;

    public Vector2 m_randNumOfMainZags;
    public Vector2 m_randNumOfSubZags;

    public Vector2 m_randMainWidth;
    [Range(0.0f, 100.0f)]
    public float m_randMinMainWidthDegradation;
    [Range(0.0f, 100.0f)]
    public float m_randMaxMainWidthDegradation;
    [Range(0.0f, 100.0f)]
    public float m_randMinSubWidthDegradation;
    [Range(0.0f, 100.0f)]
    public float m_randMaxSubWidthDegradation;

    public Vector2 m_randMainLength;
    public Vector2 m_randSubLength;

    public Vector2 m_duration, m_interval;
    [Range(0.0f, 100.0f)]
    public float m_randMinFadeoutPercent, m_rnadMaxFadeoutPercent;

    float m_durationElapsed, m_interalElapsed;

    LightningMainBranch m_mainBranch;

    void Start()
    {
        m_durationElapsed = Random.Range(m_duration.x, m_duration.y);
        GenerateLightning();
    }

    void GenerateLightning()
    {
        if (m_mainBranch)
        {
            m_mainBranch.Destroy();
            Destroy(m_mainBranch.gameObject);
        }
        GameObject mainBranchObj = new GameObject();
        mainBranchObj.name = "Main";
        mainBranchObj.layer = 8;
        m_mainBranch = mainBranchObj.AddComponent<LightningMainBranch>();
        m_mainBranch.m_lightning = this;
        m_mainBranch.transform.parent = this.transform;
        m_mainBranch.m_branchNum = 0;
    }

    void Update()
    {
        m_durationElapsed -= Time.deltaTime;
        if (m_durationElapsed <= 0.0f)
        {
            m_durationElapsed = Random.Range(m_duration.x, m_duration.y);
            GenerateLightning();
        }
    }
}