using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerBlock _playerBlock;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private float invulnerabilitySeconds = 2.5f;
    public string enemyProjectileTag = "Enemy Projectile";
    private IEnumerator invulnerabilityTimer;
    
    private void Start()
    {
        _playerBlock = GetComponent<PlayerBlock>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(enemyProjectileTag))
        {
            if (invulnerabilityTimer != null) return;
            
            if (!_playerBlock.isBlocking)
            {
                currentHealth--;
                Debug.Log("Hit!"); 
                StartCoroutine(InvulnerabilityTime());
            }
                
        }
    }

    IEnumerator InvulnerabilityTime()
    {
        invulnerabilityTimer = InvulnerabilityTime();
        
        yield return new WaitForSecondsRealtime(invulnerabilitySeconds);

        invulnerabilityTimer = null;

    }
}
