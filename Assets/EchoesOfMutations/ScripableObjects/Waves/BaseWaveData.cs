using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseWaveData", menuName = "EchoesOfMutations/BaseWaveData")]
public class BaseWaveData : ScriptableObject
{
    [SerializeField] private List<EnemySpawnData> enemies = new();
    [SerializeField] private float timeUntilNextWave = 10f;
    [SerializeField] private int enemyCount;
    [SerializeField] private int waveNumber;

    public List<EnemySpawnData> Enemies => enemies;
    public float TimeUntilNextWave => timeUntilNextWave;
    public int EnemyCount => enemyCount;
    public int WaveNumber => waveNumber;
}
