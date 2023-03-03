using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float SensX;
    public float SensY;

    public Transform Orientation;

    private float m_xRotation;
    private float m_yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * SensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * SensY * Time.deltaTime;
        m_xRotation -= mouseY;
        m_yRotation += mouseX;

        transform.rotation = Quaternion.Euler(m_xRotation, m_yRotation, 0f);
        Orientation.rotation = Quaternion.Euler(0f, m_yRotation, 0f);
    }

}
