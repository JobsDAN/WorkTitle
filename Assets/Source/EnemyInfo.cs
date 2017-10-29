using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour {
    public float movementSpeed;
    public int heals;

	void Start()
	{
        WorldObserver.Instance.Enemies.Add(this);
		heals = 5;
	}
	
	void Update()
	{
		
	}
}
