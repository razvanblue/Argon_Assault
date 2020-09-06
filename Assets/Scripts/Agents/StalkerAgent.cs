using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class StalkerAgent : Agent
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 15f;
    [SerializeField] float searchRadius = 15f;

    Rigidbody rBody;

    public override void Initialize()
    {
        MaxStep = 0;
    }

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        if (Mathf.Abs(transform.localPosition.x) > searchRadius ||
            Mathf.Abs(transform.localPosition.y) > searchRadius)
        {
            // If the Agent is too far away, zero its momentum
            rBody.angularVelocity = Vector3.zero;
            rBody.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0, 0, 4);
        }

        // Move the target to a new spot
        target.localPosition = new Vector3(Random.Range(-searchRadius + 1f, searchRadius - 1f),
                                           Random.Range(-searchRadius + 1f, searchRadius - 1f),
                                           0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // Actions, size = 2
        rBody.velocity = (transform.right * Mathf.Clamp(vectorAction[0], -1f, 1f) + transform.up * Mathf.Clamp(vectorAction[1], -1f, 1f)) * speed;
        //AddReward(-0.001f);

        // Reached target
        int layerMask = 1 << 8;                 //Player Mask
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            AddReward(1.0f);
            EndEpisode();
        }

        //Went too far
        if (Mathf.Abs(transform.localPosition.x) > searchRadius ||
           Mathf.Abs(transform.localPosition.y) > searchRadius)
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
    }
}