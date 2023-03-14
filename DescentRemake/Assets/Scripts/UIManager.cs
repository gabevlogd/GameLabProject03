using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager m_Instance;
    public Text m_Healt;
    public Text m_Energy;
    public Text m_PrimaryWeapon;
    public Text m_SecondaryWeapon;
    public Text m_SecondaryAmmo;

    private float m_energyLeft;
    private float m_healt;
    private BasicWeapon m_lastSecondaryEquiped;
    private BasicWeapon m_lastPrimaryEquiped;
    [HideInInspector] public BasicWeapon m_CurrentSecondaryEquiped;
    [HideInInspector] public BasicWeapon m_CurrentPrimaryEquiped;

    private void Awake()
    {
        m_Instance = this;
        InitializeUI();
    }

    void Update()
    {
        UpdateEnergyOnUI();
        UpdatePrimaryWeaponOnUI();
        UpdateSecondaryWeaponOnUI();
    }

    private void InitializeUI()
    {
        m_healt = PlayerInventory.m_Instance.m_Healt;
        m_energyLeft = PlayerInventory.m_Instance.m_Energy;
        m_Healt.text = "Shield: " + m_healt.ToString();
        m_Energy.text = "Energy: " + m_energyLeft.ToString();

        m_CurrentPrimaryEquiped = PlayerInventory.m_Instance.m_PrimaryHand.GetComponentInChildren<BasicWeapon>();
        m_PrimaryWeapon.text = m_CurrentPrimaryEquiped.m_WeaponName;
        m_lastPrimaryEquiped = m_CurrentPrimaryEquiped;

        m_CurrentSecondaryEquiped = PlayerInventory.m_Instance.m_SecondaryHand.GetComponentInChildren<BasicWeapon>();
        m_SecondaryWeapon.text = m_CurrentSecondaryEquiped.m_WeaponName;
        m_SecondaryAmmo.text = m_CurrentSecondaryEquiped.m_BulletsLeft.ToString();
        m_lastSecondaryEquiped = m_CurrentSecondaryEquiped;

    }

    private void UpdateHealtOnUI()
    {
        if (m_healt != PlayerInventory.m_Instance.m_Healt)
        {
            m_healt = PlayerInventory.m_Instance.m_Healt;
            m_Healt.text = "Shield: " + m_healt.ToString();
        }
    }


    /// <summary>
    /// Update the value of energy on the UI
    /// </summary>
    private void UpdateEnergyOnUI()
    {
        if (m_energyLeft != PlayerInventory.m_Instance.m_Energy)
        {
            m_energyLeft = PlayerInventory.m_Instance.m_Energy;

            int energyLeftInt = (int)m_energyLeft;
            float energyLeftDecimalPart = m_energyLeft - energyLeftInt;

            if (Mathf.Approximately(energyLeftDecimalPart, 0f)) m_Energy.text = "Energy: " + m_energyLeft.ToString();
        }
        
    }

    private void UpdatePrimaryWeaponOnUI()
    {
        if(m_CurrentPrimaryEquiped != m_lastPrimaryEquiped)
        {
            m_PrimaryWeapon.text = m_CurrentPrimaryEquiped.m_WeaponName;
            m_lastPrimaryEquiped = m_CurrentPrimaryEquiped;
        }
    }

    private void UpdateSecondaryWeaponOnUI()
    {
        if (m_CurrentSecondaryEquiped != m_lastSecondaryEquiped)
        {
            m_SecondaryWeapon.text = m_CurrentSecondaryEquiped.m_WeaponName;
            m_SecondaryAmmo.text = m_CurrentSecondaryEquiped.m_BulletsLeft.ToString();
            m_lastSecondaryEquiped = m_CurrentSecondaryEquiped;
        }
    }
}
