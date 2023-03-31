using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public AudioClip m_DoorSound;

    private AITypeBoss m_boss;
    private Animator m_animator;
    private float m_endingTimer;

    private void Awake()
    {
        m_boss = FindObjectOfType<AITypeBoss>();
        m_animator = GetComponent<Animator>();
        m_endingTimer = 50f;
    }

    void Update()
    {
        CheckOpeningCondition();
        CheckTimerStatus();
    }

    private void CheckOpeningCondition()
    {
        if (m_boss == null)
        {
            if(!m_animator.GetBool("IsOpen"))
            {
                m_animator.SetBool("IsOpen", true);
                if (m_DoorSound != null) SoundManager.Instance.PlayWorldSound(m_DoorSound);
            }

            StartEndingTimer();
        }
    }

    private void StartEndingTimer()
    {
        if (!HUDManager.m_Instance.m_Timer.gameObject.activeInHierarchy) HUDManager.m_Instance.m_Timer.gameObject.SetActive(true);
        m_endingTimer -= Time.deltaTime;
        int endingTimer = (int)m_endingTimer;
        HUDManager.m_Instance.m_Timer.text = endingTimer.ToString();
    }

    private void CheckTimerStatus()
    {
        if (m_endingTimer <= 0f)
        {
            PlayerInventory.m_Instance.m_Healt = 0;
            PlayerInventory.m_Instance.m_Lives = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
