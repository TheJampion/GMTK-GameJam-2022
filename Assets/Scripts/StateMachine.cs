using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour
{
    protected float timeTillStateOver;
    private Animator _animator;
    public State currentState { get; private set; }
    public State hitstunState;
    protected float stateTimer;
    public float hitstunTime;
    public float hitstunDistance;
    [SerializeField]
    private State startingState;
    private PlayerController playerController;
    private AttackController attackController;
    private Enemy enemy;
    private bool isPlayer;
    public bool isInHitstun;
    public bool isAttacking { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        enemy = GetComponent<Enemy>();
        attackController = GetComponent<AttackController>();
        isPlayer = playerController != null;
    }

    private void Start()
    {
        currentState = startingState;
    }
    private void Update()
    {
        stateTimer += Time.deltaTime;
        float actualStateTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        if (timeTillStateOver != actualStateTime)
        {
            timeTillStateOver = actualStateTime;
        }
        if (isPlayer)
        {
            CheckStateConditionsPlayer();
        }
        else
        {
            CheckStateConditionsEnemy();
        }
    }

    public void CheckStateConditionsPlayer()
    {
        if (isInHitstun)
        {
            if (currentState != hitstunState)
            {
                EnterState(hitstunState, 0);
            }
            else if (stateTimer >= hitstunTime)
            {
                isInHitstun = false;
                EnterState(startingState, 0);
            }
        }
        if (currentState.Transitions.Count > 0)
        {
            for (int i = 0; i < currentState.Transitions.Count; i++)
            {
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.AnimationFinished && stateTimer >= timeTillStateOver)
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.MoveInput && playerController.isMoving)
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.NoMoveInput && !playerController.isMoving)
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.Light && Input.GetKeyDown(KeyCode.F))
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
            }
        }
    }
    public void CheckStateConditionsEnemy()
    {
        if (isInHitstun)
        {
            if (currentState != hitstunState)
            {
                EnterState(hitstunState, 0);
            }
            else if(stateTimer >= hitstunTime)
            {
                isInHitstun = false;
                EnterState(startingState, 0);
            }
        }

        if (currentState.Transitions.Count > 0)
        {
            for (int i = 0; i < currentState.Transitions.Count; i++)
            {
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.AnimationFinished && stateTimer >= timeTillStateOver)
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.MoveInput && enemy.isMoving)
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.NoMoveInput && !enemy.isMoving)
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
            }
        }
    }

    public void EnterState(State state, float transitionDuration)
    {
        _animator.Play(state.StateName);
        if (state.AttackData)
        {
            attackController.currentAttack = state.AttackData;
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        stateTimer = 0;
        timeTillStateOver = _animator.GetCurrentAnimatorStateInfo(0).length;
        currentState = state;
    }
}
