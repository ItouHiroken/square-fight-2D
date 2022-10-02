using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform _teleportPoint;
    [SerializeField] List<GameObject> _bullets = new();
    [SerializeField] List<string> _bulletTagName = new();
    [SerializeField] float _normalBulletPower = 10.0f;
    [SerializeField] float _strongBulletPower = 15.0f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_bulletTagName[2]))//ÉvÉåÉCÉÑÅ[
        {
            collision.gameObject.transform.position = _teleportPoint.position;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_bulletTagName[0]))//ïÅí 
        {
            GameObject a = Instantiate(_bullets[0], _teleportPoint.transform.position, _bullets[0].transform.rotation);
            Vector3 forceDirection = gameObject.transform.position.normalized - _teleportPoint.transform.position.normalized;
            Vector3 force = _normalBulletPower * forceDirection;
            a.layer = collision.gameObject.layer;
            a.gameObject.GetComponent<SpriteRenderer>().color = collision.GetComponent<SpriteRenderer>().color;
            a.GetComponent<TrailRenderer>().startColor = collision.GetComponent<TrailRenderer>().startColor;
            a.GetComponent<TrailRenderer>().endColor = collision.GetComponent<TrailRenderer>().endColor;
            a.tag = "Teleported";
            Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(force, ForceMode2D.Impulse);
            _bullets[0].gameObject.transform.position = _teleportPoint.position;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag(_bulletTagName[1]))//ã≠âª
        {
            GameObject a = Instantiate(_bullets[1], _teleportPoint.transform.position, _bullets[1].transform.rotation);
            Vector3 forceDirection = gameObject.transform.position.normalized - _teleportPoint.transform.position.normalized;
            Vector3 force = _strongBulletPower * forceDirection;
            a.layer = collision.gameObject.layer;
            a.tag = "Teleported";
            Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(force, ForceMode2D.Impulse);
            _bullets[0].gameObject.transform.position = _teleportPoint.position;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Teleported"))
        {
            Destroy(collision.gameObject);
        }
    }

}
