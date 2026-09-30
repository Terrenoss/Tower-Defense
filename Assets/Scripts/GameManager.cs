using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int money = 100;

    // [ADDITION] The diagram has an Enemy -> GameManager arrow, but no method to receive it.
    private void OnEnable()
    {
        Time.timeScale = 1f;
        Enemy.AddGold += OnAddGold;
    }

    private void OnDisable()
    {
        Enemy.AddGold -= OnAddGold;
    }

    private void OnAddGold(int amount)
    {
        money += amount;
    }

    // [ADDITION] 'money' is private and has no accessor method: it is impossible to buy a tower without it.
    public bool TrySpendMoney(int amount)
    {
        if (money < amount)
            return false;

        money -= amount;
        return true;
    }

    // [ADD][DEBUG] Minimal display to allow gameplay.
    private void OnGUI()
    {
        GUI.matrix = Matrix4x4.Scale(Vector3.one * Screen.height / 360f);
        GUI.Label(new Rect(10, 10, 300, 20), $"Argent : {money}");
    }
}
