using System.Collections;
using System.Collections.Generic;
using Assets.Scripty;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Planetascript : MonoBehaviour
{

    private Destovac _destovac;

    void Start()
    {
        // Prseni
        _destovac = Camera.main.GetComponent<Destovac>();
        _destovac.ZacniMoznaPrset();


        if (GameState.Instance.Level == 0)
        {
            GameState.Instance.GetComponent<Generator>().generateTrees(3);
        }
        else if (GameState.Instance.Level == 1)
        {
            GameState.Instance.GetComponent<Generator>().generateTrees(5);
        }
        else if (GameState.Instance.Level == 2)
        {

            GameState.Instance.GetComponent<Generator>().generateTrees(7);
        }

    }
}