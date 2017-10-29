using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialAttack : MonoBehaviour {

	public float Radius = 2;
	public int Power = 4;

	float growSpeed = 2f;
	List<GameObject> hitted;

	void Start()
	{
		hitted = new List<GameObject>();
	}

	void Update()
	{
		Vector3 scale = transform.localScale;
		scale.x += growSpeed * Time.deltaTime;
		scale.z += growSpeed * Time.deltaTime;
		transform.localScale = scale;
		if (scale.x > Radius)
		{
			StopGrowth();
			return;
		}

		Collider[] colliders = Physics.OverlapSphere(transform.position, scale.x / 2);
		foreach (Collider c in colliders)
		{
			GameObject go = c.gameObject;
			if (!go)
				continue;

			if (hitted.Contains(go))
				continue;

			EnemyBehavior eb = go.GetComponent<EnemyBehavior>();
			if (!eb)
				continue;

			hitted.Add(go);
			eb.TakeDamage(Power);
			Debug.Log("!");
		}
	}

	public void StopGrowth()
	{
		Destroy(gameObject);
		Destroy(this);
	}
}
