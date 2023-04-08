using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class ParkourButton : MonoBehaviour
{

    [SerializeField] GameObject platform;

    //[SerializeField] Vector3 upperPosition;
    //[SerializeField] Vector3 lowerPosition;
    

    [SerializeField] bool open = false;

    public void ChangePosition()
    {
        if (open)
        {
            platform.transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);
            open = !open;
        }
        else
        {
            platform.transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
            open = !open;
        }
    }
}
