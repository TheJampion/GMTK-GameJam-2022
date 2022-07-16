using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCollisionHandler : MonoBehaviour
{
    private HitHandler myHitHandler;
    public float damage = 5f;

    private void Awake()
    {
        myHitHandler = GetComponentInParent<HitHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitHandler hitHandler = collision.GetComponentInParent<HitHandler>();
        if (hitHandler == null)
            return;
        
        if (hitHandler.HitIndex != myHitHandler.HitIndex)
        {
            bool onSamePlane = Mathf.Abs(myHitHandler.transform.position.y - hitHandler.transform.position.y) <= 0.35f;
            if (onSamePlane)
            {
                hitHandler.GetHit(damage); // Eventually should pass in the attackData associated with the hitbox (Should hold knockback, hitstun, damage, etc.)

            }
        }
    }
}
