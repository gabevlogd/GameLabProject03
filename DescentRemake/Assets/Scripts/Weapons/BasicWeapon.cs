using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType { Primary, Secondary }

public abstract class BasicWeapon : MonoBehaviour
{
    #region Public fields:
    public BasicBullet m_Bullet;
    //=========================================
    public Transform m_FirePoint;
    public Transform m_FirePointsParent;
    public Vector3[] m_FirePointsPositions;
    //=========================================
    public string m_WeaponName;
    public int m_WeaponID;
    //=========================================
    public WeaponType m_WeaponType;
    public enum FireType { Simultaneous, Alternated }
    public FireType m_FireType;
    //=========================================
    public float m_FirePointsRotation;
    public float m_BulletSpeed;
    public float m_TimeBetweenShots;
    [Range(1f,5f)]
    public int m_BulletsPerShot;
    public float m_TimeBetweenBullets;
    #endregion

    #region Protected fields:
    protected Transform[] m_firePoints;
    //=========================================
    protected int m_bulletsShots;
    [HideInInspector] public int m_BulletsLeft;
    protected int m_indexForDelegate;
    //=========================================
    protected bool m_shooting;
    protected bool m_readyToShoot;
    protected bool m_allowInvoke;
    protected bool m_alreadyShoot;
    //=========================================
    protected delegate void SpawnTypeHandler();
    protected SpawnTypeHandler m_spawnTypeHandler;
    #endregion

    protected virtual void Awake()
    {
        ResetShoot();
    }

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        GetInput();
        if (m_shooting && m_readyToShoot && m_BulletsLeft > 0) StartCoroutine(Shoot());
    }

    protected abstract void GetInput();

    protected virtual void Initialize()
    {
        //Debug.Log("Initiallize");
        m_firePoints = new Transform[m_FirePointsPositions.Length];

        for (int i = 0; i < m_FirePointsPositions.Length; i++)
        {
            m_firePoints[i] = Instantiate(m_FirePoint, m_FirePointsParent);
            m_firePoints[i].localPosition = m_FirePointsPositions[i];

            if (m_FireType == FireType.Simultaneous) m_spawnTypeHandler += SimultaneousSpawn;
            else m_spawnTypeHandler += AlternatedSpawn;
        }

        Destroy(m_FirePoint.gameObject);

        ResetShoot();
    }

    protected virtual IEnumerator Shoot()
    {
        m_readyToShoot = false;

        SpawnBullets();

        if (m_allowInvoke)
        {
            Invoke("ResetShoot", m_TimeBetweenShots);
            m_allowInvoke = false;
        }

        yield return new WaitUntil(() => m_readyToShoot);

        if (m_shooting && m_BulletsLeft > 0) StartCoroutine(Shoot());
    }

    protected virtual void ResetShoot()
    {
        m_readyToShoot = true;
        m_allowInvoke = true;
        m_bulletsShots = 0;
        m_alreadyShoot = false;
    }

    protected virtual void SpawnBullets()
    {
        m_bulletsShots++;
        m_BulletsLeft--;
        

        m_spawnTypeHandler();

        if (m_firePoints.Length > 1) RotateFirePoints();

        if (m_bulletsShots < m_BulletsPerShot && !m_readyToShoot && m_FireType == FireType.Simultaneous) Invoke("SpawnBullets", m_TimeBetweenBullets);
    }

    /// <summary>
    /// Causes the weapon to fire simultaneously from all fire points
    /// </summary>
    protected virtual void SimultaneousSpawn()
    {
        BasicBullet Bullet = Instantiate(m_Bullet, m_firePoints[m_indexForDelegate].position, m_firePoints[m_indexForDelegate].rotation);
        Bullet.GetComponent<Rigidbody>().velocity = Bullet.transform.forward * m_BulletSpeed;
        Bullet.m_ShotedFromID = this.m_WeaponID;

        UpdateDelegateIndex();
    }

    /// <summary>
    /// Causes the weapon to fire from only one fire point at a time
    /// </summary>
    protected virtual void AlternatedSpawn()
    {
        if (m_alreadyShoot) return;

        BasicBullet Bullet = Instantiate(m_Bullet, m_firePoints[m_indexForDelegate].position, m_firePoints[m_indexForDelegate].rotation);
        Bullet.GetComponent<Rigidbody>().velocity = Bullet.transform.forward * m_BulletSpeed;
        Bullet.m_ShotedFromID = this.m_WeaponID;

        //if (m_bulletsShots == m_BulletsPerShot)
        //{
            UpdateDelegateIndex();
            m_alreadyShoot = true;
        //}
    }

    /// <summary>
    /// Difficult to explain its use in just a line (See tool's documentation)
    /// </summary>
    protected virtual void UpdateDelegateIndex()
    {
        if (m_indexForDelegate == m_firePoints.Length - 1) m_indexForDelegate = 0;
        else m_indexForDelegate++;
    }

    protected virtual void RotateFirePoints()
    {
        m_FirePointsParent.localRotation *= Quaternion.Euler(0f, 0f, m_FirePointsRotation);
    }



}
