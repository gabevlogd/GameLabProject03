using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory m_Instance;

    public float m_Energy;
    public float m_Healt;

    public BasicWeapon[] m_Weapons;
    public Transform m_PrimaryHand;
    public Transform m_SecondaryHand;

    private WeaponType m_weaponToEquip;
    private WeaponType m_lastEquipedWeapon;

    private void Awake()
    {
        m_Instance = this;
        Instantiate(m_Weapons[0], m_PrimaryHand.transform);
        Instantiate(m_Weapons[3], m_SecondaryHand.transform);
    }

    private void Update()
    {
        SwitchWeapon();
    }

    /// <summary>
    /// Switch weapon to equip
    /// </summary>
    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_weaponToEquip != WeaponType.PrimaryA) m_weaponToEquip = WeaponType.PrimaryA;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_weaponToEquip != WeaponType.PrimaryB) m_weaponToEquip = WeaponType.PrimaryB;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (m_weaponToEquip != WeaponType.PrimaryC) m_weaponToEquip = WeaponType.PrimaryC;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (m_weaponToEquip != WeaponType.SecondaryA) m_weaponToEquip = WeaponType.SecondaryA;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (m_weaponToEquip != WeaponType.SecondaryB) m_weaponToEquip = WeaponType.SecondaryB;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (m_weaponToEquip != WeaponType.SecondaryC) m_weaponToEquip = WeaponType.SecondaryC;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (m_weaponToEquip != WeaponType.SecondaryD) m_weaponToEquip = WeaponType.SecondaryD;
        }

        if (m_weaponToEquip != m_lastEquipedWeapon) EquipWeapon(m_weaponToEquip);
    }

    /// <summary>
    /// Equip weaponToEquip
    /// </summary>
    /// <param name="weaponToEquip">the chosen weapon to equip</param>
    private void EquipWeapon(WeaponType weaponToEquip)
    {
        if((int)weaponToEquip < 3)
        {
            Destroy(m_PrimaryHand.GetComponentInChildren<BasicWeapon>().gameObject);
            Instantiate(m_Weapons[(int)weaponToEquip], m_PrimaryHand.transform);
        }
        else
        {
            Destroy(m_SecondaryHand.GetComponentInChildren<BasicWeapon>().gameObject);
            Instantiate(m_Weapons[(int)weaponToEquip], m_SecondaryHand.transform);
        }
        m_lastEquipedWeapon = weaponToEquip;
    }
}

public enum WeaponType
{
    PrimaryA,
    PrimaryB,
    PrimaryC,
    SecondaryA,
    SecondaryB,
    SecondaryC,
    SecondaryD,
}
