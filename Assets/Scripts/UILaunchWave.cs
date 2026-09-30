using UnityEngine;

public class UILaunchWave : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;

    public void LaunchWave() {
        waveManager.StartNextWave();
    }
}
