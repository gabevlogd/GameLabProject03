using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeedHorizontal;
    public float MaxSpeedLateral;
    public float MaxSpeedVertical;

    public float HorizontalAcceleration;
    public float LateralAcceleration;
    public float VerticalAcceleration;

    public float Friction;

    public Transform Orientation;

    private float m_horizontalInput;
    private float m_lateralInput;
    private float m_verticalInput;

    private float m_horizontalSpeed;
    private float m_lateralSpeed;
    private float m_verticalSpeed;

    private float m_moveHorizontal;
    private float m_moveLateral;
    private float m_moveVertical;

    private Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        GetInput();
        SetSpeed();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        m_horizontalInput = Input.GetAxisRaw("Horizontal");
        m_lateralInput = Input.GetAxisRaw("Lateral");
        m_verticalInput = Input.GetAxisRaw("Vertical");
        //Debug.Log("horizontalInput: " + m_horizontalInput);
        //Debug.Log("lateralInput: " + m_lateralInput);
        //Debug.Log("verticalInput: " + m_verticalInput);
    }

    private void MovePlayer()
    {
        m_rigidbody.AddForce(10f * m_horizontalSpeed * Orientation.forward.normalized, ForceMode.Force);
        m_rigidbody.AddForce(10f * m_lateralSpeed * Orientation.right.normalized, ForceMode.Force);
        m_rigidbody.AddForce(10f * m_verticalSpeed * Orientation.up.normalized, ForceMode.Force);
    }

    private void SetSpeed()
    {
        m_horizontalSpeed = Mathf.Lerp(m_horizontalSpeed, MaxSpeedHorizontal * m_horizontalInput, HorizontalAcceleration * Time.deltaTime);
        m_lateralSpeed = Mathf.Lerp(m_lateralSpeed, MaxSpeedLateral * m_lateralInput, LateralAcceleration * Time.deltaTime);
        m_verticalSpeed = Mathf.Lerp(m_verticalSpeed, MaxSpeedVertical * m_verticalInput, VerticalAcceleration * Time.deltaTime);

        //if (m_horizontalInput == 0) m_horizontalSpeed = Mathf.Lerp(m_horizontalSpeed, 0f, Friction * Time.deltaTime);
        //if (m_lateralInput == 0) m_lateralSpeed = Mathf.Lerp(m_lateralSpeed, 0f, Friction * Time.deltaTime);
        //if (m_verticalInput == 0) m_verticalSpeed = Mathf.Lerp(m_verticalSpeed, 0f, Friction * Time.deltaTime);
    }
}
