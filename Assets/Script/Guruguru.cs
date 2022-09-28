using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guruguru : MonoBehaviour
{
    [SerializeField] float _kaitenSpeed;
    void Update()
    {
        Mawaru();
    }
    void Mawaru()
    {
        Quaternion rot = Quaternion.AngleAxis(_kaitenSpeed, Vector3.back);// 現在の自信の回転の情報を取得する。
        Quaternion q = gameObject.transform.rotation;// 合成して、自身に設定
        gameObject.transform.rotation = q * rot;
    }
}
