using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    //object to assign the loading screen
    public GameObject loadingScreen;
    //assign the loading screen progress bar
    public Slider progressBar;
    //assigns the percentage
    public Text progressText;
    [HideInInspector]
    public string sceneName;

    // Method for changing to the next scene
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }


    // Method that handles Quit game
    public void Quit()
    {
        // Quits aplication
        Application.Quit();

        // Exits play mode if playing in Editor
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
    }


    //calls a loading screen to appear while scene is not loaded
    IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        if (loadingScreen && progressBar)
        {
            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .09f);

                progressBar.value = progress;
                progressText.text = progress * 100 + "%";
                yield return null;

            }
        }
    }


    // Method to get actual scene name
    public string GetActualSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
