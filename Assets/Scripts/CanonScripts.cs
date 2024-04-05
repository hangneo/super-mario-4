using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonScripts : MonoBehaviour
{
    public GameObject _danphao;
    public float _fireRate;
    public float _maxDistance; // Khoảng cách tối đa để dừng animation
    public Transform player;
    private float _nextFireTime;
    Animator _animator;
    private bool _isInRange; // Biến để kiểm tra xem người chơi có trong khoảng cách cho phép hay không

    void Start()
    {
        _fireRate = 2f;
        _maxDistance = 10.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _nextFireTime = 0f;
        _animator = GetComponent<Animator>();
        _isInRange = false; // Ban đầu không trong khoảng cách cho phép
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= _maxDistance)
            {
                if (!_isInRange)
                {
                    _isInRange = true; // Đánh dấu là trong khoảng cách cho phép
                    _animator.SetBool("Ban", true);
                }

                if (Time.time >= _nextFireTime)
                {
                    // Bắn đạn
                    FireBullet();
                    // Cập nhật thời điểm bắn đạn tiếp theo
                    _nextFireTime = Time.time + _fireRate;
                }
            }
            else
            {
                if (_isInRange)
                {
                    _isInRange = false; // Đánh dấu là vượt quá khoảng cách cho phép
                    _animator.SetBool("Ban", false);
                }
            }
        }
    }

    private void FireBullet()
    {
        Instantiate(_danphao, transform.position, Quaternion.identity);
    }
}