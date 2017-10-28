using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyInfo))]
public class EnemyBehavior : MonoBehaviour {
    public GameObject MainTarget { get; private set; }
    public GameObject CurrentTarget { get; private set; }

    private EnemyInfo enemyInfo;

	void Start () {
        enemyInfo = GetComponent<EnemyInfo>();

        MainTarget = WorldObserver.Instance.Players[Random.Range(0, WorldObserver.Instance.Players.Count)].gameObject;
        CurrentTarget = MainTarget;
	}
	
	void Update () {
        transform.LookAt(CurrentTarget.transform);
        transform.Translate(0, 0, enemyInfo.movementSpeed * Time.deltaTime);
	}
}
