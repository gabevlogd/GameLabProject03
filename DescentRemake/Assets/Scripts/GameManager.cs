using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerInventory m_Stats;
    public GameObject m_AmmoDropPrefab;

    private void Awake()
    {
        m_Stats = PlayerInventory.m_Instance;
    }

    private void Update()
    {
        CheckGameOverCondition();
    }

    private void CheckGameOverCondition()
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
        Vector3 dropAmmoSpawnPointPosition = m_Stats.m_PlayerTransform.transform.position;
        Quaternion dropAmmoSpawnPointRotation = m_Stats.m_PlayerTransform.transform.rotation;

        //reset position and rotation
        m_Stats.m_PlayerTransform.transform.position = m_Stats.m_SpawnPoint.position;
        m_Stats.m_PlayerTransform.transform.rotation = m_Stats.m_SpawnPoint.rotation;

        //reset player stats
        m_Stats.m_Healt = m_Stats.m_DefaultHealt;
        m_Stats.m_Energy = m_Stats.m_DefaultEnergy;

        //drop all ammo
        Instantiate(m_AmmoDropPrefab, dropAmmoSpawnPointPosition, dropAmmoSpawnPointRotation);
    }
}
