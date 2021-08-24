using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameManager gm;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private UI_PlayController playController;

    [SerializeField]
    private Vector3 spawnPosition;

    public enum PlayerState {Idle, MovingLeft, MovingRight, Harboured}

    private PlayerState state;

    private List<GameObject> loadedPassengers = new List<GameObject>();
    private int passengerCount = 0;

    private List<GameObject> spawnedPassengers = new List<GameObject>();

    [SerializeField]
    private List<Transform> seats = new List<Transform>();

    private int score = 0; // TODO should be held in game manager only,
    private int lastHarbourID; // remove by equal checking TODO
    private Harbour currentHarbour;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
      //Debug.Log(state);
    }

    private void HandleDelivery(Harbour harbour){
      // TODO show press button hint, dont do loading passenngers on enter
      // if allowed to unload, do that and load new passengers
      Debug.Log("Unloaded  Passengers");
      passengerCount = loadedPassengers.Count;
      score += passengerCount;
      gm.UpdateScore(score);
      // unload over Time TODO
      foreach (GameObject p in spawnedPassengers) {
        Destroy(p);
      }
      loadedPassengers = harbour.loadPassengers();
      SpawnPassengersInRandomSeats();
      playController.OnScoreChanged(score);
      playController.OnPassengerChanged(loadedPassengers.Count);
    }

    // changing player state to harboured and therefore blocking player input and movement into harbours dir
    // change back to Moving, when player has left harbour zone and potentially loaded Passengers
    // PlayerMovement Class depens on this state deciding moving direction in fixed update
    public void EnteredHarbour(Harbour harbour){
        state = PlayerState.Harboured;
        if (lastHarbourID != harbour.GetHarbourID()) {
          lastHarbourID = harbour.GetHarbourID();
          HandleDelivery(harbour);
        }
    }

    public void ExitedHarbour(){
      if (lastHarbourID == 1) {
        state = PlayerState.MovingRight;
      } else if (lastHarbourID == 2){
        state = PlayerState.MovingLeft;
      } else { return;}
    }

    private void SpawnPassengersInRandomSeats(){
        int i = 0;
        foreach (Transform s in seats) {
          if (i < loadedPassengers.Count) {
            Debug.Log(i);
            GameObject prefab = loadedPassengers[i];
            Vector3 pos = new Vector3(s.position.x, s.position.y+0.35f, s.position.z);
            GameObject passenger = Instantiate(prefab, pos, Quaternion.identity);
            passenger.transform.SetParent(this.transform);
            spawnedPassengers.Add(passenger);
            i++;
          } else {
            return;
          }
        }
    }

    // TODO Refracftor thissssss
    // determines whether current input should be blocked because harbour is in the same direction
    public bool ShouldBlockDirection(float input) {
      if (state == PlayerState.Harboured) {
        if (input < 0 && lastHarbourID == 1) { return true;}               // left input and last harbour is on the left
        else if (input > 0 && lastHarbourID == 2) { return true;}        // right input and last harbour is on the right
        else {return false;}
      }
      else if (state == PlayerState.MovingLeft && lastHarbourID == 1) {
        return true; }
        else if (state == PlayerState.MovingRight && lastHarbourID == 2) {
        return true;
      } else {
        return false;
      }
    }

    public void Reset(){
        state = PlayerState.Idle;
        score = 0;
        lastHarbourID = 0;
        loadedPassengers = new List<GameObject>();
        foreach (GameObject p in spawnedPassengers) {
          Destroy(p);
        }
        this.transform.position = spawnPosition;
    }

    public void Deactivate(){
      this.gameObject.SetActive(false);
      playerMovement.SetActiveState(false);
    }

    public void Activate(){
      this.gameObject.SetActive(true);
      playerMovement.SetActiveState(true);
    }

}
