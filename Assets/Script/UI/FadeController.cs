using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private CanvasGroup cg;
    public float fadeTime; 
    public float startTime;
    float accumTime = 0f;
    private Coroutine fadeCor;

    private void Awake()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        StartFadeIn();
    }

    public void StartFadeIn()
    {
        if (fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(startTime);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 1f;

        StartCoroutine(FadeOut()); //�����ð� ������ �������� Fade out �ڷ�ƾ ȣ��

    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.0f + startTime);
        accumTime = 0f;
        while (accumTime < fadeTime / 2)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;
    }
}
