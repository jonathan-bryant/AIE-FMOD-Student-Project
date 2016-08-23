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
    public List<GameObject> m_gears;

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
        for (int i = m_numOfTiles - 1; i >= 0; --i)
        {
            AddTrack(transform.position + transform.forward * m_floorTextures[0].transform.localScale.z * 2.0f * i);
        }
    }
    void Update()
    {
        MoveTreadmill();
        MoveGears();
        RaycastHit m_info;
        Ray ray = new Ray(m_actor.position - new Vector3(0.0f, m_actor.localScale.y, 0.0f), -m_actor.up);

        int layerMask = (1 << 8);

        Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out m_info, Mathf.Infinity, layerMask))
        {
            m_type.text = "Type:\n" + m_info.collider.gameObject.name;
            if (m_info.collider.gameObject.name == "Grass")
                m_paramValue.text = "Value:\n2.0f";
            if (m_info.collider.gameObject.name == "Carpet")
                m_paramValue.text = "Value:\n1.0f";
            if (m_info.collider.gameObject.name == "Tile")
                m_paramValue.text = "Value:\n3.0f";
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

        m_index++;
        m_index %= 3 * m_numOfRepititions;
    }
    void MoveTreadmill()
    {
        for (int i = 0; i < m_floors.Count; ++i)
        {
            m_floors[i].transform.position += transform.forward * m_speed * Time.deltaTime;

            if (m_floors[i].transform.position.z <= -19.75 + m_floors[i].transform.localScale.z * 0.5f)
            {
                if (m_floors[i].tag == "Grass")
                {
                    m_grassParticleEmitter.Play();
                }
                else if (m_floors[i].tag == "Tile")
                {
                    m_tileParticleEmitter.Play();
                }
                else if (m_floors[i].tag == "Carpet")
                {
                    m_carpetParticleEmitter.Play();
                }
            }
        }
        if (Mathf.Abs(m_floors[0].transform.position.z - transform.position.z) >= m_floorTextures[0].transform.localScale.z * 2.0f * (m_numOfTiles - 1))
        {
            Destroy(m_floors[0]);
            m_floors.RemoveAt(0);
            AddTrack(m_floors[0].transform.position - transform.forward * m_floorTextures[0].transform.localScale.z * 2.0f * (m_numOfTiles - 1));
        }
    }
    void MoveGears()
    {
        for (int i = 0; i < m_gears.Count; ++i)
        {
            m_gears[i].transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -m_speed / (m_gears[i].transform.localScale.x * 0.5f));
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