using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAreaMovement : MonoBehaviour
{
    [SerializeField] float radiusX = 10f;
    [SerializeField] float radiusY = 10f;
    [SerializeField] float radiusZ = 50f;
    public float speed = 2f;

    public Vector3 direction;
    private Vector3 originalPosition;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)                          //Change position
        {
            timer = Random.Range(0.2f, 2f);

            float newX = Random.Range(originalPosition.x - radiusX, originalPosition.x + radiusX);
            float newY = Random.Range(originalPosition.y - radiusY, originalPosition.y + radiusY);
            float newZ = Random.Range(originalPosition.z - radiusZ, originalPosition.z + radiusZ);
            Vector3 targetPos = new Vector3(newX, newY, newZ);

            direction = Vector3.Normalize(targetPos - transform.position);
        }

        transform.Translate(direction * speed * Time.deltaTime);        // Move in direction

    }

    public void Reset()
    {
        transform.position = originalPosition;
        timer = 0f;
        
    }
}
