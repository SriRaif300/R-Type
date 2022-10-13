using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private float speedX, speedY;
    private float finalY;
    public int lifes;
    [HideInInspector] public EnemyManager manager;
    public GameObject explosion;

    void Start()
    {
        speedX = Random.Range(2f, 4f);
        speedY = Random.Range(2f, 6f);
        finalY = Random.Range(-4.5f, 4.5f);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speedX * Time.deltaTime);
        float currentY = Mathf.MoveTowards(transform.position.y, finalY, speedY * Time.deltaTime);
        transform.position = new Vector2(transform.position.x, currentY);
        if(transform.position.y == finalY)
        {
            finalY = Random.Range(-4.5f, 4.5f);
        }
        if (transform.position.x < -9.5f)
        {
            PlayerValues.life -= 10;
            Destroy(gameObject);
            manager.CheckEnemies();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BulletPlayer")
        {
            Destroy(other.gameObject);
            GetDamage();
        }
    }
    private void GetDamage()
    {
        lifes--;
        if (lifes <= 0)
        {
            PlayerValues.score += 20;
            GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            ParticleSystem particle = newExplosion.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = particle.main;
            main.startColor = GetComponent<SpriteRenderer>().color;
            Destroy(newExplosion, 1);
            Destroy(gameObject);
            manager.CheckEnemies();
        }
    }
}
