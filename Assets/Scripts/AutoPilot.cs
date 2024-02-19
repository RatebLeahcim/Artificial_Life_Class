using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoPilot : MonoBehaviour
{
    public float topSpeed = 4;
    
    Vector3 moveVector = new Vector3(0, 0, 1);

    public GameObject sword;

    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the distance and direction to the sword
        //Ignoring the y axis.
        moveVector = new Vector3(sword.transform.position.x, this.transform.position.y, sword.transform.position.z) - this.transform.position;
        //moveVector = sword.transform.position - this.transform.position; // Calulates full 3D vector

        moveVector = moveVector.normalized;

        //Rotate toward target
        this.transform.forward = moveVector;

        //Move knight toward target
        this.transform.Translate(moveVector * topSpeed * Time.deltaTime, Space.World);

        // Communicate speed to animater
        anim.SetFloat("Speed", topSpeed);
    }
}
