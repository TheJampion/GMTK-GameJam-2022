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
    private bool isAttacking;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    private void AttackAction()
    {
        StartCoroutine(Attack(attackHitbox));
    }

    IEnumerator Attack(GameObject hitbox)
    {
        isAttacking = true;
        Sprite originalSprite = spriteRenderer.sprite;
        spriteRenderer.sprite = attackSprite;

        GameObject attack = Instantiate(hitbox, transform);
        yield return new WaitForSeconds(0.25f);
        Destroy(attack);
        spriteRenderer.sprite = originalSprite;
        isAttacking = false;
    }
    void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        numberOfContacts = feetCollider.GetContacts(currentCollisions);
        
        if(numberOfContacts > 0)
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
                spriteRenderer.flipX = true;
                break;
            case > 0f:
                spriteRenderer.flipX = false;
                break;
        }
        transform.position += new Vector3(horizontalInput * horizontalSpeed * Time.deltaTime, verticalInput * verticalSpeed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            AttackAction();
        }
        MoveCharacter();
    }
}