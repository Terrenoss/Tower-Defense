using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Waypoint[] waypoints;
    private Enemy[] enemies = new Enemy[0];
    [SerializeField] private SpawnPoint spawnPoint;

    // [ADDITION] Essential internal data missing from the diagram.
    [SerializeField] private float spawnInterval = 0.8f;
    private int[] enemyWaypointIndex = new int[0];
    private int currentWave = -1;
    private bool isSpawning;

    // [ADDED] No public method in the diagram: UILaunchWave had nothing to call.
    public void StartNextWave()
    {
        // A single "enemies" array: launching a wave while another is in progress would overwrite the references.
        if (isSpawning || HasAliveEnemies())
        {
            Debug.Log("Vague en cours, attends qu'elle soit terminée.");
            return;
        }
        if (currentWave + 1 >= waves.Length)
        {
            Debug.Log("Toutes les vagues ont déjà été lancées.");
            return;
        }

        currentWave++;
        StartCoroutine(SpawnWave(waves[currentWave]));
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        int total = 0;
        foreach (WaveData data in wave.WaveData)
            total += data.Quantity;

        enemies = new Enemy[total];
        enemyWaypointIndex = new int[total];

        int index = 0;
        foreach (WaveData data in wave.WaveData)
        {
            for (int i = 0; i < data.Quantity; i++)
            {
                enemies[index++] = Instantiate(data.Enemy, spawnPoint.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        isSpawning = false;
    }

    private void Update()
    {
        MoveEnemies();
    }

    private void MoveEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];
            if (enemy == null || enemyWaypointIndex[i] >= waypoints.Length)
                continue;

            Vector3 target = waypoints[enemyWaypointIndex[i]].transform.position;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.MoveSpeed * Time.deltaTime);

            if (enemy.transform.position == target)
                enemyWaypointIndex[i]++;
        }
    }

    private bool HasAliveEnemies()
    {
        foreach (Enemy enemy in enemies)
            if (enemy != null)
                return true;
        return false;
    }

    // [ADD][DEBUG] Minimal display to allow gameplay.
    private void OnGUI()
    {
        bool victory = currentWave + 1 >= waves.Length && !isSpawning && !HasAliveEnemies();
        GUI.matrix = Matrix4x4.Scale(Vector3.one * Screen.height / 360f);
        GUI.Label(new Rect(10, 50, 400, 20), $"Vague : {currentWave + 1} / {waves.Length}" + (victory ? "  -  VICTOIRE !" : ""));
    }
}
