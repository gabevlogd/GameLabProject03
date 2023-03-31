/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * current script's info: class for player inventory
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory m_Instance;

    public int m_DefaultEnergy, m_MaxEnergy;
    public int m_DefaultHealt, m_MaxHealt;
    public int m_DefaultLives, m_MaxLives;

    [HideInInspector]
    public float m_Energy, m_Healt, m_Lives;
    [HideInInspector]
    public int m_Score, m_Keys;

    public BasicWeapon[] m_WeaponsPrefabs;
    public Dictionary<int, BasicWeapon> m_Weapons;

    public Transform m_PlayerTransform;
    public Transform m_WeaponsDepot;
    public Transform m_SpawnPoint;

    private BasicWeapon m_primaryToEquip;
    private BasicWeapon m_secondaryToEquip;

    [HideInInspector]
    public BasicWeapon m_LastPrimaryEquiped;
    [HideInInspector]
    public BasicWeapon m_LastSecondaryEquiped;

    private void Awake()
    {
        InitializeInventory();
    }

    private void OnGUI()
    {
        SwitchWeapon();
    }

    /// <summary>
    /// Initializes the starting stats of the inventory
    /// </summary>
    private void InitializeInventory()
    {
        m_Instance = this;
        m_Weapons = new Dictionary<int, BasicWeapon>();
        m_Healt = m_DefaultHealt;
        m_Energy = m_DefaultEnergy;
        m_Lives = m_DefaultLives;
        m_Score = 0;
        m_Keys = 0;

        //initializes the weapons dictionary and put the weapons in the depot 
        foreach(BasicWeapon weapon in m_WeaponsPrefabs)
        {
            m_Weapons.Add(weapon.m_WeaponID, null);
            m_Weapons[weapon.m_WeaponID] = Instantiate(weapon, m_WeaponsDepot);
            m_Weapons[weapon.m_WeaponID].gameObject.SetActive(false);

            weapon.m_Unlocked = false;
        }

        //equips the first two weapons of the game
        m_Weapons[m_WeaponsPrefabs[0].m_WeaponID].gameObject.SetActive(true);
        m_Weapons[m_WeaponsPrefabs[0].m_WeaponID].transform.SetParent(m_PlayerTransform, false);
        m_primaryToEquip = m_Weapons[m_WeaponsPrefabs[0].m_WeaponID];
        m_LastPrimaryEquiped = m_primaryToEquip;
        m_Weapons[m_WeaponsPrefabs[0].m_WeaponID].m_Unlocked = true;

        m_Weapons[m_WeaponsPrefabs[2].m_WeaponID].gameObject.SetActive(true);
        m_Weapons[m_WeaponsPrefabs[2].m_WeaponID].transform.SetParent(m_PlayerTransform, false);
        m_secondaryToEquip = m_Weapons[m_WeaponsPrefabs[2].m_WeaponID];
        m_LastSecondaryEquiped = m_secondaryToEquip;
        m_Weapons[m_WeaponsPrefabs[2].m_WeaponID].m_Unlocked = true;
    }

    /// <summary>
    /// Switch weapon to equip
    /// </summary>
    private void SwitchWeapon()
    {
        Event PressedKey = Event.current;

        if (PressedKey.isKey) //if a key is pressed
        {   
            //if keycode match one of the cases set the weapon to equip
            switch (PressedKey.keyCode)
            {
                case KeyCode.Alpha1:
                    if (m_Weapons[m_WeaponsPrefabs[0].m_WeaponID].m_Unlocked == false) //Check if the selected weapon is unlocked
                    {
                        HUDManager.m_Instance.ShowMessageOnHUD("WEAPON LOCKED");
                        break; 
                    }
                    m_primaryToEquip = m_WeaponsPrefabs[0];
                    break;

                case KeyCode.Alpha2:
                    if (m_Weapons[m_WeaponsPrefabs[1].m_WeaponID].m_Unlocked == false)
                    {
                        HUDManager.m_Instance.ShowMessageOnHUD("WEAPON LOCKED");
                        break;
                    }
                    m_primaryToEquip = m_WeaponsPrefabs[1];
                    break;

                case KeyCode.Alpha5:
                    if (m_Weapons[m_WeaponsPrefabs[2].m_WeaponID].m_Unlocked == false)
                    {
                        HUDManager.m_Instance.ShowMessageOnHUD("WEAPON LOCKED");
                        break;
                    }
                    m_secondaryToEquip = m_WeaponsPrefabs[2];
                    break;

                case KeyCode.Alpha6:
                    if (m_Weapons[m_WeaponsPrefabs[3].m_WeaponID].m_BulletsLeft <= 0)
                    {
                        HUDManager.m_Instance.ShowMessageOnHUD("NO AMMO AVAILABLE");
                        break;
                    }
                    m_secondaryToEquip = m_WeaponsPrefabs[3];
                    break;
            }
        }

        //if a new primary weapon is selected equip the new wapon
        if (m_primaryToEquip.m_WeaponID != m_LastPrimaryEquiped.m_WeaponID)
        {
            m_LastPrimaryEquiped.transform.SetParent(m_WeaponsDepot, false);
            m_LastPrimaryEquiped.gameObject.SetActive(false);
            EquipWeapon(m_primaryToEquip.m_WeaponID);
        }
        //if a new primary weapon is selected equip the new wapon
        if (m_secondaryToEquip.m_WeaponID != m_LastSecondaryEquiped.m_WeaponID)
        {
            m_LastSecondaryEquiped.transform.SetParent(m_WeaponsDepot, false);
            m_LastSecondaryEquiped.gameObject.SetActive(false);
            EquipWeapon(m_secondaryToEquip.m_WeaponID);
        }
    }


    /// <summary>
    /// Equip the weapon with the corresponding weaponID
    /// </summary>
    /// <param name="weaponID">ID of the weapon to equip</param>
    private void EquipWeapon(int weaponID)
    {
        m_Weapons[weaponID].gameObject.SetActive(true); //set active the weapon to equip
        m_Weapons[weaponID].transform.SetParent(m_PlayerTransform, false); //set weapon to equip parent of player

        //update the last equiped weapon 
        if (m_Weapons[weaponID].m_WeaponType == WeaponType.Primary)
        {
            m_LastPrimaryEquiped = m_Weapons[weaponID];
            HUDManager.m_Instance.m_CurrentPrimaryEquiped = m_Weapons[weaponID]; //updates the primary equipped weapon reference on UI manager
        }
        else
        {
            m_LastSecondaryEquiped = m_Weapons[weaponID];
            HUDManager.m_Instance.m_CurrentSecondaryEquiped = m_Weapons[weaponID]; //updates the secondary equipped weapon reference on UI manager
        }
    }

}

