using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadBomb : Bead
{
    


    private void OnMouseUp()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        if (beadsPocket.isMoveEnd && PlayManager.Instance.isGameStart)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int tmpX = index.x - 1 + i;
                    int tmpY = index.y - 1 + j;

                    if (tmpX >= 0 && tmpX < 7 && tmpY >= 0 && tmpY < 7)
                        beadsPocket.OffBead(tmpX, tmpY);
                }
            }
            PlayManager.Instance.UpdateScoreText();
            beadsPocket.AllMoveDown();
        }
     
        
    }

}
