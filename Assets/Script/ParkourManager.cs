using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourManager : MonoBehaviour
{

    [SerializeField] GameObject[] platforms;

    private bool firstOpen= false;
    private bool secondOpen = false;
    private bool thirdOpen = false;

    private static ParkourManager _instance;

    public static ParkourManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ParkourManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "ParkourManager";
                    _instance = go.AddComponent<ParkourManager>();
                    //DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SetPlatforms(int button)
    {
        if (button == 0)
        {
            if (firstOpen)
            {
                platforms[0].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);
                platforms[1].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
                platforms[2].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
            }
            else
            {
                platforms[0].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
                platforms[1].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);
                platforms[2].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);
            }
            firstOpen = !firstOpen;
        }
        else if (button==1)
        {
            if (secondOpen)
            {
                platforms[1].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);
                platforms[0].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
                platforms[2].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
            }
            else
            {
                platforms[1].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
                platforms[0].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);
                platforms[2].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);

            }
            secondOpen = !secondOpen;
        }
        else
        {
            if (thirdOpen)
            {
                platforms[2].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);
                platforms[0].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
                platforms[1].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
            }
            else
            {
                platforms[2].transform.DOMoveY(0f, 1).SetEase(Ease.OutQuad);
                platforms[0].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);
                platforms[1].transform.DOMoveY(-3f, 1).SetEase(Ease.OutQuad);

            }
            thirdOpen= !thirdOpen;
        }
    }
}
