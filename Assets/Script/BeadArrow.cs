using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadArrow : Bead
{
    // Start is called before the first frame update
    private void OnMouseUp()
    {

    }

    private void OnMouseUpAsButton()
    {
        if (beadsPocket.isMoveEnd && PlayManager.Instance.isGameStart)
        {
            for (int i = 0; i < 7; i++)
            {
                beadsPocket.OffBead(index.x, i);

                
            }

            for (int j = 0; j < 7; j++)
            {
                if(j != index.x)
                {
                    beadsPocket.OffBead(j, index.y);
                }
            }

            PlayManager.Instance.UpdateScoreText();
            beadsPocket.AllMoveDown();
        }


    }
}
