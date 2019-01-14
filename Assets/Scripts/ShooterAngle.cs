using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAngle : MonoBehaviour {
	public int spawner;
	private float angle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (spawner == 0) {
			angle = -1 * Mathf.Rad2Deg * (Mathf.Atan2 (Input.mousePosition.y, Input.mousePosition.x - (Screen.width / 4))) + 90;
		} else {
			angle = -1 * Mathf.Rad2Deg * (Mathf.Atan2 (Input.mousePosition.y, Input.mousePosition.x - (Screen.width * 3 / (8 - 2 * spawner)))) + 90;
		}
		transform.rotation = Quaternion.Euler (-45, 0, angle);
		
	}
}
