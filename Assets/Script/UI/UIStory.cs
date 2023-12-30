using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStory : MonoBehaviour
{
    [SerializeField] private TitleScene parentScene = null;
    [SerializeField] private Image[] storyImages = null;

    private int currentImageIndex = 0;
    private float fadeTime = 1f; 
    private float startTime = 1f;
    private Image currentImage = null;

    float accumTime = 0f;
    private Coroutine fadeRoutine;

    private void Start()
    {
        //cg = gameObject.GetComponent<CanvasGroup>();
        Init();
        StartFadeIn();
    }

    private void NextImage()
    {
        currentImageIndex = currentImageIndex + 1;
        currentImage = storyImages[currentImageIndex];
        StartFadeIn();
    }

    private void Init()
    {
        foreach(Image image in storyImages)
        {
            image.color = new Color(1f, 1f, 1f, 0f);
        }

        currentImageIndex = 0;
        currentImage = storyImages[currentImageIndex];
    }

    public void StartFadeIn()
    {
        if (fadeRoutine != null)
        {
            StopAllCoroutines();
            fadeRoutine = null;
        }
        fadeRoutine = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(startTime);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            currentImage.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, accumTime / fadeTime));
            yield return null;
            accumTime += Time.deltaTime;
        }
        currentImage.color = new Color(1f, 1f, 1f, 1f);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.0f + startTime);
        accumTime = 0f;
        while (accumTime < fadeTime / 2)
        {
            currentImage.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, accumTime / fadeTime));
            yield return null;
            accumTime += Time.deltaTime;
        }

        currentImage.color = new Color(1f, 1f, 1f, 0f);

        if (currentImageIndex >= storyImages.Length - 1)
        {
            parentScene.ChangeMainScene();
            yield break;
        }
        NextImage();
    }
}
