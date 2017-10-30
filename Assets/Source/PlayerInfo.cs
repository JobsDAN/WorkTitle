using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    public int heals;

    void Start()
    {
        WorldObserver.Instance.Players.Add(this);
    }

    void Update()
    {
		
	}

    public void TakeDamage(int damage)
    {
        heals -= damage;
        GetComponent<Renderer>().material.color = Color.red;

        if (heals <= 0)
            Die();
    }

    public void Die()
    {
        WorldObserver.Instance.Players.Remove(this);
        Destroy(gameObject);
    }
}
