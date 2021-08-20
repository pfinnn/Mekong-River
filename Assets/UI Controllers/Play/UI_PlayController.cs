using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PlayController : MonoBehaviour
{

    // TODO implement UI Controller interface (Init, Uninit)

    private State state;

    public enum State {Playing}

    [SerializeField]
    private TextMeshProUGUI highscoreTMP;

    [SerializeField]
    private TextMeshProUGUI scoreTMP;

    [SerializeField]
    private TextMeshProUGUI passengerTMP;

    [SerializeField]
    private TextMeshProUGUI timerTMP;

    [SerializeField]
    private DifficultyManager dM;


    public void Init(int highscore){
      this.gameObject.SetActive(true);
      OnHighscoreChanged(highscore);
      OnScoreChanged(0);
      OnPassengerChanged(0);
    }

    public void Uninit(){
      this.gameObject.SetActive(false);
    }

    public void OnScoreChanged(int newScore){
      scoreTMP.text = newScore.ToString();
    }

    public void OnHighscoreChanged(int newHighscore){
      highscoreTMP.text = newHighscore.ToString();
    }
    public void OnPassengerChanged(int newPassenger){
      passengerTMP.text = newPassenger.ToString();
    }

    public void OnTimerChanged(int seconds){
      timerTMP.text = seconds.ToString();
    }

    public void OnDifficultyChange(){
        timerTMP.color = dM.GetDifficultyColor();
    }

}
