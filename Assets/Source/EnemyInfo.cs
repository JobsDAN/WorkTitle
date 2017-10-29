using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour {
    public float movementSpeed;
    public int heals;
    public float attackRange;
    public int attackPower;
    public int attackCooldown;
    
    public float lastAttackTime;

	void Start()
	{
		heals = 5;
	}
	
	void Update()
	{
		
	}
}
