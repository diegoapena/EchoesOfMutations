using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Linq : MonoBehaviour
{
    public DataBaseEnemies dataBaseEnemies;
    void Start()
    {
        //dataBaseEnemies = GetComponent<DataBaseEnemies>();
    }
    
    
    void Update()
    {
        
    }
    /*
    [Button]
    public void TestSelect()
    {
        var enemyNames = dataBaseEnemies.enemyDataBase[EnemiesTypes.AllEnemies].Select(enemy => enemy.EnemyName).ToList();
        enemyNames.ForEach(enemy => Debug.Log(enemy));
    }
    [Button]
    public void TestWhere()
    {
        var strongEnemies = dataBaseEnemies.enemyDataBase[EnemiesTypes.AllEnemies].Where(enemy => enemy.DamageQuantity > 4).ToList();
        strongEnemies.ForEach(enemy => Debug.Log(enemy.EnemyName + " has damage: " + enemy.DamageQuantity));
    }
    [Button]
    public void TestOrderByDescending()
    {
        var sortedEnemies = dataBaseEnemies.enemyDataBase[EnemiesTypes.AllEnemies].OrderByDescending(enemy => enemy.Id).ToList();
        sortedEnemies.ForEach(enemy => Debug.Log(enemy.EnemyName + " has ID: " + enemy.Id));
    }

    [Button]
    public void ChainLinq()
    {
        var strongSortedEnemies = dataBaseEnemies.enemyDataBase[EnemiesTypes.AllEnemies]
            .Where(enemy => enemy.DamageQuantity > 3)
            .OrderByDescending(enemy => enemy.Id)
            .Take(1)
            .Select(enemy => enemy.EnemyName).ToList();

        strongSortedEnemies.ForEach(enemy => Debug.Log(enemy));
    }
    */
}
