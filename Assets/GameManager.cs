using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // ############### Gameplay classes ####################
    [SerializeField]
    private DifficultyManager difficultyManager;

    [SerializeField]
    private Player player;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private LaneManager laneManager;

    [SerializeField]
    private HarbourManager harbourManager;

    // ############### UI Controllers ####################

    [SerializeField]
    private UI_StartController startController;

    [SerializeField]
    private UI_PlayController playController;

    [SerializeField]
    private UI_GameOverController gameOverController;

    public enum GameState {
      Idle,
      Playing,
      GameOver,
      StartMenu
    }

    private GameState gameState = GameState.StartMenu;

    private int highscore = 0;
    private int score = 0;
    private bool highscoreChanged = false;
    private float timer = 100;

    // Start is called before the first frame update
    void Start()
    {
       SetToStartMenu();
       gameState = GameState.StartMenu;
       difficultyManager.SetTimer(timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Playing()) {
          timer -= Time.deltaTime;
          int seconds = (int) timer;
          playController.OnTimerChanged(seconds);
          difficultyManager.OnTimerChanged(timer);
          if (seconds < 1) {
            SetGameState(GameState.GameOver);
          }
        }
    }


    public void SetGameState( GameState newState )
      {
         // check if already in the new state
         if ( gameState == newState )
           return;
         // here you can call functions that only fire once on state change
         switch ( newState )
         {
           case GameState.Idle :
            // SetToIdle();
             break;

           case GameState.StartMenu :
             DeactivateCurrentUI();
             SetToStartMenu();
             // set the new gameState
             gameState = newState;
             break;

           case GameState.Playing :
                DeactivateCurrentUI();
                SetToPlaying();
                highscoreChanged = false;
                // set the new gameState
                gameState = newState;
                break;

           case GameState.GameOver :
             DeactivateCurrentUI();
             SetToGameOver();
             // set the new gameState
             gameState = newState;
             break;
         }
      }

      private void DeactivateCurrentUI(){
        switch ( gameState )
        {
          case GameState.Playing :
            playController.Uninit();
            break;

          case GameState.StartMenu :
            startController.Uninit();
            break;

          case GameState.GameOver :
            gameOverController.Uninit();
            break;
        }
      }

      private void SetToStartMenu(){
        // Display Main Menu
        startController.Init(highscore);
        // Block PLayer Movement
        player.Deactivate();
      }

      private void SetToPlaying(){
        laneManager.ResetLanes();
        harbourManager.Reset();
        player.Reset();
        score = 0;
        playController.Init(highscore);
        player.Activate();
      }

      private void SetToGameOver(){
        gameOverController.Init(highscoreChanged, score);
        player.Deactivate();
        timer = 100;
        difficultyManager.SetTimer(timer);
        difficultyManager.Reset();
        highscoreChanged = false;
      }

      public void TriggerPlaying(){
        SetGameState(GameState.Playing);
      }

      public void TriggerGameOver(){
        SetGameState(GameState.GameOver);
      }

      public GameState CurrentState() {
        return gameState;
      }

      public bool Playing(){
        return gameState.Equals(GameState.Playing);
      }

      public void UpdateScore(int currentScore){
        if (highscore < currentScore) {
          highscore = currentScore;
          playController.OnHighscoreChanged(highscore);
          highscoreChanged = true;
        }
        score = currentScore;
      }
}
