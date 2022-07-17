using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    //Class that handles what to do when the character gets hit
    //May also be useful for triggering what happens when a hit connects
    private StateMachine stateMachine;
    private HealthHandler healthHandler;
    public GameObject deathParticle;
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
        healthHandler.TakeDamage(damage);
        if (healthHandler.currentHealth <= 0)
        {
            Instantiate(deathParticle, transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            yield return new WaitForSeconds(timeInHitstun);
        }
    }
}
