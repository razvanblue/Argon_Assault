using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class Stalker : Agent
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 10f;

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
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(target.position);
        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // Actions, size = 2
        rBody.velocity = (transform.right * Mathf.Clamp(vectorAction[0], -1f, 1f) + transform.up * Mathf.Clamp(vectorAction[1], -1f, 1f)) * speed;
        AddReward(-0.001f);

        // Reached target
        int layerMask = 1 << 8;                 //Player Mask
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            AddReward(1.0f);
            EndEpisode();
        }

        //Went too far
        if (Mathf.Abs(transform.localPosition.x) > 4 ||
           Mathf.Abs(transform.localPosition.y) > 4)
        {
            EndEpisode();
        }
    }
    public override void Heuristic(float[] actionsOut)
    {
    }
}