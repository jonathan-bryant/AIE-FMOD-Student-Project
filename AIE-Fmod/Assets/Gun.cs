using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject m_bullet;
    public float m_firePower;
    public float m_fireRate;
    float m_fireRateElapsed;
    [FMODUnity.EventRef]
    public string m_fireSoundPath;
    FMOD.Studio.EventInstance m_fireSoundEvent;

    void Start()
    {
        m_fireRateElapsed = 0.0f;
        m_fireSoundEvent = FMODUnity.RuntimeManager.CreateInstance(m_fireSoundPath);
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        m_fireRateElapsed = Mathf.Min(m_fireRate, m_fireRateElapsed + Time.deltaTime);
        if (Input.GetMouseButton(1))
        {
            if (m_fireRateElapsed >= m_fireRate)
            {
                m_fireSoundEvent.start();
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_fireSoundEvent, transform, null);
                GameObject bullet = Instantiate(m_bullet, transform.position + (transform.forward * transform.localScale.z) + (transform.forward * m_bullet.transform.localScale.z * 2.0f), new Quaternion()) as GameObject;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * m_firePower);
                m_fireRateElapsed = 0.0f;
            }
        }
    }
}
