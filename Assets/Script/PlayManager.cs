using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        isGameStart = false;
        instance = this;
    }

    private void Start()
    {
        animator.SetTrigger("gamestart");
        Invoke("EndAnime",1f);
    }

    void EndAnime()
    {
        isGameStart = true;
        BeadsPocket.Instance.SubCheckWhole();
        //StartCoroutine(BeadsPocket.Instance.SubCheckWhole());

    }
}
