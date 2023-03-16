using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable, IDestroyable
{
    [HideInInspector]
    public PlayerInventory m_Stats;
    private void Awake()
    {
        m_Stats = PlayerInventory.m_Instance;
    }

    private void Update()
    {
        if (m_Stats.m_Healt <= 0) GetDestroyed();
    }

    public void GetDamage(int Damage) 
    {
        Debug.Log("Player Damaged");
        m_Stats.m_Healt -= Damage;
    }

    public void GetDestroyed()
    {
        Debug.Log("GameOver");
        //Destroy(gameObject);
    }

}
