using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyobj;
    public Transform[] spawnPoints;

    public float max;
    public float min;
    public GameObject player;
    void Update()
    {
        min += Time.deltaTime;
        if(min >= max)
        {
            spawnenemy();
            max = Random.Range(0.5f, 3);
            min = 0;
        }
    }
    private void spawnenemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranpoint = Random.Range(0, 9);
        GameObject enemy = Instantiate(enemyobj[ranEnemy], spawnPoints[ranpoint].position, spawnPoints[ranpoint].rotation);

        Rigidbody2D rigidbody = enemy.GetComponent<Rigidbody2D>();
        EnemyControl enemylogic = enemy.GetComponent<EnemyControl>();
        enemylogic.player = player;
        if(ranpoint == 8 || ranpoint == 7)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigidbody.velocity = new Vector2(enemylogic.speed * (-1),-1);
        }
        else if (ranpoint == 6 || ranpoint == 5)
        {
            enemy.transform.Rotate(Vector3.back * -90);
            rigidbody.velocity = new Vector2(enemylogic.speed, -1);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, enemylogic.speed * (-1));
        }
    }

    public void PlayerRespond()
    {
        Invoke("PlayerRespondExe", 2f);
        player.transform.position = Vector3.down * 3.5f;
        player.gameObject.SetActive(true);
    }
    void PlayerRespondExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.gameObject.SetActive(true);
    }
}