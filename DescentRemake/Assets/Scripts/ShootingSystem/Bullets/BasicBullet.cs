using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [HideInInspector] public int m_ShotedFromID;
    public MeshFilter m_Mesh;
    public Material m_Material;
    public int m_Damage;

    public AudioClip m_BulletSound;

    private float m_selfDstructionTimer;

    protected virtual void Update()
    {
        if (m_selfDstructionTimer > 10f) Destroy(gameObject);
        else m_selfDstructionTimer += Time.deltaTime;
    }


}
