using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviours : MonoBehaviour , IDamageable, IDestroyable
{
    public bool m_IsDestroyable;
    public bool m_NeedsKey;
    public float m_DefaultClosingTime;
    public int m_Healt;

    private MeshRenderer m_meshRenderer;
    private Animator m_animator;
    private Collider m_collider;
    private bool m_isOpen;
    private float m_closingTime;

    private void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();

        InitializeDoor();
    }

    private void Update()
    {
        if (m_isOpen) CheckClosingConditions();
        if (m_IsDestroyable && m_Healt <= 0) GetDestroyed(/* needs param */);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (!m_isOpen) CheckOpeningConditions(collision);
    }

    private void InitializeDoor()
    {
        if (m_IsDestroyable)
        {
            m_NeedsKey = false;
            if (m_Healt == 0) m_Healt = 10; //Random value if user do not sets the Health 
        }

        m_closingTime = m_DefaultClosingTime;
        m_isOpen = false;
    }

    private void CheckOpeningConditions(Collision collision)
    {
        Debug.Log("CheckOpeningConditions");
        if (m_IsDestroyable)
        {
            GetDamage(1);
            return;
        }

        if (!m_NeedsKey) 
        {
            OpenDoor();
            return;
        }
        else
        {
            if (PlayerInventory.m_Instance.m_Keys >= 1) OpenDoor();
            else
            {
                //Debug.Log("Needs a key");
                HUDManager.m_Instance.ShowMessageOnHUD("DOOR LOCKED: KEY REQUIRED");
            }

            return;
        }
    }


    private void CheckClosingConditions()
    {
        m_closingTime -= Time.deltaTime;
        if (m_closingTime <= 0)
        {
            m_closingTime = m_DefaultClosingTime;
            CloseDoor();
        }
    }


    public void OpenDoor()
    {
        if (/*Need to find a method to check if the door is running the close animation*/ true) 
        {
            m_collider.isTrigger = true;
            m_animator.SetBool("IsOpen", true);
            m_isOpen = true;
        }
    }

    private void CloseDoor()
    {
        m_animator.SetBool("IsOpen", false);
        m_collider.isTrigger = false;
        m_isOpen = false;
    }

    public void GetDamage(int Damage = 0)
    {
        m_Healt -= Damage;
        Debug.Log("Door damaged, healt left: " + m_Healt.ToString());
    }

    public void GetDestroyed(int waitTime = 0)
    {
        //Run VFX 
        //m_meshRenderer.forceRenderingOff = true; //Test if it work

        //Temporary
        m_collider.isTrigger = true;
        m_animator.SetBool("IsOpen", true);
        m_isOpen = true;
        //end 

        Debug.Log("Destruction in: " + waitTime + " seconds");
        Destroy(gameObject, waitTime);
    }
}
