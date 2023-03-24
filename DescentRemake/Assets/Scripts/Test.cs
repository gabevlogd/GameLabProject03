using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    #region Public Params:
    [Header("Speeds")]
    public float m_MaxSpeedHorizontal;
    public float m_MaxSpeedLateral;
    public float m_MaxSpeedVertical;
    public float m_MaxSpeedRoll;
    [Header("Accelerations")]
    public float m_HorizontalAcceleration;
    public float m_LateralAcceleration;
    public float m_VerticalAcceleration;
    public float m_RollAcceleration;
    [Header("Mouse Sensitivity")]
    public float m_SensX;
    public float m_SensY;
    #endregion
    #region Private Params:
    private float m_horizontalInput;
    private float m_lateralInput;
    private float m_verticalInput;
    private float m_rollInput;

    private float m_horizontalSpeed;
    private float m_lateralSpeed;
    private float m_verticalSpeed;
    private float m_rollSpeed;

    private float m_xRotation;
    private float m_yRotation;

    private Rigidbody m_rigidbody;
    /// <summary>
    /// normal of the reference plane for calculating rotations
    /// </summary>
    private Vector3 m_planeNormal;
    private Vector3 m_mousePosition;
    #endregion

    private void Start()
    {
        m_mousePosition = Input.mousePosition;
        m_planeNormal = transform.up;
        m_rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GetInput();
        SetSpeed();
        RotatePlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    /// <summary>
    /// receives and saves player's inputs
    /// </summary>
    private void GetInput()
    {
        m_horizontalInput = Input.GetAxisRaw("Horizontal");
        m_lateralInput = Input.GetAxisRaw("Lateral");
        m_verticalInput = Input.GetAxisRaw("Vertical");
        m_rollInput = Input.GetAxisRaw("Roll");

        m_yRotation = Input.GetAxisRaw("Mouse X") * m_SensY * Time.deltaTime;
        m_xRotation = -Input.GetAxisRaw("Mouse Y") * m_SensX * Time.deltaTime;
    }
    /// <summary>
    /// Manages the translation speeds on the three axes and the roll speed
    /// </summary>
    private void SetSpeed()
    {
        m_horizontalSpeed = Mathf.Lerp(m_horizontalSpeed, m_MaxSpeedHorizontal * m_horizontalInput, m_HorizontalAcceleration * Time.deltaTime);
        m_lateralSpeed = Mathf.Lerp(m_lateralSpeed, m_MaxSpeedLateral * m_lateralInput, m_LateralAcceleration * Time.deltaTime);
        m_verticalSpeed = Mathf.Lerp(m_verticalSpeed, m_MaxSpeedVertical * m_verticalInput, m_VerticalAcceleration * Time.deltaTime);
        m_rollSpeed = Mathf.Lerp(m_rollSpeed, m_MaxSpeedRoll * m_rollInput, m_RollAcceleration * Time.deltaTime);

        float[] speeds = { Mathf.Abs(m_horizontalSpeed), Mathf.Abs(m_lateralSpeed), Mathf.Abs(m_verticalSpeed) };
        if (m_rigidbody.velocity.magnitude > Mathf.Max(speeds)) m_rigidbody.velocity = m_rigidbody.velocity.normalized * Mathf.Max(speeds);
    }
    /// <summary>
    /// Manages the translation of player on the three axes
    /// </summary>
    private void MovePlayer()
    {
        m_rigidbody.AddForce(m_horizontalSpeed * transform.forward.normalized, ForceMode.Force);
        m_rigidbody.AddForce(m_lateralSpeed * transform.right.normalized, ForceMode.Force);
        m_rigidbody.AddForce(m_verticalSpeed * transform.up.normalized, ForceMode.Force);
    }
    /// <summary>
    /// Manages the rotation of player on the three axes
    /// </summary>
    private void RotatePlayer()
    {
        transform.rotation *= Quaternion.Euler(m_xRotation, m_yRotation, -m_rollSpeed);
    }

    /// <summary>
    /// Returns true if the player is moving in any direction (considering both rotation and translation)
    /// </summary>
    /// <returns></returns>
    public bool IsMoving()
    {
        bool keyInput = !Mathf.Approximately(m_horizontalInput, 0f) || !Mathf.Approximately(m_lateralInput, 0f) || !Mathf.Approximately(m_verticalInput, 0f) || !Mathf.Approximately(m_rollInput, 0f);
        bool mouseInput = false;

        if (m_mousePosition != Input.mousePosition)
        {
            mouseInput = true;
            m_mousePosition = Input.mousePosition;
        }
        else mouseInput = false;

        if (keyInput || mouseInput) return true;
        return false;
    }

}