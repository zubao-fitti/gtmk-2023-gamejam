using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private bool _gameStarted = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && this.GetGameStarted())
        {
            Button pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();

            // Verifica se o botão não é nulo e, em seguida, simula o clique nele
            if (pauseButton != null)
            {
                pauseButton.onClick.Invoke();
            }
        }
    }

    public void OnSceneChange(string sceneName)
    {
        SetGameStarted(false);
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }


    public bool GetGameStarted()
    {
        return _gameStarted;
    }

    public void SetGameStarted(bool newValue)
    {
        _gameStarted = newValue;
    }
}
