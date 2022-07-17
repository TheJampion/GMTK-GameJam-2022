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
    private State attackState;
    private StateMachine stateMachine;
    private bool isTryingToAttack;
    private PlayerController player;
    public bool isMoving;
    private float moveTimer;
    private Vector2 target;
    [SerializeField]
    private State heavyState;

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
        float random = UnityEngine.Random.Range(0f, 1f);
        if(random > 0.5f || !heavyState)
        {
            stateMachine.EnterState(attackState, 0);
        }
        else if (heavyState)
        {
            stateMachine.EnterState(heavyState, 0);
        }
        EnemyManager.instance.attackTimer = 0f;
        EnemyManager.instance.waitingForAttackFinish = false;
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke(this);
        EnemyManager.instance.UpdateQueue();
    }

    private void MoveInDirection(Vector3 direction, float speed)
    {
        transform.right = player.transform.position.x - transform.position.x > 0 ? Vector3.right : -Vector3.right;
        transform.position += direction * speed * Time.deltaTime;
        isMoving = true;
    }



    private void Update()
    {
        if (stateMachine.isInHitstun)
        {
            transform.position += -transform.right * stateMachine.hitstunDistance * Time.deltaTime;
            return;
        }
        if (!activated)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 5.0f)
            {
                activated = true;
                EnemyManager.instance.waveStarted = true;
                EnemyManager.instance.AddEnemyToQueue(this);
            }
        }
        else
        {
            if (isTryingToAttack)
            {
                if (Vector3.Distance(player.transform.position, transform.position) > 2.0f)
                {
                    MoveInDirection((player.transform.position - transform.position).normalized, speed);
                }
                else if (Mathf.Abs(transform.position.y - player.transform.position.y) > 0.2f)
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
                if (!stateMachine.isAttacking)
                {
                    moveTimer += Time.deltaTime;
                    if (Vector2.Distance((Vector2)transform.position, target) < 0.25f)
                    {
                        isMoving = false;
                    }
                    else
                    {
                        transform.right = target.x - transform.position.x > 0 ? Vector3.right : -Vector3.right;
                        transform.position += ((Vector3)target - transform.position).normalized * speed * Time.deltaTime;
                        isMoving = true;
                    }
                    if (moveTimer > 4f)
                    {
                        target = PlayerCamera.randomPointInsideCameraView();
                        moveTimer = 0;
                    }
                }
            }
        }
    }
}
