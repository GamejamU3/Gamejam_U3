using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class ParkourButton : MonoBehaviour
{

    [SerializeField] int index;

    public void ChangePosition()
    {
        ParkourManager.Instance.SetPlatforms(index);
    }
}
