using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour {
    public float MovementSpeed;
    public int HealthPoints;

	void Start()
	{
        WorldObserver.Instance.Enemies.Add(this);
		HealthPoints = 5;
	}
	
	void Update()
	{
		
	}
}
