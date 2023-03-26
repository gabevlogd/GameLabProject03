using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDamageable, IDestroyable
{
    [HideInInspector]
    public PlayerInventory m_Stats;
    public GameObject m_PlayerUI;
    public Camera m_MiniMap;
    private void Awake()
    {
        m_Stats = PlayerInventory.m_Instance;
        m_MiniMap.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (m_Stats.m_Healt <= 0) GetDestroyed();
        CheckEndGameCondition();
        if (Input.GetKeyDown(KeyCode.Tab)) ShowOrHideMap();
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
        if (m_Stats.m_Score >= 10 || m_Stats.m_Healt <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void ShowOrHideMap()
    {
        if (m_MiniMap.gameObject.activeInHierarchy)
        {
            m_MiniMap.gameObject.SetActive(false);
            m_PlayerUI.SetActive(true);
        }
        else 
        {
            m_MiniMap.gameObject.SetActive(true);
            m_PlayerUI.SetActive(false);
        }
    }


}
