using System.Collections;
using System.Collections.Generic;
using Coords;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PolarCoord PolarCoord;
    private Animator _napovedaAnimator;
    private bool _alive = true;
    public AudioClip boom;

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
            PolarCoord.Phi += 0.003f;
        }

        transform.localPosition = PolarCoord.ToCartesian().ToVector3();
        // nataceni spritu
        transform.rotation = Quaternion.EulerRotation(0, 0, PolarCoord.Phi - Mathf.PI / 2);

        if (!_alive && !GetComponentInChildren<ParticleSystem>().isPlaying)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameState.Instance.AudioManager.ZahrajZvuk(boom);
            _alive = false;
            GetComponentInChildren<ParticleSystem>().Play();
            GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.SetActive(false);
            GameState.Instance.GameOver();
        }
    }
}
