using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void onDestroyDelegate(Enemy enemy);
    public onDestroyDelegate onDestroy;

    public float speed;
    public bool activated;
    [SerializeField]
    private GameObject attackHitbox;
    private StateMachine stateMachine;
    private bool isTryingToAttack;
    private PlayerController player;
    public bool isMoving;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        stateMachine = GetComponent<StateMachine>();
    }

    public void PrepareToAttack()
    {
        isTryingToAttack = true;
    }

    private void AttackAction()
    {
        StartCoroutine(Attack(attackHitbox));
    }

    IEnumerator Attack(GameObject hitbox)
    {
        yield return new WaitForSeconds(0.3f);
        GameObject attack = Instantiate(hitbox, transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(attack);
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke(this);
        EnemyManager.instance.UpdateQueue();
    }

    private void MoveInDirection(Vector3 direction, float speed)
    {
        transform.right = player.transform.position.x - transform.position.x > 0 ? Vector3.right : -Vector3.right ;
        transform.position += direction * speed * Time.deltaTime;
        isMoving = true;
    }

    private void Update()
    {
        if (activated)
        {
            if (isTryingToAttack)
            {
                if (Vector3.Distance(player.transform.position, transform.position) > 2.0f)
                {
                    MoveInDirection((player.transform.position - transform.position).normalized, speed);
                }
                else if(Mathf.Abs(transform.position.y - player.transform.position.y) > 0.2f)
                {
                    Vector3 direction = player.transform.position.y - transform.position.y > 0 ? Vector3.up : -Vector3.up;
                    MoveInDirection(direction, speed);
                }
                else
                {
                    AttackAction();
                    EnemyManager.instance.AddEnemyToQueue(this);
                    isTryingToAttack = false;
                }
            }
            else
            {
                isMoving = false;
            }
        }
    }
}
