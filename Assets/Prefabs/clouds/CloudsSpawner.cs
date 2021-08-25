using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private List<GameObject> cloudPrefabs = new List<GameObject>();

    private List<GameObject> clouds = new List<GameObject>();

    [SerializeField]
    private Transform spawn;

    [SerializeField]
    private float spawnIntervalSeconds = 3;

    private float randomizerVal = 0.25f;

    [SerializeField]
    private Transform target;

    private Vector3 targetDirection;

    // timer
    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = spawnIntervalSeconds;
        targetDirection = target.position - spawn.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining <= 0)
        {
            SpawnCloud();
            float randomSpawn = Random.Range(-1f, 0.5f);
            timeRemaining = spawnIntervalSeconds + randomSpawn;
            if (timeRemaining <= 1) { timeRemaining = 2; }
        }
        else
        {
            timeRemaining -= Time.deltaTime;
        }
    }


    private void SpawnCloud()
    {
        Vector3 lookDirection = new Vector3(targetDirection.x, 0, targetDirection.z);
        Vector3 randomSpawnPos = new Vector3(spawn.position.x + Random.Range(-1, 1), spawn.position.y, spawn.position.z + Random.Range(-5, 5));
        Cloud cloud = Instantiate(GetRandomCloudPrefab(), randomSpawnPos, Quaternion.LookRotation(lookDirection)).GetComponent<Cloud>();
        cloud.gameObject.transform.SetParent(this.transform);
        // Speed with randomness
        float speedRandomness = Random.Range(-(randomizerVal), randomizerVal);
        cloud.SetTravelSpeed(speed + speedRandomness);

        // Direction of destroying trigger, with randomness
        Vector3 dir = (targetDirection).normalized;
        cloud.SetDestination(dir);

        Debug.Log("spawned cloud");
    }

    public GameObject GetRandomCloudPrefab()
    {
        return cloudPrefabs[Random.Range(0, cloudPrefabs.Count - 1)];
    }
}
