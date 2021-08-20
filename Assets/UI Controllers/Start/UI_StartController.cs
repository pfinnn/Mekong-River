using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_StartController : MonoBehaviour
{
  private State state;

  public enum State {Playing}

  [SerializeField]
  GameManager gm;

  [SerializeField]
  private GameObject startMenuParent;

  public void Init(int highscore){
    startMenuParent.SetActive(true);
  }

  public void Uninit(){
    startMenuParent.SetActive(false);
  }

  public void OnPlayButtonPressed(){
    gm.TriggerPlaying();
  }

}
