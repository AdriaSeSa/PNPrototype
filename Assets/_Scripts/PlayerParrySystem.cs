using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParrySystem : MonoBehaviour
{
    public string enemyProjectileTag = "Enemy Projectile";
    public float parryDuration;
    public Collider2D parryArea;
    private IEnumerator _playParry;
    
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
        _playParry = PlayParry();
        parryArea.enabled = true;
        yield return new WaitForSecondsRealtime(parryDuration);

        _playParry = null;
        parryArea.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyProjectileTag))
        {
            other.gameObject.SetActive(false);
        }
    }
}
