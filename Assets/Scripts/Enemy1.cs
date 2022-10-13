using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private float speed;
    public int lifes;
    [HideInInspector]public EnemyManager manager;
    public GameObject explosion;

    void Start()
    {
        speed = Random.Range(2f, 4f);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if(transform.position.x < -9.5f)
        {
            PlayerValues.life -= 10;
            Destroy(gameObject);
            manager.CheckEnemies();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "BulletPlayer")
        {
            Destroy(other.gameObject);
            GetDamage();
        }
    }
    private void GetDamage()
    {
        lifes--;
        if(lifes <= 0)
        {
            PlayerValues.score += 10;
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
