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
                //RemoveOldParticleSystems();
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
        foreach (GameObject particleGO in particleSystems)
        {
            ParticleSystem[] components = particleGO.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particleSys in components)
            {
                Debug.Log(particleSys.time);
                if(particleSys.time > 1.25f) // only this is working, particle system is still alive and playing. why?
                {
                    // destroy particle game object when one of the ps are inactive
                    particleSystems.Remove(particleGO);
                    ///Destroy(particleGO);
                    break; // break out of inner loop and go back to all particle Systems
                }
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
        //Debug.Log("Added particle system at :" + Time.deltaTime);
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
