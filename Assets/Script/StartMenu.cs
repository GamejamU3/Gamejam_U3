using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    public GameObject[] buttons;
    public Vector3[] buttonsStartPos;
    public float duration;

    public AudioClip buttonSwoosh;
    public AudioClip buttonClick;

    #region Butonlarin Ekrana Kayarak Yerlesmesi
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
            yield return new WaitForSeconds(duration-buttonSwoosh.length); //Butonun tam ekrana yerleþmeden önceki anda sesi oynatmasýný saðlýyor
            AudioSource.PlayClipAtPoint(buttonSwoosh, Vector3.zero);
        }
    }

    #endregion

    #region Bölüm Yükleme
    public void loadScene(string levelName)
    {
        StartCoroutine(openScene(levelName));
    }

    IEnumerator openScene(string levelName)
    {
        AudioSource.PlayClipAtPoint(buttonClick, Vector3.zero);
        yield return new WaitForSeconds(buttonClick.length);
        SceneManager.LoadScene(levelName);

    }
    #endregion

    #region Oyunu Kapatma
    public void closeGame()
    {
        StartCoroutine(closeGame2());
    }

    IEnumerator closeGame2()
    {
        AudioSource.PlayClipAtPoint(buttonClick, Vector3.zero);
        yield return new WaitForSeconds(buttonClick.length);
        Application.Quit();

    }
    #endregion
}
