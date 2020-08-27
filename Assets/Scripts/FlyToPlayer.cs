using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyToPlayer : MonoBehaviour {

	private Transform target;
	private float speed = 20f;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
