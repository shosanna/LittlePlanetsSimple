using Coords;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public float Radius;
    public float yVelocity = 0;
    public float speed = 7;
    public PolarCoord PolarCoord;
    private GameObject _cilAkce;

    private bool _isGrounded = true;

    private void Start()
    {
        PolarCoord = new PolarCoord(Radius, 1.58f);
    }

    void Update()
    {
        // pro dvoj hopik
        _isGrounded = PolarCoord.R > Radius;

        // pohyb
        var movement = Input.GetAxisRaw("Horizontal");
        yVelocity -= speed * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded)
        {
            yVelocity = 1.7f;
        }

        // SEKANI
        if (Input.GetKeyDown(KeyCode.X) && _cilAkce != null && _cilAkce.GetComponent<Stromoscript>() != null)
        {
            if (_cilAkce.GetComponent<Poctoscript>().Kapacita > 0)
            {
                // zakomentovano protoze se pak seka hrac
                //Camera.main.GetComponent<CameraScript>().Shake();
                _cilAkce.GetComponent<Stromoscript>().Seknuto();
            }
        }


        PolarCoord.R += yVelocity * Time.deltaTime;
        if (PolarCoord.R < Radius)
        {
            yVelocity = 0;
            PolarCoord.R = Radius;
        }

        // 0.54 je radius originalni planety, toto upravi rychlost pro jinak velke planety
        PolarCoord.Phi += -movement * (Mathf.PI / speed) * Time.deltaTime * 0.54f / Radius;
        // oprava polarnich souradnic pro chuzi po jizni polokouli
        if (PolarCoord.Phi < 0)
        {
            PolarCoord.Phi = PolarCoord.Phi + (2 * Mathf.PI);
        }
        PolarCoord.Phi = PolarCoord.Phi % (2 * Mathf.PI);

        Vector2 x = PolarCoord.ToCartesian().ToVector2();
        transform.localPosition = new Vector3(x.x, x.y, transform.localPosition.z);

        // nataceni spritu
        transform.rotation = Quaternion.EulerRotation(0, 0, PolarCoord.Phi - Mathf.PI / 2);
    }

    public void NastavCil(GameObject cil)
    {
        _cilAkce = cil;
    }

    public void ZrusCil()
    {
        _cilAkce = null;
    }
}