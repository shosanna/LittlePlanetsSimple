﻿using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    private Text _axeCount;
    public GameObject GameOverUI;
    public GameObject WinUI;
    private int _totalTrees;
    private int _currentTrees;
    private bool _running = true;
    // Start is called before the first frame update
    void Start()
    {
        _totalTrees = GameObject.FindGameObjectsWithTag("Stump").Length;
        _axeCount = GameObject.Find("axe count").GetComponent<Text>();
        WinUI.GetComponent<Canvas>().enabled = false;
        GameOverUI.GetComponent<Canvas>().enabled = false;

        // Setup all the callbacks on all buttons
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(GameState.Instance.Restart);
        GameObject.Find("Abandon").GetComponent<Button>().onClick.AddListener(GameState.Instance.ToMainMenu);
        GameObject.Find("Continue").GetComponent<Button>().onClick.AddListener(GameState.Instance.Continue);
    }


    // Update is called once per frame
    void Update()
    {
        if (_running)
        {
            _currentTrees = GameObject.FindGameObjectsWithTag("Stump").Length;

            if (_currentTrees == 0)
            {
                DisplayWin();
            }


            if (_axeCount)
            {
                _axeCount.text = $"{_currentTrees}x";
            }
        }
    }

    public void DisplayGameOver()
    {
        _running = false;
        GameOverUI.GetComponent<Canvas>().enabled = true;
    }

    public void DisplayWin()
    {
        Time.timeScale = 0;
        _running = false;
        WinUI.GetComponent<Canvas>().enabled = true;
    }
}