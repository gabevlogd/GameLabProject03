using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicWeapon : MonoBehaviour
{
    public GameObject m_Bullet;
    public Transform m_LeftShotPoint;
    public Transform m_RightShotPoint;
    public string m_WeaponName;


    protected float m_bulletSpeed;
    protected float m_fireRate;

    protected int m_bulletShots;
    protected int m_bulletsLeft;

    protected bool m_shooting;
    protected bool m_readyToShoot;
    protected bool m_allowInvoke;

    protected KeyCode m_keyToShoot;

    protected virtual void Awake()
    {
        Initialize();
        m_readyToShoot = true;
        m_allowInvoke = true;
    }

    protected virtual void Update()
    {
        GetInput();
        if (m_shooting && m_readyToShoot && m_bulletsLeft > 0) StartCoroutine(Shoot());
    }


    private void GetInput()
    {
        if (Input.GetKeyDown(m_keyToShoot)) m_shooting = true;
        else m_shooting = false;
    }
    protected abstract IEnumerator Shoot();

    protected void ResetShoot()
    {
        m_readyToShoot = true;
        m_allowInvoke = true;
    }

    protected virtual void Initialize() { }


}
