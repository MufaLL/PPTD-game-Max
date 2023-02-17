using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Loader<Manager>
{
    public static Manager instance = null;

    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;
    

    private int enemiesOnScreen = 0;
    const float spawnDelay = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() 
    
    { 
        if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
        for (int i = 0; i < enemiesPerSpawn; i++) 
        
            if (enemiesOnScreen < maxEnemiesOnScreen)
            {
                GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen += 1;
            }

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }
    public void removeEnemyFromScreen()
    {
        if (enemiesOnScreen > 0)
        {
            enemiesOnScreen -= 1;
        }
    }
}