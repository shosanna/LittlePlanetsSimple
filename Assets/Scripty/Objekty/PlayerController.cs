using Coords;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

        origCamera = Camera.main.transform;
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

        if (Input.GetKeyDown(KeyCode.X) && _cilAkce != null && _cilAkce.GetComponent<Stromoscript>() != null) {
            if (_cilAkce.GetComponent<Poctoscript>().Kapacita > 0) {
                origCamera = Camera.main.transform;
                StartCoroutine(Neshake());
                shake = true;
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

        // shake obrazovky pri sekani stromu
        if (shake) {
            float shakeOffset = Random.RandomRange(0, 0.015f);
            Camera.main.transform.position = origCamera.position + new Vector3(shakeOffset, shakeOffset, shakeOffset);
        } else {
            Camera.main.transform.position = origCamera.position;
        }
    }


    IEnumerator Neshake() {
        yield return new WaitForSeconds(0.07f);
        shake = false;
    }

    Transform origCamera;
    bool shake = false;

    public void NastavCil(GameObject cil) {
        _cilAkce = cil;
    }

    public void ZrusCil() {
        _cilAkce = null;
    }
}