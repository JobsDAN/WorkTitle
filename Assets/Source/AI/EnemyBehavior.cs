using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyInfo))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehavior : MonoBehaviour {
    public GameObject MainTarget { get; private set; }

    private EnemyInfo enemyInfo;
    private Color defaulColor;

    [SerializeField]
    private float scanRadius;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        defaulColor = GetComponent<Renderer>().material.color;
        enemyInfo = GetComponent<EnemyInfo>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        MainTarget = WorldObserver.Instance.Players[Random.Range(0, WorldObserver.Instance.Players.Count)].gameObject;
    }

    void Update()
    {
        GameObject currentTarget = FindPriorityTarget();
        if (currentTarget == null)
        {
            if (MainTarget == null)
            {
                StopMoving();
                return;
            }

            currentTarget = MainTarget;
        }


        if (enemyInfo.attackRange >= Vector3.Distance(transform.position, currentTarget.transform.position))
        {
            DealDamage(currentTarget);
        }
        else
        {
            MoveToTarget(currentTarget);
        }
    }

    public void DealDamage(GameObject target)
    {
        if (enemyInfo.lastAttackTime + enemyInfo.attackCooldown < Time.time)
        {
            PlayerInfo player = target.GetComponent<PlayerInfo>();
            if (player != null)
            {
                player.TakeDamage(enemyInfo.attackPower);
                enemyInfo.lastAttackTime = Time.time;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemyInfo.HealthPoints -= damage;
        GetComponent<Renderer>().material.color = Color.red;
        CancelInvoke("ResetColor");
        Invoke("ResetColor", 0.1f);
        if (enemyInfo.HealthPoints <= 0)
            Die();
    }

    private GameObject FindPriorityTarget()
    {
        // just for test
        Collider[] units = Physics.OverlapSphere(transform.position, scanRadius);

        GameObject target = null;
        foreach (var unit in units)
        {
            if (unit.GetComponent<PlayerInfo>() != null)
            {
                target = unit.gameObject;
                break;
            }
        }

        return target;
    }

    private void MoveToTarget(GameObject target)
    {
        navMeshAgent.destination = target.transform.position;
    }

    private void StopMoving()
    {
        navMeshAgent.destination = transform.position;
    }

    private void ResetColor()
    {
        GetComponent<Renderer>().material.color = defaulColor;
    }

    void Die()
    {
        WorldObserver.Instance.EnemyKilled(enemyInfo);
        Destroy(gameObject);
    }
}