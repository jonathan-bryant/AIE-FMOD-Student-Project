using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour
{
    public float m_explosionRadius = 3.0f;
    public float m_explosionForce = 3.0f;

    ParticleSystem m_particle;

	void Start ()
    {
        m_particle = FindObjectOfType<ParticleSystem>();
	}

    void OnCollisionEnter(Collision a_collision)
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(m_explosionForce, explosionPos, m_explosionRadius, 3.0f);
            }
        }

        //if(a_collision.gameObject.tag == "Ground")
        {
            m_particle.transform.position = transform.position;
            m_particle.Play();
            Destroy(this.gameObject);
        }
    }
}