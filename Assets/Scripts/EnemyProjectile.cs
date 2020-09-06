using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public TurretAgent shooter;
    public Transform target;

    private float distanceFromTarget = 50f;            // Initialize with max value.
    private float penaltyThreshold = 20f;              // Should be smaller than distanceFromTarget.
    private float rewardMultiplier = 0.0001f;
    private float pozitiveRewardMult = 500f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Player collision");
            shooter.AddReward(1f);
            shooter.EndEpisode();
        }
    }

    private void Update()
    {
        distanceFromTarget = Mathf.Min(distanceFromTarget, Vector3.Distance(target.position, transform.position));
    }

    private void OnDestroy()
    {
        float reward = (penaltyThreshold - distanceFromTarget);
        if (reward > 0)
            reward *= pozitiveRewardMult;

        print("Reward" + reward);
        shooter.AddReward(reward * rewardMultiplier);
    }
}
