using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class TransitionData
{
    public enum Trigger { None, AnimationFinished, Light, Heavy, Jump, Knockup, Special1, Special2, MoveInput, NoMoveInput }
    public Trigger trigger;
    public State nextState;
}
[CreateAssetMenu(fileName = "State", menuName = "Create State")]
public class State : ScriptableObject
{
    [SerializeField]
    private string stateName;
    [SerializeField]
    private List<TransitionData> transitions;
    [SerializeField]
    private Attack attackData;
    public string StateName { get => stateName; }
    public List<TransitionData> Transitions { get => transitions; }
    public Attack AttackData { get => attackData; }
}
