using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public enum ColorState
    {
        White,
        Black,
    }
    [SerializeField] ColorState _colorState;
    [SerializeField] LayerMask Black;
    [SerializeField] LayerMask White;
    [SerializeField] Color BlackColor = Color.black;
    [SerializeField] Color WhiteColor = Color.white;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ChangeColor();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeColor();
    }
    void ChangeColor()
    {
        switch (_colorState)
        {
            case ColorState.White:
                Debug.Log("WhiteÅ®Black");
                _colorState = ColorState.Black;
                gameObject.layer = 10;
                gameObject.GetComponent<SpriteRenderer>().color = BlackColor;
                break;
            case ColorState.Black:
                Debug.Log("BlackÅ®White");
                _colorState = ColorState.White;
                gameObject.layer = 11;
                gameObject.GetComponent<SpriteRenderer>().color = WhiteColor;
                break;
        }
    }
}
