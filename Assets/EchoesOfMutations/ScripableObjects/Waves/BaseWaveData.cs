using UnityEngine;

[CreateAssetMenu(fileName = "BaseWaveData", menuName = "EchoesOfMutations/BaseWaveData")]
public class BaseWaveData : ScriptableObject
{
    public BaseEnemiesData enemies;
    [SerializeField] private int enemyCount;
    [SerializeField] private int waveNumber;

    public int EnemyCount => enemyCount;
    public int WaveNumber => waveNumber;
}
