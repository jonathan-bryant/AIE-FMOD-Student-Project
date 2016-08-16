using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreadmillSpawner : MonoBehaviour
{

    public GameObject[] m_surfaces;
    int m_index;
    float m_elapsed;
    public float m_speed;

    public List<GameObject> m_floors;

    // Use this for initialization
    void Start()
    {
        AddFloor(transform.position + transform.right * 10.0f);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void AddFloor(Vector3 a_position)
    {
        GameObject floor = Instantiate(m_surfaces[m_index++]);
        if (a_position == Vector3.zero)
        {
            floor.transform.position = transform.position;
        }
        else
        {
            floor.transform.position = a_position;
        }
        m_index %= 3;
        m_floors.Add(floor);
    }
    void MoveFloors()
    {

    }
}

