using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum gameStatus
{
    next, play, gameOver, win
}


public class Manager : Loader<Manager>
{
    [SerializeField]
    int totalWaves = 10;
    [SerializeField]
    TMP_Text totalMoneyLabel;
    [SerializeField]
    TMP_Text currentWave;
    [SerializeField]
    TMP_Text totalEscapedLabel;
    [SerializeField]
    TMP_Text PlayBtnLabel;
    [SerializeField]
    Button PlayBtn;
    [SerializeField]
    public static Manager instance = null;
    [SerializeField]
    GameObject spawnPoint;
    [SerializeField]
    Enemy[] enemies;
    
    [SerializeField]
    int totalEnemies = 10;
    [SerializeField]
    int enemiesPerSpawn;
  

    
    int waveNumber = 0;
    int totalMoney = 10;
    int totalEscaped = 0;
    int roundEscaped = 0;
    int totalKilled = 0;
    int whichEnemiesToSpawn = 0;
    int enemiesToSpawn = 0;
    gameStatus currentState = gameStatus.play;


    public List<Enemy> EnemyList = new List<Enemy>();
   

    const float spawnDelay = 0.8f;

    

    public int TotalEscaped
    {
        get
        {
            return totalEscaped;
        }
        set
        {
            totalEscaped = value;
        }
    }
  

    public int RoundEscaped
    {
        get
        {
            return roundEscaped;
        }
        set
        {
            roundEscaped = value;
        }
    }

    public int TotalKilled
    {
        get
        {
            return totalKilled;
        }
        set
        {
            totalKilled = value;
        }
    }

   

    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;
            totalMoneyLabel.text = TotalMoney.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        PlayBtn.gameObject.SetActive(false);
        ShowMenu();
    }

    private void Update()
    {
        HandleEscape();
    }



    IEnumerator Spawn() 
    
    { 
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
        for (int i = 0; i < enemiesPerSpawn; i++) 
        
            if (EnemyList.Count < totalEnemies)
            {
                Enemy newEnemy = Instantiate(enemies[Random.Range(0, enemiesToSpawn)]) as Enemy;
                newEnemy.transform.position = spawnPoint.transform.position;  
            }

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }


    

    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
        
    }

    public void DestroyEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
    }


  




    public void addMoney (int amount)
    {
        TotalMoney += amount;
    }

    public void subtractMoney(int amount)
    {
        TotalMoney -= amount;
    }


    public void IsWaveOver()
    {
        totalEscapedLabel.text = "" + TotalEscaped + "";

        if((RoundEscaped + TotalKilled) == totalEnemies)
        {
            if (waveNumber <= enemies.Length)
            {
                enemiesToSpawn = waveNumber;
            }
            SetCurrentGameState();
            ShowMenu();
        }
    }

    public void SetCurrentGameState()
    {
        if (totalEscaped >= 10)
        {
            currentState = gameStatus.gameOver;
        }
        
        else if (waveNumber == 0 && (RoundEscaped + TotalKilled) == 0)
        {
            currentState = gameStatus.play;
        }

        else if (waveNumber >= totalWaves)
        {
            currentState = gameStatus.win;
        }

        else 
        {
            currentState = gameStatus.next;
        }
    }

    public void PlayButtonPressed()
    {
        switch (currentState)
        {
            case gameStatus.next:
                waveNumber += 1;
                totalEnemies += waveNumber;
                break;
            default:
                totalEnemies = 10;
                TotalEscaped = 0;
                TotalMoney = 20;
                enemiesToSpawn = 0;
                TowerManager.Instance.DestroyAllTowers();
                TowerManager.Instance.RenameTagBuildSite();
                totalMoneyLabel.text = TotalMoney.ToString();
                totalEscapedLabel.text = " " + TotalEscaped + " ";
                break;
        }
        
        DestroyEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        currentWave.text = "Волна " + (waveNumber + 1);
        StartCoroutine(Spawn());
        PlayBtn.gameObject.SetActive(false);
    }


    public void ShowMenu()
    {
        switch (currentState)
        {
            case gameStatus.gameOver:
                PlayBtnLabel.text = "Сыграть Ещё!";

                break;

            case gameStatus.next:
                PlayBtnLabel.text = "Продолжить";

                break;

            case gameStatus.play:
                PlayBtnLabel.text = "Начать игру";

                break;

            case gameStatus.win:
                PlayBtnLabel.text = "Начать игру";

                break;
        }
        PlayBtn.gameObject.SetActive(true);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDrag();
            TowerManager.Instance.towerBtnPressed = null;
        }
    }

   

  

    
}
