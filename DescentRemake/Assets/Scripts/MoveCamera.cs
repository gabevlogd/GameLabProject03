using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float m_DistanceFromCenterZ;
    [Range(0f, 1f)]
    public float m_BobbingAmplitude;
    public float m_BobbingSpeed;

    private float m_distanceFromCenterY;
    private PlayerMovement m_player;
    private void Awake()
    {
        m_player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        BobbingMotion();
    }

    private void BobbingMotion()
    {
        if (!m_player.IsMoving())
        {
            m_distanceFromCenterY = m_BobbingAmplitude * Mathf.Sin(m_BobbingSpeed * Time.time);
            transform.localPosition = new Vector3(0f, m_distanceFromCenterY, m_DistanceFromCenterZ);
        }
    }


}
