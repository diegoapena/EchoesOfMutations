using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;


public class WavePool : MonoBehaviour
{
    [Serializable]
    public class PoolEntry
    {
        public EnemiesTypes enemyType;
        public int initialSize = 10;
    }
    [FoldoutGroup("Pool Settings")]
    [SerializeField] private List<PoolEntry> poolEntries = new();

    private Dictionary<EnemiesTypes, MyQueue<BaseEnemy>> pools = new();
    private Dictionary<EnemiesTypes, BaseEnemy> prefabs = new();

    public static Action<BaseEnemy> OnEnemyDeath;
    private void OnEnable()
    {
        OnEnemyDeath += EnqueueEnemy;
    }
    private void OnDisable()
    {
        OnEnemyDeath -= EnqueueEnemy;
    }
    void Start()
    {
        foreach (PoolEntry entry in poolEntries)
        {
            CreateEnemyPrefab(entry.enemyType, entry.initialSize);
        }
    }

    
    void Update()
    {
        
    }
    public BaseEnemy SpawnEnemy(EnemiesTypes enemyType , Vector3 position , Quaternion rotation)
    {
        if(!pools.TryGetValue(enemyType , out MyQueue<BaseEnemy> pool) || pool.Count == 0 || pool.Peek().gameObject.activeSelf)
        {
            Debug.LogWarning("There aren't Enemies objects available , expading pool");
            CreateEnemyPrefab(enemyType, 5);
            pool = pools[enemyType];
        }
        BaseEnemy enemyPrefab = pool.Dequeue();
        enemyPrefab.transform.SetPositionAndRotation(position , rotation);
        enemyPrefab.gameObject.SetActive(true);
        return enemyPrefab;
    }
    private void EnqueueEnemy(BaseEnemy enemy)
    {
        enemy.gameObject.SetActive(true);
        if(!pools.TryGetValue(enemy.enemyType , out MyQueue<BaseEnemy> pool))
        {
            pool = new();
            pools[enemy.enemyType ] = pool;
        }
        pool.Enqueue(enemy);
    }
    private void CreateEnemyPrefab(EnemiesTypes enemyType , int quantity)
    {
        if(!prefabs.TryGetValue(enemyType , out BaseEnemy enemyPrefab))
        {
            enemyPrefab = GameManager.Instance.databasewave.GetPrefab(enemyType);
            prefabs[enemyType] = enemyPrefab;
        }
        if(!pools.TryGetValue(enemyType , out MyQueue<BaseEnemy> pool))
        {
            pool = new();
            pools[enemyType] = pool;
        }
        for (int i = 0; i < quantity; i++)
        {
            BaseEnemy newEnemy = Instantiate(enemyPrefab , transform);
            newEnemy.gameObject.SetActive(false);
            pool.Enqueue(newEnemy);
        }
    }
}
