using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    private bool _sound = true;
    private int _theme = 1;
    private GameObject _soundButton;
    private GameObject _noSoundButton;
    private GameObject _blueColorButton;
    private GameObject _orangeColorButton;

    public AudioClip Sound;

    void Start()
    {
        _soundButton = GameObject.Find("soundButton");
        _noSoundButton = GameObject.Find("noSoundButton");
        _blueColorButton = GameObject.Find("colorBlueButton");
        _orangeColorButton = GameObject.Find("colorOrangeButton");

        if (SceneManager.GetActiveScene().name == "settings")
        {
            GameObject.Find("okButton").GetComponent<Button>().onClick.AddListener(GameState.Instance.ToMainMenu);

            _soundButton.SetActive(false);
            _blueColorButton.SetActive(false);
            _noSoundButton.SetActive(_sound);
            _soundButton.SetActive(!_sound);
        }
        _sound = GameState.Instance.AudioManager.IsPlaying;
    }

    public void StartButton()
    {
        StartCoroutine(DelayedLoad());
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("settings");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ToggleSound()
    {
        _sound = !_sound;
        _noSoundButton.SetActive(_sound);
        _soundButton.SetActive(!_sound);

        if (!_sound)
        {
            GameState.Instance.AudioManager.VypniVse();
        }
        else
        {
            GameState.Instance.AudioManager.ZapniVse();
        }
    }

    public void ToggleSchema()
    {
        _theme = _theme == 1 ? 2 : 1;
        _orangeColorButton.SetActive(_theme == 1);
        _blueColorButton.SetActive(_theme != 1);
        GameState.Instance.SetTheme(_theme);
    }

    IEnumerator DelayedLoad()
    {
        Time.timeScale = 1;
        GameState.Instance.AudioManager.ZahrajZvuk(Sound);
        yield return new WaitForSeconds(Sound.length);
        SceneManager.LoadScene("planet1");
    }
}
