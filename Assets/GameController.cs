using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    public enum GameState
    {
        Entering,
        WaitingForReady,
        Play,
        EndOfRound,
        StartOfRound,
        PostMatch,
        Exiting,
        COUNT
    }
    public GameState gameState = GameState.Entering;

    public GameplayCanvasManager gameplayCanvas;

    public List<Player> players = new List<Player>();

    //public Map currentMap;  // -- map loading to pick spawn points and what not...

    void Start()
    {
        Debug.Log("GAMECONTROLLER STARTING");

        Player[] playerArray = FindObjectsOfType<Player>();
        for (int i = 0; i < playerArray.Length; i++)
        {
            players.Add(playerArray[i]);
        }

        gameplayCanvas = FindObjectOfType<GameplayCanvasManager>();
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Entering:            HandleEnteringGame(); break;
            case GameState.WaitingForReady:     CheckForPlayerReady(); break;
            case GameState.Play:                HandleGameplay(); break;
            case GameState.EndOfRound:          HandleEndOfRound(); break;
        }
    }

    void HandleGameplay()
    { 
        int alivePlayers = players.Count;
        foreach(Player p in players)
        {
            if (!p.alive)
            {
                alivePlayers--;
            }
        }
        if (alivePlayers <= 1)
        {
            Debug.Log("Ending Round");
            gameState = GameState.EndOfRound;
        }
    }


    public void HandleEnteringGame()
    {
        Debug.Log("Entering Game");
        // Play enter game sequence;
        gameState = GameState.WaitingForReady;
    }


    void CheckForPlayerReady()
    {
        int readyCount = 0;
        foreach (Player p in players)
        {
            if (p.ready)
            {
                readyCount++;
            }
        }
        if (readyCount == players.Count && gameState != GameState.StartOfRound)
        {
            StartCoroutine(StartRound());
        }
    }

    IEnumerator StartRound()
    {
        gameState = GameState.StartOfRound;

        gameplayCanvas.SetCountdownText("3");

        yield return new WaitForSeconds(0.5f);

        gameplayCanvas.SetCountdownText("2");

        yield return new WaitForSeconds(0.5f);

        gameplayCanvas.SetCountdownText("1");

        yield return new WaitForSeconds(0.5f);

        gameplayCanvas.SetCountdownText("GO!");

        yield return new WaitForSeconds(0.5f);

        gameState = GameState.Play;
        yield return null;
    }

    void HandleEndOfRound()
    {

    }
}
