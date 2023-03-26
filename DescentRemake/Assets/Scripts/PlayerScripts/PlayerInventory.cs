using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory m_Instance;

    public int m_DefaultEnergy;
    public int m_DefaultHealt;
    [HideInInspector] public float m_Energy;
    [HideInInspector] public float m_Healt;
    [HideInInspector] public int m_Score;

    public BasicWeapon[] m_WeaponsPrefabs;
    public Dictionary<int, BasicWeapon> m_Weapons;

    public Transform m_Player;
    public Transform m_WeaponsDepot;

    private BasicWeapon m_primaryToEquip;
    private BasicWeapon m_secondaryToEquip;

    [HideInInspector]
    public BasicWeapon m_LastPrimaryEquiped;
    [HideInInspector]
    public BasicWeapon m_LastSecondaryEquiped;

    public int m_DoorsKey { get; set; }

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
        m_Healt = m_DefaultHealt;
        m_Energy = m_DefaultEnergy;
        m_Score = 0;

        //initializes the weapons dictionary and put the weapons in the depot 
        foreach(BasicWeapon weapon in m_WeaponsPrefabs)
        {
            m_Weapons.Add(weapon.m_WeaponID, null);
            m_Weapons[weapon.m_WeaponID] = Instantiate(weapon, m_WeaponsDepot);
            m_Weapons[weapon.m_WeaponID].gameObject.SetActive(false);
            //Debug.Log(m_weapons[weapon.m_WeaponID].m_WeaponName);
        }

        //equips the first two weapons of the game
        m_Weapons[m_WeaponsPrefabs[0].m_WeaponID].gameObject.SetActive(true);
        m_Weapons[m_WeaponsPrefabs[0].m_WeaponID].transform.SetParent(m_Player, false);
        m_primaryToEquip = m_Weapons[m_WeaponsPrefabs[0].m_WeaponID];
        m_LastPrimaryEquiped = m_primaryToEquip;

        m_Weapons[m_WeaponsPrefabs[2].m_WeaponID].gameObject.SetActive(true);
        m_Weapons[m_WeaponsPrefabs[2].m_WeaponID].transform.SetParent(m_Player, false);
        m_secondaryToEquip = m_Weapons[m_WeaponsPrefabs[2].m_WeaponID];
        m_LastSecondaryEquiped = m_secondaryToEquip;
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
                    //Debug.Log(1);
                    m_primaryToEquip = m_WeaponsPrefabs[0];
                    break;
                case KeyCode.Alpha2:
                    //Debug.Log(2);
                    m_primaryToEquip = m_WeaponsPrefabs[1];
                    break;
                //case KeyCode.Alpha3:
                //    //Debug.Log(3);
                //    m_primaryToEquip = m_WeaponsPrefabs[2];
                //    break;
                case KeyCode.Alpha5:
                    //Debug.Log(5);
                    m_secondaryToEquip = m_WeaponsPrefabs[2];
                    break;
                case KeyCode.Alpha6:
                    //Debug.Log(6);
                    m_secondaryToEquip = m_WeaponsPrefabs[3];
                    break;
                //case KeyCode.Alpha7:
                //    //Debug.Log(7);
                //    m_secondaryToEquip = m_WeaponsPrefabs[5];
                //    break;
                //case KeyCode.Alpha8:
                //    //Debug.Log(8);
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

