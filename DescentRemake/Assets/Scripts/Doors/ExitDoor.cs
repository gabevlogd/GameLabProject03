using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private AITypeBoss m_boss;
    private Animator m_animator;

    private void Awake()
    {
        m_boss = FindObjectOfType<AITypeBoss>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckOpeningCondition();
    }

    private void CheckOpeningCondition()
    {
        if (m_boss == null) m_animator.SetBool("IsOpen", true);
    }
}
