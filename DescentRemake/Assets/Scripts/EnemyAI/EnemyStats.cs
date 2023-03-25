using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class EnemyStats : MonoBehaviour, IDamageable, IDestroyable
{
    public int m_LifePoint;
    public ParticleSystem m_ExplosionEffect;
    private MeshRenderer m_meshRenderer;

    private void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (m_LifePoint <= 0) GetDestroyed();
    }

    public void GetDamage(int Damage)
    {
        m_LifePoint -= Damage;
        Debug.Log("GetDamage: " + m_LifePoint);
    }

    public void GetDestroyed(float waitTime = 0)
    {
        //m_meshRenderer.forceRenderingOff = true;
        //m_ExplosionEffect.Play();
        Instantiate(m_ExplosionEffect, transform.position, Quaternion.identity);
        PlayerInventory.m_Instance.m_Score++;
        Destroy(gameObject, waitTime);
    } 
}

public interface IDamageable
{
    public abstract void GetDamage(int Damage);
}

public interface IDestroyable
{
    public abstract void GetDestroyed(float waitTime = 0);
}
