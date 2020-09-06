using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour {

	[SerializeField] float lifetime = 5f;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
