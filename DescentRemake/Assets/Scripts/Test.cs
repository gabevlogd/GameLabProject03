using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    #region Public Params:
    [Header("Speeds")]
    public float MaxSpeedHorizontal;
    public float MaxSpeedLateral;
    public float MaxSpeedVertical;
    public float MaxSpeedRoll;
    [Header("Accelerations")]
    public float HorizontalAcceleration;
    public float LateralAcceleration;
    public float VerticalAcceleration;
    public float RollAcceleration;
    [Header("Mouse Sensitivity")]
    public float SensX;
    public float SensY;
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
    #endregion

    private void Start()
    {
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

        m_yRotation = Input.GetAxisRaw("Mouse X") * SensX * Time.deltaTime;
        m_xRotation = -Input.GetAxisRaw("Mouse Y") * SensY * Time.deltaTime;
    }
    /// <summary>
    /// Manages the translation speeds on the three axes and the roll speed
    /// </summary>
    private void SetSpeed()
    {
        m_horizontalSpeed = Mathf.Lerp(m_horizontalSpeed, MaxSpeedHorizontal * m_horizontalInput, HorizontalAcceleration * Time.deltaTime);
        m_lateralSpeed = Mathf.Lerp(m_lateralSpeed, MaxSpeedLateral * m_lateralInput, LateralAcceleration * Time.deltaTime);
        m_verticalSpeed = Mathf.Lerp(m_verticalSpeed, MaxSpeedVertical * m_verticalInput, VerticalAcceleration * Time.deltaTime);
        m_rollSpeed = Mathf.Lerp(m_rollSpeed, MaxSpeedRoll * m_rollInput, RollAcceleration * Time.deltaTime);

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
        //calculates rotations on x and y axes and saves them in newForward (i.e. calculates the new transform.forward based on player's input)
        Quaternion yawPitch = transform.rotation * Quaternion.Euler(m_xRotation, m_yRotation, 0f);
        Vector3 newForward = yawPitch * Vector3.forward;

        //calculates the new local green axis of the player (transform.up) with respect to the normal of the reference plane (m_planeNormal)
        float alignment = Vector3.Dot(transform.up, m_planeNormal);
        Vector3 referenceUp = Mathf.Sign(alignment) * m_planeNormal;
        Vector3 targetUp = Vector3.Lerp(transform.up, referenceUp, alignment * alignment); //[approximate calculation]

        //new orientation to assign to the player (i.e. new transform.forward and transform.up based on player's input)
        Quaternion newOrientation = Quaternion.LookRotation(newForward, targetUp);

        //if (!Mathf.Approximately(m_rollInput, 0))
        //{
            //adds roll rotation
            newOrientation = newOrientation * Quaternion.Euler(0f, 0f, -m_rollSpeed * Time.deltaTime);
            //Saves the new reference plane, according to the player's input.
            m_planeNormal = newOrientation * Vector3.up;
        //}

        transform.rotation = newOrientation;
    }

}
