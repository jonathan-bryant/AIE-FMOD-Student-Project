using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreadmillSpawner : MonoBehaviour
{

    public GameObject[] m_surfaces;
    public float m_speed;
    public int m_numOfTiles;
    public int m_numOfRepititions;
    public List<GameObject> m_gears;

    int m_index;

    List<GameObject> m_floors;
    
    void Start()
    {
        m_floors = new List<GameObject>();
        for (int i = m_numOfTiles - 1; i >= 0; --i)
        {
            AddFloor(transform.position + transform.forward * m_surfaces[0].transform.localScale.z * i);
        }
    }    
    void Update()
    {
        MoveFloors();
        MoveGears();
    }

    void AddFloor(Vector3 a_position)
    {
        int index = m_index / m_numOfRepititions;
        GameObject floor = Instantiate(m_surfaces[Mathf.Clamp(index, 0, 2)]);
        if (a_position == Vector3.zero)
        {
            floor.transform.position = transform.position;
        }
        else
        {
            floor.transform.position = a_position;
        }
        m_index++;
        m_index %= 3 * m_numOfRepititions;
        m_floors.Add(floor);
    }
    void MoveFloors()
    {
        for (int i = 0; i < m_floors.Count; ++i)
        {
            m_floors[i].transform.position += transform.forward * m_speed * Time.deltaTime;
        }
        if (Mathf.Abs(m_floors[0].transform.position.z - transform.position.z) >= m_surfaces[0].transform.localScale.z * (m_numOfTiles - 1))
        {
            Destroy(m_floors[0]);
            m_floors.RemoveAt(0);
            AddFloor(m_floors[0].transform.position - transform.forward * m_surfaces[0].transform.localScale.z * (m_numOfTiles - 1));
        }
    }
    void MoveGears()
    {
        for(int i = 0; i < m_gears.Count; ++i)
        {
            m_gears[i].transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -m_speed / (m_gears[i].transform.localScale.x * 0.5f));
        }
    }

    void OnTriggerStay(Collider a_col)
    {
        if(a_col.gameObject.tag == "Player")
        {
            a_col.gameObject.transform.position += transform.forward * m_speed * Time.deltaTime;
        }
    }

}