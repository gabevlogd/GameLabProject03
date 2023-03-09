using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory m_Instance;
    public float m_Energy;
    public float m_Healt;
    public WeaponType m_PrimaryWeapon;
    public WeaponType m_SecondaryWeapon;

    private void Awake()
    {
        m_Instance = this;
    }

    private void Update()
    {
        SwitchPrimaryWeapon();
        SwitchSecondaryWeapon();
    }

    private void SwitchPrimaryWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_PrimaryWeapon != WeaponType.WeaponA)
            {
                m_PrimaryWeapon = WeaponType.WeaponA;
                PrimaryWeaponHandler.m_Instance.EquipWeapon();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_PrimaryWeapon != WeaponType.WeaponB)
            {
                m_PrimaryWeapon = WeaponType.WeaponB;
                PrimaryWeaponHandler.m_Instance.EquipWeapon();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (m_PrimaryWeapon != WeaponType.WeaponC)
            {
                m_PrimaryWeapon = WeaponType.WeaponC;
                PrimaryWeaponHandler.m_Instance.EquipWeapon();
            }
        }

        
    }

    private void SwitchSecondaryWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (m_SecondaryWeapon != WeaponType.WeaponD)
            {
                m_SecondaryWeapon = WeaponType.WeaponD;
                SecondaryWeaponHandler.m_Instance.EquipWeapon();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (m_SecondaryWeapon != WeaponType.WeaponE)
            {
                m_SecondaryWeapon = WeaponType.WeaponE;
                SecondaryWeaponHandler.m_Instance.EquipWeapon();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (m_SecondaryWeapon != WeaponType.WeaponF)
            {
                m_SecondaryWeapon = WeaponType.WeaponF;
                SecondaryWeaponHandler.m_Instance.EquipWeapon();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (m_SecondaryWeapon != WeaponType.WeaponG)
            {
                m_SecondaryWeapon = WeaponType.WeaponG;
                SecondaryWeaponHandler.m_Instance.EquipWeapon();
            }
        }
    }
}

public enum WeaponType
{
    WeaponA,
    WeaponB,
    WeaponC,
    WeaponD,
    WeaponE,
    WeaponF,
    WeaponG,
}
