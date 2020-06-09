using Assets.Scripty;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameState : MonoBehaviour {
    // Core
    private static GameState _instance = null;

    public AudioManager AudioManager;


    // Casovac
    public float UbehlyCas = 0f;

    private int _den = 0;
    private float _delkaDne = 30f;
    private float _procentoDne = 0f;

    public static GameState Instance {
        get { return _instance; }
    }


    void Start() {
        AudioManager = new AudioManager(GetComponents<AudioSource>());
        AudioManager.PustHudbu();
    }

    void Awake() {
        // Hned znic jine instance GameState - tento musi byt unikantni (Singleton)
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            _instance = this;
        }

        // Tento GameState se nikdy neznici
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update() {
        // Casovac
        UbehlyCas += Time.deltaTime;
        _procentoDne = (float) Math.Round(UbehlyCas / _delkaDne, 2);
    }

    public void NastavKonecDne() {
        UbehlyCas = 0;
        _den++;
        Debug.Log("Den: " + _den);
    }

    public float ProcentoDne() {
        return _procentoDne;
    }

    public int Den() {
        return _den;
    }

    public void NastavDen(int den) {
        _den = den;
    }
}