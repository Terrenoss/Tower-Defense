using UnityEngine;

public class TowerShop : Interactable
{
    [SerializeField] private Tower[] towers;
    [SerializeField] private GameObject shopUI;

    // [ADDITION] No link to GameManager in the diagram: unable to pay for the tower.
    [SerializeField] private GameManager gameManager;

    public override void Interact()
    {
        shopUI.SetActive(true);
        // Use "=" instead of "+=": multiple TowerShops share the same ShopUI; only the last one clicked should build.
        // (This is only possible because ValidateBuy is a public Action field, not an event.)
        shopUI.GetComponent<ShopUI>().ValidateBuy = OnBuyTower;
    }

    // [MODIF] + int towerIndex (see ShopUI.ValidateBuy).
    private void OnBuyTower(int towerIndex)
    {
        Tower tower = towers[towerIndex];
        if (!gameManager.TrySpendMoney(tower.Cost))
        {
            Debug.Log($"Pas assez d'argent pour {tower.name} ({tower.Cost}).");
            return;
        }

        Instantiate(tower, transform.position, Quaternion.identity);
        shopUI.GetComponent<ShopUI>().ValidateBuy = null;
        shopUI.SetActive(false);
        gameObject.SetActive(false);
    }
}
