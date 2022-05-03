using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeadsPocket : MonoBehaviour
{
    private static BeadsPocket instance;

    public static BeadsPocket Instance
    {
        get
        {
            return instance;
        }
    }

    //public List<BeadBlue> beadBlues;
    public Bead[] NewBead;
    public Bead[,] beadMatrix;

    public List<BeadBlue> beadBlues;
    public List<BeadGreen> beadGreens;
    public List<BeadPurple> beadPurples;
    public List<BeadRed> beadReds;
    public List<BeadOrange> beadOranges;
    public List<BeadYellow> beadYellows;
    public List<BeadBomb> beadBombs;
    public List<BeadArrow> beadArrows;

    public bool isMoveEnd = true;

    Coroutine[] coroutines;

    public AudioSource audio;
    private void Awake()
    {
        instance = this;

        beadMatrix = new Bead[7,7];
        //beadBlues = Blue.GetComponentsInChildren<BeadBlue>();
        beadBlues = new List<BeadBlue>(GetComponentsInChildren<BeadBlue>());
        beadGreens = new List<BeadGreen>(GetComponentsInChildren<BeadGreen>());
        beadOranges = new List<BeadOrange>(GetComponentsInChildren<BeadOrange>());
        beadPurples = new List<BeadPurple>(GetComponentsInChildren<BeadPurple>());
        beadYellows = new List<BeadYellow>(GetComponentsInChildren<BeadYellow>());
        beadReds = new List<BeadRed>(GetComponentsInChildren<BeadRed>());
        beadBombs = new List<BeadBomb>(GetComponentsInChildren<BeadBomb>());
        beadArrows = new List<BeadArrow>(GetComponentsInChildren<BeadArrow>());


        coroutines = new Coroutine[7];
        for(int i = 0; i < coroutines.Length; i++)
        {
            coroutines[i] = null;
        }
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            beadBlues[i].gameObject.SetActive(false);
            beadGreens[i].gameObject.SetActive(false);
            beadOranges[i].gameObject.SetActive(false);
            beadPurples[i].gameObject.SetActive(false);
            beadYellows[i].gameObject.SetActive(false);
            beadReds[i].gameObject.SetActive(false);
            beadBombs[i].gameObject.SetActive(false);
            beadArrows[i].gameObject.SetActive(false);
        }

        MakeMapFirst();
        //StartCoroutine(CheckWhole());
        //StartCoroutine(SubCheckWhole());
        //printMatrix();
        //Debug.Log(beadMatrix[1,1]);
    }

    void MakeMapFirst()
    {
        for(int i = 0; i < 7; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                Vector3 pos = new Vector3(-1.5f + 0.5f * i, -1.5f + 0.5f * j, 0);

                MakeBead(i, j,pos);


          
            }
        }
    }

    Bead MakeBead(int i, int j, Vector2 pos)
    {

        int tmp = Random.Range(0, 122);

        bool tmp_3 = false;

        List<Bead> tmpList;
        if(tmp < 20)
        {
            tmpList = new List<Bead>(beadBlues);

        }
        else if(tmp < 40)
        {
            tmpList = new List<Bead>(beadGreens);
        }
        else if(tmp < 60)
        {
            tmpList = new List<Bead>(beadOranges);
        }
        else if (tmp < 80)
        {
            tmpList = new List<Bead>(beadReds);
        }
        else if (tmp < 100)
        {
            tmpList = new List<Bead>(beadPurples);
        }
        else if (tmp < 120)
        {
            tmpList = new List<Bead>(beadYellows);
        }
        else if(tmp == 121)
        {
            //Debug.Log(tmp);
            tmpList = new List<Bead>(beadBombs);
       
        }else
        {
            tmpList = new List<Bead>(beadArrows);
        }

        Bead bead = new Bead();

        for (int k = 0; k < tmpList.Count; k++)
        {
            if (!tmpList[k].gameObject.activeInHierarchy)
            {

                //tmpList[k].transform.position = pos;
                tmpList[k].MovePosition(pos);
                beadMatrix[i, j] = tmpList[k];
                
                beadMatrix[i, j].index.x = i;// = new Vector2Int(i, j);

                beadMatrix[i, j].index.y = j;

                beadMatrix[i, j].gameObject.SetActive(true);

                
               
                break;
            }
            
            else if (k == tmpList.Count - 1)
            {
                bead = MakeBead(i, j, pos);

            }
            
        }

        


        return bead;
    }


    public void SwapBead(Bead a, Bead b)
    {
        
        Vector2 tmp = a.parentTransform.position;
        a.MovePosition(b.parentTransform.position);
        b.MovePosition(tmp);
        

        
        Vector2Int tmp_2 = a.index;//인덱스 바꾸고
        a.index = b.index;
        b.index = tmp_2;

        beadMatrix[a.index.x, a.index.y] = a; // 매트릭스 바꾸고
        beadMatrix[b.index.x, b.index.y] = b;
        
    }

    public void OffBead(int k, int l)
    {
        beadMatrix[k, l].gameObject.SetActive(false);
        beadMatrix[k, l] = null;
        PlayManager.Instance.score++;
        audio.Play();
        //Debug.Log(PlayManager.Instance.score);
    }

    public bool SubCheckVerHorFunc(Bead bead, Bead swapBead)
    {
        bool isDo = false;

        for(int l=0; l<7; l++)//0 ~ 6번까지 체크
        {
            for (int i = 0; i < 5; i++)
            {
                int ctmp = 0;
                for (int j = i + 1; j < 7; j++)
                {
                    //가로 체크 먼저
                    if (beadMatrix[i, l].GetComponent<Bead>().GetType() == beadMatrix[j, l].GetComponent<Bead>().GetType())//같은 클래스면 진행
                    {
                        ctmp++;
                        if (j == 6 && ctmp > 1)//j가 마지막이면 다음 인덱스 존재x이므로
                        {

                            for (int k = i; k <= j; k++)//k==j???
                            {
                                OffBead(k, l);
                                /*
                                beadMatrix[k, l].gameObject.SetActive(false);
                                beadMatrix[k, l] = null;
                                */
                            }


                            i += ctmp;
                            isDo = true;
                            /*

                            i = 6;//2중배열 컷
                            */
                            break;
                        }
                    }
                    else if (ctmp > 1)//다른데 ctmp가 2 이상이면
                    {

                        for (int k = i; k < j; k++)
                        {
                            OffBead(k, l);
                            /*
                            beadMatrix[k, l].gameObject.SetActive(false);
                            beadMatrix[k, l] = null;
                            */
                        }

                        i += ctmp;
                        isDo = true;
                        /*
                        i = 6;//2중배열 컷
                        */
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



        for (int l = 0; l < 7; l++)//0 ~ 6번까지 체크
        {
            for (int i = 0; i < 5; i++)//아래에서 위로 체크
            {
                int ctmp = 0;
                for (int j = i + 1; j < 7; j++)
                {
                    if(beadMatrix[l, i] != null && beadMatrix[l, j] != null)
                    {
                        if (beadMatrix[l, i].GetComponent<Bead>().GetType() == beadMatrix[l, j].GetComponent<Bead>().GetType())
                        {

                            ctmp++;
                            if (j == 6 && ctmp > 1)//j가 마지막이면 다음 인덱스 존재x이므로
                            {

                                for (int k = i; k <= j; k++)//k==j???
                                {
                                    OffBead(l, k);
                                    /*
                                    beadMatrix[l, k].gameObject.SetActive(false);

                                    beadMatrix[l, k] = null;
                                    */
                                }

                                isDo = true;
                                i += ctmp;
                                //i = 6;
                                break;
                            }
                        }
                        else if (ctmp > 1)//다른데 ctmp가 2 이상이면
                        {

                            for (int k = i; k < j; k++)
                            {
                                OffBead(l, k);
                                /*
                                beadMatrix[l, k].gameObject.SetActive(false);

                                beadMatrix[l, k] = null;
                                */
                            }


                            isDo = true;
                            i += ctmp;
                            //i = 6;
                            break;
                        }
                        else
                        {
                            i += ctmp;
                            break;
                        }
                    }
                    else
                    {
                        i = 6;
                        break;
                        
                    }

                }

            }

        }

        if (isDo == true)
        {
            PlayManager.Instance.UpdateScoreText();
            AllMoveDown();
            //블록 아래로!
        }
        else if (swapBead != null)
        {
            SwapBead(bead, swapBead);
        }
        return isDo;
    }

    public void AllMoveDown()
    {
        isMoveEnd = false;

        int[] additionNum = new int[7];

        bool[] isMoveDown = new bool[7];

        Bead[] moveDownBead = new Bead[7];

        for (int i = 0; i < 7; i++)
        {
            for (int j = 6; j >= 0; j--)//위에서 아래로
            {
                if (beadMatrix[i, j] == null)
                {
                    for(int k = j + 1; k < 7; k++)
                    {
                        beadMatrix[i, k - 1] = beadMatrix[i, k];
                        beadMatrix[i, k - 1].index.x = i;
                        beadMatrix[i, k - 1].index.y = k-1;
                    }

                    

                    additionNum[i]++;

                    MakeBead(i, 6, new Vector2(-1.5f + 0.5f * i, 1.5f + 0.5f * additionNum[i]));

                    moveDownBead[i] = beadMatrix[i, j];
                }
            }
        }

        for (int i = 0; i < 7; i++)
        {
            if(moveDownBead[i] != null)
            {
                //moveDownBead[i].DoMoveDown();
                coroutines[i] = StartCoroutine(CoMoDo(moveDownBead[i]));
                
                
            }
        }

        StartCoroutine(stfu());

        //Invoke("stfu", 1.1f);

        /*
        for(int i = 0; i < 7; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                if(beadMatrix[i,j] == null)
                {
                    int tmpj = j;
                    for(int k=j+1; k<7; k++)
                    {

                        if(beadMatrix[i,k] != null || k == 6)
                        {
                            //아래로 내리고 위에 생성

                            for(int l = k; l < 7; l++)
                            {
                                Debug.Log(tmpj+" " + l);
                                beadMatrix[i, tmpj] = beadMatrix[i, l];
                                tmpj++;
                            }

                            for(int l = j; l < 7; l++)
                            MakeBead(i, l, new Vector2((float)(-1.5 + 0.5f * i), -1.5f + 0.5f * l));

                        }
                    }
                    break;

                }
            }
        }
        */
    }

    IEnumerator stfu()
    {
        bool isAllEnd = false;
        while (true)
        {
            int tmp = 0;
            for(int i = 0; i < coroutines.Length; i++)
            {
                //Debug.Log(coroutines[i]);
                if(coroutines[i] == null)
                {
                    tmp++;
                    
                }

            }

            if (tmp == coroutines.Length)
            {
                isAllEnd = true;
            }

            if (isAllEnd)
            {
                isMoveEnd = true;

                SubCheckWhole();

                break;
            }
            yield return null;
        }
    }

    IEnumerator CoMoDo(Bead bead)
    {
        Vector3 targetPos;

        targetPos = new Vector2(-1.5f + 0.5f * bead.index.x, -1.5f + 0.5f * bead.index.y);

        float time = 0;

        while (true)
        {
            for (int j = bead.index.y; j < 7; j++)
            {
                Bead doBead = beadMatrix[bead.index.x, j];
                //doBead.transform.position = Vector3.Lerp(doBead.transform.position, targetPos + Vector3.up * 0.5f * (j - bead.index.y), Time.deltaTime * 5);
                doBead.MovePosition(Vector3.Lerp(doBead.parentTransform.position, targetPos + Vector3.up * 0.5f * (j - bead.index.y), Time.deltaTime * 7));
            }
            //Debug.Log("a");
            time += Time.deltaTime;

            if (time > 0.6f)
            {
                for (int i = bead.index.y; i < 7; i++)
                {
                    Bead doBead = beadMatrix[bead.index.x, i];
                    //beadMatrix[indexx, i].transform.position = destinationPos + Vector3.up * 0.5f * (i - indexy + tmp - 1);
                    //doBead.transform.position = targetPos + Vector3.up * 0.5f * (i - bead.index.y);
                    doBead.MovePosition(targetPos + Vector3.up * 0.5f * (i - bead.index.y));
                }
                coroutines[bead.index.x] = null;
                //Debug.Log("끝");
                break;
            }

            yield return null;
        }

        //isMoveEnd = true;
    }



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


    public void SubCheckWhole()
    {
        bool isDo = false;

        for (int i = 0; i < 7; i++)
        {
            isDo = SubCheckVerHorFunc(beadMatrix[i, i], null);
            if (isDo)
            {
                break;
            }
        }

        
    }

}

