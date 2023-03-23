using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BasicBullet
{
    public GameObject m_Radar;
    public enum BulletType { StdBullet, Missile, HomingMissile, Mine }
    public BulletType m_BulletType;
    public enum DamageType { Standard, AOE };
    public DamageType m_DamageType;

    public Transform m_HomingMissileTarget { get; set; }
    public float m_HomingMissilePrecision;
    public float m_DamageRange;


    private int m_damageableLayer = 7;
    private Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        CheckRadarRequest();
    }

    private void Update()
    {
        if (m_HomingMissileTarget != null) HomingMissileBehaviour();
    }



    private void OnTriggerEnter(Collider other)
    {
        //prevents the auto-damage when player shoots
        if (other.TryGetComponent(out PlayerMovement player)) return;
        if (other.TryGetComponent(out BasicBullet bullet)) return;
        if (other.TryGetComponent(out Radar radar)) return;

        //Debug.Log("Bullet: " + m_DamageType.ToString() + " OnTriggerEnter");

        //mines deal damage in the OnTriggerEnter of Radar 
        if (m_BulletType != BulletType.Mine) DealsDamage(other);

        //Bullet gets destroyed by every type of impact
        Destroy(gameObject);
    }

    /// <summary>
    /// Deals damage to the target
    /// </summary>
    /// <param name="target"></param>
    private void DealsDamage(Collider target)
    {
        //select the type of damage
        if (m_DamageType == DamageType.Standard) StandardDamage(target);
        else AOEDamage();
    }

    /// <summary>
    /// Does damage to the hitted target
    /// </summary>
    /// <param name="hittedCollider">the hitted target</param>
    private void StandardDamage(Collider hittedCollider)
    {
        if (hittedCollider.TryGetComponent(out IDamageable target)) target.GetDamage(m_Damage);
    }

    /// <summary>
    /// Does damage to every target in the DamageRange(radius of the area) area
    /// </summary>
    public void AOEDamage()
    {
        LayerMask damageableLayerMask = 1 << m_damageableLayer;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_DamageRange, damageableLayerMask, QueryTriggerInteraction.Collide);
        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
            hitCollider.SendMessage("GetDamage", m_Damage);
        }
    } 

    /// <summary>
    /// Based on bullet type, checks if the bullet needs a radar
    /// </summary>
    private void CheckRadarRequest()
    {
        if (m_BulletType == BulletType.HomingMissile || m_BulletType == BulletType.Mine) m_Radar.SetActive(true);
        else m_Radar.SetActive(false);
    }

    /// <summary>
    /// Makes the homingMissile follows the target acquired by the radar
    /// </summary>
    private void HomingMissileBehaviour()
    {
        //Determine which direction to rotate towards
        Vector3 targetDirection = m_HomingMissileTarget.position - transform.position;

        //Determine how fast is the rotation to the target
        float singleStep = m_HomingMissilePrecision * Time.deltaTime;

        // Rotate the velocity vector towards the target direction by one step
        m_rigidbody.velocity = Vector3.RotateTowards(m_rigidbody.velocity, targetDirection, singleStep, 0.0f);

        //keeps the mesh in the correct orientation (rotate the velocity of rigidbody do not mean rotate also the gameobject)
        transform.rotation = Quaternion.LookRotation(m_rigidbody.velocity);

        //Keeps the radar in the correct position (bug fix)
        m_Radar.transform.localPosition = Vector3.zero;

    }

}
