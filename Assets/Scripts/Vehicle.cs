using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    Vector3 velocity;
    
    [SerializeField]
    float maxSpeed = 5.0f, mass = 1.0f;

    public Vector3 CalculateSeek(Transform target)
    {
        Vector3 totarget = target.position - this.transform.position;
        Vector3 desiredVelocity = totarget.normalized * maxSpeed;

        Vector3 steering = desiredVelocity - velocity;

        VisualizeForce(velocity, Color.green);
        VisualizeForce(desiredVelocity, Color.red);
        VisualizeForce(steering, Color.blue);

        return steering;
    }

    public Vector3 CalculateFlee(Transform target)
    {
        Vector3 fromtarget = this.transform.position - target.position;
        Vector3 desiredVelocity = fromtarget.normalized * maxSpeed;

        Vector3 steering = desiredVelocity - velocity;

        VisualizeForce(velocity, Color.green);
        VisualizeForce(desiredVelocity, Color.red);
        VisualizeForce(steering, Color.blue);

        return steering;
    }

    public Vector3 CalculateArrive(Transform target, float slowingRadius)
    {
        Vector3 toTarget = target.position - this.transform.position;
        float distance = toTarget.magnitude;
        Vector3 desiredVelocity;

        if (distance > slowingRadius)
        {
            desiredVelocity = toTarget/distance * maxSpeed;
        }
        else
        {
            float speedMultiplier = distance/slowingRadius;
            desiredVelocity = toTarget/distance * maxSpeed * speedMultiplier;
        }

        return desiredVelocity - velocity;
    }

    public Vector3 CalculateSeparation(Transform target, float separationRadius) 
    {
        Vector3 fromtarget = this.transform.position - target.position;
        float distance = fromtarget.magnitude;
        Vector3 desiredVelocity;
        
        if (distance < separationRadius)
        {
            return new Vector3(0, 0, 0);
        }
        else
        {
            float speedMultiplier = (separationRadius - distance) / separationRadius;
            desiredVelocity = fromtarget / distance * maxSpeed * speedMultiplier;
            return desiredVelocity - velocity;
        }
    }

    public void Accelerate(Vector3 force, float deltaTime)
    {
        Vector3 acceleration = force / mass;
        velocity += acceleration; 

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * deltaTime;

        transform.forward = velocity;
    }

    void VisualizeForce(Vector3 force, Color color)
    {
        Debug.DrawRay(transform.position, force, color);
    }

}
