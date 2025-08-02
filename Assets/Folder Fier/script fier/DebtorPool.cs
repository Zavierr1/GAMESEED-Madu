using UnityEngine;

public class DebtorPool : MonoBehaviour
{
    public static DebtorPool Instance;
    
    [SerializeField] private Debtor[] availableDebtors;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public Debtor GetRandomDebtor()
    {
        if (availableDebtors.Length == 0)
        {
            Debug.LogError("No debtors available in the pool!");
            return null;
        }
        
        int randomIndex = Random.Range(0, availableDebtors.Length);
        return availableDebtors[randomIndex];
    }
}