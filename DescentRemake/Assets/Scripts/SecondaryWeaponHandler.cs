using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponHandler : MonoBehaviour
{
    public static SecondaryWeaponHandler m_Instance;
    public BasicWeapon[] m_Weapons;

    private void Awake()
    {
        m_Instance = this;
    }
    private void Start()
    {
        Instantiate(m_Weapons[3], this.transform);
    }

    public void EquipWeapon()
    {
        Destroy(GetComponentInChildren<BasicWeapon>().gameObject);

        switch (PlayerInventory.m_Instance.m_PrimaryWeapon)
        {
            case WeaponType.WeaponD:
                Instantiate(m_Weapons[3], this.transform);
                break;
            case WeaponType.WeaponE:
                Instantiate(m_Weapons[4], this.transform);
                break;
            case WeaponType.WeaponF:
                Instantiate(m_Weapons[5], this.transform);
                break;
            case WeaponType.WeaponG:
                Instantiate(m_Weapons[6], this.transform);
                break;
        }
    }
}
