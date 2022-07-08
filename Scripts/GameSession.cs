using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score;

    // Start is called before the first frame update
    private void Awake()
    {
        SetUpSingleton();
    }
    private void SetUpSingleton()
    {
        int numberGameSessiosn = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessiosn > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int getScore()
    {
        return score;
    }

    public void addToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void resetGame()
    {
        Destroy(gameObject);
    }
}
