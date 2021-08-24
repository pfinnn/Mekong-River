using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    Rigidbody rb;
    private Vector3 destination;
    private float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + destination * Time.deltaTime * m_Speed);
    }

    public void SetTravelSpeed(float newSpeed)
    {
        m_Speed = newSpeed;
    }

    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
    }
}

