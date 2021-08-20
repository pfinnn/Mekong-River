using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private Collider despawn;

    private float spawnIntervalSeconds = 4;

    private float ferryTravelSpeed = 2f;

    private float randomizerVal = 0.25f;

    // timer
    private float timeRemaining;
    private GameManager gm;
    private DifficultyManager dm;

    [SerializeField]
    private LaneManager lm;

    private List<GameObject> ferries = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // TODO dont search on init, create and reference dynamically in manager class
         GameObject go =  GameObject.Find("GameManager");
         gm = go.GetComponent<GameManager>();
         dm = go.GetComponent<DifficultyManager>();
         spawnIntervalSeconds = dm.GetDefaultSpawnInterval();
         ferryTravelSpeed = dm.GetDefaultFerrySpeed();
         randomizerVal = dm.GetRandomizerVal();
         timeRemaining = spawnIntervalSeconds;
    }

    // Update is called once per frame
    void Update()
    {
      //Debug.Log("Randomness: "+randomizerVal);
      if (gm.Playing()){
        if (timeRemaining <= 0) {
          SpawnFerry();
          float randomSpawn = Random.Range(-1f, 1.5f);
          timeRemaining =  spawnIntervalSeconds+randomSpawn;
          if (timeRemaining <= 1){timeRemaining = 2;}
        } else {
          timeRemaining -= Time.deltaTime;
        }
      }

    }

    private void SpawnFerry(){

      //int randFerryIndex = Mathf.FloorToInt(Random.Range( 0 ,1.99f)); // TODO dangerous, possibly out of range
      Debug.Log(lm.GetRandomFerryPrefab());
      Vector3 dir = (despawn.transform.position-spawn.position).normalized;

      Ferry ferry = Instantiate(lm.GetRandomFerryPrefab(), spawn.position, Quaternion.LookRotation(despawn.transform.position-spawn.position)).GetComponent<Ferry>();
      ferry.gameObject.transform.Rotate(0,90,0);
      ferry.gameObject.transform.SetParent(this.transform);
      // Speed with randomness
      float speedRandomness = Random.Range(-randomizerVal+1.5f, randomizerVal+1.5f);
      ferry.SetTravelSpeed(ferryTravelSpeed+speedRandomness);

      // Direction of destroying trigger, with randomness
      float destinationRandomness = Random.Range(-0.01f, 0.01f);
      Vector3 dirRa = new Vector3(dir.x+destinationRandomness,dir.y, dir.z);
      ferry.SetDestination(dirRa);
      Debug.Log("Destination : "+destinationRandomness);
      ferries.Add(ferry.gameObject);
      Debug.Log("spawned");
    }

    public void Reset(){
      spawnIntervalSeconds = dm.GetDefaultSpawnInterval();
      ferryTravelSpeed = dm.GetDefaultFerrySpeed();
      randomizerVal = dm.GetRandomizerVal();
      foreach (GameObject ferry in ferries) {
        //ferries.Remove(ferry);
        Destroy(ferry);
      }
    }

    public void OnDifficultyChange() {
        spawnIntervalSeconds = dm.GetSpawnInterval();
        ferryTravelSpeed = dm.GetFerrySpeed();
        randomizerVal = dm.GetRandomizerVal();
    }

}
