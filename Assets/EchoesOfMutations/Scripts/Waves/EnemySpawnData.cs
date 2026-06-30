using UnityEngine;

[System .Serializable]
public class EnemySpawnData
{
    [SerializeField] private EnemiesTypes enemyType;
    [SerializeField] private int amount = 1;

    public EnemiesTypes EnemyType => enemyType;
    public int Amount => amount;
}
