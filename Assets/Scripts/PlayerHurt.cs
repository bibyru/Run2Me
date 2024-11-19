using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHurt : MonoBehaviour
{
    [SerializeField] float knockedX = -20f;
    [SerializeField] float knockedY = 30f;
    [SerializeField] int hurt_time = 1;

    Rigidbody2D rigid;
    Component playerinput;
    Vector3 startpos;

    int lives = 5;
    bool can_hurt = true;
    float timer = 0;

    private void Start()
    {
        startpos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Hurt(bool by_falling = true)
    {
        if (can_hurt == true)
        {
            can_hurt = false;

            if (lives > 1)
            {
                lives -= 1;

                if (by_falling == true)
                {
                    ResetPosition();
                    can_hurt = true;
                }
                else
                {
                    Knockback();
                }
            }
            else
            {
                ResetPosition();
                NoInput();

                GameObject.FindGameObjectWithTag("Ground").GetComponent<Platform>().StopAllPlatforms();
            }
        }
    }

    void Knockback()
    {
        rigid.velocity = new Vector2(knockedX, knockedY);
        timer = hurt_time;
    }

    void NoInput()
    {
        rigid.isKinematic = true;
        GetComponent<Inputs>().enabled = false;
    }

    void ResetPosition()
    {
        rigid.velocity = new Vector2();
        transform.position = startpos;
    }

    void SetAnim(int animnum)
    {
        //0 = idle
        //1 = run
        //2 = hurt
        //3 = die
        //4 = yump
        GetComponent<Animator>().SetInteger("anim", animnum);
    }

    private void Update()
    {
        if (can_hurt == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                can_hurt = true;
            }
        }

        if (lives >= 1)
        {
            if (GameObject.FindGameObjectWithTag("Ground").GetComponent<Platform>().started == false)
            {
                if (rigid.velocity.x == 0)
                {
                    SetAnim(0);
                }
                else
                {
                    SetAnim(1);
                }
            }
            else
            {
                if (GetComponent<CharMove>().is_on_floor == true)
                {
                    if (can_hurt == false)
                    {
                        Debug.Log("HURT");
                        SetAnim(2);
                    }
                    else
                    {
                        SetAnim(1);
                    }
                }
                else
                {
                    SetAnim(4);
                }
            }
        }
        else
        {
            SetAnim(3);
        }
    }
}
