using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldGun : MonoBehaviour
{
    [SerializeField] GameObject Gun;


    public void SetHold(bool value){
        Gun.GetComponent<Gun>().gunPlace = value;
    }

}
