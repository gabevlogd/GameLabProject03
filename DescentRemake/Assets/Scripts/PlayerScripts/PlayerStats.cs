using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDamageable, IDestroyable
{
    [HideInInspector]
    public PlayerInventory m_Stats;
    public GameObject m_PlayerUI;
    public GameObject m_AmmoDropPrefab;
    private void Awake()
    {
        m_Stats = PlayerInventory.m_Instance;
        //m_MiniMap.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckEndGameCondition();
    }

    public void GetDamage(int Damage = 0) 
    {
        Debug.Log("Player Damaged");
        m_Stats.m_Healt -= Damage;
    }

    public void GetDestroyed(int waitTime = 0)
    {
        Debug.Log("GameOver");
        //Destroy(gameObject);
    }

    private void CheckEndGameCondition()
    {
        if (m_Stats.m_Healt <= 0)
        {
            if (m_Stats.m_Lives <= 0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                m_Stats.m_Lives--;
                RespawnPlayer();
            }
        }
        
    }

    private void RespawnPlayer()
    {
        Vector3 dropAmmoSpawnPointPosition = transform.position;
        Quaternion dropAmmoSpawnPointRotation = transform.rotation;

        //reset position and rotation
        transform.position = m_Stats.m_SpawnPoint.position;
        transform.rotation = m_Stats.m_SpawnPoint.rotation;

        //reset player stats
        m_Stats.m_Healt = m_Stats.m_DefaultHealt;
        m_Stats.m_Energy = m_Stats.m_DefaultEnergy;

        //drop all ammo
        Instantiate(m_AmmoDropPrefab, dropAmmoSpawnPointPosition, dropAmmoSpawnPointRotation);
    }



}
