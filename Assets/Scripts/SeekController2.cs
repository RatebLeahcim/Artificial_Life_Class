using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class SeekController2 : MonoBehaviour
{
    Vehicle vehicle;
    public Transform target;
    public float maxSeekForce = 1;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }
    private void Update()
    {
        Vector3 force = vehicle.CalculateSeek(target);
        force = Vector3.ClampMagnitude(force, maxSeekForce);

        vehicle.Accelerate(force, Time.deltaTime);
    }
}
