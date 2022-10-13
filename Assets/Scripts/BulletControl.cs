using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        speed = 15f;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if(transform.position.x > 8.91f)
        {
            Destroy(gameObject);
        }
    }
}
