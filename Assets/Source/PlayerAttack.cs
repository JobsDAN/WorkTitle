using System.Collections;
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
	private float RadialCooldown = 1;
	[SerializeField]
	private float KickCooldown = 1;
	[SerializeField]
	private float ShotCooldown = 2;

	private List<AttackInfo> attacks;

	void Start () {
		attacks = new List<AttackInfo>();
		attacks.Add(new AttackInfo(KeyCode.A, RadialCooldown, RadialAttack));
		attacks.Add(new AttackInfo(KeyCode.S, KickCooldown, Kick));
		attacks.Add(new AttackInfo(KeyCode.D, ShotCooldown, Shot));
	}

	void RadialAttack() {
		Debug.Log("Radial!");
	}
	
	void Kick() {
		Debug.Log("Kick!");
	}

	void Shot() {
		Debug.Log("Shot!");
	}

	void UseAttack(AttackInfo attack) {
		if (Time.time - attack.last < attack.cooldown)
			return;

		attack.attack();
		attack.last = Time.time;
	}

	// Update is called once per frame
	void Update () {
		foreach (AttackInfo attack in attacks) {
			if (Input.GetKey(attack.key)) {
				UseAttack(attack);
			}
		}
	}

}
