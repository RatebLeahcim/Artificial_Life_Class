using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    Vector3 velocity;
    Vector3 acceleration;
    Vector3 gravity = new Vector3(0, -9.81f, 0);

    public Transform bob;
    float restLength;

    [SerializeField]
    float stiffness = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 toTarget = transform.position - bob.position;
        restLength = toTarget.magnitude;
    }

    void FixedUpdate()
    {
        acceleration = Vector3.zero;

        Vector3 toBob = bob.position - transform.position;
        float distance = toBob.magnitude;
        toBob /= distance;

        float displacement = distance - restLength;

        Vector3 springForce = toBob * displacement * stiffness;

        acceleration += springForce;
        acceleration += gravity;
        
        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
