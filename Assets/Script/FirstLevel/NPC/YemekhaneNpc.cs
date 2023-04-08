using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YemekhaneNpc : MonoBehaviour
{
    public YemekhaneNpc kod;
    public Animator anim;
    public bool isTalking;

    private void Start()
    {
        anim.SetBool("isTalk", isTalking);
        if (isTalking==true)
        {
            StartCoroutine(go());
        }
    }
    public void degis()
    {
        
        isTalking = !isTalking;
        anim.SetBool("isTalk", isTalking);
        if(isTalking==true)
        {
            StartCoroutine(go());
        }
    }

    IEnumerator go()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        kod.degis();
        degis();
    }
}
