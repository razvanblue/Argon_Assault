using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerWithinRange : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float range = 15f;

    Vector3 parentPosition;

    void Start()
    {
        parentPosition = transform.parent.position;
    }

    void Update()
    {
        transform.position = parentPosition + Vector3.Normalize(target.position - parentPosition) * range;
    }
}
