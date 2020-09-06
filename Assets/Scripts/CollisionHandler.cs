using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

	[Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;
	[Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;

    private void OnCollisionEnter(Collision collision)
    {
		StartDeathSequence();
	}
	
    private void OnParticleCollision(GameObject particle)
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
}
