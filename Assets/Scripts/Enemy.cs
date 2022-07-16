using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void onDestroyDelegate(Enemy enemy);
    public onDestroyDelegate onDestroy;

    public float speed;
    public bool activated;
    private PlayerController player;

    private void Awake()
    {
       player =  FindObjectOfType<PlayerController>();
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke(this);
    }

    private void Update()
    {
        if (activated)
        {
            if(Vector3.Distance(player.transform.position, transform.position) > 2.0f)
            {
                transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
            }
        }
    }
}
