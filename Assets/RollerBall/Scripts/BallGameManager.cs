using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallGameManager : Singleton<BallGameManager>
{
    enum State
    {
        TITLE,
        PLAYER_START,
        GAME,
        PLAYER_DEAD,
        PLAYER_WINS,
        GAME_OVER

    }

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawn;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject gameoverScreen;
    [SerializeField] GameObject playerWinsScreen;
    [SerializeField] GameObject[] collectibles;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] TMP_Text timeUI;
    [SerializeField] Slider healthBarUI;

    public float playerHealth { set { healthBarUI.value = value; } }

    public delegate void GameEvent();

    public event GameEvent startGameEvent;
    public event GameEvent stopGameEvent;

    int score = 0;
    int lives = 3;
    State state = State.TITLE;
    float stateTimer;
    float gameTime = 0;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreUI.text = score.ToString("D2");
        }
    }

    public int Lives
    {
        get { return lives; }
        set
        {
            lives = value;
            livesUI.text = "LIVES: " + lives.ToString();
        }
    }

    public float GameTime
    {
        get { return gameTime; }
        set
        {
            gameTime = value;
            timeUI.text = "<mspace=mspace=36>" + gameTime.ToString("0.0");
        }
    }

    private void Update()
    {
        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case State.TITLE:
                break;
            case State.PLAYER_START:
                Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
                startGameEvent?.Invoke();
                mainCamera.SetActive(false);
                state = State.GAME;
                break;
            case State.GAME:
                GameTime -= Time.deltaTime;
                if (gameTime <= 0)
                {
                    GameTime = 0;
                    state = State.GAME_OVER;
                    stateTimer = 5;
                }
                if (score == 12)
                {
                    state = State.PLAYER_WINS;
                    stateTimer = 4;
                    GameTime = 0;
                    
                }
                break;
            case State.PLAYER_DEAD:
                if(stateTimer <= 0)
                {
                    state = State.PLAYER_START;
                }
                break;
            case State.PLAYER_WINS:
                playerWinsScreen.SetActive(true);
                if(stateTimer <= 0)
                {
/*                    var player = FindObjectOfType<RollerPlayer>();
                    Destroy(player.gameObject);*/
                    state = State.GAME_OVER;
                }
                break;
            case State.GAME_OVER:
                if (stateTimer <= 0)
                {
                    DestroyPickUps();
                    playerWinsScreen.SetActive(false);
                    gameoverScreen.SetActive(false);
                    titleScreen.SetActive(true);
                    state = State.TITLE;
                }
                break;
            default:
                break;
        }
    }

    public void OnStartGame()
    {
        state = State.PLAYER_START;
        Score = 0;
        Lives = 3;
        foreach (GameObject pickup in collectibles)
        {
            Instantiate(pickup, pickup.transform.position, pickup.transform.rotation);
        }
        GameTime = 60;
        titleScreen.SetActive(false);
    }

    public void OnPlayerDead()
    {
        mainCamera.SetActive(true);
        Lives -= 1;
        if(lives <= 0)
        {
            state = State.GAME_OVER;
            stateTimer = 5;

            gameoverScreen.SetActive(true);
        }
        else
        {
            state = State.PLAYER_DEAD;
            stateTimer = 3;
        }
        stopGameEvent?.Invoke();
    }

    public void OnStartTitle()
    {
        state = State.TITLE;
        titleScreen.SetActive(true);
        stopGameEvent?.Invoke();
    }

    public void DestroyPickUps()
    {
        RollerPickup[] destroyPickups = FindObjectsOfType<RollerPickup>();
        foreach(RollerPickup pickup in destroyPickups)
        {
            Destroy(pickup.gameObject);
        }
    }
}