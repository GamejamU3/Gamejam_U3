using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonMng : MonoBehaviour
{
    public bool a;
    public bool b;
    public bool c;
    public bool d;
    public Vector3 startPos;
    public float gitgelsure;

    private GameObject soruMng;
    private void Awake()
    {
        soruMng = GameObject.Find("SoruMng");
        startPos = transform.localPosition;
    }

    public void isaretle()
    {
        Debug.Log("Deydi");
        if(soruMng.GetComponent<soruMng>().soruYazildiMi==true && soruMng.GetComponent<soruMng>().canSelect == true)
        {
            if (a == true)
            {
                soruMng.GetComponent<soruMng>().selectA();
            }
            if (b == true)
            {
                soruMng.GetComponent<soruMng>().selectB();
            }
            if (c == true)
            {
                soruMng.GetComponent<soruMng>().selectC();
            }
            if (d == true)
            {
                soruMng.GetComponent<soruMng>().selectD();
            }

            transform.DOLocalMoveX(startPos.x+0.4f, gitgelsure/2).OnComplete(() => transform.DOLocalMoveX(startPos.x, gitgelsure/2));

        }
       
    }
}
