using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] int scorePerSecond = 1;

	int score;
	Text scoreText;

	float time = 0f;

	void Start ()
	{
		scoreText = GetComponent<Text>();
		scoreText.text = score.ToString();
	}

	void Update ()
    {
		time += Time.deltaTime;
		if (time > 1f)
        {
			time = 0f;
			ScoreHit(scorePerSecond);
		}
    }
	public void ScoreHit(int bonus)
    {
		score += bonus;
		scoreText.text = score.ToString();
    }
}
