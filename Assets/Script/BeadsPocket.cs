using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeadsPocket : MonoBehaviour
{
    //public List<BeadBlue> beadBlues;
    public GameObject Blue;

    public List<BeadBlue> beadBlues;
    public List<BeadGreen> beadGreens;
    public List<BeadPurple> beadPurples;
    public List<BeadRed> beadReds;
    public List<BeadOrange> beadOranges;
    public List<BeadYellow> beadYellows;

    private void Awake()
    {
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
                        if (k.Equals(tmpList.Count - 1))
                        {
                            Bead bead = Instantiate(tmpList[0],this.transform);

                            tmpList.Add(bead);
                            k++;
                        }
                        tmpList[k].transform.position = new Vector3(-1.5f + 0.5f * i, -1.5f + 0.5f * j, 0);
                        tmpList[k].gameObject.SetActive(true);
                        break;
                    }
                }
                
            }
        }
    }
    /*
    public void MoveDown(Bead bead)
    {
        Debug.Log("角青");
        RaycastHit2D[] hitDown = Physics2D.RaycastAll(bead.transform.position, Vector2.down, 10f);
        RaycastHit2D[] hitUp = Physics2D.RaycastAll(bead.transform.position, Vector2.up, 10f);
        Debug.Log(hitDown[1].collider.transform.position);
        if (hitDown.Length == 1)
        {
            float tmp = -1.5f;

            for (int i = 0; i < hitUp.Length; i++)
            {
                AnimationCurve curve = AnimationCurve.Linear(0.0F, hitUp[i].collider.transform.position.y, 2.0F, tmp + 0.5f * i);
                //AnimationCurve curve = AnimationCurve.Linear(0.0F, this.transform.position.y, 2.0F, tmp + 0.5f * i);
                AnimationClip clip = new AnimationClip();

                clip.legacy = true;
                clip.SetCurve("", typeof(Transform), "localPosition.y", curve);
                hitUp[i].collider.GetComponent<Bead>().anim.AddClip(clip, "test");
                hitUp[i].collider.GetComponent<Bead>().anim.Play("test");
            }
        }
        else
        {
            float tmp = hitDown[1].collider.transform.position.y + 0.5f;

            for (int i = 0; i < hitUp.Length; i++)
            {
                AnimationCurve curve = AnimationCurve.Linear(0.0F, hitUp[i].collider.transform.position.y, 2.0F, tmp + 0.5f * i);
                //AnimationCurve curve = AnimationCurve.Linear(0.0F, this.transform.position.y, 2.0F, tmp + 0.5f * i);
                AnimationClip clip = new AnimationClip();

                clip.legacy = true;
                clip.SetCurve("", typeof(Transform), "localPosition.y", curve);
                hitUp[i].collider.GetComponent<Bead>().anim.AddClip(clip, "test");
                hitUp[i].collider.GetComponent<Bead>().anim.Play("test");
            }
        }




    }
    */

    public IEnumerator MoveDown_2(Bead bead)
    {
        Vector2 pos = bead.transform.position;

        RaycastHit2D[] hitUp = Physics2D.RaycastAll(pos, Vector2.up, 10f);
        
        RaycastHit2D[] hitDown = Physics2D.RaycastAll(pos, Vector2.down, 10f);

        float tmp = 0f;

        if(hitDown.Length < 2)
        {
            tmp = 1.5f;
        }
        else
        {
            Debug.Log(hitDown.Length);
            tmp = pos.y - hitDown[1].collider.transform.position.y - 0.5f;
        }
        
        //float tmp = pos.y - hitDown[1].collider.transform.position.y - 0.5f;

        Vector3 vec = bead.transform.position + new Vector3(0, -tmp, 0);

        if (hitUp.Length > 0)//构老锭?
        {
            while (true)
            {


                for (int i = 0; i < hitUp.Length; i++)
                {
                    hitUp[i].collider.transform.position = Vector3.Lerp(hitUp[i].collider.transform.position, vec + Vector3.up * 0.5f * i, Time.deltaTime * 3);
                }
                //Debug.Log(hitUp[hitUp.Length - 1].collider.transform.position.y - (vec.y + 0.5f * (hitUp.Length - 1)));

                if (hitUp[hitUp.Length - 1].collider.transform.position.y - (vec.y + 0.5f * (hitUp.Length - 1)) < 0.0001f)
                {
                    for (int i = 0; i < hitUp.Length; i++)
                    {
                        hitUp[i].collider.transform.position = vec + Vector3.up * 0.5f * i;
                    }
                    break;
                }

                yield return null;
            }
            Debug.Log("场");
        }

    }

}
