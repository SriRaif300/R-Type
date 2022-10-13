using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<WaveProperties> waves;
    private int currentEnemy;
    private float rateEnemy;
    public bool ramdoWaves;

    public void CheckEnemies()
    {
        StartCoroutine(CheckEnemiesDelay());
    }
    IEnumerator CheckEnemiesDelay()
    {
        yield return new WaitForSeconds(0.1f);
        bool nextWave = true;
        for (int i = 0; i < waves[0].Enemies.Count; i++)
        {
            if(waves[0].Enemies[i].enemy != null)
            {
                nextWave = false;
                break;
            }
        }
        if(nextWave == true)
        {
            waves.RemoveAt(0);
            rateEnemy = 0;
            currentEnemy = 0;
        }
    }

    void Update()
    {
        if (waves.Count > 0)
        {
            if (currentEnemy < waves[0].Enemies.Count)
            {
                rateEnemy += Time.deltaTime;
                if (rateEnemy > waves[0].Enemies[currentEnemy].spawn)
                {
                    GameObject newEnemy = Instantiate(waves[0].Enemies[currentEnemy].enemy, new Vector2(9.5f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
                    waves[0].Enemies[currentEnemy].enemy = newEnemy;
                    if (newEnemy.GetComponent<Enemy1>() != null)
                    {
                        newEnemy.GetComponent<Enemy1>().manager = this;
                    }
                    else if (newEnemy.GetComponent<Enemy2>() != null)
                    {
                        newEnemy.GetComponent<Enemy2>().manager = this;
                    }
                    else if (newEnemy.GetComponent<Enemy3>() != null)
                    {
                        newEnemy.GetComponent<Enemy3>().manager = this;
                    }
                    rateEnemy = 0;
                    currentEnemy++;
                    
                }
            }
        }else
        {
            if(ramdoWaves == true){
                createWaves();
            }else
            {
                print("Fin del juego");
            }
        }
    }

    private void createWaves()
    {
        int toltalEnemies = Random.Range(1, 6);
        WaveProperties newWaves = new WaveProperties();
        newWaves.Enemies = new List<WaveProperties.EnemyProperties>();
        for (int i = 0; i < toltalEnemies; i++)
        {
            WaveProperties.EnemyProperties newEnemy = new WaveProperties.EnemyProperties();
            float spwnTime = Random.Range(0.5f, 2f);
            if (i == 0)
            {
                spwnTime = 5;
            }
            int ramdonEnemy = Random.Range(0, 3);
            GameObject getEnemy = Resources.Load<GameObject>("Enemy_" + ramdonEnemy);
            //Asigno los valores al nuevo enemigo
            newEnemy.spawn = spwnTime;
            newEnemy.enemy = getEnemy;
            newWaves.Enemies.Add(newEnemy);
        }
        //Guardo la oleada
        waves.Add(newWaves);
    }
}

[System.Serializable]
public class WaveProperties
{
    [System.Serializable]
    public class EnemyProperties
    {
        public float spawn;
        public GameObject enemy;
    }
    public List<EnemyProperties> Enemies;
}