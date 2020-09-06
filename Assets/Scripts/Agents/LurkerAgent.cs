using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class LurkerAgent : Agent
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 10f;
    [SerializeField] float searchRadius = 10f;

    float rewardMultiplier;
    Rigidbody rBody;

    public override void Initialize()
    {
        rewardMultiplier = 0.4f / (2 * searchRadius * (MaxStep + 1));         // The divident is the maximum reward over MaxStep steps
    }

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        /*
        if (Mathf.Abs(transform.localPosition.x) > searchRadius ||
           Mathf.Abs(transform.localPosition.y) > searchRadius ||
           Mathf.Abs(transform.localPosition.z) > searchRadius)
        {
            // If the Agent is too far away, zero its momentum and reset its position
            rBody.angularVelocity = Vector3.zero;
            rBody.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0, 0, 0);
        }
        // Move the target to a new spot
        target.localPosition = new Vector3(Random.Range(-searchRadius + 1f, searchRadius - 1f),
                                           Random.Range(-searchRadius + 1f, searchRadius - 1f),
                                           Random.Range(-searchRadius + 1f, searchRadius - 1f));
        */
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // Actions, size = 3
        rBody.velocity = new Vector3(Mathf.Clamp(vectorAction[0], -1f, 1f) * speed,
                                    Mathf.Clamp(vectorAction[1], -1f, 1f) * speed,
                                    Mathf.Clamp(vectorAction[2], -1f, 1f) * speed);

        // Rewards
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        AddReward((2f * searchRadius) / Mathf.Max(distanceToTarget, 1f) * rewardMultiplier);

        if (distanceToTarget < 1f)
        {
            AddReward(0.6f);                                                                // Target reached reward
            AddReward(2f * searchRadius * rewardMultiplier * (MaxStep - StepCount));        // Reward for all remaining steps
            EndEpisode();
        }

        //Went too far
        if (Mathf.Abs(transform.localPosition.x) > searchRadius ||
           Mathf.Abs(transform.localPosition.y) > searchRadius  ||
           Mathf.Abs(transform.localPosition.z) > searchRadius)
        {
            EndEpisode();
        }
    }
    public override void Heuristic(float[] actionsOut)
    {
        float xVal = Input.GetAxis("Horizontal");
        float YVal = Input.GetAxis("Vertical");

        actionsOut[0] = xVal;
        actionsOut[1] = YVal;
        actionsOut[2] = Random.Range(-1f, 1f);
    }
}