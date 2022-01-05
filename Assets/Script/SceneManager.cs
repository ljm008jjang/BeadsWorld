using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance = null;

    public static SceneManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public bool isMSEnd = true;
    public IEnumerator MoveScene(int SceneIndex)
    {
        AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneIndex, LoadSceneMode.Single);

        isMSEnd = false;

        while(op.isDone == false)
        {
            yield return null;
        }

        isMSEnd = true;
    }
}
