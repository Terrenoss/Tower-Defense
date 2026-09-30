using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private WaveData[] waveData;

    // [ADDITION] Accessor: private field read by WaveManager.
    public WaveData[] WaveData => waveData;
}
