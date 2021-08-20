using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    [SerializeField]
    private DifficultyManager dM;

    [SerializeField]
    private List<Lane> activeLanes = new List<Lane>();

    [SerializeField]
    private List<GameObject> ferryPrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetLanes(){
      foreach (Lane lane in activeLanes) {
        lane.Reset();
      }
    }

    public void OnDifficultyChange() {
      foreach (Lane lane in activeLanes) {
        lane.OnDifficultyChange();
      }
    }

    public GameObject GetRandomFerryPrefab(){
      return ferryPrefabs[Random.Range(0, ferryPrefabs.Count-1)];
    }



}
