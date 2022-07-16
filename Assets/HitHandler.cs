using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    //Class that handles what to do when the character gets hit
    //May also be useful for triggering what happens when a hit connects
    private StateMachine stateMachine;
    public Collider2D feetCollider { get; private set; }
    [SerializeField]
    private int hitIndex;

    public int HitIndex { get { return hitIndex; } private set { hitIndex = value; } }

    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        feetCollider = GetComponent<Collider2D>();
    }
    public void GetHit()
    {
        StartCoroutine(GetHitCoroutine(0.3f));
    }

    IEnumerator GetHitCoroutine(float timeInHitstun)
    {
        stateMachine.IsInHitstun = true;
        yield return new WaitForSeconds(timeInHitstun);
        stateMachine.IsInHitstun = false;
    }
}
