using NUnit.Framework;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [FoldoutGroup("Wave Settings")]
    [SerializeField] private List<BaseWaveData> waves = new();
    [FoldoutGroup("Wave Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [FoldoutGroup("Wave Settings/ Spawn Settings")]
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [FoldoutGroup("Wave Settings/ Spawn Settings")]
    [SerializeField] private float timeBeforeFirstWave = 3f;
    public UnityEvent<int> OnWaveStarted;
    public UnityEvent<int> OnWaveCleared;
    public UnityEvent<float> OnWaveCountDown;
    public UnityEvent OnAllWavesCompleted;

    private MyQueue<EnemiesTypes> spawnQueue = new();
    private List<BaseEnemy> aliveEnemies = new();
    private int currentWaveIndex = -1;
    private bool isSpawning = false;

    private void OnEnable()
    {
        WavePool.OnEnemyDeath += HandleEnemyDeath;
    }
    private void OnDisable()
    {
        WavePool.OnEnemyDeath -= HandleEnemyDeath;
    }


    void Start()
    {
        StartCoroutine(BeginWaveSystem());
    }

    
    void Update()
    {
        
    }
    private IEnumerator BeginWaveSystem()
    {
        yield return new WaitForSeconds(timeBeforeFirstWave);
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex >= waves.Count)
        {
            OnAllWavesCompleted?.Invoke();
            return;
        }
        BaseWaveData wave = waves[currentWaveIndex];
        FillQueueWithWave(wave);
        OnWaveStarted?.Invoke(currentWaveIndex + 1);
        StartCoroutine(SpawnWave(wave));

    }

    private void FillQueueWithWave(BaseWaveData wave)
    {
        spawnQueue.Clear();
        foreach (EnemySpawnData spawnData in wave.Enemies)
        {
            for (int i = 0; i < spawnData.Amount; i++) 
            { 
                spawnQueue.Enqueue(spawnData.EnemyType);         
            }
        }
    }
    private IEnumerator SpawnWave(BaseWaveData wave)
    {
        isSpawning = true;
        while (spawnQueue.Count > 0) 
        { 
            EnemiesTypes typeToSpawn = spawnQueue.Dequeue();
            SpawnEnemy(typeToSpawn);
            yield return new WaitForSeconds(timeBetweenSpawns);
        
        }
        isSpawning = false;

        yield return new WaitUntil(() => AllEnemiesDead());

        OnWaveCleared?.Invoke(currentWaveIndex + 1);

        yield return StartCoroutine(WaveCountdown(wave.TimeUntilNextWave));
        StartNextWave();
    }

    private IEnumerator WaveCountdown(float duration)
    {
        float reaniming = duration;
        while(reaniming > 0f)
        {
            OnWaveCountDown?.Invoke(reaniming);
            reaniming -= Time.deltaTime;
            yield return null;
        }
        OnWaveCountDown?.Invoke(0f);
    }

    private void SpawnEnemy(EnemiesTypes enemyType)
    {
        if (GameManager.Instance.wavePool == null || spawnPoints == null || spawnPoints.Length == 0) return;

        Transform point = spawnPoints[Random.Range(0,spawnPoints.Length)];
        BaseEnemy enemyInstance = GameManager.Instance.wavePool.SpawnEnemy(enemyType , point.position , point.rotation);
        aliveEnemies.Add(enemyInstance);
    }
    private void HandleEnemyDeath(BaseEnemy enemy)
    {              
        aliveEnemies.Remove(enemy);
    }
    private bool AllEnemiesDead()
    {
        aliveEnemies.RemoveAll(e => e == null);
        return aliveEnemies.Count == 0;
    }
    public int CurrentWaveNumber => currentWaveIndex + 1;
    public int TotalWaves => waves.Count;
    public int EnemiesReanimingInQueue => spawnQueue.Count;
    public int EnemiesAlive => aliveEnemies.Count;
    public bool IsSpawning => isSpawning;
}


