using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public int bestScore = 0;

    private void Awake()
    {
        instance = this;
        bestScore = PlayerPrefs.GetInt("bestScore");

        StartCoroutine(resolution());
    }

    IEnumerator resolution()
    {
        WaitForSeconds ws = new WaitForSeconds(1f);
        while (true)
        {
            int width = Screen.width;
            int x = width * 16 / 9;
            Screen.SetResolution(width, x, false);

            yield return ws;
        }
        
    }

    
    private void Start()
    {
        UIManager.Instance.UpdateBestScoreText();
    }

}
