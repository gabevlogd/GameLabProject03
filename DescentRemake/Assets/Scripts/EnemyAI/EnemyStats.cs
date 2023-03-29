using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable, IDestroyable
{
    public int m_LifePoint;
    public ParticleSystem m_ExplosionEffect;

    private void Update()
    {
        if (m_LifePoint <= 0) GetDestroyed();
    }

    public void GetDamage(int Damage = 0)
    {
        m_LifePoint -= Damage;
        Debug.Log("GetDamage: " + m_LifePoint);
    }

    public void GetDestroyed(int waitTime = 0)
    {
        Instantiate(m_ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject, waitTime);
    } 
}

public interface IDamageable
{
    public abstract void GetDamage(int Damage = 0);
}

public interface IDestroyable
{
    public abstract void GetDestroyed(int waitTime = 0);
}
