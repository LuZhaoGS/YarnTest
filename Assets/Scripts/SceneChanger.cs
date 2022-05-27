using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public string changeToSceneName = "";
    public float fadeAnimLength = 1;
    private Image fadeScreen;

    private void Awake()
    {
        fadeScreen = GetComponentInChildren<Image>();
        StartCoroutine(FadeIn());
    }

    public void ChangeScene()
    {
        StartCoroutine(FadeOutAndChangeScene());
    }
    
    private IEnumerator FadeOutAndChangeScene()
    {
        yield return FadeOut();
        
        if (!string.IsNullOrEmpty(changeToSceneName))
        {
            SceneManager.LoadScene(changeToSceneName);
        }
    }
    private IEnumerator FadeOut()
    {
        fadeScreen.gameObject.SetActive(true);
        yield return FadeAnim(Color.clear, Color.black, fadeAnimLength);
    }

    private IEnumerator FadeIn()
    {
        fadeScreen.gameObject.SetActive(true);
        yield return FadeAnim(Color.black, Color.clear, fadeAnimLength);
        fadeScreen.gameObject.SetActive(false);
    }

    private IEnumerator FadeAnim(Color startColor, Color endColor, float fadeAnimLength)
    {
        fadeScreen.color = startColor;
        float ratio = 0;
        while (ratio < 1)
        {
            ratio += Time.deltaTime / fadeAnimLength;
            fadeScreen.color = Color.Lerp(startColor, endColor, ratio);
            yield return null;
        }

        fadeScreen.color = endColor;
    }
}
