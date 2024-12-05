using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : DynamicObject {

    public bool isPickedUp = false;
    public bool isMarkedForPickup = false;
    // Use this for initialization
    public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
