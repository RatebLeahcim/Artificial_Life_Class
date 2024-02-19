using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 0, 0);
    [SerializeField]
    Vector3 forwardThrust = new Vector3(0, 0, 1);
    Vector3 gravity = new Vector3(0, -9.81f, 0);

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 acceleration = Vector3.zero;
        acceleration += forwardThrust;
        acceleration += gravity;

        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
