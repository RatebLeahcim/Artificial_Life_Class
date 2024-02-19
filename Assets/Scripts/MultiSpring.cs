using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class MultiSpring : MonoBehaviour
{
    
    [SerializeField]
    List<Transform> bobs; // A list of static anchor points (transforms)
    [SerializeField]
    float stiffness = 1.0f, restLength = 5.0f, dampingCoefficient = 1.0f, mass = 1.0f; // k Constant of the springs and rest length of springs
    [SerializeField]
    bool applyGravity;

    Vector3 acceleration = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    Vector3 gravityAcceleration = new Vector3(0, -9.81f, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        // zero out acceleration
        acceleration = Vector3.zero;

        // loop through anchor points, adding up all the vectors from spring forces
        foreach (Transform bob in bobs)
        {
            acceleration += CalculateSpringForce(bob, stiffness, restLength);
        }

        if (applyGravity)
        {
            acceleration += gravityAcceleration;
        }

        //Add the damping force.
        acceleration += -(velocity * dampingCoefficient);

        acceleration /= mass;

        // Last two lines add our acceleration to our velocity, and our velocity to our position
        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }

    private Vector3 CalculateSpringForce(Transform bob, float stiffness, float restLength)
    {
        // Check to make sure bob exists
        if (bob != null)
        {
            // Get the direction and distance to anchor point
            Vector3 toBob = bob.position - transform.position;
            float currentLength = toBob.magnitude;
            // Normalize spring force
            toBob /= currentLength;
            // How far have we moved from the rest length? Spring force is directly proportional to the displacement
            float displacement = currentLength - restLength;
            Vector3 springForce = toBob * displacement * stiffness;

            return springForce;
        }

        else return Vector3.zero; // In case bob gets destroyed
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0]; 
        Vector3 normal = contactPoint.normal;
        velocity = Vector3.Reflect(velocity, normal);
    }
}
