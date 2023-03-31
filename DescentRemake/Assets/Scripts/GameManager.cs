using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerInventory m_Stats;
    public GameObject m_AmmoDropPrefab;
    public GameObject m_MiniMapController;

    public GameObject m_PauseUI, m_HUD, m_MiniMapUI;

    public AudioClip m_MusicLevelOne;

    private void Awake()
    {
        m_Stats = PlayerInventory.m_Instance;
        SoundManager.Instance.PlayMusic(m_MusicLevelOne);
    }

    private void Update()
    {
        CheckUIState();
        CheckGameOverCondition();
        CheckMusicOption();
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

        //restart music
        SoundManager.Instance.PlayMusic(m_MusicLevelOne);
    }

    private void CheckUIState()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !m_MiniMapUI.activeInHierarchy)
        {
            ShowOrHideUI(m_PauseUI);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !m_PauseUI.activeInHierarchy)
        {
            m_MiniMapController.SetActive(m_MiniMapController.activeInHierarchy ^ true);
            ShowOrHideUI(m_MiniMapUI);
            return;
        }
    }

    private void ShowOrHideUI(GameObject UI)
    {
        m_Stats.m_PlayerTransform.gameObject.SetActive(m_Stats.m_PlayerTransform.gameObject.activeInHierarchy ^ true);
        m_HUD.gameObject.SetActive(m_HUD.gameObject.activeInHierarchy ^ true);
        UI.SetActive(UI.activeInHierarchy ^ true);
    }

    private void CheckMusicOption()
    {
        if (SoundManager.Instance.m_MusicOn && !SoundManager.Instance.MusicSource.isPlaying) SoundManager.Instance.PlayMusic(m_MusicLevelOne);
        else if (!SoundManager.Instance.m_MusicOn && SoundManager.Instance.MusicSource.isPlaying) SoundManager.Instance.MusicSource.Stop();
    }
}
