﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripty;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Planetascript : MonoBehaviour {

    // UI Casovac

    private Destovac _destovac;

    void Start () {
        // Prseni
        _destovac = Camera.main.GetComponent<Destovac>();
        _destovac.ZacniMoznaPrset();
    }
}