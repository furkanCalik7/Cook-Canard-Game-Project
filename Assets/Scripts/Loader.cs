using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static GameScene targetScene;

    public enum GameScene
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    public static void Load(GameScene targetScene)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(GameScene.LoadingScene.ToString());
    }

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
