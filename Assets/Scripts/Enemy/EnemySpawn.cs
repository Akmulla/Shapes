﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    public static int destroyedEnemyCount;
    [SerializeField]
    float spawnDelay;
    //[SerializeField]
    int enemyCount;
    [SerializeField]
    int maxEnemyCount;
    public Line[] lines;
    public Pool[] pools;
    bool isSpawning;
    //[SerializeField]
    //bool moveByCurve;
    int enemyTypeCount;


    void Start()
    {
        destroyedEnemyCount = 0;
        enemyCount = 0;
        enemyTypeCount = pools.Length;
        StartCoroutine(Spawn());
        
        isSpawning = true;

    }

    void Update()
    {
        if (destroyedEnemyCount == maxEnemyCount)
            MainController.mainController.NextLevel();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.0f);
        while (isSpawning)
        {
            if (enemyCount >= maxEnemyCount)
            {
                isSpawning = false;
                yield return null;
            }
            else
            {
                GenerateMultipleEnemies();
                yield return new WaitForSeconds(spawnDelay + Random.value/2.0f);
            }

        }
    }

    void GenerateMultipleEnemies()
    {
        List<int> enemiesOnLine = new List<int>();
        int j = (int)Random.Range(0, pools.Length);
        enemiesOnLine.Add(j);

        int h = (int)Random.Range(0, lines.Length);
        bool leftPosition= Random.value > 0.5 ?
                        true : false;
        Vector3 position = leftPosition ?
                        lines[h].spawnPositions[0].position : lines[h].spawnPositions[1].position;
        GameObject obj=(GameObject)pools[j].Activate(position, Quaternion.identity);

        obj.GetComponent<EnemyMove>().leftDirection = !leftPosition;
        enemyCount++;
        if (lines[h].curve != null)
        {
            obj.GetComponent<EnemyMove>().mgcurves = lines[h].curve;
        }
        for (int i = 0; i < lines.Length-1; i++)
        {
            if ((i!=h) && (Random.value > 0.5f))
            {
                h = (int)Random.Range(0, pools.Length);
                if (!enemiesOnLine.Contains(h))
                {
                    position = Random.value > 0.5 ?
                        lines[i].spawnPositions[0].position : lines[i].spawnPositions[1].position;
                   obj=pools[h].Activate(position, Quaternion.identity);
                   if (lines[i].curve != null)
                    {
                        obj.GetComponent<EnemyMove>().mgcurves = lines[i].curve;
                    }
                    enemyCount++;
                }

                
            }
        }
    }


}
