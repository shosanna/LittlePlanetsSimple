using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introScript : MonoBehaviour
{
    public AudioClip Sound;

    public void StartButton()
    {
        StartCoroutine(DelayedLoad());
    }


    IEnumerator DelayedLoad()
    {
        GameState.Instance.AudioManager.ZahrajZvuk(Sound);
        yield return new WaitForSeconds(Sound.length);
        SceneManager.LoadScene("planet1");

    }
}
