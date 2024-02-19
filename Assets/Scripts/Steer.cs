using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : MonoBehaviour
{
    Vector3 velocity;
    Vector3 steering;


    public float maxSpeed;
    public float seekForce = 1;
    public float seekMultiplier = 1;

    GameObject target;

    public float checkpointDistance = 5;
    public List<GameObject> checkpoints;
    int currentCheckpoint = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        target = checkpoints[currentCheckpoint];

        if (Vector3.Distance(this.transform.position, target.transform.position) < checkpointDistance)
        {
            currentCheckpoint += 1;
            if (currentCheckpoint >= checkpoints.Count)
            {
                currentCheckpoint = 0;
            }
        }

        steering = CalculateSeek(target.transform.position);
        steering = Vector3.ClampMagnitude(steering, seekForce);
        steering *= seekMultiplier;

        velocity += steering * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed); // Don't exceed the maximum speed.

        this.transform.position += velocity * Time.deltaTime;
    }
    public Vector3 CalculateSeek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - this.transform.position;
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity *= maxSpeed;

        // Visyualize forces
        Debug.DrawRay(this.transform.position, velocity, Color.yellow);
        Debug.DrawRay(this.transform.position, desiredVelocity, Color.red);
        Debug.DrawRay(this.transform.position, steering, Color.blue);

        return desiredVelocity - this.velocity;
    }

    public Vector3 CalculateFlee(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = this.transform.position - targetPosition;
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity *= maxSpeed;

        Debug.DrawRay(this.transform.position, velocity, Color.yellow);
        Debug.DrawRay(this.transform.position, desiredVelocity, Color.red);
        Debug.DrawRay(this.transform.position, steering, Color.blue);

        return desiredVelocity - this.velocity;
    }
}
