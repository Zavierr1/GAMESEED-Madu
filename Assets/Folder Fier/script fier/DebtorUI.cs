using UnityEngine;
using TMPro;

public class DebtorUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private TextMeshProUGUI debtAmountText;
    [SerializeField] private Vector3 uiOffset = new Vector3(0, 2f, 0);
    
    private Debtor debtor;
    private Camera playerCamera;
    
    private void Start()
    {
        playerCamera = Camera.main;
        
        // Make sure canvas faces camera
        if (worldCanvas != null)
        {
            worldCanvas.worldCamera = playerCamera;
        }
    }
    
    private void Update()
    {
        // Make UI face camera
        if (playerCamera != null && worldCanvas != null)
        {
            worldCanvas.transform.LookAt(playerCamera.transform);
            worldCanvas.transform.Rotate(0, 180, 0); // Flip to face camera correctly
        }
    }
    
    public void SetDebtor(Debtor newDebtor)
    {
        debtor = newDebtor;
        UpdateDebtDisplay();
    }
    
    private void UpdateDebtDisplay()
    {
        if (debtor != null && debtAmountText != null)
        {
            debtAmountText.text = $"${debtor.debtAmount:F0}";
        }
    }
    
    public void SetUIPosition(Vector3 worldPosition)
    {
        if (worldCanvas != null)
        {
            worldCanvas.transform.position = worldPosition + uiOffset;
        }
    }
}
