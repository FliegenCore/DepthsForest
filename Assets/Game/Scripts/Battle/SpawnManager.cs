using System.Collections;
using System.Collections.Generic;
using Game.World;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnPoint _playerSpawnPoint;
    [SerializeField] private SpawnPoint _enemySpawnPoint;
    
    public SpawnPoint PlayerSpawnPoint => _playerSpawnPoint;
    public SpawnPoint EnemySpawnPoint => _enemySpawnPoint;
}
