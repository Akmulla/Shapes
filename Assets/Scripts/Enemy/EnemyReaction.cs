﻿using UnityEngine;
using System.Collections;

public class EnemyReaction : MonoBehaviour
{
    public Pool pool;

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemySpawn.enemySpawn.destroyedEnemyCount++;
        pool.Deactivate(gameObject);
    }

    
}
