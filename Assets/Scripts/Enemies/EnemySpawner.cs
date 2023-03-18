using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject Boss1;
    private bool isSpawning = false;
    private bool isSpawning3 = false;
    private int enemiesLeftInWave;
    private int waveNumber;
    private int enemiesPerWave;
    public TextMeshProUGUI enemiesLeftInWaveText;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI waveCompleteText;
    public bool enemy3Spawning = false;
    

    /* isWaveOver relates to spawning enemies, it is flipped to 
       true while the delay happens between waves */
 
    private bool isWaveOver;

    /* allEnemiesSpawned relates to the amount of enemies left in the wave.
     If the amount is <= 0 then it cancels the InvokeRepeating spawning
     and moves onto the next step */
    private bool allEnemiesSpawned;

    /* waveUpdated flips to true when all enemies are spawned and all enemies
      have been killed. It prevents the waves from infinitely ticking up after
      wave 1 has been beaten. It is flipped back to false after the next wave
      starts and the InvokeRepeating spawner restarts for the next wave */
    private bool waveUpdated;


    // Start is called before the first frame update
    void Start()
    {
        

        if (isWaveOver == false)
        {
            
            
            float spawnInterval = UnityEngine.Random.Range(3f, 3f);
            InvokeRepeating("SpawnEnemy", 3f, spawnInterval);

        }

        enemiesLeftInWave = 5;
        waveNumber = 1;
        enemiesPerWave = 5;

        allEnemiesSpawned = false;
        waveUpdated = false;

        waveCompleteText.enabled = false;
        

    }

    // Update is called once per frame
    void Update()
    {

        // Update Wave and Enemies UI
        enemiesLeftInWaveText.text = "Enemies Left: " + enemiesLeftInWave;
        waveNumberText.text = "Wave: " + waveNumber;
        waveCompleteText.text = "Wave " + waveNumber + " Complete";

       
        if (allEnemiesSpawned == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waveUpdated = true;
            allEnemiesSpawned = false;
                UpdateWave();
        }

      


    }
    private void SpawnEnemy()
    {
        if (isSpawning) return;
        isSpawning = true;

        //Get camera's position and size
        Camera camera = Camera.main;
        Vector3 cameraPos = camera.transform.position;
        float cameraWidth = camera.orthographicSize * camera.aspect;

        //calculate position of the right side of the screen
        Vector3 spawnPos = new Vector3(cameraPos.x + cameraWidth + 3, 0, 0);

        //randomly generate the y position of the enemy with an offset

        float offset = 1f;
        spawnPos.y = Random.Range(camera.orthographicSize * -1 + offset, camera.orthographicSize - offset);


        //Wave 1 Spawning
        if (waveNumber <=1 && enemiesLeftInWave > 0)
        {
            GameObject enemy = Instantiate(enemy1Prefab, spawnPos, Quaternion.identity);
            isSpawning = false;
            enemy.GetComponent<Enemy>().health += (5 * waveNumber);

            CancelInvoke("SpawnEnemy");
            float spawnInterval = UnityEngine.Random.Range(2f, 5f);
            InvokeRepeating("SpawnEnemy", spawnInterval, 0);
            enemiesLeftInWave--;
            
        }

        //Wave 3 Spawning
        if (waveNumber >= 2 && waveNumber <= 4 && enemiesLeftInWave > 0)
        {
            float spawnChance = Random.value;
            if (spawnChance <= 0.5)
            {
                GameObject enemy = Instantiate(enemy1Prefab, spawnPos, Quaternion.identity);
                isSpawning = false;
                enemy.GetComponent<Enemy>().health += (5 * waveNumber);
                CancelInvoke("SpawnEnemy");
                float spawnInterval = UnityEngine.Random.Range(2f, 5f);
                InvokeRepeating("SpawnEnemy", spawnInterval, 0);
                enemiesLeftInWave--;
            }
            else
            {
                GameObject enemy = Instantiate(enemy2Prefab, spawnPos, Quaternion.identity);
                isSpawning = false;
                enemy.GetComponent<Enemy>().health += (5 * waveNumber);

                CancelInvoke("SpawnEnemy");
                float spawnInterval = UnityEngine.Random.Range(2f, 5f);
                InvokeRepeating("SpawnEnemy", spawnInterval, 0);
                enemiesLeftInWave--;
            }

        }

            // Wave 5 Spawning

            if (waveNumber == 5 && enemiesLeftInWave > 0)
            {
                float spawnChance = Random.value;
                if (spawnChance < 0.5)
                {
                    GameObject enemy = Instantiate(enemy1Prefab, spawnPos, Quaternion.identity);
                    isSpawning = false;
                    enemy.GetComponent<Enemy>().health += (5 * waveNumber);

                CancelInvoke("SpawnEnemy");
                float spawnInterval = UnityEngine.Random.Range(2f, 5f);
                InvokeRepeating("SpawnEnemy", spawnInterval, 0);
                enemiesLeftInWave--;
                }
                else 
                {
                    GameObject enemy = Instantiate(enemy2Prefab, spawnPos, Quaternion.identity);
                    isSpawning = false;
                    enemy.GetComponent<Enemy>().health += (5 * waveNumber);

                CancelInvoke("SpawnEnemy");
                float spawnInterval = UnityEngine.Random.Range(2f, 5f);
                InvokeRepeating("SpawnEnemy", spawnInterval, 0);
                enemiesLeftInWave--;
                }
                
            }

            // Wave 6 (Boss Wave) Spawning

            if (waveNumber == 6 && enemiesLeftInWave > 0)
            {
            enemiesLeftInWave = 1;
            GameObject enemy = Instantiate(Boss1, spawnPos, Quaternion.identity);
            isSpawning = false;
            CancelInvoke("SpawnEnemy");
            float spawnInterval = UnityEngine.Random.Range(2f, 5f);
            InvokeRepeating("SpawnEnemy", spawnInterval, 0);
            enemiesLeftInWave--;
            
            }
            
            // Wave 7 onwards Spawning

            if (waveNumber >= 7 && enemiesLeftInWave > 0)
            {
            enemiesLeftInWave = 5 + (waveNumber * 5);
            float spawnChance = Random.value;
            if (spawnChance < 0.5)
            {
                GameObject enemy = Instantiate(enemy1Prefab, spawnPos, Quaternion.identity);
                isSpawning = false;
                enemy.GetComponent<Enemy>().health += (5 * waveNumber);

                CancelInvoke("SpawnEnemy");
                float spawnInterval = UnityEngine.Random.Range(2f, 5f);
                InvokeRepeating("SpawnEnemy", spawnInterval, 0);
                enemiesLeftInWave--;
            }
            else
            {
                GameObject enemy = Instantiate(enemy2Prefab, spawnPos, Quaternion.identity);
                isSpawning = false;
                enemy.GetComponent<Enemy>().health += (5 * waveNumber);

                CancelInvoke("SpawnEnemy");
                float spawnInterval = UnityEngine.Random.Range(2f, 5f);
                InvokeRepeating("SpawnEnemy", spawnInterval, 0);
                enemiesLeftInWave--;
            }
        }
        

        if (enemiesLeftInWave <= 0 && waveUpdated == false)
        {
            allEnemiesSpawned = true;
            CancelInvoke("SpawnEnemy");
            CancelInvoke("SpawnEnemy3");

        }

    }



    private void SpawnEnemy3()
    {
        if (isSpawning3) return;
        isSpawning3 = true;

        //Get camera's position and size
        Camera camera = Camera.main;
        Vector3 cameraPos = camera.transform.position;
        float cameraWidth = camera.orthographicSize * camera.aspect;

        //calculate position of the right side of the screen
        Vector3 spawnPos = new Vector3(cameraPos.x + cameraWidth + 2, 0, 0);

        //randomly generate the y position of the enemy with an offset

        float offset = 0.5f;
        spawnPos.y = Random.Range(camera.orthographicSize * -1 + offset, camera.orthographicSize - offset);

        if (waveNumber >= 5 && waveNumber !=6 && enemiesLeftInWave > 0)
        {
            GameObject enemy3 = Instantiate(enemy3Prefab, spawnPos, Quaternion.identity);
            isSpawning3 = false;
            enemy3.GetComponent<Enemy>().health += (5 * waveNumber);
            enemiesLeftInWave--;
        }

        if (enemiesLeftInWave <= 0 && waveUpdated == false)
        {
            allEnemiesSpawned = true;
            CancelInvoke("SpawnEnemy");
            CancelInvoke("SpawnEnemy3");

        }
    }


    void UpdateWave()
    {
        StartCoroutine(WaveDelay());
        

    }

    private IEnumerator WaveDelay()
    {
        isWaveOver = true;
        waveCompleteText.enabled = true;
        yield return new WaitForSeconds(5);
        waveCompleteText.enabled = false;
        isWaveOver = false;
        waveNumber++;
        enemiesPerWave += 5;
        enemiesLeftInWave = enemiesPerWave;
        allEnemiesSpawned = false;
        waveUpdated = false;
        float spawnInterval = UnityEngine.Random.Range(3f, 3f);
        InvokeRepeating("SpawnEnemy", 3f, spawnInterval);

        if (waveNumber >= 5 && waveNumber !=6)
        {
            InvokeRepeating("SpawnEnemy3", 2f, 7f);
        }
        
    }

}
