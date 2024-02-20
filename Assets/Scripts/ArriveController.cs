using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveController : MonoBehaviour
{
    public Transform target;
    Vehicle vehicle;
    // Start is called before the first frame update
    void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 arrivalForce = vehicle.CalculateArrive(target, 10);
        Vector3 separationForce = vehicle.CalculateSeparation(target, 20);
        //arrivalForce = Vector3.ClampMagnitude(arrivalForce, 1);
        vehicle.Accelerate(separationForce, Time.deltaTime);
    }
}
