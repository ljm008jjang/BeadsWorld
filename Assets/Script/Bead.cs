using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bead : MonoBehaviour
{
    Vector2 Spos;
    Vector2 Epos;
    bool isClicked = false;
    Animator animator;
    public Animation anim;
    BeadsPocket beadsPocket;

    private void Awake()
    {
        anim = GetComponent<Animation>();

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
        
        if(isClicked == true)
        {
            Spos = transform.position;
            //Debug.Log("Spos : "+Spos);
            Epos = Epos - Spos;
            float angle = Vector2.SignedAngle(Vector2.up, Epos);

            RaycastHit2D[] hit;
            //Debug.Log(angle);
            if (angle >= -45f && angle <= 45f)//위
            {
                
                hit = Physics2D.RaycastAll(Spos, Vector2.up, 0.5f);
            }
            else if (angle <= 135f && angle > 45f)//왼
            {
                hit = Physics2D.RaycastAll(Spos, Vector2.left, 0.5f);
            }
            else if (angle < -45f && angle >= -135f)//오
            {
                hit = Physics2D.RaycastAll(Spos, Vector2.right, 0.5f);
            }
            else//아래
            {
                hit = Physics2D.RaycastAll(Spos, Vector2.down, 0.5f);
            }

            if (hit.Length > 1)
            {
                
               
                if (hit[1].collider.gameObject != null && hit[1].collider.GetComponent(GetType()) == null)
                {
                    /*
                    RaycastHit2D[] checkUp = Physics2D.RaycastAll(hit[1].transform.position, Vector2.up,3f);
                    RaycastHit2D[] checkDown = Physics2D.RaycastAll(hit[1].transform.position, Vector2.down, 3f);
                    RaycastHit2D[] checkLeft = Physics2D.RaycastAll(hit[1].transform.position, Vector2.left, 3f);
                    RaycastHit2D[] checkRight = Physics2D.RaycastAll(hit[1].transform.position, Vector2.right, 3f);
                    */
                    Vector2 tmp = transform.position;
                    transform.position = hit[1].collider.transform.position;
                    hit[1].collider.transform.position = tmp;


                    StartCoroutine(CheckVerHorFunc(hit));
                    
                }
            }
          

            isClicked = false;
        }
        //StartCoroutine(MoveDown_2());
        //MoveDown();
    }

    IEnumerator CheckVerHorFunc(RaycastHit2D[] hit)
    {
        yield return new WaitForSeconds(0.1f);
        
        RaycastHit2D[] checkHor = Physics2D.RaycastAll(transform.position + Vector3.left * 3f, Vector2.right, 6f);
        RaycastHit2D[] checkVer = Physics2D.RaycastAll(transform.position + Vector3.up * 3f, Vector2.down, 6f);

        bool isDo = false;


        for (int i = 0; i < checkHor.Length - 2; i++)
        {
            int ctmp = 0;
            for (int j = i + 1; j < checkHor.Length; j++)
            {
                if (checkHor[i].collider.GetComponent<Bead>().GetType() == checkHor[j].collider.GetComponent<Bead>().GetType())
                //checkVer[i].collider.GetComponent(GetType()).Equals(checkVer[j].collider.GetComponent(GetType())))
                {

                    ctmp++;
                    if (j == checkHor.Length - 1 && ctmp > 1)//j가 마지막이면 다음 인덱스 존재x이므로
                    {
                        RaycastHit2D[] checkHor_2 = Physics2D.RaycastAll(transform.position + Vector3.up * 0.5f + Vector3.left * 3f, Vector2.right, 6f);
                        for (int k = i; k <= j; k++)//k==j???
                        {
                            checkHor[k].collider.gameObject.SetActive(false);
                            beadsPocket.StartCoroutine(beadsPocket.MoveDown_2(checkHor_2[k].collider.GetComponent<Bead>()));
                        }

                        //RaycastHit2D[] checkHor_2 = Physics2D.RaycastAll(transform.position + Vector3.up * 0.5f + Vector3.left * 3f, Vector2.right, 6f);
                        

                        isDo = true;
                        break;
                    }
                }
                else if (ctmp > 1)//다른데 ctmp가 2 이상이면
                {
                    RaycastHit2D[] checkHor_2 = Physics2D.RaycastAll(transform.position + Vector3.up * 0.5f + Vector3.left * 3f, Vector2.right, 6f);
                    for (int k = i; k < j; k++)
                    {

                        checkHor[k].collider.gameObject.SetActive(false);
                        beadsPocket.StartCoroutine(beadsPocket.MoveDown_2(checkHor_2[k].collider.GetComponent<Bead>()));
                    }

                    

                    isDo = true;
                    break;
                }
                else
                {
                    i += ctmp;
                    break;
                }
            }

        }
        
        if(isDo == false)
        {
             
            for (int i = 0; i < checkVer.Length - 2; i++)
            {
                int ctmp = 0;
                for (int j = i + 1; j < checkVer.Length; j++)
                {
                    
                    if (checkVer[i].collider.GetComponent<Bead>().GetType() == checkVer[j].collider.GetComponent<Bead>().GetType())
                    //checkVer[i].collider.GetComponent(GetType()).Equals(checkVer[j].collider.GetComponent(GetType())))
                    {

                        ctmp++;
                        if (j == checkVer.Length - 1 && ctmp > 1)//j가 마지막이면 다음 인덱스 존재x이므로
                        {

                            for (int k = i; k <= j; k++)//k==j???
                            {

                                checkVer[k].collider.gameObject.SetActive(false);
                            }
                            //yield return new WaitForSeconds(0.1f);
                            if (i != 0)
                            {
                                Debug.Log(checkVer[i - 1].collider.GetComponent<Bead>());
                                beadsPocket.StartCoroutine(beadsPocket.MoveDown_2(checkVer[i - 1].collider.GetComponent<Bead>()));
                                //처음부터 사라짐
                            }
                            
                                //checkVer[i - 1].collider.GetComponent<Bead>().MoveDown();

                            isDo = true;
                            break;
                        }
                    }
                    else if (ctmp > 1)//다른데 ctmp가 2 이상이면
                    {

                        for (int k = i; k < j; k++)
                        {

                            checkVer[k].collider.gameObject.SetActive(false);
                        }
                        //yield return new WaitForSeconds(0.1f);

                        if (i != 0)
                        {
                            Debug.Log(checkVer[i - 1].collider.GetComponent<Bead>());
                            beadsPocket.StartCoroutine(beadsPocket.MoveDown_2(checkVer[i - 1].collider.GetComponent<Bead>()));
                            //StartCoroutine(beadsPocket.MoveDown_2(checkVer[i - 1].collider.GetComponent<Bead>()));
                        }//처음부터 사라짐

                        //StartCoroutine(MoveDown_2());
                        //beadsPocket.MoveDown(checkVer[i - 1].collider.GetComponent<Bead>());
                        //checkVer[i-1].collider.GetComponent<Bead>().MoveDown();


                        isDo = true;
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
        
        if(isDo == true)
        {
            
            //블록 아래로!
        }
        else
        {
            Vector2 tmp = transform.position;
            transform.position = hit[1].collider.transform.position;
            hit[1].collider.transform.position = tmp;

            //블럭 원상복귀
        }
    }
    /*
    void MoveDown()//안사용하는게 좋을듯?
    {
        RaycastHit2D[] hitDown = Physics2D.RaycastAll(transform.position, Vector2.down, 10f);
        RaycastHit2D[] hitUp = Physics2D.RaycastAll(transform.position, Vector2.up, 10f);

        float tmp = hitDown[1].collider.transform.position.y + 0.5f;

        
            AnimationCurve curve = AnimationCurve.Linear(0.0F, transform.position.y, 2.0F, tmp);
            AnimationCurve curve_x = AnimationCurve.Constant(0.0F, 2.0F, transform.position.x);
            //AnimationCurve curve = AnimationCurve.Linear(0.0F, this.transform.position.y, 2.0F, tmp + 0.5f * i);
            AnimationClip clip = new AnimationClip();
            
            clip.legacy = true;
            clip.SetCurve("", typeof(Transform), "localPosition", curve);
            //clip.SetCurve("", typeof(Transform), "localPosition.X", curve_x);
            anim.AddClip(clip, "test");
            anim.Play("test");



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
    
    IEnumerator MoveDown_2()
    {
        Vector2 pos = transform.position;

        RaycastHit2D[] hitUp = Physics2D.RaycastAll(pos, Vector2.up, 10f);
        /*
        if(hitUp.Length > 1)
        {
            StartCoroutine(hitUp[1].collider.GetComponent<Bead>().MoveDown_2());
        }
        */
        RaycastHit2D[] hitDown = Physics2D.RaycastAll(pos, Vector2.down, 10f);
        
        float tmp = pos.y - hitDown[1].collider.transform.position.y - 0.5f;

        Vector3 vec = transform.position + new Vector3(0, -tmp, 0);

        if (hitDown.Length > 1)
        {
            while (true)
            {
                /*
                transform.position = Vector3.Lerp(transform.position, vec, Time.deltaTime * 3);

                if (transform.position.y - vec.y < 0.0001f)
                {
                    transform.position = vec;

                    break;
                }
                */

                

                for(int i = 0; i < hitUp.Length; i++)
                {
                    hitUp[i].collider.transform.position = Vector3.Lerp(hitUp[i].collider.transform.position, vec + Vector3.up * 0.5f * i, Time.deltaTime * 3);
                }
                Debug.Log(hitUp[hitUp.Length - 1].collider.transform.position.y - (vec.y + 0.5f * (hitUp.Length - 1)));
                
                if(hitUp[hitUp.Length-1].collider.transform.position.y-(vec.y + 0.5f * (hitUp.Length-1)) < 0.0001f)
                {
                    for (int i = 0; i < hitUp.Length; i++)
                    {
                        hitUp[i].collider.transform.position = vec + Vector3.up * 0.5f * i;
                    }
                    break;
                }
                
                yield return null;
            }
        }

        Debug.Log("끝");
    }
    
}
