using System.Collections;
using System.Collections.Generic;
using Coords;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public PolarCoord PolarCoord;
    private Animator _napovedaAnimator;
    private bool _alive = true;

    public float Radius;
    void Start()
    {
        // Radius of the planet
        PolarCoord.R = Radius;
    }

    void Update()
    {
        if (_alive)
        {
            //PolarCoord.Phi += speed;
        }

        transform.localPosition = PolarCoord.ToCartesian().ToVector3();
        // nataceni spritu
        transform.rotation = Quaternion.EulerRotation(0, 0, PolarCoord.Phi - Mathf.PI / 2);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Planet").GetComponent<Planetascript>().SetupTrees(1, 5);
        }
    }
}
