using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harbour : MonoBehaviour
{
    private float timeRemaining;
    private int spawnIntervalSeconds = 2;
    private int maxPassengers = 30;

    private List<GameObject> passengers = new List<GameObject>();

    private List<Vector3> usedSpawnPoints = new List<Vector3>();

    [SerializeField]
    private int ID; // TODO  remove, use equals

    [SerializeField]
    GameManager gm;

    [SerializeField]
    HarbourManager hm;

    [SerializeField]
    GameObject spawnCenter;

    // Update is called once per frame
    void Update()
    {
      if (gm.Playing()) {
        if (passengers.Count < maxPassengers){
          if (timeRemaining <= 0) {
            SpawnPassenger();
            timeRemaining = spawnIntervalSeconds;
          } else {
            timeRemaining -= Time.deltaTime;
          }
        }
      }
    }

    public List<GameObject> loadPassengers(){
        List<GameObject> result = passengers;
        Reset();
        return result;
    }

    public int GetHarbourID(){
      return ID;
    }

    public void Reset(){
      foreach (GameObject p in passengers) {
          Destroy(p);
      }
      passengers = new List<GameObject>();
    }

   private void SpawnPassenger(){
      GameObject p = Instantiate(hm.GetRandomPassengerPrefab(), FindRandomSpawnPoint(), Quaternion.identity);
      p.transform.SetParent(this.transform);
      passengers.Add(p);
    }

    private Vector3 FindRandomSpawnPoint(){
        float rX = Random.Range(-.3f, .3f) +spawnCenter.transform.position.x;
        float rZ = Random.Range(-.4f, .4f) +spawnCenter.transform.position.z;
        float rY = spawnCenter.transform.position.y;
        Vector3 found = new Vector3(rX,rY,rZ);
        //usedSpawnPoints.Add(found); // TODO regenerate when point + margin in use
        return found;
    }
}
