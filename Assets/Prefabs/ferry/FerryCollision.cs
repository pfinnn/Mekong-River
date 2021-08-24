using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerryCollision : MonoBehaviour
{
  private void OnTriggerEnter(Collider other) {
    if(other.gameObject.CompareTag("Player")){
      this.gameObject.GetComponent<Ferry>().SetActiveState(false);
    } else if (other.gameObject.CompareTag("Obstacle")) {
      Debug.Log("Despawned?");
      Destroy(gameObject);
    }
  }
}
