using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{
    public GameObject[] buttons;
    public Vector3[] buttonsStartPos;
    public float duration;

    public AudioClip buttonSwoosh;
    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonsStartPos[i] = buttons[i].transform.localPosition;
            buttons[i].transform.localPosition = new Vector3(buttons[i].transform.localPosition.x-2000, buttons[i].transform.localPosition.y, buttons[i].transform.localPosition.z);
        }
        StartCoroutine(go());
    }
    IEnumerator go()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            yield return new WaitForSeconds(duration/2);
            buttons[i].transform.DOLocalMoveX(buttonsStartPos[i].x, duration);
            yield return new WaitForSeconds(duration-buttonSwoosh.length);
            AudioSource.PlayClipAtPoint(buttonSwoosh, Vector3.zero);
        }
    }

   
}
