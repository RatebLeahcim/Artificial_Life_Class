using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorVisualizer : MonoBehaviour
{
    [SerializeField]
    Vector3 pos = new Vector3(0, 0, 0);
    [SerializeField]
    Vector3 scale = new Vector3 (1, 1, 1);

    [SerializeField]
    Transform target;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = pos;
        //transform.localScale = scale;

        //Calculate direction to target
        Vector3 toTarget = target.position - transform.position;

        //Normalize vector
        toTarget.Normalize();

        //Move towards target
        transform.Translate(toTarget * Time.deltaTime * speed);
    }
}
