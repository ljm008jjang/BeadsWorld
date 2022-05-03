using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayManager : MonoBehaviour
{
    private static PlayManager instance = null;
    public Animator animator;

    public static PlayManager Instance
    {
        get
        {
            return instance;
        }
    }

    public bool isGameStart;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public int remainTime = 0;
    public GameObject endUI;
    int skillCoolTime = 0;
    public Image skillImage;
    public Animator explosionAnimator;

    private void Awake()
    {
        isGameStart = false;
        instance = this;
        skillCoolTime = 0;
    }

    private void Start()
    {
       
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        WaitForSeconds os = new WaitForSeconds(1f);
        yield return os;
        animator.SetTrigger("gamestart");
        yield return os;
        isGameStart = true;
        BeadsPocket.Instance.SubCheckWhole();

        while (remainTime >= 0) {
            timeText.text = "Time : " + remainTime.ToString();

            remainTime--;

            yield return os;
        }

        isGameStart = false;

        if(score > GameManager.Instance.bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
            GameManager.Instance.bestScore = score;
        }

        endUI.gameObject.SetActive(true);
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void GoToMainScene()
    {
        UIManager.Instance.ClickEndGame();
    }

    public void SkillBomb()
    {
        if (BeadsPocket.Instance.isMoveEnd && isGameStart && skillCoolTime == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int tmpX = i+1;
                    int tmpY = j+1;

                    if (tmpX >= 0 && tmpX < 7 && tmpY >= 0 && tmpY < 7)
                        BeadsPocket.Instance.OffBead(tmpX, tmpY);
                }
            }
            UpdateScoreText();
            BeadsPocket.Instance.AllMoveDown();

            explosionAnimator.gameObject.SetActive(true);

            explosionAnimator.SetTrigger("Boom");

            StartCoroutine(SetSkillCooltime());
        }
    }

    IEnumerator SetSkillCooltime()
    {
        skillCoolTime = 20;
        WaitForSeconds ws = new WaitForSeconds(1f);
        while(skillCoolTime != 0)
        {
            skillCoolTime--;
            skillImage.fillAmount = 1 - skillCoolTime * 0.05f;
            yield return ws;
        }
    }
}
