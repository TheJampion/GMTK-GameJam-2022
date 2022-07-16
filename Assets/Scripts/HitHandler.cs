using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    //Class that handles what to do when the character gets hit
    //May also be useful for triggering what happens when a hit connects
    private StateMachine stateMachine;
    private HealthHandler healthHandler;
    public Collider2D feetCollider { get; private set; }
    [SerializeField]
    private int hitIndex;

    public int HitIndex { get { return hitIndex; } private set { hitIndex = value; } }

    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        feetCollider = GetComponent<Collider2D>();
        healthHandler = GetComponent<HealthHandler>();
    }
    public void GetHit(float damage)
    {
        StartCoroutine(GetHitCoroutine(0.3f, damage));
    }

    IEnumerator GetHitCoroutine(float timeInHitstun, float damage)
    {
        stateMachine.IsInHitstun = true;
        healthHandler.TakeDamage(damage);
        if (healthHandler.currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
        else
        {
            yield return new WaitForSeconds(timeInHitstun);
            stateMachine.IsInHitstun = false;
        }
    }

    private IEnumerator Death()
    {
        yield return null;
        //play Death animation
        //wait for animation to finish
        Destroy(gameObject);
    }
}
