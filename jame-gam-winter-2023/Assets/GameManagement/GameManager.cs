using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<string> sceneNames;
    int currentSceneIndex = 1;
    [SerializeField] string finalScene;

           


    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;    
        }
        else
        {
            Destroy (this.gameObject);
            return;
        }
    }
    #endregion

    public void ResetGame()
    {
        LoadScene (sceneNames [0]);
    }

    public void StartGame ()
    {
        // hide UI
        Debug.Log ("STARTING GAME");
    }

    public void OnLevelComplete()
    {
        Debug.Log ("LEVEL COMPLETE");
        //GoToNextScene ();
    }

    public void GoToNextScene ()
    {
        if (currentSceneIndex > sceneNames.Count - 1)
        {
            GoToFinalScene ();
        }
        else
        {
            LoadScene (sceneNames [currentSceneIndex]);
        }
    }

    public void GoToFinalScene()
    {
        StartCoroutine (LoadYourAsyncScene (finalScene));
    }

    void LoadScene(string sceneName)
    {
        // Show loading UI
        StartCoroutine (LoadYourAsyncScene (sceneName));
    }

    IEnumerator LoadYourAsyncScene (string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            // Hide loading UI
            yield return null;
        }
    }
}
