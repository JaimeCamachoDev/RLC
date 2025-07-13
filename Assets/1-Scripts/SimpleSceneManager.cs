using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour
{
    public string sceneName = "MainScene";

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MWaitAndLoad(float time)
    {
        StartCoroutine(WaitAndLoad(time));
    }
    IEnumerator WaitAndLoad(float time)
    {
        yield return new WaitForSeconds(time);
        LoadScene();
    }
}
