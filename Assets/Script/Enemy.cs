using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// îíï«Ç…ìñÇΩÇ¡ÇΩÇÁçïÇ…êıÇﬂÇÈ
/// íeÇ…ìñÇΩÇ¡ÇΩÇÁîöî≠Ç∑ÇÈ
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _deathEffect;
    [SerializeField] float _speed = 0.1f;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        FollowPlayer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet") ||
            collision.gameObject.CompareTag("StrongBullet") ||
            collision.gameObject.CompareTag("Teleported"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("a");
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Bullet") ||
            collision.gameObject.CompareTag("StrongBullet") ||
            collision.gameObject.CompareTag("Teleported"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
    private void OnDestroy()
    {
        Instantiate(_deathEffect,gameObject.transform.position-new Vector3(0,0,3),_deathEffect.transform.rotation);
    }
    void FollowPlayer()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(_player.transform.position.x, _player.transform.position.y), _speed * Time.deltaTime);
    }
}
