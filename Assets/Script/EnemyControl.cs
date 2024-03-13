using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    public int health;
    public float reloadtime;
    public float maxtime;
    public string enemyname;
    public GameObject player;
    public GameObject bulletA;
    public GameObject bulletB;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Fire();
        Reload();
    }
    void Fire()
    {
        if (reloadtime <= maxtime)
            return;

        if(enemyname == "S")
        {
            GameObject bullet = Instantiate(bulletA, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            
            Vector3 dirVec = player.transform.position - transform.position;
            rb.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else if (enemyname == "L")
        {
            GameObject bulletL = Instantiate(bulletB, transform.position + Vector3.left * 0.3f, transform.rotation);
            GameObject bulletR = Instantiate(bulletB, transform.position + Vector3.right * 0.3f, transform.rotation);
            Rigidbody2D rbR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rbR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);
            rbL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);
        }
        reloadtime = 0;
    }
    void Reload()
    {
        reloadtime += Time.deltaTime;
    }


    void Onhit(int damage)
    {
        health -= damage;
        spriteRenderer.sprite = sprites[1];
        Invoke("returnsprite", 0.1f);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void returnsprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BulletBorder")
        {
            Destroy(gameObject );
        }
        if(collision.tag == "PlayerBullet")
        {
            BulletControl bullet = collision.gameObject.GetComponent<BulletControl>();
            Onhit(bullet.damage);
            Destroy(collision.gameObject);
        }
    }
}
