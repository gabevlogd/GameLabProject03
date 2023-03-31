/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDamageable, IDestroyable
{
    private PlayerInventory m_playerStats;

    private void Awake() => m_playerStats = PlayerInventory.m_Instance;
    

    public void GetDamage(int Damage = 0) 
    {
        m_playerStats.m_Healt -= Damage;
    }

    public void GetDestroyed(int waitTime = 0)
    {
        Debug.Log("GameOver");
    }



}
