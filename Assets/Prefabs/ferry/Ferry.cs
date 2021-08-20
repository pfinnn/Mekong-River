using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferry : MonoBehaviour
{
  private bool active = true;

  private float m_Speed = 1f;
  Rigidbody rb;

  private GameManager gm;
  private Vector3 destination;

  // Start is called before the first frame update
  void Start()
  {
      gm = GameObject.Find("GameManager").GetComponent<GameManager>();
      rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (gm.Playing()){
      //Store user input as a movement vector
      //Apply the movement vector to the current position, which is
      //multiplied by deltaTime and speed for a smooth MovePosition

      rb.MovePosition(transform.position + destination * Time.deltaTime * m_Speed);
    }

  }

  public void SetActiveState(bool newState){
    active = newState;
  }

  public void SetTravelSpeed(float newSpeed){
    m_Speed = newSpeed;
  }

  public void SetDestination(Vector3 newDestination){
    destination = newDestination;
  }
}
