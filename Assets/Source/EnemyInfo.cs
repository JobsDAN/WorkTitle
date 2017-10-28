using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour {
    public float movementSpeed;
	// Use this for initialization
	void Start () {
        WorldObserver.Instance.Enemies.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
