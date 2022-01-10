using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeadsPocket : MonoBehaviour
{
    //public List<BeadBlue> beadBlues;
    public GameObject Blue;
    public Bead[,] beadMatrix;

    public List<BeadBlue> beadBlues;
    public List<BeadGreen> beadGreens;
    public List<BeadPurple> beadPurples;
    public List<BeadRed> beadReds;
    public List<BeadOrange> beadOranges;
    public List<BeadYellow> beadYellows;

    public bool isMoveEnd = true;

    private void Awake()
    {
        beadMatrix = new Bead[7,7];
        //beadBlues = Blue.GetComponentsInChildren<BeadBlue>();
        beadBlues = new List<BeadBlue>(GetComponentsInChildren<BeadBlue>());
        beadGreens = new List<BeadGreen>(GetComponentsInChildren<BeadGreen>());
        beadOranges = new List<BeadOrange>(GetComponentsInChildren<BeadOrange>());
        beadPurples = new List<BeadPurple>(GetComponentsInChildren<BeadPurple>());
        beadYellows = new List<BeadYellow>(GetComponentsInChildren<BeadYellow>());
        beadReds = new List<BeadRed>(GetComponentsInChildren<BeadRed>());

        for(int i = 0; i < 10; i++)
        {
            beadBlues[i].gameObject.SetActive(false);
            beadGreens[i].gameObject.SetActive(false);
            beadOranges[i].gameObject.SetActive(false);
            beadPurples[i].gameObject.SetActive(false);
            beadYellows[i].gameObject.SetActive(false);
            beadReds[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        MakeMapFirst();
        //printMatrix();
        //Debug.Log(beadMatrix[1,1]);
    }

    void MakeMapFirst()
    {
        for(int i = 0; i < 7; i++)
        {
            for(int j = 0; j < 7; j++)
            {

                int tmp = Random.Range(0, 6);

                List<Bead> tmpList;

                switch (tmp)
                {
                    case 0:
                        //tmpList = new List<Bead>(beadBlues);
                        tmpList = new List<Bead>(beadBlues);

                        break;
                    case 1:
                        tmpList = new List<Bead>(beadGreens);
                        break;
                    case 2:
                        tmpList = new List<Bead>(beadOranges);
                        break;
                    case 3:
                        tmpList = new List<Bead>(beadPurples);
                        break;
                    case 4:
                        tmpList = new List<Bead>(beadYellows);
                        break;
                    default:
                        tmpList = new List<Bead>(beadReds);
                        break;
                }

                for (int k = 0; k < tmpList.Count; k++)
                {
                    if (!tmpList[k].gameObject.activeInHierarchy)
                    {
                        /*
                        if (k.Equals(tmpList.Count - 1))
                        {
                            Bead bead = Instantiate(tmpList[0], this.transform);

                            tmpList.Add(bead);
                            k++;
                        }
                        */
                        tmpList[k].transform.position = new Vector3(-1.5f + 0.5f * i, -1.5f + 0.5f * j, 0);
                        tmpList[k].gameObject.SetActive(true);

                        beadMatrix[i, j] = tmpList[k];
                        beadMatrix[i, j].index = new Vector2Int(i, j);
                        break;
                    }
                    else if (k == tmpList.Count - 1)
                    {
                        Bead bead = new Bead();

                        bead = Instantiate(tmpList[0], this.transform);

                        tmpList.Add(bead);
                        k++;
                        tmpList[k].transform.position = new Vector3(-1.5f + 0.5f * i, -1.5f + 0.5f * j, 0);
                        tmpList[k].gameObject.SetActive(true);

                        beadMatrix[i, j] = tmpList[k];
                        beadMatrix[i, j].index = new Vector2Int(i, j);
                    }
                }
            }
        }
    }

    Bead MakeBead(int i, int j, Vector2 pos)
    {
        int tmp = Random.Range(0, 6);

        List<Bead> tmpList;

        switch (tmp)
        {
            case 0:
                //tmpList = new List<Bead>(beadBlues);
                tmpList = new List<Bead>(beadBlues);
                break;
            case 1:
                tmpList = new List<Bead>(beadGreens);
                break;
            case 2:
                tmpList = new List<Bead>(beadOranges);
                break;
            case 3:
                tmpList = new List<Bead>(beadPurples);
                break;
            case 4:
                tmpList = new List<Bead>(beadYellows);
                break;
            default:
                tmpList = new List<Bead>(beadReds);
                break;
        }
        Bead bead = new Bead();
        //Debug.Log(bead);
        for (int k = 0; k < tmpList.Count; k++)
        {
            if (!tmpList[k].gameObject.activeInHierarchy)
            {
                /*
                if (k.Equals(tmpList.Count - 1))
                {
                    bead = Instantiate(tmpList[0], this.transform);

                    tmpList.Add(bead);
                    k++;
                }
                */
                tmpList[k].transform.position = pos;
                tmpList[k].gameObject.SetActive(true);

                beadMatrix[i, j] = tmpList[k];
                beadMatrix[i, j].index.x = i;// = new Vector2Int(i, j);
                beadMatrix[i, j].index.y = j;
                break;
            }
            
            else if (k == tmpList.Count - 1)
            {
                bead = Instantiate(tmpList[0], this.transform);

                bead.transform.position = pos;
                bead.gameObject.SetActive(true);
                    

                /*
                tmpList.Add(bead);
                k++;
                tmpList[k].transform.position = pos;
                tmpList[k].gameObject.SetActive(true);
                beadMatrix[i, j] = tmpList[k];
                */
                bead.index.x = i;
                bead.index.y = j;
                beadMatrix[i, j] = bead;
                //beadMatrix[i, j].index = new Vector2Int(i, j);
            }
            
        }

        


        return bead;
    }


    public void SwapBead(Bead a, Bead b)
    {
        Vector2 tmp = a.transform.position;//포지션 바꾸고
        a.transform.position = b.transform.position;
        b.transform.position = tmp;

        Vector2Int tmp_2 = a.index;//인덱스 바꾸고
        a.index = b.index;
        b.index = tmp_2;

        beadMatrix[a.index.x, a.index.y] = a; // 매트릭스 바꾸고
        beadMatrix[b.index.x, b.index.y] = b;
    }

    public void CheckVerHorFunc(Bead bead, Bead swapBead)
    {
        bool isDo = false;


        for (int i = 0; i < 5; i++)//왼족에서 오른쪽으로 체크
        {
            int ctmp = 0;
            for (int j = i + 1; j < 7; j++)
            {
                //가로 체크 먼저
                if (beadMatrix[i,bead.index.y].GetComponent<Bead>().GetType() == beadMatrix[j,bead.index.y].GetComponent<Bead>().GetType())//같은 클래스면 진행
                {
                    ctmp++;
                    if (j == 6 && ctmp > 1)//j가 마지막이면 다음 인덱스 존재x이므로
                    {
                        
                        for (int k = i; k <= j; k++)//k==j???
                        {
                            beadMatrix[k, bead.index.y].gameObject.SetActive(false);
                            StartCoroutine(MoveDown(beadMatrix[k, bead.index.y]));
                            //beadMatrix[k, bead.index.y] = null;
                            //beadMatrix[k, bead.index.y] = null;
                        }

                        


                        isDo = true;
                        i = 6;//2중배열 컷
                        break;
                    }
                }
                else if (ctmp > 1)//다른데 ctmp가 2 이상이면
                {
                   
                    for (int k = i; k < j; k++)
                    {
                        beadMatrix[k, bead.index.y].gameObject.SetActive(false);
                        StartCoroutine(MoveDown(beadMatrix[k, bead.index.y]));//인덱스도 바꿔야하는데
                        //beadMatrix[k, bead.index.y] = null;
                        //beadMatrix[k, bead.index.y] = null;
                        //checkHor[k].collider.gameObject.SetActive(false);
                        //beadsPocket.StartCoroutine(beadsPocket.MoveDown_2(checkHor_2[k].collider.GetComponent<Bead>()));
                    }



                    isDo = true;
                    i = 6;//2중배열 컷
                    break;
                }
                else
                {
                    i += ctmp;
                    break;
                }
            }

        }
        //printMatrix();
        if (isDo == false)
        {

            for (int i = 0; i < 5; i++)//아래에서 위로 체크
            {
                int ctmp = 0;
                for (int j = i + 1; j < 7; j++)
                {
                    //printMatrix();
                   // Debug.Log("1" + beadMatrix[bead.index.x, i].GetComponent<Bead>().GetType());
                   // Debug.Log("2" + beadMatrix[bead.index.x, j].GetComponent<Bead>().GetType());

                    if (beadMatrix[bead.index.x,i].GetComponent<Bead>().GetType() == beadMatrix[bead.index.x,j].GetComponent<Bead>().GetType())
                    {

                        ctmp++;
                        if (j == 6 && ctmp > 1)//j가 마지막이면 다음 인덱스 존재x이므로
                        {

                            for (int k = i; k <= j; k++)//k==j???
                            {
                                beadMatrix[bead.index.x, k].gameObject.SetActive(false);
                                if(k != j)
                                {
                                    beadMatrix[bead.index.x, k] = null;
                                }
                            }
                            StartCoroutine(MoveDown(beadMatrix[bead.index.x, j]));

                            isDo = true;
                            i = 6;
                            break;
                        }
                    }
                    else if (ctmp > 1)//다른데 ctmp가 2 이상이면
                    {

                        for (int k = i; k < j; k++)
                        {

                            beadMatrix[bead.index.x, k].gameObject.SetActive(false);

                            if (k != j-1)
                            {
                                beadMatrix[bead.index.x, k] = null;
                            }
                        }
                        StartCoroutine(MoveDown(beadMatrix[bead.index.x, j-1]));


                        isDo = true;
                        i = 6;
                        break;
                    }
                    else
                    {
                        i += ctmp;
                        break;
                    }
                }

            }
        }

        if (isDo == true)
        {

            //블록 아래로!
        }
        else
        {
            SwapBead(bead, swapBead);

            //블럭 원상복귀
        }
        
    }

    IEnumerator MoveDown(Bead bead)
    {
        isMoveEnd = false;
        //bead.gameObject.SetActive(false);//없앤 후 위에것을 아래로
        //인덱스 정리하기
        //yield return new WaitForSeconds(0.1f);
        //Debug.Log(beadMatrix[bead.index.x, bead.index.y]);
        //Debug.Log(bead);
        beadMatrix[bead.index.x, bead.index.y] = null;
        Debug.Log(bead.index.y);
        int tmp = 0;
        bool isFirstCheck = false;
        for (int i = 0; i < 7; i++)//업앤것의 yindex부터 0까지 탐색함
        {
            if(beadMatrix[bead.index.x, i] == null)
            {
                isFirstCheck = true;
            }

            if (isFirstCheck)
            {
                if (beadMatrix[bead.index.x, i] != null)
                {
                    break;
                }
                else
                {
                    tmp++;
                }
            }
           
        }
        Debug.Log(bead.index.y);
        for (int i = bead.index.y+1; i < 7; i++)// i 는 옮길 인덱스
        {
           
            beadMatrix[bead.index.x, i - tmp] = beadMatrix[bead.index.x, i];
            beadMatrix[bead.index.x, i] = null;
            beadMatrix[bead.index.x, i - tmp].index.y = i - tmp;
        }
        Debug.Log(bead.index.y);
        for (int i = 7 - tmp; i < 7; i++)
        {
            //beadMatrix[bead.index.x, i] = MakeBead(bead.index.x, i, new Vector2(bead.transform.position.x, 2.0f + 0.5f * (i-7+tmp)));
            MakeBead(bead.index.x, i, new Vector2(bead.transform.position.x, 2.0f + 0.5f * (i - 7 + tmp)));
            //makebead에서 무언가 잘못됨
            //Debug.Log(beadMatrix[bead.index.x, i]);
        }
        Debug.Log(bead.index.y);
        //인덱스는 모두 완성됨

        Vector3 destinationPos;
        if (bead.index.y - tmp > -1)
        {
            destinationPos = beadMatrix[bead.index.x, bead.index.y - tmp].transform.position + Vector3.up * 0.5f;
            Debug.Log("1" + beadMatrix[bead.index.x, bead.index.y - tmp].transform.position + " "+ bead.index.y +" " + tmp);
        }
        else
        {
            destinationPos = new Vector3(-1.5f + 0.5f * bead.index.x, -1.5f, 0f);// beadMatrix[bead.index.x, bead.index.y].transform.position + Vector3.up * 0.5f;
            Debug.Log("2" + bead.index.y + " " + tmp);
        }
            
        //Debug.Log(destinationPos);
        //Debug.Log(destinationPos);
        /*
        for(int i = bead.index.y; i >= 0; i--)//업앤것의 yindex부터 0까지 탐색함
        {
            if(beadMatrix[bead.index.x,i] != null)
            {
                destinationPos = beadMatrix[bead.index.x, i].transform.position + Vector3.up * 0.5f;
                break;
            }
            
            else
            {
                beadMatrix[bead.index.x, i] = MakeBead(bead.index.x, i, new Vector2(bead.transform.position.x, 1.5f + 0.5f * i));
            }
            
            else if(i == 0)
            {
                destinationPos = new Vector3(bead.transform.position.x, -1.5f, 0);
            }
        }
        */

        printMatrix();
        /*
        O
        O
        X = bead
        X
        X
        O
        O
        tmp = 3
         */
        float time = 0f;
        
        while (true)
        {
            //Debug.Log("실행");
            for(int i = bead.index.y-tmp+1; i < 7; i++)
            {
                //Debug.Log(i);
                beadMatrix[bead.index.x,i].transform.position = Vector3.Lerp(beadMatrix[bead.index.x, i].transform.position, destinationPos + Vector3.up * 0.5f * (i - bead.index.y + tmp - 1), Time.deltaTime * 10);
            }
            time += Time.deltaTime;
            /*
            if(beadMatrix[bead.index.x, bead.index.y - tmp + 1].transform.position.y - destinationPos.y < 0.0001f)
            {
                for (int i = bead.index.y-tmp+1; i < 7; i++)
                {
                    beadMatrix[bead.index.x, i].transform.position = destinationPos + Vector3.up * 0.5f * (i - bead.index.y + tmp - 1);
                }
                break;
            }
            */

            if (time > 1f)
            {
                for (int i = bead.index.y - tmp + 1; i < 7; i++)
                {
                    beadMatrix[bead.index.x, i].transform.position = destinationPos + Vector3.up * 0.5f * (i - bead.index.y + tmp - 1);
                }
                //Debug.Log("끝");
                break;
            }
            yield return null;
        }
        isMoveEnd = true;
    }

    /*
     d
     */

    void printMatrix()
    {
        Debug.Log(beadMatrix[0, 6] + "  " + beadMatrix[1, 6] + " " + beadMatrix[2, 6] + " " + beadMatrix[3, 6] + " " + beadMatrix[4, 6]+ " " + beadMatrix[5, 6] + " " + beadMatrix[6, 6] + "\n" +
            beadMatrix[0, 5] + " " + beadMatrix[1, 5] + " " + beadMatrix[2, 5] + " " + beadMatrix[3, 5] + " " + beadMatrix[4, 5] + " " + beadMatrix[5, 5] + " " + beadMatrix[6, 5] + "\n" +
            beadMatrix[0, 4] + " " + beadMatrix[1, 4] + " " + beadMatrix[2, 4] + " " + beadMatrix[3, 4] + " " + beadMatrix[4, 4] + " " + beadMatrix[5, 4] + " " + beadMatrix[6, 4] + "\n" +
            beadMatrix[0, 3] + " " + beadMatrix[1, 3] + " " + beadMatrix[2, 3] + " " + beadMatrix[3, 3] + " " + beadMatrix[4, 3] + " " + beadMatrix[5, 3] + " " + beadMatrix[6, 3] + "\n" +
            beadMatrix[0, 2] + " " + beadMatrix[1, 2] + " " + beadMatrix[2, 2] + " " + beadMatrix[3, 2] + " " + beadMatrix[4, 2] + " " + beadMatrix[5, 2] + " " + beadMatrix[6, 2] + "\n" +
            beadMatrix[0, 1] + " " + beadMatrix[1, 1] + " " + beadMatrix[2, 1] + " " + beadMatrix[3, 1] + " " + beadMatrix[4, 1] + " " + beadMatrix[5, 1] + " " + beadMatrix[6, 1] + "\n"+
            beadMatrix[0, 0] + " " + beadMatrix[1, 0] + " " + beadMatrix[2, 0] + " " + beadMatrix[3, 0] + " " + beadMatrix[4, 0] + " " + beadMatrix[5, 0] + " " + beadMatrix[6, 0]);
    }

}

