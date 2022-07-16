using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour
{
    protected float timeTillStateOver;
    private Animator _animator;
    public State currentState { get; private set; }
    protected float stateTimer;
    [SerializeField]
    private State startingState;
    private PlayerController playerController;
    private Enemy enemy;
    private bool isPlayer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        enemy = GetComponent<Enemy>();
        isPlayer = playerController != null;
    }

    private void Start()
    {
        currentState = startingState;
    }
    private void Update()
    {
        stateTimer += Time.deltaTime;
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
        if (currentState.Transitions.Count > 0)
        {
            for (int i = 0; i < currentState.Transitions.Count; i++)
            {
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.AnimationFinished && stateTimer >= timeTillStateOver)
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.MoveInput)
                {
                    if (enemy.isMoving)
                    {
                        EnterState(currentState.Transitions[i].nextState, 0);
                    }
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.NoMoveInput)
                {
                    if (!enemy.isMoving)
                    {
                        EnterState(currentState.Transitions[i].nextState, 0);
                    }
                }
            }
        }
    }
    public void CheckStateConditionsEnemy()
    {
        if (currentState.Transitions.Count > 0)
        {
            for (int i = 0; i < currentState.Transitions.Count; i++)
            {
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.AnimationFinished && stateTimer >= timeTillStateOver)
                {
                    EnterState(currentState.Transitions[i].nextState, 0);
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.MoveInput)
                {
                    if (playerController)
                    {
                        if (playerController.isMoving)
                        {
                            EnterState(currentState.Transitions[i].nextState, 0);
                        }
                    }
                    if (enemy)
                    {
                        if (enemy.isMoving)
                        {
                            EnterState(currentState.Transitions[i].nextState, 0);
                        }
                    }
                }
                if (currentState.Transitions[i].trigger == TransitionData.Trigger.NoMoveInput)
                {
                    if (playerController)
                    {
                        if (!playerController.isMoving)
                        {
                            EnterState(currentState.Transitions[i].nextState, 0);
                        }
                    }
                    if (enemy)
                    {
                        if (!enemy.isMoving)
                        {
                            EnterState(currentState.Transitions[i].nextState, 0);
                        }
                    }
                }
            }
        }
    }
    public void EnterState(State state, float transitionDuration)
    {
        _animator.Play(state.StateName);
        timeTillStateOver = _animator.GetCurrentAnimatorStateInfo(0).length;
        currentState = state;
    }
}
