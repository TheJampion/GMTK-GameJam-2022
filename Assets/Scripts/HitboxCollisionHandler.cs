using System;
using UnityEngine;

public class HitboxCollisionHandler : MonoBehaviour
{
    private HitHandler myHitHandler;
    public Attack attackData;
    public float damage = 5f;
    public Action onHit;

    private void Awake()
    {
        myHitHandler = GetComponentInParent<HitHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitHandler hitHandler = collision.GetComponentInParent<HitHandler>();
        StateMachine enemyStateMachine = collision.GetComponentInParent<StateMachine>();
        Enemy enemy = collision.GetComponentInParent<Enemy>();
        if (hitHandler == null)
            return;
        if (hitHandler.HitIndex != myHitHandler.HitIndex)
        {
            bool onSamePlane = Mathf.Abs(myHitHandler.transform.position.y - hitHandler.transform.position.y) <= 0.35f;
            if (onSamePlane)
            {
                hitHandler.transform.right = -myHitHandler.transform.right;
                enemyStateMachine.isInHitstun = true;
                enemyStateMachine.hitstunTime = attackData.HitstunDuration;
                enemyStateMachine.hitstunDistance = attackData.KnockbackDistance;
                hitHandler.GetHit(damage); // Eventually should pass in the attackData associated with the hitbox (Should hold knockback, hitstun, damage, etc.)
                if (enemy)
                {
                    EnemyManager.instance.UpdateEnemyHealthBar(enemy);
                }
                onHit?.Invoke();
            }
        }
    }
}
