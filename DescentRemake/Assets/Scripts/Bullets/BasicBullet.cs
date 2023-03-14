using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [HideInInspector] public int m_ShotedFromID;
    public MeshFilter m_Mesh;
    public Material m_Material;
    public int m_Damage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player) || other.TryGetComponent(out BasicBullet bullet)) return;
        Debug.Log("OnTriggerEnter");
        Destroy(gameObject);
    }
}
