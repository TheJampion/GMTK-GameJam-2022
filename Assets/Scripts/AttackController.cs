using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private HitboxCollisionHandler currentHitbox;
    public Attack currentAttack;
    public bool attackActive;
    public void ActivateAttack()
    {

        if (currentHitbox != null)
            Destroy(currentHitbox.gameObject);
        GameObject newAttack = Instantiate(currentAttack.Hitbox, transform);
        currentHitbox = newAttack.GetComponent<HitboxCollisionHandler>();
        currentHitbox.attackData = currentAttack;
        attackActive = true;
        if (transform.rotation.y >= 180)
        {
            Vector3 attackPos = newAttack.transform.localPosition;
            newAttack.transform.localPosition = new Vector3(-attackPos.x, attackPos.y, attackPos.z);
        }
        currentHitbox.onHit += DeactivateAttack;
    }

    public void DeactivateAttack()
    {
        if (attackActive)
        {
            attackActive = false;
            currentHitbox.onHit -= DeactivateAttack;
            Destroy(currentHitbox.gameObject);
            currentHitbox = null;
        }
    }
}
