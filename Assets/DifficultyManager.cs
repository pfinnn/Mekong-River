using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    // ############ Difficulty Enum #################

    public enum Difficulty
    {
      Easy,
      Medium,
      Hard
    }

    private Difficulty difficulty = Difficulty.Easy;

    // ############ Timer #################
    private float mediumTimerCeiling;
    private float hardTimerCeiling;


    // ############ Difficulty UI Variables #################
    [Header("UI Variables")]
    [SerializeField]
    private Color easyColor;
    [SerializeField]
    private Color mediumColor;
    [SerializeField]
    private Color hardColor;

    // ############ Difficulty Gameplay Variables #################
    [Header("Gameplay Variables")]
    [SerializeField]
    private int easyLaneCount = 3;
    [SerializeField]
    private float easyFerrySpeed = 1f;
    [SerializeField]
    private float easySpawnInterval = 6f;
    [SerializeField]
    private float easyRandomness = 0.25f;

    [SerializeField]
    private int mediumLaneCount = 4;
    [SerializeField]
    private float mediumFerrySpeed = 1.5f;
    [SerializeField]
    private float mediumSpawnInterval = 5.5f;

    [SerializeField]
    private int hardLaneCount = 5;
    [SerializeField]
    private float hardFerrySpeed = 2f;
    [SerializeField]
    private float hardSpawnInterval = 5f;

    // ############ GameplayManagers #################
    [Header("Gameplay Managers")]
    [SerializeField]
    private LaneManager laneManager;

    // ############ UI Controllers   #################
    [Header("UI Controllers")]
    [SerializeField]
    private UI_PlayController playController;

    public void Reset(){
      difficulty = Difficulty.Easy;
      OnDifficultyChange();
    }

    public void SetTimer(float initialTimer) {
      hardTimerCeiling = initialTimer - initialTimer/3*2;
      mediumTimerCeiling = initialTimer - initialTimer/3;
    }

    // TODO notify play controller, or let pc ask current difficulty

    public void OnTimerChanged(float time){
      // if medium time is lower than (init - medium), but time is not lower than hard time limit
      // and difficulty is not yet set to medium
      if (time < mediumTimerCeiling && time > hardTimerCeiling && !difficulty.Equals(Difficulty.Medium)) {
        // then set difficulty to medium and alter Gameplay
        difficulty = Difficulty.Medium;
        // notify lane manager
        OnDifficultyChange();
      }

      // if difficulty is not yet set to hard and time is lower than hard time Limit
      if (time < hardTimerCeiling && !difficulty.Equals(Difficulty.Hard)){
        // then set difficulty to hard
        difficulty = Difficulty.Hard;
        // notify lane manager
        OnDifficultyChange();
      }
    }

    private void OnDifficultyChange(){
        laneManager.OnDifficultyChange();
        playController.OnDifficultyChange();
    }

    public Difficulty GetDifficulty(){
      return difficulty;
    }

    public float GetSpawnInterval(){
      return difficulty switch
      {
        Difficulty.Easy => easySpawnInterval,
        Difficulty.Medium => mediumSpawnInterval,
        Difficulty.Hard => hardSpawnInterval
      };
    }

    public float GetFerrySpeed(){
      return difficulty switch
      {
        Difficulty.Easy => easyFerrySpeed,
        Difficulty.Medium => mediumFerrySpeed,
        Difficulty.Hard => hardFerrySpeed
      };
    }

    public float GetRandomizerVal(){
      return easyRandomness;
    }

    public float GetDefaultFerrySpeed()
    {
      return easyFerrySpeed;
    }

    public float GetDefaultSpawnInterval(){
      return easySpawnInterval;
    }

    public Color GetDifficultyColor(){
      return difficulty switch
      {
        Difficulty.Easy => easyColor,
        Difficulty.Medium => mediumColor,
        Difficulty.Hard => hardColor
      };
    }

  }
