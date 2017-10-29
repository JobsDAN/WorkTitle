using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float Speed = 20f;
	public int Power = 4;
	
	void Update ()
	{
		transform.position += transform.forward * Speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		EnemyBehavior eb = other.gameObject.GetComponent<EnemyBehavior>();
		if (!eb)
			return;

		eb.TakeDamage(Power);
	}
}
