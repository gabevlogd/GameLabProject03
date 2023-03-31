/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * current script's info: class for the bobbing motion of camera
 * author: Gabriele Garofalo
 */
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
    private float m_speed;
    private PlayerMovement m_player;
    private void Awake()
    {
        m_player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        SetSpeed();
        BobbingMotion();
    }

    private void BobbingMotion()
    {
        if (!m_player.IsMoving())
        {
            m_distanceFromCenterY = m_BobbingAmplitude * Mathf.Sin(m_speed);
            transform.localPosition = new Vector3(0f, m_distanceFromCenterY, m_DistanceFromCenterZ);
        }
    }

    private void SetSpeed()
    {
        if (m_speed > 1f) m_BobbingSpeed *= -1f;
        else if (m_speed < -1f) m_BobbingSpeed *= -1f;

        if (!m_player.IsMoving()) m_speed += Time.deltaTime * m_BobbingSpeed;
        //Debug.Log(m_speed);
    }


}
