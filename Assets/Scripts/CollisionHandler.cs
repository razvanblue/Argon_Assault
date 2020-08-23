using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

	[Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;
	[Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;

	void OnTriggerEnter(Collider other)
	{
		StartDeathSequence();
	}

    private void StartDeathSequence()
    {
		deathFX.SetActive(true);

		SendMessage("OnPlayerDeath");

		Invoke("ReloadScene", levelLoadDelay);
    }

	private void ReloadScene()	// String reference
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
