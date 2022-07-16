using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour
{
    public bool IsAttacking;
    public bool IsInHitstun;
    public bool IsMoving;
    public float timeTillStateOver;
    private Animator _animator;
    private int currentState;
    private float stateTimer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckStateConditions();
        stateTimer += Time.deltaTime;
    }
    private void CheckStateConditions()
    {
        if (IsAttacking)
        {
            SetState("LightAttack1", 0.1f);
            return;
        }
        if (IsInHitstun)
        {
            SetState("Hitstun", 0.1f);
            return;
        }
        if (IsMoving)
        {
            SetState("Run", 0.1f);
            return;
        }
            SetState("Idle", 0);
    }
    private void SetState(string stateName, float transitionDuration)
    {
        int state = Animator.StringToHash(stateName);
        if (state == currentState)
            return;

        _animator.CrossFade(state, transitionDuration);
        timeTillStateOver = _animator.GetCurrentAnimatorStateInfo(0).length;

        stateTimer = 0;
        currentState = state;
    }
}
