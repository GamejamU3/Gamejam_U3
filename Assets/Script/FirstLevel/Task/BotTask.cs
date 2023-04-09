using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTask : MonoBehaviour
{
    public GenelManager mng;
    public AudioClip ses;
    
    private void Awake()
    {
        mng = GameObject.Find("Manager").GetComponent<GenelManager>();
    }
    public void die()
    {
        mng.dieBotCount++;
        if(mng.dieBotCount >= 10)
        {
            mng.taskNum = 4;
            AudioSource.PlayClipAtPoint(ses, GameObject.Find("Egitmen").transform.position);
        }
        else
        {
            mng.spawnBot();
        }
        Destroy(this.gameObject);
    }
}
