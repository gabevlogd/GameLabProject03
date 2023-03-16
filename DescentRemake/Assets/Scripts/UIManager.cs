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

    private PlayerInventory m_playerInventory;

    private void Awake()
    {
        m_Instance = this;
        m_playerInventory = PlayerInventory.m_Instance;
        InitializeUI();
    }

    void Update()
    {
        UpdateHealtOnUI();
        UpdateEnergyOnUI();
        UpdatePrimaryWeaponOnUI();
        UpdateSecondaryWeaponOnUI();
        UpdateAmmoOnUI();
    }

    /// <summary>
    /// Sets the initial stats of the UI
    /// </summary>
    private void InitializeUI()
    {
        m_healt = m_playerInventory.m_Healt;
        m_energyLeft = m_playerInventory.m_Energy;
        m_Healt.text = "Shield: " + m_healt.ToString();
        m_Energy.text = "Energy: " + m_energyLeft.ToString();

        m_CurrentPrimaryEquiped = m_playerInventory.m_Weapons[m_playerInventory.m_LastPrimaryEquiped.m_WeaponID];
        m_PrimaryWeapon.text = m_CurrentPrimaryEquiped.m_WeaponName;

        m_CurrentSecondaryEquiped = m_playerInventory.m_Weapons[m_playerInventory.m_LastSecondaryEquiped.m_WeaponID];
        m_SecondaryWeapon.text = m_CurrentSecondaryEquiped.m_WeaponName;

    }

    /// <summary>
    /// Updates the value of healt on UI
    /// </summary>
    private void UpdateHealtOnUI()
    {
        if (m_healt != m_playerInventory.m_Healt)
        {
            m_healt = m_playerInventory.m_Healt;
            m_Healt.text = "Shield: " + m_healt.ToString();
        }
    }


    /// <summary>
    /// Update the value of energy on the UI
    /// </summary>
    private void UpdateEnergyOnUI()
    {
        if (m_energyLeft != m_playerInventory.m_Energy)
        {
            m_energyLeft = m_playerInventory.m_Energy;

            int energyLeftInt = (int)m_energyLeft;
            float energyLeftDecimalPart = m_energyLeft - energyLeftInt;

            if (Mathf.Approximately(energyLeftDecimalPart, 0f)) m_Energy.text = "Energy: " + m_energyLeft.ToString();
        }
        
    }

    /// <summary>
    /// Updates the current equipped primary weapon on UI
    /// </summary>
    private void UpdatePrimaryWeaponOnUI()
    {
        if(m_CurrentPrimaryEquiped != m_lastPrimaryEquiped)
        {
            //Debug.Log("Update Primary Info");
            m_PrimaryWeapon.text = m_CurrentPrimaryEquiped.m_WeaponName;
            m_lastPrimaryEquiped = m_CurrentPrimaryEquiped;
        }
    }

    /// <summary>
    /// Updates the current equipped secondary weapon on UI
    /// </summary>
    private void UpdateSecondaryWeaponOnUI()
    {
        if (m_CurrentSecondaryEquiped != m_lastSecondaryEquiped)
        {
            //Debug.Log("Update Secondary Info");
            m_SecondaryWeapon.text = m_CurrentSecondaryEquiped.m_WeaponName;
            m_lastSecondaryEquiped = m_CurrentSecondaryEquiped;
        }
    }

    /// <summary>
    /// Updates the counter of the current secondary weapon's ammo equipped on UI
    /// </summary>
    private void UpdateAmmoOnUI()
    {
        m_SecondaryAmmo.text = m_CurrentSecondaryEquiped.m_BulletsLeft.ToString();
    }
}
