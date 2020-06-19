using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private bool _shaking = false;
    private GameObject _player;
    private Tweener _tween;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_shaking)
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
        }
    }

    public void Shake()
    {
        _shaking = true;
        transform.DOShakePosition(1, strength: .01f).OnComplete(() => _shaking = false);
    }
}
