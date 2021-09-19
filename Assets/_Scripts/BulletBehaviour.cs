using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] private float _speed;


    public void ShootBullet(Vector2 direction)
    {
        _direction = direction;
    }

    private void DestroyBullet()
    {
        this.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy Projectile") || other.gameObject.CompareTag("Enemy"))
            return;
        
        DestroyBullet();
    }
}
