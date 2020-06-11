using System.Collections.Generic;
using UnityEngine;
using Coords;
using UnityEditor;
using DG.Tweening;

[RequireComponent(typeof(Poctoscript))]
public class Stromoscript : MonoBehaviour {
    private bool _moznoSekat = false;
    public AudioClip chop1;
    public AudioClip chop2;
    public AudioClip chop3;
    private List<AudioClip> _sounds;
    private GameObject _hrac;
    public PolarCoord PolarStromu;
    private Poctoscript _poctoscript;
    private bool _active = true;
    private GameObject _stump;

    // Use this for initialization
    void Start() {
        _sounds = new List<AudioClip> {chop1, chop2, chop3};
        _stump = GetComponentInChildren<Stump>().gameObject;
        _poctoscript = GetComponent<Poctoscript>();
        PolarStromu = new CartesianCoord(transform.position.x, transform.position.y).ToPolar();
    }

    private void Update() {
        transform.position = PolarStromu.ToCartesian().ToVector3();
        if (_poctoscript.Kapacita == 0 && _active)
        {
            _stump.SetActive(false);
            _active = false;
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerEnter2D(Collider2D collision) {
        _hrac = collision.gameObject;
        _hrac.GetComponent<PlayerController>().NastavCil(this.gameObject);
        _moznoSekat = true;
    }

    public void Seknuto() {
        if (_moznoSekat) {
            ZrusNapovedu();

            _stump.transform.DORewind(false);
            _stump.transform.DOShakeScale(0.3f, 0.4f);

            _poctoscript.Kapacita--;
            var rnd = new System.Random();
            int index = rnd.Next(_sounds.Count - 1);
            var sound = _sounds[index];
            
            GetComponentInChildren<ParticleSystem>().Play();
            GameState.Instance.AudioManager.ZahrajZvuk(sound);
        }
    }

    private void ZrusNapovedu() {
        var napoveda = GetComponentInChildren<Napovedascript>();
        if (napoveda != null)
        {
            Destroy(napoveda.gameObject);
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerExit2D(Collider2D collision) {
        if (_hrac) {
            _hrac.GetComponent<PlayerController>().ZrusCil();
            _hrac = null;
            _moznoSekat = false;
        }
    }

    public void OnDrawGizmos() {
        if (_poctoscript) {
            var style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 20;
            Handles.Label(transform.position, string.Format("{0}", _poctoscript.Kapacita), style);
        }

        if (_moznoSekat) {
            Handles.color = Color.green;
        } else {
            Handles.color = Color.red;
        }
        Handles.DrawSolidDisc(transform.position, Vector3.back, 0.03f);
    }
}