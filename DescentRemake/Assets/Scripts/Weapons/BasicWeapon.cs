using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicWeapon : MonoBehaviour
{
    public BasicBullet m_Bullet;
    public Transform m_FirePointOne;
    public Transform m_FirePointTwo;
    public Vector3 m_FirePointOnePosition;
    public Vector3 m_FirePointTwoPosition;
    public KeyCode m_KeyToShoot;
    public string m_WeaponName;
    public int m_WeaponID;
    public enum FireType { Simultaneous, Alternating }
    public FireType m_FireType;
    public bool m_allowButtonHold;
    public float m_BulletSpeed;
    public float m_TimeBetweenShots;
    public int m_BulletsPerShot;
    public float m_TimeBetweenBullets;

    protected int m_bulletsShots;
    protected int m_bulletsLeft;

    protected bool m_shooting;
    protected bool m_readyToShoot;
    protected bool m_allowInvoke;


    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        GetInput();
        if (m_shooting && m_readyToShoot && m_bulletsLeft > 0) StartCoroutine(Shoot());
    }


    protected abstract void GetInput();
    protected abstract IEnumerator Shoot();

    protected virtual void ResetShoot()
    {
        m_readyToShoot = true;
        m_allowInvoke = true;
        m_bulletsShots = 0;
    }

    protected virtual void Initialize()
    {
        ResetShoot();
        m_FirePointOne.localPosition = m_FirePointOnePosition;
        m_FirePointTwo.localPosition = m_FirePointTwoPosition;
    }


}
