using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    public Transform m_Player;

    private void Update()
    {
        transform.position = m_Player.position;
    }
}
