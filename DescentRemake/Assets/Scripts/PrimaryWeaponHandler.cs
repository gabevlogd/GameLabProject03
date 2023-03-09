using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryWeaponHandler : MonoBehaviour
{
    public static PrimaryWeaponHandler m_Instance;
    public BasicWeapon[] m_Weapons;

    private void Awake()
    {
        m_Instance = this;
    }

    private void Start()
    {
        Instantiate(m_Weapons[0], this.transform);
    }

    public void EquipWeapon()
    {
        Destroy(GetComponentInChildren<BasicWeapon>().gameObject);

        switch (PlayerInventory.m_Instance.m_PrimaryWeapon)
        {
            case WeaponType.WeaponA:
                Instantiate(m_Weapons[0], this.transform);
                break;
            case WeaponType.WeaponB:
                Instantiate(m_Weapons[1], this.transform);
                break;
            case WeaponType.WeaponC:
                Instantiate(m_Weapons[2], this.transform);
                break;
        }
    }
}
