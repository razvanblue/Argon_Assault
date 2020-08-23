using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int scorePerHit = 10;
    [SerializeField] int healthPoints = 3;
    [SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;

	Scoreboard scoreboard;

	// Use this for initialization
	void Start ()
    {
        AddBoxCollider();
        scoreboard = FindObjectOfType<Scoreboard>();
    }

    private void AddBoxCollider()
    {
        Collider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        scoreboard.ScoreHit(scorePerHit);
        healthPoints--;

        //TODO: consider hit FX
        if (healthPoints < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;

        Destroy(gameObject);
    }
}
