using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadContorller : MonoBehaviour
{
    Bead bead;

    private void Awake()
    {
        bead = GetComponentInChildren<Bead>();
    }
    public void MoveToChild(int num)
    {
        StartCoroutine(tmp(num));
    }

    IEnumerator tmp(int num)
    {
        yield return null;
        transform.position = bead.transform.position;
        bead.transform.localPosition = Vector3.zero;
        switch (num)
        {
            case 0:
                bead.DoDo(BeadsPocket.Instance.beadMatrix[bead.index.x,bead.index.y+1]);
                break;
            case 1:
                bead.DoDo(BeadsPocket.Instance.beadMatrix[bead.index.x-1, bead.index.y]);
                break;
            case 2:
                bead.DoDo(BeadsPocket.Instance.beadMatrix[bead.index.x+1, bead.index.y]);
                break;
            case 3:
                bead.DoDo(BeadsPocket.Instance.beadMatrix[bead.index.x, bead.index.y - 1]);
                break;
        }
        
        //yield return null;
    }

}
