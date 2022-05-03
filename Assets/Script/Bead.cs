using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bead : MonoBehaviour
{
    Vector2 Spos;
    Vector2 Epos;
    public Vector2Int index;
    bool isClicked = false;
    public BeadsPocket beadsPocket;

    public Transform parentTransform;
    Animator parentAnimator;

    private void Awake()
    {
        parentTransform = transform.parent;//GetComponentInParent<Transform>();
        parentAnimator = transform.GetComponentInParent<Animator>();
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
        
        if(isClicked == true && beadsPocket.isMoveEnd && PlayManager.Instance.isGameStart)
        {
            Spos = GetComponentInParent<Transform>().position;
            //Debug.Log("Spos : "+Spos);
            Epos = Epos - Spos;
            float angle = Vector2.SignedAngle(Vector2.up, Epos);

            int arrow = 0;

            Bead swapBead = null;
            if (angle >= -45f && angle <= 45f && index.y<6)//위
            {
                swapBead = beadsPocket.beadMatrix[index.x,index.y+1];
                arrow = 0;
            }
            else if (angle <= 135f && angle > 45f && index.x > 0)//왼
            {
                swapBead = beadsPocket.beadMatrix[index.x-1, index.y];

                arrow = 1;
            }
            else if (angle < -45f && angle >= -135f && index.x < 6)//오
            {
                swapBead = beadsPocket.beadMatrix[index.x+1, index.y];

                arrow = 2;
            }
            else if(index.y > 0)//아래
            {
                swapBead = beadsPocket.beadMatrix[index.x, index.y - 1];

                arrow = 3;
            }

            
            if (swapBead != null)
            {
                //여기 고쳐야됨
                
                
                
                beadsPocket.SwapBead(this, swapBead);
                beadsPocket.SubCheckVerHorFunc(this, swapBead);
                
                //beadsPocket.CheckVerHorFunc(this,swapBead);

            }
          

            isClicked = false;
        }

    }

    public void DoDo(Bead swapBead)
    {
        beadsPocket.SwapBead(this, swapBead);
        beadsPocket.SubCheckVerHorFunc(this, swapBead);
    }
  
    public void MovePosition(Vector3 pos)
    {
        parentTransform.position = pos;
    }

}
