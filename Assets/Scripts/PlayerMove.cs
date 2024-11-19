using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpforce = 20f;

    Rigidbody2D rigid;
    public bool is_on_floor = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Move(int dir = 0)
    {
        rigid.velocity = new Vector2(dir * speed, rigid.velocity.y);
    }

    public void Yump()
    {
        if (is_on_floor == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpforce);
            is_on_floor = false;
        }
        GameObject.FindGameObjectWithTag("Ground").GetComponent<Platform>().StartAllPlatforms();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_on_floor = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<PlayerHurt>().Hurt(false);
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            GetComponent<PlayerHurt>().Hurt();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            GetComponent<PlayerHurt>().Hurt();
        }
    }
}
