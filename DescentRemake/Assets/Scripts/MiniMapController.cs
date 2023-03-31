using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public Transform m_Player;
    public float m_YawSens, m_PitchSens;
    public int m_MaxZoom, m_MinZoom;

    private Camera m_mapCamera;
    private float m_yawInput, m_pitchInput, m_zoomInput;
    private float m_yawRotation, m_pitchRotation;
    private float m_zoom;

    private void Awake()
    {
        m_mapCamera = GetComponentInChildren<Camera>();
        m_YawSens = 400f;
        m_PitchSens = 400f;
        m_zoom = 20f;
    }

    private void Update()
    {
        FollowPlayer();
        GetMovementInput();
        RotateMap();
        ZoomMap();
    }

    /// <summary>
    /// Makes the Gameobject that script is attached to, follow the player position
    /// </summary>
    private void FollowPlayer()
    {
        transform.position = m_Player.position;
    }

    /// <summary>
    /// Gets the map rotation and zoom inputs
    /// </summary>
    private void GetMovementInput()
    {
        m_yawInput = -Input.GetAxisRaw("Lateral"); // "Lateral" detect the input of A and D keys
        m_pitchInput = -Input.GetAxisRaw("Horizontal"); // "Horizontal" detect the input of W and S keys
        m_zoomInput = Input.GetAxisRaw("Mouse ScrollWheel");
        //Debug.Log(m_zoomInput);
    }

    /// <summary>
    /// Rotates map based on player inputs 
    /// </summary>
    private void RotateMap()
    {
        m_yawRotation = m_yawInput * m_YawSens * Time.deltaTime;
        m_pitchRotation = m_pitchInput * m_PitchSens * Time.deltaTime;

        transform.rotation *= Quaternion.Euler(m_pitchRotation, m_yawRotation, 0f);
    }

    /// <summary>
    /// Zooms in and out the mini map based on player inputs
    /// </summary>
    private void ZoomMap()
    {
        if (m_zoomInput != 0f) m_zoom += m_zoomInput * 5f;
        m_zoom = Mathf.Clamp(m_zoom, m_MinZoom, m_MaxZoom);
        //Debug.Log(m_zoom);
        m_mapCamera.transform.localPosition = new Vector3(0f, 0f, m_zoom);
    }
}
