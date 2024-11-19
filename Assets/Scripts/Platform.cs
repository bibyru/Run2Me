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
        newplat.transform.position = platspawn.transform.position;
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
