﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	class AttackInfo {
		public AttackInfo(KeyCode key, float cooldown, AttackImpl attack)
		{
			this.key = key;
			this.cooldown = cooldown;
			this.last = 0;
			this.attack = attack;
		}

		public KeyCode key { get; private set; }

		public float cooldown { get; private set; }
		public float last { get; set; }

		public delegate void AttackImpl();
		public AttackImpl attack { get; private set; }
	}

	[SerializeField]
	private float RadialCooldown = 0.5f;
	[SerializeField]
	private float KickCooldown = 0.5f;
	[SerializeField]
	private float ShotCooldown = 1;

	public GameObject RadialAreaPrefab;

	public float KickRadius = 2;
	public int KickForce = 2;

	public float ShotRadius = 4;
	public int ShotForce = 1;

	private List<AttackInfo> attacks;

	void Start()
	{
		attacks = new List<AttackInfo>();
		attacks.Add(new AttackInfo(KeyCode.A, RadialCooldown, RadialAttack));
		attacks.Add(new AttackInfo(KeyCode.S, KickCooldown, Kick));
		attacks.Add(new AttackInfo(KeyCode.D, ShotCooldown, Shot));
	}

	void RadialAttack()
	{
		Debug.Log("Radial!");
		Instantiate(RadialAreaPrefab, transform.position, new Quaternion());
	}
	
	void Kick()
	{
		Debug.Log("Kick!");
		Collider[] colliders = Physics.OverlapSphere(transform.position, KickRadius);
		foreach (Collider c in colliders)
		{
			GameObject go = c.gameObject;
			if (!go)
				continue;

			EnemyBehavior eb = go.GetComponent<EnemyBehavior>();
			if (!eb)
				continue;

			Debug.Log("Find!");
			Vector3 dir = c.transform.position - transform.position;
			Quaternion enemyRot = Quaternion.LookRotation(dir);
			float diff = Quaternion.Angle(transform.rotation, enemyRot);
			if (diff > 40)
				continue;

			eb.TakeDamage(KickForce);
		}
	}

	void Shot()
	{
		Debug.Log("Shot!");
	}

	void UseAttack(AttackInfo attack)
	{
		if (Time.time - attack.last < attack.cooldown)
			return;

		attack.attack();
		attack.last = Time.time;
	}

	void Update()
	{
		foreach (AttackInfo attack in attacks)
		{
			if (Input.GetKey(attack.key))
				UseAttack(attack);
		}
	}

}
