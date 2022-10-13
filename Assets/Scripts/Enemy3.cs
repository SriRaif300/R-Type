using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    private float speed;
    private float limits;
    private float initY;
    public int lifes;
    [HideInInspector] public EnemyManager manager;
    public GameObject explosion;

    void Start()
    {
        speed = Random.Range(2f, 4f);
        limits = Random.Range(1.5f, 4f);
        initY = transform.position.y;
        if(initY > 4.5f - limits)
        {
            initY = 4.5f - limits;
        }
        if(initY < -4.5f + limits)
        {
            initY = -4.5f + limits;
        }
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        Vector2 finalPos = transform.position;
        finalPos.y = initY + limits * Mathf.Sin(Time.time * speed);
        transform.position = finalPos;
        if (transform.position.x < -9.5f)
        {
            PlayerValues.life -= 15;
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
            PlayerValues.score += 30;
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
