using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    public TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        instance = this;
    }

    public void ClickStartGame()
    {
        StartCoroutine(SceneManager.Instance.MoveScene(1));
    }

    public void ClickEndGame()
    {
        StartCoroutine(SceneManager.Instance.MoveScene(0));
    }

    public void UpdateBestScoreText()
    {
        bestScoreText.text = "Best Score : " + GameManager.Instance.bestScore.ToString();
    }
}
