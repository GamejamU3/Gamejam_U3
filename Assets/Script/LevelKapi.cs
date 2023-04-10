using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKapi : MonoBehaviour
{

	public int correctCount=0;

    private void Update()
    {
        if (correctCount!=3)
        {
            this.gameObject.GetComponent<DoorMng>().isLocked = true;
        }
        else
        {
            this.gameObject.GetComponent<DoorMng>().isLocked = false;
        }
    }
}
