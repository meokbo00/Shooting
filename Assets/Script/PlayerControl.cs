using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public bool TopTouch;
    public bool BottomTouch;
    public bool LeftTouch;
    public bool RightTouch;
    public float reloadtime;
    public float maxtime;
    public int power;

    public GameManager manager;
    public GameObject bulletA;
    public GameObject bulletB;

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Fire();
        Reload();
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if ((RightTouch && x == 1) || (LeftTouch && x == -1))
            x = 0;

        float y = Input.GetAxisRaw("Vertical");
        if ((TopTouch && y == 1) || (BottomTouch && y == -1))
            y = 0;

        transform.Translate(new Vector3(x, y, 0) * speed * Time.deltaTime);

        if (Input.GetButtonDown("Horizontal") ||
            Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)x);
        }
    }

    void Fire()
    {
        if (!Input.GetMouseButton(0))
            return;
        if (reloadtime < maxtime)
            return;

        switch (power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletA, transform.position, transform.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletL = Instantiate(bulletA, transform.position + Vector3.left*0.1f, transform.rotation);
                GameObject bulletR = Instantiate(bulletA, transform.position + Vector3.right*0.1f, transform.rotation);
                Rigidbody2D rbR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                rbR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rbL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletLL = Instantiate(bulletA, transform.position + Vector3.left * 0.2f, transform.rotation);
                GameObject bulletRR = Instantiate(bulletA, transform.position + Vector3.right * 0.2f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletB, transform.position, transform.rotation);


                Rigidbody2D rbRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rbCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rbLL = bulletLL.GetComponent<Rigidbody2D>();                
                rbRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rbCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rbLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }


        reloadtime = 0;
    }
    void Reload()
    {
        reloadtime += Time.deltaTime;
    }











    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    TopTouch = true;
                    break;
                case "Bottom":
                    BottomTouch = true;
                    break;
                case "Left":
                    LeftTouch = true;
                    break;
                case "Right":
                    RightTouch = true;
                    break;
            }
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            manager.PlayerRespond();
            gameObject.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    TopTouch = false;
                    break;
                case "Bottom":
                    BottomTouch = false;
                    break;
                case "Left":
                    LeftTouch = false;
                    break;
                case "Right":
                    RightTouch = false;
                    break;
            }
        }
    }
}
