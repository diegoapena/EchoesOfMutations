using UnityEngine;

[System .Serializable]
public class EnemySpawnData
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int amount = 1;

    public GameObject EnemyPrefab => enemyPrefab;
    public int Amount => amount;
}
