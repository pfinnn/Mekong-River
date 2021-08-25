using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrailEffect : MonoBehaviour
{

    [SerializeField]
    private GameObject effectPrefab;

    [SerializeField]
    private Transform spawn;

    [SerializeField]
    private float spawnInterval = 0.1f;

    private float timeRemaining;

    private List<GameObject> particleSystems = new List<GameObject>();
    private List<float> timestamps = new List<float>();

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (active)
        {
            if (timeRemaining <= 0)
            {
                // when effect is active, this method spawns effects and "drops" them behind the ship
                // in short but and random intervals with varying positions and (size?)
                SpawnWaterTrail();
                //RemoveOldParticleSystems(); // more performant would be to use object bool and just reactivate particle systemsloop TODO
                timeRemaining = spawnInterval + Random.Range(-0.05f, 0.05f);
                if (timeRemaining <= 0) { timeRemaining = 0.1f; }
            }
            else
            {
                timeRemaining -= Time.deltaTime;
            }

        }
    }

    private void RemoveOldParticleSystems()
    {
        for (int i = 0; i < timestamps.Count; i++)
        {
            Debug.Log("Comparing :" + Time.deltaTime +" and ts: "+timestamps[i]);
            if (Time.deltaTime - timestamps[i] >= 2)
            {
                Debug.Log("Removing old particle system");
                timestamps.RemoveAt(i);
                Destroy(particleSystems[i]);
            }
        }
    }

    private void SpawnWaterTrail()
    {
        Vector3 randSpawnPosition = new Vector3(
            spawn.position.x + Random.Range(-.2f, .2f), 
            spawn.position.y ,
            spawn.position.z + Random.Range(-.2f, .2f));

        particleSystems.Add(Instantiate(effectPrefab, randSpawnPosition, Quaternion.identity));
        timestamps.Add(Time.deltaTime);
        Debug.Log("Added particle system at :" + Time.deltaTime);
    } 

    public void ToggleActive()
    {
        if(active)
        {
            active = false;
        } else
        {
            active = true;
        }
    }
    public bool IsActive()
    {
        return active;
    }
}
