using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable, IDestroyable
{
    public int m_LifePoint;

    private void Update()
    {
        if (m_LifePoint <= 0) GetDestroyed();
    }

    public void GetDamage(int Damage)
    {
        Debug.Log("GetDamage: " + m_LifePoint);
        m_LifePoint -= Damage;
    }

    public void GetDestroyed() => Destroy(gameObject);
}

public interface IDamageable
{
    public abstract void GetDamage(int Damage);
}

public interface IDestroyable
{
    public abstract void GetDestroyed();
}
