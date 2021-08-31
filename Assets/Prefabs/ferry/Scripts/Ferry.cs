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

  private WaterTrailEffect waterTrailEffect;

  [SerializeField]
  private AudioSource audioSource;

  // Start is called before the first frame update
  void Start()
  {
      gm = GameObject.Find("GameManager").GetComponent<GameManager>();
      waterTrailEffect = this.GetComponent<WaterTrailEffect>(); // TODO Require Component
      rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (gm.Playing()){
      rb.MovePosition(transform.position + destination * Time.deltaTime * m_Speed);
    }

    if (!rb.velocity.Equals(Vector3.zero) && !waterTrailEffect.IsActive())
    {
        waterTrailEffect.ToggleActive();
    }
    else if (rb.velocity.Equals(Vector3.zero) && waterTrailEffect.IsActive())
    {
        waterTrailEffect.ToggleActive();
    }
    // Sound based on velocity
    if (rb.velocity.Equals(Vector3.zero))
    {
        audioSource.volume = 0;
    } 
    else
    {
        if(audioSource.volume == 0)
        {
            audioSource.volume = 0.015f;
        }
    }

    // turn off sound when not playing and the other way around
    if (!active && audioSource.isPlaying) 
    {
        audioSource.Stop();
    } else if (active && !audioSource.isPlaying)
    {
        audioSource.Play();
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
