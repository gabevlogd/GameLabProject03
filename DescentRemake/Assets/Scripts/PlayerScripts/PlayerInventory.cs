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
    public int m_Score, m_Keys, m_Survivors;

    public BasicWeapon[] m_WeaponsPrefabs;
    public Dictionary<int, BasicWeapon> m_Weapons;
    public Dictionary<int, int> m_AmmoCounters;
    public Dictionary<int, bool> m_UnlockedWeapon;

    public Transform m_Player;
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
        DontDestroyOnLoad(gameObject);
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
        m_AmmoCounters = new Dictionary<int, int>();
        m_UnlockedWeapon = new Dictionary<int, bool>();
        m_Healt = m_DefaultHealt;
        m_Energy = m_DefaultEnergy;
        m_Lives = m_DefaultLives;
        m_Score = 0;
        m_Keys = 0;
        m_Survivors = 0;

        //initializes the weapons dictionary and put the weapons in the depot 
        foreach(BasicWeapon weapon in m_WeaponsPrefabs)
        {
            m_Weapons.Add(weapon.m_WeaponID, null);
            m_Weapons[weapon.m_WeaponID] = Instantiate(weapon, m_WeaponsDepot);
            m_Weapons[weapon.m_WeaponID].gameObject.SetActive(false);

            m_UnlockedWeapon.Add(weapon.m_WeaponID, false);
        }

        //Initialize the AmmoCounters dictionary
        foreach(BasicWeapon weapon in m_WeaponsPrefabs)
        {
            //if the weapon is primary does not use ammo but energy so continue
            if (m_Weapons[weapon.m_WeaponID].m_WeaponType == WeaponType.Primary) continue;

            //Pass to the ammo dictionary the number of ammo associated to the weapon ID
            SecondaryWeapon secondaryWeapon = m_Weapons[weapon.m_WeaponID] as SecondaryWeapon;
            m_AmmoCounters.Add(weapon.m_WeaponID, secondaryWeapon.m_MagazineCapacity);
        }

        //equips the first two weapons of the game
        m_Weapons[m_WeaponsPrefabs[0].m_WeaponID].gameObject.SetActive(true);
        m_Weapons[m_WeaponsPrefabs[0].m_WeaponID].transform.SetParent(m_Player, false);
        m_primaryToEquip = m_Weapons[m_WeaponsPrefabs[0].m_WeaponID];
        m_LastPrimaryEquiped = m_primaryToEquip;
        m_UnlockedWeapon[m_WeaponsPrefabs[0].m_WeaponID] = true;

        m_Weapons[m_WeaponsPrefabs[2].m_WeaponID].gameObject.SetActive(true);
        m_Weapons[m_WeaponsPrefabs[2].m_WeaponID].transform.SetParent(m_Player, false);
        m_secondaryToEquip = m_Weapons[m_WeaponsPrefabs[2].m_WeaponID];
        m_LastSecondaryEquiped = m_secondaryToEquip;
        m_UnlockedWeapon[m_WeaponsPrefabs[2].m_WeaponID] = true;
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
                    if (m_UnlockedWeapon[m_WeaponsPrefabs[0].m_WeaponID] == false) break; //Check if the selected weapon is unlocked
                    m_primaryToEquip = m_WeaponsPrefabs[0];
                    break;
                case KeyCode.Alpha2:
                    if (m_UnlockedWeapon[m_WeaponsPrefabs[1].m_WeaponID] == false) break;
                    m_primaryToEquip = m_WeaponsPrefabs[1];
                    break;
                //case KeyCode.Alpha3:
                //    if (m_UnlockedWeapon[m_WeaponsPrefabs[2].m_WeaponID] == false) break;
                //    m_primaryToEquip = m_WeaponsPrefabs[2];
                //    break;
                case KeyCode.Alpha5:
                    if (m_UnlockedWeapon[m_WeaponsPrefabs[2].m_WeaponID] == false) break;
                    m_secondaryToEquip = m_WeaponsPrefabs[2];
                    break;
                case KeyCode.Alpha6:
                    if (m_UnlockedWeapon[m_WeaponsPrefabs[3].m_WeaponID] == false) break;
                    m_secondaryToEquip = m_WeaponsPrefabs[3];
                    break;
                    //case KeyCode.Alpha7:
                    //    if (m_UnlockedWeapon[m_WeaponsPrefabs[5].m_WeaponID] == false) break;
                    //    m_secondaryToEquip = m_WeaponsPrefabs[5];
                    //    break;
                    //case KeyCode.Alpha8:
                    //    if (m_UnlockedWeapon[m_WeaponsPrefabs[6].m_WeaponID] == false) break;
                    //    m_secondaryToEquip = m_WeaponsPrefabs[6];
                    //    break;
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
        m_Weapons[weaponID].transform.SetParent(m_Player, false); //set weapon to equip parent of player

        //update the last equiped weapon 
        if (m_Weapons[weaponID].m_WeaponType == WeaponType.Primary)
        {
            m_LastPrimaryEquiped = m_Weapons[weaponID];
            UIManager.m_Instance.m_CurrentPrimaryEquiped = m_Weapons[weaponID]; //updates the primary equipped weapon reference on UI manager
        }
        else
        {
            m_LastSecondaryEquiped = m_Weapons[weaponID];
            UIManager.m_Instance.m_CurrentSecondaryEquiped = m_Weapons[weaponID]; //updates the secondary equipped weapon reference on UI manager
        }
    }

}

