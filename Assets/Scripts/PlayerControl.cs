using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public Transform shootPoint;
    public GameObject bullet;
    private float fireRate;
    public Text scoreText, scoreFinal;
    public Image lifeBar;
    private int totalLife;
    public GameObject gameOverPanel;
    private bool isDead;

    void Start()
    {
        isDead = false;
        gameOverPanel.SetActive(false);
        PlayerValues.life = 100;
        PlayerValues.score = 0;
        totalLife = PlayerValues.life;
    }

    void LateUpdate()
    {
        scoreText.text = "Score: " + PlayerValues.score;
        if(PlayerValues.life >= 100)
        {
            PlayerValues.life = 100;
        }
        if (PlayerValues.life <= 0 && isDead == false)
        {
            isDead = true;
            gameOverPanel.SetActive(true);
            scoreFinal.text = " " + PlayerValues.score;
            Time.timeScale = 0;
        }
    }

    void Update()
    {
        float finaLife = (float)PlayerValues.life / (float)totalLife;
        lifeBar.fillAmount = Mathf.MoveTowards(lifeBar.fillAmount, finaLife, 0.5f * Time.deltaTime);
        Movement();
        ShootControl();
    }
    void Movement()
    {
        speed = 15;
        transform.Translate(Vector2.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.Translate(Vector2.up * speed * Time.deltaTime * Input.GetAxis("Vertical"));

        if (transform.position.y > 4.5f)
        {
            transform.position = new Vector2(transform.position.x, 4.5f);
        }
        if (transform.position.y < -4.5f)
        {
            transform.position = new Vector2(transform.position.x, -4.5f);
        }

        if (transform.position.x < -8.4f)
        {
            transform.position = new Vector2(-8.4f, transform.position.y);
        }
        if (transform.position.x > -0.5f)
        {
            transform.position = new Vector2(-0.5f, transform.position.y);
        }
    }
    void ShootControl() 
    {
        fireRate += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireRate > 0.3f) 
        {
            fireRate = 0;
            GameObject newBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            Destroy(newBullet, 3);
        }
    }

}
