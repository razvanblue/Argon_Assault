using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine.Playables;

public class TurretAgent : Agent
{

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float velocity = 10f;

    [SerializeField] Transform Target;
    [SerializeField] Transform parent;

    private void Shoot()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectileInstance.GetComponent<Rigidbody>().velocity = transform.forward * velocity;
        projectileInstance.transform.parent = parent;

        EnemyProjectile enemyProjectile = projectileInstance.GetComponent<EnemyProjectile>();
        enemyProjectile.target = Target;
        enemyProjectile.shooter = this;
    }

    public override void Initialize()
    {
        parent = GameObject.Find("Spawned At Runtime").transform;
        MaxStep = 1000;
    }

    public override void OnEpisodeBegin()
    {
        Target.GetComponent<RandomAreaMovement>().Reset();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.position);
        sensor.AddObservation(this.transform.position);

        // Target movement direction
        //sensor.AddObservation(Target.GetComponent<RandomAreaMovement>().direction);
        //sensor.AddObservation(Target.parent.forward);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        //Stop condition: Turret behind Player
        if (Vector3.Dot(Target.forward,transform.position - Target.position) < 0)
        {
            foreach (Transform turretProjectile in parent)
            {
                Destroy(turretProjectile.gameObject);
            }
            print("Cumulative rewards: " + GetCumulativeReward());
            EndEpisode();
        }

        // Actions, size = 3
        Vector3 directionVector;
        for (var i = 0; i < vectorAction.Length; i++)
        {
            vectorAction[i] = Mathf.Clamp(vectorAction[i], -1f, 1f);
        }
        directionVector.x = vectorAction[0];
        directionVector.y = vectorAction[1];
        directionVector.z = vectorAction[2];
        if (directionVector == Vector3.zero) directionVector = Vector3.forward;         //Fix for vector zero in Quaternion.LookRotation()

        transform.rotation = Quaternion.LookRotation(directionVector);

        Shoot();
        //Rewards are added by projectile collision
    }

    public override void Heuristic(float[] actionsOut)
    {
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = Random.Range(-1f, 1f);
            actionsOut[1] = Random.Range(-1f, 1f);
            actionsOut[2] = Random.Range(-1f, 1f);
        }
    }
}
