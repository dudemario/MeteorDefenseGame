using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {
	//private Rigidbody rb;
	//private AudioSource ac;
	//public float speed;
	//public Boundary boundary;
	//public float tilt;
	public GameObject shot;
	//private Vector3 shotSpawn = new Vector3(0, 0, -5);
	private float fireRate = 0.5f;
	private float nextFire;
	private float angle;
	private float speed = 1;
	private int spawner = 0;
	private GameObject bolt;
	public GameObject[] spawners;
	private float[] spawnersTimes = new float[3];

	void Start() {
		for (int i = 0; i < 3; i++) {
			spawnersTimes[i] = 0;
		}
	}

	void Update() {
		if (Input.GetButtonDown("Speed Up")) {
			fireRate -= 0.1f;
			speed += 0.5f;
		}
		if (Input.GetButton ("Fire1") && (Time.time > spawnersTimes[spawner])) {
			//Debug.Log (Screen.width);
			//Debug.Log (Screen.height);
			//Debug.Log (Input.mousePosition);
			spawnersTimes[spawner] = Time.time + fireRate;
			fireRate += 0.001f;
			if (spawner == 0) {
				angle = -1*Mathf.Rad2Deg*(Mathf.Atan2 (Input.mousePosition.y, Input.mousePosition.x-(Screen.width/4)))-90;
			} else {
				angle = -1*Mathf.Rad2Deg*Mathf.Atan2 (Input.mousePosition.y, Input.mousePosition.x-(Screen.width*3/(8-2*spawner)))-90;
			}
			//Debug.Log (angle);
			if (spawners [spawner] != null) {
				bolt = Instantiate (shot, spawners[spawner].transform.position + new Vector3(0, 0, 0.5f), Quaternion.Euler(0,angle,0)); //new Vector3(spawner*4-4, 0, -5)
				Rigidbody rb = bolt.GetComponent<Rigidbody>();
				if (spawner == 0) {
					rb.velocity = new Vector3 (12 * (Input.mousePosition.x - (Screen.width / 4)) / Screen.width, 0, 10 * (Input.mousePosition.y) / Screen.height) * speed;
				} else {
					rb.velocity = new Vector3 (12 * (Input.mousePosition.x - (Screen.width * 3 / (8 - 2 * spawner))) / Screen.width, 0, 10 * (Input.mousePosition.y) / Screen.height) * speed;
				}
			}
			//ac.Play ();
		}
		if (Input.GetButtonDown ("RSpawn")) {
			if (spawner == 0) {
				if (spawners [1] != null) {
					spawner += 1;
				} else {
					if (spawners [2] != null) {
						spawner += 2;
					}
				}
			} else {
				if (spawner == 1) {
					if (spawners [2] != null) {
						spawner += 1;
					}
				}
			}
		}
		if (Input.GetButtonDown ("LSpawn")) {
			if (spawner == 2) {
				if (spawners [1] != null) {
					spawner -= 1;
				} else {
					if (spawners [0] != null) {
						spawner -= 2;
					}
				}
			} else {
				if (spawner == 1) {
					if (spawners [0] != null) {
						spawner -= 1;
					}
				}
			}
		}
	}
}
