using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponC : BasicWeapon
{
    public float m_BulletSpeed;
    public float m_FireRate;


    protected override void Initialize()
    {
        m_bulletSpeed = m_BulletSpeed;
        m_fireRate = m_FireRate;
        m_keyToShoot = KeyCode.Mouse0;
        m_bulletsLeft = (int)PlayerInventory.m_Instance.m_Energy;
        m_bulletShots = 0;
    }

    protected override IEnumerator Shoot()
    {
        m_readyToShoot = false;

        GameObject LeftBullet = Instantiate(m_Bullet, m_LeftShotPoint.position, m_LeftShotPoint.rotation);
        GameObject RightBullet = Instantiate(m_Bullet, m_RightShotPoint.position, m_RightShotPoint.rotation);
        LeftBullet.GetComponent<Rigidbody>().velocity = LeftBullet.transform.forward * m_bulletSpeed;
        RightBullet.GetComponent<Rigidbody>().velocity = RightBullet.transform.forward * m_bulletSpeed;

        m_bulletShots++;
        m_bulletsLeft--;

        if (m_allowInvoke)
        {
            Invoke("ResetShoot", m_fireRate);
            m_allowInvoke = false;
        }

        yield return new WaitUntil(() => m_readyToShoot);

        if (Input.GetKey(m_keyToShoot)) StartCoroutine(Shoot());
    }
}
