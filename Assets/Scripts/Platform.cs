using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    [SerializeField] GameObject platspawn;
    [SerializeField] GameObject platform;

    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigid.velocity = new Vector2(speed, 0);
    }

    void SpawnPlatform()
    {
        GameObject newplat = Instantiate(platform);

        int randheight = Random.Range(1, 3);
        float height = 0;
        if (randheight == 1)
        {
            height = -2.5f;
        }

        newplat.transform.position = new Vector2(platspawn.transform.position.x, height);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlatEater"))
        {
            SpawnPlatform();
            Destroy(gameObject);
        }
    }
}
