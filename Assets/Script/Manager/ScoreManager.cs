using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TMP_Text scoreText;
    public void Init()
    {
        scoreText = GameObject.Find("ScoreNumber").GetComponent<TMP_Text>();
        scoreText.text = "0";
    }
    public void SetScore(float score)
    {
        scoreText.text = Math.Round(score).ToString();
    }
}
