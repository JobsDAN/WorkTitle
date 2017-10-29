using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour {
    public float movementSpeed;
    public int HealthPoints;
    public float attackRange;
    public int attackPower;
    public int attackCooldown;
    
    public float lastAttackTime;

	void Start()
	{
		HealthPoints = 5;
	}
	
	void Update()
	{
		
	}
}
