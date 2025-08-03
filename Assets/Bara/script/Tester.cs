using UnityEngine;

public class Tester : MonoBehaviour
{
    public float interactionRange = 3f;
    public bool hasBeenInteracted = false;

    private Transform player;
    private bool isInteracting = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Pastikan player sudah diberi tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null || hasBeenInteracted) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange)
        {
            // Di dalam jangkauan interaksi
            if (Input.GetKeyDown(KeyCode.E))
            {
                var interactionManager = FindObjectOfType<InteractionManager>();
                if (interactionManager != null)
                {
                    if (!isInteracting)
                    {
                        interactionManager.StartInteraction();
                        isInteracting = true;
                        // hasBeenInteracted = true; // Uncomment jika hanya boleh interaksi sekali
                    }
                    else
                    {
                        interactionManager.EndInteraction();
                        isInteracting = false;
                    }
                }
            }
        }
    }
}
