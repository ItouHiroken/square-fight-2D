using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponer : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] List<Transform> _spawnPosition = new(10);
    int i = 1;
    void Start()
    {
        StartCoroutine(SummonEnemy());
        StartCoroutine(SummonEnemyBigWave());
    }

    IEnumerator SummonEnemy()
    {
        if (i != _spawnPosition.Count)
        {
            Instantiate(Enemy, _spawnPosition[i - 1].position, Enemy.transform.rotation);
            i++;
        }
        else
        {
            Instantiate(Enemy, _spawnPosition[i - 1].position, Enemy.transform.rotation);
            i = 1;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(SummonEnemy());
    }
    IEnumerator SummonEnemyBigWave()
    {
        if (i != _spawnPosition.Count)
        {
            Instantiate(Enemy, _spawnPosition[i - 1].position, Enemy.transform.rotation);
            i++;
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            Instantiate(Enemy, _spawnPosition[i - 1].position, Enemy.transform.rotation);
            i = 1;
            yield return new WaitForSeconds(15f);
        }
        StartCoroutine(SummonEnemyBigWave());
    }
}
