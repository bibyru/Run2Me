using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] public bool started = false;

    [SerializeField] GameObject platspawn;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject enemy;

    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        platspawn = GameObject.FindGameObjectWithTag("PlatSpawn");
    }

    private void Update()
    {
        if (started == true)
        {
            rigid.velocity = new Vector2(speed, 0);
        }
        else
        {
            rigid.velocity = new Vector2();
        }
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

        GameObject newenemy = null;
        float randspawnnpc = Random.Range(0, 2);
        if (randspawnnpc == 1)
        {
            newenemy = Instantiate(enemy);
        }

        newplat.transform.position = new Vector2(platspawn.transform.position.x, height);
        if (newenemy != null)
        {
            newenemy.transform.position = new Vector2(platspawn.transform.position.x, height+0.5f);
        }
    }

    public void StartAllPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Ground");
        foreach (var plat in platforms)
        {
            plat.GetComponent<Platform>().started = true;
        }
    }

    public void StopAllPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Ground");
        foreach (var plat in platforms)
        {
            plat.GetComponent<Platform>().started = false;
        }
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
