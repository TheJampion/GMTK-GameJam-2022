using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private Sprite attackSprite;
    [SerializeField]
    private GameObject attackHitbox;

    private SpriteRenderer spriteRenderer;
    private Collider2D feetCollider;
    private ContactPoint2D[] currentCollisions = new ContactPoint2D[2];
    int numberOfContacts;
    private StateMachine stateMachine;
    public bool isMoving;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        feetCollider = GetComponent<BoxCollider2D>();
        stateMachine = GetComponent<StateMachine>();
    }

    void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        numberOfContacts = feetCollider.GetContacts(currentCollisions);

        if (PlayerCamera.cameraLocked)
        {
            if (transform.position.x <= PlayerCamera.cameraMin.x)
            {
                horizontalInput = Mathf.Max(0f, horizontalInput);
            }
            else if (transform.position.x >= PlayerCamera.cameraMax.x)
            {
                horizontalInput = Mathf.Min(0f, horizontalInput);
            }
        }

        if (numberOfContacts > 0)
        {
            if (currentCollisions[0].normal.y > 0f)
            {
                verticalInput = Mathf.Max(0f, verticalInput);
            }
            else if (currentCollisions[0].normal.y < 0f)
            {
                verticalInput = Mathf.Min(0f, verticalInput);
            }
        }

        switch (horizontalInput)
        {
            case < 0f:
                transform.right = -Vector2.right;
                break;
            case > 0f:
                transform.right = Vector2.right;
                break;
            case 0f:
                break;
        }
        if (verticalInput != 0 || horizontalInput != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        transform.position += new Vector3(horizontalInput * horizontalSpeed * Time.deltaTime, verticalInput * verticalSpeed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stateMachine.isAttacking)
        {
            MoveCharacter();
        }
    }
}
