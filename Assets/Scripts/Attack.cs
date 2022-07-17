using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Create Attack")]
public class Attack : ScriptableObject
{
    [SerializeField]
    private GameObject hitbox;
    [SerializeField]
    private float hitstunDuration;
    [SerializeField, Range(0f,3f)]
    private float knockbackDistance;
    [SerializeField]
    private bool isKnockUp;
    [SerializeField, Range(0f,2f)]
    private float knockUpDistance;
    [SerializeField]
    private float minimumDistance;
    [SerializeField]
    private float moveSpeed;

    public GameObject Hitbox { get => hitbox; }
    public float HitstunDuration { get => hitstunDuration;}
    public float KnockbackDistance { get => knockbackDistance;}
    public bool IsKnockUp { get => isKnockUp;}
    public float KnockUpDistance { get => knockUpDistance;}
    public float MinimumDistance { get => minimumDistance;}
    public float MoveSpeed { get => moveSpeed;}
}
