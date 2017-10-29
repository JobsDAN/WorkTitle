using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyInfo))]
[RequireComponent(typeof(Renderer))]
public class EnemyBehavior : MonoBehaviour {
    public GameObject MainTarget { get; private set; }
    public GameObject CurrentTarget { get; private set; }

    private EnemyInfo enemyInfo;
    private Color defaulColor;

    void Start()
    {
        defaulColor = GetComponent<Renderer>().material.color;
        enemyInfo = GetComponent<EnemyInfo>();

        MainTarget = WorldObserver.Instance.Players[Random.Range(0, WorldObserver.Instance.Players.Count)].gameObject;
        CurrentTarget = MainTarget;
    }

    void Update()
    {
        transform.LookAt(CurrentTarget.transform);
        transform.Translate(0, 0, enemyInfo.MovementSpeed * Time.deltaTime);
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

    private void ResetColor()
    {
        GetComponent<Renderer>().material.color = defaulColor;
    }

    void Die()
    {
        WorldObserver.Instance.EnemyKilled();
        Destroy(gameObject);
    }
}