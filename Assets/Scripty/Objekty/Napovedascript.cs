using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Napovedascript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.DOScale(2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
