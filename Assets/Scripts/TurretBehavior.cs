using UnityEngine;
using System.Collections;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float forwardProjection = 0f;
    [SerializeField] float yawLimit = 90f;

    private float initialYaw;

    private void Start()
    {
        initialYaw = transform.localEulerAngles.y;
    }

    void Update()
    {
        Vector3 projectedTargetPosition = target.position + (target.forward * forwardProjection);
        transform.LookAt(projectedTargetPosition);

        // Limit local rotation on Y axis
        Vector3 angle = transform.localEulerAngles;
        angle.y = Mathf.Clamp(angle.y, initialYaw - yawLimit, initialYaw + yawLimit);
        transform.localEulerAngles = angle;
    }

    public void Target(Transform target)
    {
        this.target = target;
    }
}