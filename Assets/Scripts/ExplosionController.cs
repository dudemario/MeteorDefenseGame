using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {
	private int startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.frameCount;
	}

	// Update is called once per frame
	void Update () {
		if (this != null) {
			transform.localScale += new Vector3(0.04f, 0, 0.04f);
			Debug.Log (transform.localScale);
			if (Time.frameCount == startTime + 60) {
				Destroy (this.gameObject);
			}
		}
	}
}
