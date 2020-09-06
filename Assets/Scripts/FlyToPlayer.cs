using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyToPlayer : MonoBehaviour
{

	private Transform target;
	private float speed = 20f;

	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () {
		transform.LookAt(target);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
