using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldGun : MonoBehaviour
{
    public int status = 0; 
   
    [SerializeField] GameObject Gun;

    public void HoldingGun(){
        Gun.GetComponent<Gun>().gunPlace = !Gun.GetComponent<Gun>().gunPlace;
    }
}
