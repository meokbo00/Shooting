using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed ;
    public bool TopTouch;
    public bool BottomTouch;
    public bool LeftTouch;
    public bool RightTouch;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            switch(collision.gameObject.name)
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
