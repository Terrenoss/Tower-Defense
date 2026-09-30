using UnityEngine;

[System.Serializable]
public struct WaveData
{
    [SerializeField] private int quantity;
    [SerializeField] private Enemy enemy;

    // [ADDITION] Accessors: the fields are private, yet WaveManager needs to read them.
    public int Quantity => quantity;
    public Enemy Enemy => enemy;
}
