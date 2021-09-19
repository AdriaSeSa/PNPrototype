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

    public void DestroyBullet()
    {
        this.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
