using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoldExplosion : MonoBehaviour {
	private int startTime;
	public GameObject Explosion;
	// Use this for initialization
	void Start () {
		startTime = Time.frameCount;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount == startTime + 60) {
			Instantiate (Explosion,this.transform.position,Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
