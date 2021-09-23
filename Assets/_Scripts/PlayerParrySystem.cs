using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerParrySystem : MonoBehaviour
{
    public string enemyProjectileTag = "Enemy Projectile";
    public float parryDuration;
    public float parryFailDuration;
    public Collider2D parryArea;
    private IEnumerator _playParry;
    private bool _isParrySuccesful; // Determines if the parry action has actually stopped a projectile
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Parry();
        }
    }

    private void Parry()
    {
        // Play Parry animation

        if (_playParry == null)
        {
            StartCoroutine(PlayParry());
        }

    }
    
    private IEnumerator PlayParry()
    {
        // Activate Parry Area
        
        _playParry = PlayParry();
        parryArea.enabled = true;
        yield return new WaitForSecondsRealtime(parryDuration);
        
        // After parry duration, we check if the parry was succesful
        
        if (!_isParrySuccesful)
        {
            // If not succesful, we activate the parry Fail extra duration
            parryArea.enabled = false;
            yield return new WaitForSecondsRealtime(parryFailDuration);
        }

        // If succesful, we set the parry as available again
        
        _isParrySuccesful = false;
        _playParry = null;
        parryArea.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyProjectileTag))
        {
            _isParrySuccesful = true;
            Debug.Log("Parry!");
        }
    }
}
