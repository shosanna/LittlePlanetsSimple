using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introScript : MonoBehaviour
{
    public AudioClip Sound;

    public void StartButton()
    {
        print("fpoop");
        GameState.Instance.AudioManager.ZahrajZvuk(Sound);
    }
}
