using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField]
    private Transform spawn;

    [SerializeField]
    private GameObject editorCube;

    private float spawnIntervalSeconds = 4;

    private float ferryTravelSpeed = 2f;

    private float randomizerVal = 0.25f;

    [SerializeField]
    private Transform target;

    private Vector3 targetDirection;

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
         targetDirection = target.position - spawn.position;
         Destroy(editorCube);
    }

    // Update is called once per frame
    void Update()
    {
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

      Vector3 lookDirection = new Vector3(targetDirection.x, 0, targetDirection.z);
      Ferry ferry = Instantiate(lm.GetRandomFerryPrefab(), spawn.position, Quaternion.LookRotation(lookDirection)).GetComponent<Ferry>();
      ferry.gameObject.transform.Rotate(0,270,0);
      ferry.gameObject.transform.SetParent(this.transform);
      // Speed with randomness
      float speedRandomness = Random.Range(-(randomizerVal-1.5f), randomizerVal+2.5f);
      ferry.SetTravelSpeed(ferryTravelSpeed+speedRandomness);

      // Direction of destroying trigger, with randomness
      Vector3 dir = (targetDirection).normalized;
      ferry.SetDestination(dir);
      ferries.Add(ferry.gameObject);
    }

    public void Reset(){
      spawnIntervalSeconds = dm.GetDefaultSpawnInterval();
      ferryTravelSpeed = dm.GetDefaultFerrySpeed();
      randomizerVal = dm.GetRandomizerVal();
      foreach (GameObject ferry in ferries) {
        Destroy(ferry);
      }
        ferries = new List<GameObject>();
    }

    public void OnDifficultyChange() {
        spawnIntervalSeconds = dm.GetSpawnInterval();
        ferryTravelSpeed = dm.GetFerrySpeed();
        randomizerVal = dm.GetRandomizerVal();
    }

}
