using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "DataBaseWave", menuName = "EchoesOfMutations/DataBaseWave")]
public class DataBaseWave : SerializedScriptableObject
{
    public Dictionary<int , BaseWaveData> waveDataBase = new();
    
    public BaseWaveData GetWave(int waveNumber)
    {
        if(waveDataBase.TryGetValue(waveNumber , out BaseWaveData waveData))
        {
            return waveData;
        }
        else
        {
            throw new System.Exception("The WaveData trying to get  doesn't exist");
        }
    }
}
