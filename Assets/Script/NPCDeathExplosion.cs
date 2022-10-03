using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NPCDeathExplosion : MonoBehaviour
{
    [SerializeField] float _endScale = 3f;
    [SerializeField] float _time = 0.5f;
    void Start()
    {
        gameObject.transform.DOScale(_endScale, _time);
        Destroy(gameObject, _time);
    }
}
