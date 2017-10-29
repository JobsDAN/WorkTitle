using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float speed = 20f;
	float dist = 10f;
	int damage = 4;
	void Start ()
	{
		
	}
	
	void Update ()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		EnemyBehavior eb = other.gameObject.GetComponent<EnemyBehavior>();
		if (!eb)
			return;

		eb.TakeDamage(damage);
	}
}
