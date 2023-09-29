using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject defeatScreen;
    public GameObject victoryScreen;
    public List<Sound> sounds;

    void Awake()
    {
        SetLevelSounds();
    }

    public void Defeat()
    {
        FreezeGame(true);
        defeatScreen.SetActive(true);
        PlaySound("Defeat");
    }

    public void Victory()
    {
        FreezeGame(true);
        victoryScreen.SetActive(true);
        PlaySound("Victory");
    }

    void SetLevelSounds()
    {
        SoundManager.instance.SetGameSounds(sounds);
    }

    public void PlaySound(string name)
    {
        SoundManager.instance.Play(name);
    }

    public void SetVolumeBar(Slider bar)
    {
        SoundManager.instance.SetVolume(bar);
    }

    public void OnSceneButton(string sceneName)
    {
        GameController.instance.OnSceneChange(sceneName);
    }

    public void FreezeGame(bool flag)
    {
        if (flag) Time.timeScale = 0f;
        else Time.timeScale = 1.0f;
    }

}
