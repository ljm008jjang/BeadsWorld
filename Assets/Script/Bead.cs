using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bead : MonoBehaviour
{
    Vector2 Spos;
    Vector2 Epos;
    public Vector2Int index;
    bool isClicked = false;
    BeadsPocket beadsPocket;

    private void Awake()
    {
        index = new Vector2Int();
        beadsPocket = GetComponentInParent<BeadsPocket>();
    }

    private void OnMouseDrag()
    {
        Epos = Input.mousePosition;
        Epos = Camera.main.ScreenToWorldPoint(Epos);
        isClicked = true;
    }

    private void OnMouseUp()
    {
        
        if(isClicked == true && beadsPocket.isMoveEnd)
        {
            Spos = transform.position;
            //Debug.Log("Spos : "+Spos);
            Epos = Epos - Spos;
            float angle = Vector2.SignedAngle(Vector2.up, Epos);

            Bead swapBead = null;
            if (angle >= -45f && angle <= 45f && index.y<6)//À§
            {
                swapBead = beadsPocket.beadMatrix[index.x,index.y+1];
            }
            else if (angle <= 135f && angle > 45f && index.x > 0)//¿Þ
            {
                swapBead = beadsPocket.beadMatrix[index.x-1, index.y];
            }
            else if (angle < -45f && angle >= -135f && index.x < 6)//¿À
            {
                swapBead = beadsPocket.beadMatrix[index.x+1, index.y];
            }
            else if(index.y > 0)//¾Æ·¡
            {
                swapBead = beadsPocket.beadMatrix[index.x, index.y - 1];
            }

            
            if (swapBead != null)
            {

                beadsPocket.SwapBead(this, swapBead);
                beadsPocket.CheckVerHorFunc(this,swapBead);
                //StartCoroutine(CheckVerHorFunc(hit));
            }
          

            isClicked = false;
        }

    }

    
}
