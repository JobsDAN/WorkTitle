using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour {
    public float movementSpeed;
    public int heals;

	// Use this for initialization
	void Start () {
        WorldObserver.Instance.Enemies.Add(this);
		heals = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
