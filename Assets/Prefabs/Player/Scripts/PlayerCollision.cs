using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private GameManager gm;
    private Player player;
    // Start is called before the first frame update
    void Start(){
      gm = GameObject.Find("GameManager").GetComponent<GameManager>();
      player = this.GetComponent<Player>();
    }


    private void OnTriggerEnter(Collider other) {
      if(other.gameObject.CompareTag("Ferry")){
        Debug.Log("Obstacle hit");
        gm.TriggerGameOver();
      } else if (other.gameObject.CompareTag("Harbour")) {
        Debug.Log("Entered Harbour");
          player.EnteredHarbour(other.gameObject.GetComponent<Harbour>());
      }

    }

    private void OnTriggerExit(Collider other) {
      if (other.gameObject.CompareTag("Harbour")) {
        Debug.Log("Exited Harbour");
          player.ExitedHarbour();
      }
    }
}
