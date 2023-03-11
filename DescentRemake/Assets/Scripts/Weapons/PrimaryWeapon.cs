using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrimaryWeapon : BasicWeapon
{
    /// <summary>
    /// true = last fire point used is PointOne; false = last fire point used is PointTwo
    /// </summary>
    protected bool m_lastFiringPointUsed;

    protected override void Initialize()
    {
        base.Initialize();
        m_bulletsLeft = (int)PlayerInventory.m_Instance.m_Energy;
        m_bulletsShots = 0;
    }

    protected override void GetInput()
    {
        if (m_allowButtonHold) m_shooting = Input.GetKey(m_KeyToShoot);
        else m_shooting = Input.GetKeyDown(m_KeyToShoot);
    }

    protected override IEnumerator Shoot()
    {
        m_readyToShoot = false;

        SpawnBullets();

        if (m_allowInvoke)
        {
            Invoke("ResetShoot", m_TimeBetweenShots);
            m_allowInvoke = false;
        }

        yield return new WaitUntil(() => m_readyToShoot);

        if (m_shooting && m_bulletsLeft > 0) StartCoroutine(Shoot());
    }

    private void SpawnBullets()
    {
        m_bulletsShots++;
        m_bulletsLeft--;

        if (m_FireType == FireType.Simultaneous)
        {
            BasicBullet BulletOne = Instantiate(m_Bullet, m_FirePointOne.position, m_FirePointOne.rotation);
            BasicBullet BulletTwo = Instantiate(m_Bullet, m_FirePointTwo.position, m_FirePointTwo.rotation);
            BulletOne.GetComponent<Rigidbody>().velocity = BulletOne.transform.forward * m_BulletSpeed;
            BulletTwo.GetComponent<Rigidbody>().velocity = BulletTwo.transform.forward * m_BulletSpeed;
            BulletOne.m_ShotedFromID = this.m_WeaponID;
            BulletTwo.m_ShotedFromID = this.m_WeaponID;
        }
        else
        {
            BasicBullet Bullet;

            if (m_lastFiringPointUsed) Bullet = Instantiate(m_Bullet, m_FirePointTwo.position, m_FirePointTwo.rotation);
            else Bullet = Instantiate(m_Bullet, m_FirePointOne.position, m_FirePointOne.rotation);

            Bullet.GetComponent<Rigidbody>().velocity = Bullet.transform.forward * m_BulletSpeed;
            Bullet.m_ShotedFromID = this.m_WeaponID;

            if (m_bulletsShots == m_BulletsPerShot) m_lastFiringPointUsed ^= true; //if it's true become false and vice versa
        }

        if (m_bulletsShots < m_BulletsPerShot && !m_readyToShoot) Invoke("SpawnBullets", m_TimeBetweenBullets);
    }
}
