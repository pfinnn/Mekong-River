using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool active = true;
    private float m_Speed = 5f;
    Rigidbody rb;

    [SerializeField]
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if (active){
          //Store user input as a movement vector

          float input = Input.GetAxis("Horizontal");


          // block movement if input is in the same direction as current harbour
          if (player.ShouldBlockDirection(input)) {
              return;
          }

          Vector3 m_Input = new Vector3(input, 0, 0);

          // TODO Movement Speed divided by weight of current load,
          // altenatively chanve weight of rigidbody for more realistic behaviour

          //Apply the movement vector to the current position, which is
          //multiplied by deltaTime and speed for a smooth MovePosition

          rb.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);
      }

    }
    // used by player class to activate or deactivate movement, player class depends on game manager.
    public void SetActiveState(bool newState){
      active = newState;
    }
}
