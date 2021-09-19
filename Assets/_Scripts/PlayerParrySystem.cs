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
    public bool isBlocking;
    private bool isParrySuccesful; // Determines if the parry action has actually stopped a projectile
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Parry();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Block(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Block(false);
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

    private void Block(bool blocking)
    {
        isBlocking = blocking;
    }

    private IEnumerator PlayParry()
    {
        _playParry = PlayParry();
        parryArea.enabled = true;
        yield return new WaitForSecondsRealtime(parryDuration);
        
        if (!isParrySuccesful)
        {
            yield return new WaitForSecondsRealtime(parryFailDuration);
            parryArea.enabled = false;
        }

        isParrySuccesful = false;
        _playParry = null;
        parryArea.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyProjectileTag))
        {
            isParrySuccesful = true;
        }
    }
}
