using Assets.Scripty;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameState : MonoBehaviour
{
    // Core
    private static GameState _instance = null;
    public AudioManager AudioManager;

    // Casovac
    public float UbehlyCas = 0f;
    public int Level = 0;
    public int Theme = 1;

    private int _den = 0;
    private float _delkaDne = 30f;
    private float _procentoDne = 0f;



    public static GameState Instance
    {
        get { return _instance; }
    }


    void Start()
    {
        AudioManager = new AudioManager(GetComponents<AudioSource>());
        AudioManager.PustHudbu();
    }

    void Awake()
    {
        // Hned znic jine instance GameState - tento musi byt unikantni (Singleton)
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        // Tento GameState se nikdy neznici
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        UbehlyCas += Time.deltaTime;
        _procentoDne = (float)Math.Round(UbehlyCas / _delkaDne, 2);

        if (Input.GetKeyDown("r"))
        {
            Restart();
        }
    }

    public void SetTheme(int theme)
    {
        Theme = theme;
    }

    public void NastavKonecDne()
    {
        UbehlyCas = 0;
        _den++;
        Debug.Log("Den: " + _den);
    }

    public void GameOver()
    {
        ResetCasu();
        GameObject.Find("GUIManager").GetComponent<GuiManager>().DisplayGameOver();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        ResetCasu();
        SceneManager.LoadScene("planet1");
    }

    public void Continue()
    {
        Time.timeScale = 1;
        ResetCasu();
        Level++;
        SceneManager.LoadScene("planet1");
    }

    public void ToMainMenu()
    {
        ResetCasu();
        SceneManager.LoadScene("intro");
    }

    public float ProcentoDne()
    {
        return _procentoDne;
    }

    public int Den()
    {
        return _den;
    }

    public void NastavDen(int den)
    {
        _den = den;
    }

    public void ResetCasu()
    {
        _den = 0;
        UbehlyCas = 0;
    }
}