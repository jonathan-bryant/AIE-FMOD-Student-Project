using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TreadmillSpawner : MonoBehaviour
{
    public ParticleSystem m_grassParticleEmitter;
    public ParticleSystem m_tileParticleEmitter;
    public ParticleSystem m_carpetParticleEmitter;
    public GameObject[] m_floorTextures;
    public float m_speed;
    public int m_numOfTiles;
    public int m_numOfRepititions;

    int m_index;

    List<GameObject> m_floors;
    List<GameObject> m_walls;

    public Text m_type;
    public Text m_paramValue;

    Transform m_actor;

    void Start()
    {
        m_actor = Camera.main.transform.parent;
        m_floors = new List<GameObject>();
    }
    void Update()
    {
        MoveTreadmill();
        RaycastHit m_info;
        Ray ray = new Ray(m_actor.position - new Vector3(0.0f, m_actor.localScale.y, 0.0f), -m_actor.up);

        int layerMask = (1 << 8);

        Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out m_info, Mathf.Infinity, layerMask))
        {
            m_type.text = "Type: " + m_info.collider.gameObject.name;
            if (m_info.collider.gameObject.name.Contains("Grass"))
                m_paramValue.text = "Value:\n2";
            if (m_info.collider.gameObject.name.Contains("Carpet"))
                m_paramValue.text = "Value:\n1";
            if (m_info.collider.gameObject.name.Contains("Tile"))
                m_paramValue.text = "Value:\n3";
        }
    }

    void AddTrack(Vector3 a_position)
    {
        int index = m_index / m_numOfRepititions;
        index = Mathf.Clamp(index, 0, 2);
        GameObject floor = Instantiate(m_floorTextures[index]);
        floor.name = m_floorTextures[index].name;
        if (a_position == Vector3.zero)
        {
            floor.transform.position = transform.position;
        }
        else
        {
            floor.transform.position = a_position;
        }
        m_floors.Add(floor);
        floor.transform.SetParent(this.gameObject.transform);
        floor.transform.rotation = Quaternion.FromToRotation(floor.transform.forward, transform.forward);
        m_index++;
        m_index %= 3 * m_numOfRepititions;
    }
    void MoveTreadmill()
    {
        if (m_floors.Count == 0)
        {
            AddTrack(Vector3.zero);
        }
        else
        {
            for (int i = 0; i < m_floors.Count; ++i)
            {
                m_floors[i].transform.position += transform.forward * m_speed * Time.deltaTime;
            }
        }

        if ((transform.position - m_floors[0].transform.position).magnitude + (m_floors[0].transform.localScale.z * 0.5f) >= 6.0f)
        {
            if (m_floors[0].tag == "Grass")
            {
                m_grassParticleEmitter.Play();
            }
            else if (m_floors[0].tag == "Tile")
            {
                m_tileParticleEmitter.Play();
            }
            else if (m_floors[0].tag == "Carpet")
            {
                m_carpetParticleEmitter.Play();
            }
        }
        float diff = (transform.position - m_floors[m_floors.Count - 1].transform.position).magnitude;
        if (diff >= m_floors[m_floors.Count - 1].transform.localScale.z * 2.0f)
        {
            if (m_floors.Count == m_numOfTiles)
            {
                Destroy(m_floors[0]);
                m_floors.RemoveAt(0);
            }
            AddTrack(m_floors[m_floors.Count - 1].transform.position - (transform.forward * m_floors[0].transform.localScale.z * 2.0f));
        }
    }

    void OnTriggerStay(Collider a_col)
    {
        if (a_col.gameObject.tag == "Player")
        {
            a_col.gameObject.transform.position += transform.forward * m_speed * Time.deltaTime;
        }
    }

}