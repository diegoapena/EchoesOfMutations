using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBaseEnemy", menuName = "EchoesOfMutations/DataBaseEnemy")]
public class DataBaseEnemies : SerializedScriptableObject
{
    public Dictionary<EnemiesTypes, List<BaseEnemiesData>> enemyDataBase = new();
       public BaseEnemiesData GetEnemy(EnemiesTypes enemyType, string enemyName)
        {
            if (enemyDataBase.TryGetValue(enemyType, out List<BaseEnemiesData> enemies))
            {
                return enemies.Find(enemy => enemy.EnemyName == enemyName);
            }
            else
            {
                throw new System.Exception("Enemy type not found in database: " + enemyType);   
            }
       }
}
