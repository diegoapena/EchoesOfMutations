using NUnit.Framework;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<BaseWaveData> waves;
    [SerializeField] private List<GameObject> activesEnemies = new();
    [SerializeField] private int currentWave;
    public MyQueue<BaseWaveData> waveQueue = new();
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    [Button]
    public void WaveEnqueue(BaseWaveData waveData)
    {
        waveQueue.Enqueue(waveData);
        currentWave = waveData.WaveNumber;
        Debug.Log("Wave # : " + waveData.WaveNumber + " has started");
    }
    [Button]
    public void WaveDequeue()
    {
        Debug.Log("Wave # : " + waveQueue.Dequeue().WaveNumber + " has finished");
    }
    [Button]
    public void WavePeek()
    {
        Debug.Log("Wave Peeked: " + waveQueue.Peek().WaveNumber + " " + "Enemies Remaining: " + waveQueue.Peek().EnemyCount);
    }

    [Button]
    public void WaveClear()
    {
        waveQueue.Clear();
        Debug.Log("Wave Queue Cleared");
    }

}


