using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        WorldObserver.Instance.Players.Add(this);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
