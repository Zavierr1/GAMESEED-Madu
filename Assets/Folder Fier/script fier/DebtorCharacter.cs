using UnityEngine;

public class DebtorCharacter : MonoBehaviour
{
    [Header("Debtor Settings")]
    [SerializeField] private Debtor debtorData;
    [SerializeField] private DebtorUI debtorUI;
    [SerializeField] private float interactionRange = 3f;
    
    private bool hasBeenInteracted = false;
    private Transform player;
    
    private void Start()
    {
        // Find player (assuming player has "Player" tag)
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        
        // Set up UI
        if (debtorUI != null && debtorData != null)
        {
            debtorUI.SetDebtor(debtorData);
            debtorUI.SetUIPosition(transform.position);
        }
    }
    
    private void Update()
    {
        // Check if player is in range for interaction
        if (player != null && !hasBeenInteracted)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            
            if (distance <= interactionRange)
            {
                // Show interaction prompt (you can add UI for this)
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
                {
                    StartInteraction();
                }
            }
        }
    }
    
    public void StartInteraction()
    {
        if (hasBeenInteracted || debtorData == null) return;
        
        hasBeenInteracted = true;
        
        // Disable player movement and show cursor
        InteractionManager interactionManager = FindObjectOfType<InteractionManager>();
        if (interactionManager != null)
        {
            interactionManager.StartInteraction();
        }
        
        // Start dialogue using DialogueManager
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(debtorData);
        }
        else
        {
            Debug.LogError("DialogueManager instance not found!");
            return;
        }
        
        // Hide the debt UI since we're now interacting
        if (debtorUI != null)
        {
            debtorUI.gameObject.SetActive(false);
        }
    }
    
    public void SetDebtorData(Debtor newDebtor)
    {
        debtorData = newDebtor;
        if (debtorUI != null && debtorData != null)
        {
            debtorUI.SetDebtor(debtorData);
        }
    }
    
    // For manual interaction (e.g., clicking on the character)
    private void OnMouseDown()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactionRange)
            {
                StartInteraction();
            }
        }
    }
    
    // Visual debug for interaction range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
