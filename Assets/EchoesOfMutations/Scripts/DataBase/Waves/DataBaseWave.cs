using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "DataBaseWave", menuName = "EchoesOfMutations/DataBaseWave")]
public class DataBaseWave : SerializedScriptableObject
{
    public Dictionary<EnemiesTypes , BaseEnemy> EnemyDatabase = new();
    
    public BaseEnemy GetPrefab(EnemiesTypes enemyType)
    {
        if(EnemyDatabase.TryGetValue(enemyType , out BaseEnemy enemyPrefab))
        {
            return enemyPrefab;
        }
        else
        {
            throw new System.Exception("The BaseEnemy trying to get  doesn't exist");
        }
    }
}
