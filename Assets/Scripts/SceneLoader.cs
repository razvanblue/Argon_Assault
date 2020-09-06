using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	[SerializeField] float delay = 2f;

	// Use this for initialization
	void Start ()
	{
		Invoke("LoadFirstScene", delay);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	void LoadFirstScene()
	{
		SceneManager.LoadScene(1);
	}
}
