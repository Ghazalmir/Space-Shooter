using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float DelaySeconds = 1f;
    
    public void LoadGameOver()
    {
        StartCoroutine(Delay());
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().resetGame();
    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(DelaySeconds);
        SceneManager.LoadScene(2);

    }


}
