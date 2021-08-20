using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarbourManager : MonoBehaviour
{
    [SerializeField]
    private List<Harbour> harbours = new List<Harbour>();

    [SerializeField]
    private List<GameObject> passengerPrefabs = new List<GameObject>();

    public void Reset(){
      foreach (Harbour harbour in harbours) {
        harbour.Reset();
      }
    }

    public GameObject GetRandomPassengerPrefab(){
      return passengerPrefabs[Random.Range(0, passengerPrefabs.Count-1)];
    }
}
