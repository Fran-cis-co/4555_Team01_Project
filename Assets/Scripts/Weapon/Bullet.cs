using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            print("Hit" + collision.gameObject.name + "!");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            print("Hit a wall");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("Hit an enemy");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            Destroy(gameObject);
            SoundManager.Instance.enemyDefeated.Play();
        }
    }
}
