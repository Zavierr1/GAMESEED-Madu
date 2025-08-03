using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private SimpleFPController controller;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            controller = player.GetComponent<SimpleFPController>();
            if (controller == null)
            {
                Debug.LogWarning("SimpleFPController tidak ditemukan di player!");
            }
        }
        else
        {
            Debug.LogWarning("Player tidak ditemukan! Pastikan sudah diberi tag 'Player'");
        }
    }

    public void StartInteraction()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (controller != null)
        {
            controller.enabled = false;
        }

        Debug.Log("Interaksi dimulai.");
    }

    public void EndInteraction()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("Interaksi selesai.");
    }
}
