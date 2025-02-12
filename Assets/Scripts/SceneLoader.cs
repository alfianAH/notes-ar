﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private SceneLoader instance;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Load sceneName
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Set time to normal
        StartCoroutine(LoadNewScene(sceneName));
    }

    private IEnumerator LoadNewScene(string sceneName)
    {
        StartCoroutine(DisplayLoadingUi(1, 1));
        
        yield return new WaitForSeconds(3f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        
        while (!async.isDone)
        {
            yield return null;
        }
        
        yield return StartCoroutine(DisplayLoadingUi(0, 1));
        gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Showing and hiding the whole loading UI.
    /// </summary>
    /// <param name="fadeAmount"></param>
    /// <param name="fadeSpeed"></param>
    /// <returns></returns>
    private IEnumerator DisplayLoadingUi(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
