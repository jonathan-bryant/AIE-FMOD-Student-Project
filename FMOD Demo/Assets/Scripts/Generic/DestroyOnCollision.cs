using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour 
{
    void OnCollisionEnter(Collision a_col)
    {
        Destroy(gameObject);
    }
}
