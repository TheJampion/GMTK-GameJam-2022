using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockCamera : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            onTriggerEnter?.Invoke();
            PlayerCamera.cameraLocked = true;
        }
    }
}
