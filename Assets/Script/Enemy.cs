using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy: MonoBehaviour
{
    [Tooltip("移動速度")] float _speed = 10.0f;
    [Tooltip("弾の速度")] float _shotPowerNormal = 10.0f;
    [Tooltip("弾の速度")] float _shotPowerStrong = 15.0f;
    Rigidbody2D _rb;
    // int hp = 0;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] Color _red = Color.red;
    [SerializeField] Color _black = Color.black;
    [SerializeField] Color _grey = Color.grey;
    [SerializeField, Tooltip("照準")] List<Transform> _transformList = new(5);
    [SerializeField, Tooltip("残り弾数")] List<SpriteRenderer> _spriteRendererList = new(5);
    [SerializeField, Tooltip("弾の種類、残り弾数")] List<GameObject> _magazineList = new(5);
    [SerializeField, Tooltip("ノーマル弾")] GameObject _bulletNormal;
    [SerializeField, Tooltip("強化弾")] GameObject _bulletStrong;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;
    [SerializeField] List<string> _keyName = new();

    [SerializeField] List<GameObject> _wall;
    [SerializeField] float _time = 3;
    [SerializeField] float _timeDelta;

    private void Start()
    {
        _wall = GameObject.FindGameObjectsWithTag("Ground").ToList();
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //処理を書く
                    Debug.Log(code);
                    break;
                }
            }
        }
        ChangeSprite();

        //時間で弾が補充される
        _timeDelta += Time.deltaTime;

        if (_time < _timeDelta)
        {
            for (int i = 0; i < _magazineList.Count; i++)
            {
                if (_magazineList[i] != _bulletNormal && _magazineList[i] != _bulletStrong)
                {
                    _magazineList.Remove(_magazineList[i]);
                    _magazineList.Insert(i, _bulletNormal);
                    break;
                }
            }
            _timeDelta = 0;
        }
        //弾の上限
        if (_magazineList.Count > 5)
        {
            _magazineList.RemoveRange(5, _magazineList.Count - 5);
        }
        //弾発射
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown(_keyName[0]))
        {
            Shot(_transformList[0], _magazineList[0]);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButtonDown(_keyName[1]))
        {
            Shot(_transformList[1], _magazineList[0]);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown(_keyName[2]))
        {
            Shot(_transformList[2], _magazineList[0]);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetButtonDown(_keyName[3]))
        {
            Shot(_transformList[3], _magazineList[0]);
        }
    }
    private void FixedUpdate()
    {
        float verticalInput = _speed * Input.GetAxisRaw("Vertical");
        float horizontalInput = _speed * Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(horizontalInput, verticalInput);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StrongBulletItem"))
        {
            _magazineList.Remove(_magazineList[4]);
            _magazineList.Insert(0, _bulletStrong);
        }
        if (collision.gameObject.CompareTag("StrongBullet"))
        {

        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
        }

    }
    /// <summary>
    /// こいつはいつかアップデートから抜ける
    /// </summary>
    void ChangeSprite()
    {
        Debug.Log("色チェンジャー！");
        for (int i = 0; i < _magazineList.Count; i++)
        {
            if (_magazineList[i] == _bulletNormal)
            {
                _spriteRendererList[i].color = _black;
            }
            else if (_magazineList[i] == _bulletStrong)
            {
                _spriteRendererList[i].color = _red;
            }
            else
            {
                _spriteRendererList[i].color = _grey;
            }
        }
    }
    void Shot(Transform dir, GameObject bullet)
    {
        GameObject pos = _wall.OrderBy(x =>
                 Vector2.Distance(gameObject.transform.position, x.transform.position)
                 ).FirstOrDefault();
        if (bullet == _bulletNormal)
        {
            _magazineList.Remove(_magazineList[0]);
            _magazineList.Add(null);
            GameObject a = Instantiate(_bulletNormal, pos.transform.position + new Vector3(0, 0, -1), _bulletNormal.transform.rotation);
            a.layer = 7;
            Vector3 forceDirection = dir.position - gameObject.transform.position;
            Vector3 force = _shotPowerNormal * forceDirection;
            // 力を加えるメソッド
            Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(force, ForceMode2D.Impulse);
            _audioSource.PlayOneShot(_audioClip);
        }
        else if (bullet == _bulletStrong)
        {
            _magazineList.Remove(_magazineList[0]);
            _magazineList.Add(null);
            GameObject a = Instantiate(_bulletStrong, pos.transform.position + new Vector3(0, 0, -1), _bulletStrong.transform.rotation);
            a.layer = 7;
            Vector3 forceDirection = dir.position - gameObject.transform.position;
            Vector3 force = _shotPowerStrong * forceDirection;
            // 力を加えるメソッド
            Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(force, ForceMode2D.Impulse);
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}
