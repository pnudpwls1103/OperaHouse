using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject storyImageParentObj = null;

    private AsyncOperation asyncOper = null;
    public void PlayStory()
    {
        storyImageParentObj.SetActive(true);
        StartCoroutine(LoadMainScene());
    }

    private IEnumerator LoadMainScene()
    {
        asyncOper = SceneManager.LoadSceneAsync("MainScene");
        asyncOper.allowSceneActivation = false;

        while(!asyncOper.isDone)
        {
            yield return null;
        }
    }

    public void ChangeMainScene()
    {
        asyncOper.allowSceneActivation = true;
    }
}