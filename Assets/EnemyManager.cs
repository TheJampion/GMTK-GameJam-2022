using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;
    public HealthBarUI enemyHealthBar;
    public Queue<Enemy> enemyAttackQueue = new Queue<Enemy>();
    private float attackTimer;
    [SerializeField]
    private float timeBetweenAttacks = 1.5f;
    public bool waveStarted;

    public void Attack()
    {
        attackTimer = 0;
        if (enemyAttackQueue.Count > 0)
        {
            Enemy enemy = enemyAttackQueue.Dequeue();
            if (enemy)
            {
                enemy.PrepareToAttack();
            }
        }
    }

    public void AddEnemyToQueue(Enemy enemy)
    {
        enemyAttackQueue.Enqueue(enemy);
    }

    public void UpdateEnemyHealthBar(Enemy enemy)
    {
      enemyHealthBar.healthHandler =  enemy.GetComponent<HealthHandler>();
    }
    public void UpdateQueue()
    {
        if (enemyAttackQueue.Count(o => o == null) > 0)
        {
            enemyAttackQueue = new Queue<Enemy>(enemyAttackQueue.Where(o => o != null));
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waveStarted)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > timeBetweenAttacks)
            {
                Attack();
            }
        }
        else
        {
            attackTimer = 0;
        }
    }
}
