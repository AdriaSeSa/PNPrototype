using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Vector2 _direction;
    public float speed;
    private float _speed;
    private Transform _source;
    private bool isEnemy;
    


    public void ShootBullet(Vector2 direction, bool enemyBullet, Transform source = null)
    {
        _direction = direction;
        _source = source;
        isEnemy = enemyBullet;
        _speed = speed;
    }

    private void DestroyBullet()
    {
        this.gameObject.SetActive(false);
    }

    private void ReturnToSource()
    {
        _direction = _source.position - transform.position;
        _direction.Normalize();

        _speed *= 1.5f;
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy Projectile"))
            return;

        if (other.gameObject.CompareTag("Enemy") && isEnemy) return;

        if (other.gameObject.CompareTag("ParryArea"))
        {
            ReturnToSource();
            return;
        }
        DestroyBullet();
    }
}
