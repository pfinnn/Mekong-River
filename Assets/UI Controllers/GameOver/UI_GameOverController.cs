using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_GameOverController : MonoBehaviour
{
  private State state;

  public enum State {Playing}

  [SerializeField]
  GameManager gm;

  [SerializeField]
  private GameObject gameOverParent;

  [SerializeField]
  private GameObject congratsTMPParent;

  [SerializeField]
  private TextMeshProUGUI scoreTMP;


  public void Init(bool highscoreChanged, int score){
    gameOverParent.SetActive(true);
    string scoreText = "You safely crossed " +score.ToString()+ " passengers over the Mekong River!";
    scoreTMP.text = scoreText;
    if (highscoreChanged) {
      congratsTMPParent.SetActive(true);
    }
  }

  public void Uninit(){
    congratsTMPParent.SetActive(false);
    gameOverParent.SetActive(false);
  }

  public void SetScore(int newScore){
    scoreTMP.text = newScore.ToString();
  }

  public void OnReplayPressed(){
    gm.TriggerPlaying();
  }
}
