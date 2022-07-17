using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleWhenDone : MonoBehaviour
{
    private ParticleSystem ps;
    private bool played = false;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps.isPlaying)
        {
            played = true;
        }
        if(ps.isStopped && played)
        {
            Destroy(gameObject);
        }
    }
}
