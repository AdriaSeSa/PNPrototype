using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private IEnumerator shootBullet;
    public BulletBehaviour[] bullets;
    public float bulletRatioTime = 0.5f;
    
    // Update is called once per frame
    void Update()
    {
        if (shootBullet == null)
        {
            StartCoroutine(ShootBullet());
        }
    }

    IEnumerator ShootBullet()
    {
        shootBullet = ShootBullet();
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].gameObject.activeSelf)
            {
                bullets[i].gameObject.SetActive(true);
                bullets[i].transform.position = transform.position;
                bullets[i].ShootBullet(Vector2.left, true, transform);
                break;
            }
        }

        yield return new WaitForSecondsRealtime(bulletRatioTime);

        shootBullet = null;
    }
}
