using Coords;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class PlayerController : MonoBehaviour {
    public Transform cameraTransform;
    public float Radius;
    public float yVelocity = 0;
    public float speed = 5;
    public PolarCoord PolarCoord;
    private GameObject _cilAkce;

    private bool _isGrounded = true;

    private void Start() {
        PolarCoord = new PolarCoord(Radius, 1.50f);
    }

    void Update() {
        // pro dvoj hopik
        _isGrounded = PolarCoord.R > Radius;

        // sledovani hrace
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);

        // pohyb
        var movement = Input.GetAxisRaw("Horizontal");
        yVelocity -= 5 * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded) {
            yVelocity = 1;
        }

        // SEKANI
        if (Input.GetKeyDown(KeyCode.X) && _cilAkce != null && _cilAkce.GetComponent<Stromoscript>() != null) {
            if (_cilAkce.GetComponent<Poctoscript>().Kapacita > 0) {
                Camera.main.transform.DOShakePosition(1, strength: .01f);
                _cilAkce.GetComponent<Stromoscript>().Seknuto();
            }
        }

        // beh
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = 3;
        } else {
            speed = 10;
        }

        PolarCoord.R += yVelocity * Time.deltaTime;
        if (PolarCoord.R < Radius) {
            yVelocity = 0;
            PolarCoord.R = Radius;
        }

        // 0.54 je radius originalni planety, toto upravi rychlost pro jinak velke planety
        PolarCoord.Phi += -movement * (Mathf.PI / speed) * Time.deltaTime * 0.54f / Radius;
        // oprava polarnich souradnic pro chuzi po jizni polokouli
        if (PolarCoord.Phi < 0) {
            PolarCoord.Phi = PolarCoord.Phi + (2 * Mathf.PI);
        }
        PolarCoord.Phi = PolarCoord.Phi % (2 * Mathf.PI);

        Vector2 x = PolarCoord.ToCartesian().ToVector2();
        transform.localPosition = new Vector3(x.x, x.y, transform.localPosition.z);

        // nataceni spritu
        transform.rotation = Quaternion.EulerRotation(0, 0, PolarCoord.Phi - Mathf.PI / 2);
    }

    public void NastavCil(GameObject cil) {
        _cilAkce = cil;
    }

    public void ZrusCil() {
        _cilAkce = null;
    }
}