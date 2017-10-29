using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    
    void Start()
    {
        WorldObserver.Instance.Players.Add(this);
    }

    void Update()
    {
		
	}
}
