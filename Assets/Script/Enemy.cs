using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �v���C���[�֍ŒZ�����ňړ����邭��
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _deathEffect;
    [SerializeField] float _speed = 0.1f;
    [SerializeField] float _mapX;
    [SerializeField] float _mapY;
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
            Instantiate(_deathEffect, gameObject.transform.position - new Vector3(0, 0, 3), _deathEffect.transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
    void FollowPlayer()
    {
        float Xdistance = gameObject.transform.position.x - _player.transform.position.x;
        float Ydistance = gameObject.transform.position.y - _player.transform.position.y;

        bool XsmollerY = false;

        if (Xdistance < Ydistance)
        {
            XsmollerY = true;
        }
        else
        {
            XsmollerY = false;
        }
        if (XsmollerY)//���E�̓����D�悵�Ăق���
        {
            if (Xdistance < _mapX / 2)//�߂Â������I
            {
                if (gameObject.transform.position.x < _player.transform.position.x)//�G���v���C���[�̍��ɂ��邩��E�s����
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
            else //���ꂽ���I
            {
                if (gameObject.transform.position.x < _player.transform.position.x)//�G���v���C���[�̍��ɂ��邩�獶�ɍs��
                {
                    MoveLeft();
                }
                else
                {
                    MoveRight();
                }
            }
        }
        else//!XsmollerY//�㉺�̓�����D�悵�Ăق���
        {
            if (Ydistance < _mapY / 2)//�߂Â������I
            {
                if (gameObject.transform.position.y < _player.transform.position.y)//�G���v���C���[�̉��ɂ��邩���s����
                {
                    MoveUp();
                }
                else
                {
                    MoveDown();
                }
            }
            else //���ꂽ���I
            {
                if (gameObject.transform.position.y < _player.transform.position.y)//�G���v���C���[�̉��ɂ��邩�牺�ɍs��
                {
                    MoveDown();
                }
                else
                {
                    MoveUp();
                }
            }
        }
    }

    void MoveUp()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1), _speed * Time.deltaTime);
    }
    void MoveDown()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1), _speed * Time.deltaTime);
    }
    void MoveLeft()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y), _speed * Time.deltaTime);
    }
    void MoveRight()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y), _speed * Time.deltaTime);
    }


}
