using System.Collections;
using UnityEngine;

public class NPCFightInteraction : MonoBehaviour
{
    [Header("Referensi Komponen")]
    public QTEManager qteManager;
    public QTEEvent myQteEvent; // Script QTEEvent yang ada di NPC ini
    public Animator npcAnimator;

    [Header("Referensi Player")]
    public MonoBehaviour playerMovementScript; // Ganti dengan nama script movement player-mu, mis: FirstPersonController
    public Animator playerCameraAnimator; // Animator yang ada di kamera FPP player

    [Header("Pengaturan Pertarungan")]
    public int maxPunches = 2; // Jumlah pukulan dalam sekuens
    private bool isFighting = false;
    private Transform playerTransform;

    void Start()
    {
        // Pastikan QTEEvent ter-setup untuk memanggil fungsi yang benar
        myQteEvent.onSuccess.AddListener(OnQTESuccess);
        myQteEvent.onFail.AddListener(OnQTEFail);
    }

    void Update()
    {
        // Kunci kamera player untuk selalu melihat ke arah NPC selama pertarungan
        if (isFighting && playerTransform != null)
        {
            Vector3 direction = transform.position - playerTransform.position;
            direction.y = 0; // Jaga agar kamera tidak miring ke atas/bawah
            Quaternion rotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    // 1. FUNGSI UNTUK MEMULAI INTERAKSI
    // Fungsi ini dipanggil dari script lain, misal dari player saat menekan tombol interaksi
    public void StartFight(Transform incomingPlayer)
    {
        if (isFighting) return;

        this.playerTransform = incomingPlayer.GetComponentInChildren<Camera>().transform; // Dapatkan transform kamera player
        StartCoroutine(FightSequence());
    }

    // 2. FUNGSI UNTUK SEKUENS PERTARUNGAN
    private IEnumerator FightSequence()
    {
        // Masuk ke Fight State
        isFighting = true;
        playerMovementScript.enabled = false; // Nonaktifkan movement player

        Debug.Log("Fight Started! Player can't move.");

        // Loop untuk pukulan
        for (int i = 0; i < maxPunches; i++)
        {
            // Tunggu sejenak sebelum pukulan berikutnya
            yield return new WaitForSeconds(1.5f);

            // 3. NPC Menjalankan Animasi Pukul
            npcAnimator.SetTrigger("Punch");
            Debug.Log("NPC starts punching...");

            // Beri jeda sedikit agar animasi mulai berjalan sebelum slow-mo
            yield return new WaitForSeconds(0.5f);

            // 4. Memulai QTE dengan Slow-mo
            qteManager.startEvent(myQteEvent);
            Debug.Log("QTE Started!");

            // Tunggu sampai event QTE selesai (baik berhasil maupun gagal)
            // Kita akan menunggu sampai event di 'onEnd' dari QTEEvent dipanggil
            // Cara mudahnya adalah menunggu sampai Time.timeScale kembali normal
            yield return new WaitUntil(() => Time.timeScale == 1.0f);

            Debug.Log("QTE Round " + (i + 1) + " finished.");
        }

        // 5. Keluar dari Fight State
        Debug.Log("Fight sequence over. Player can move again.");
        isFighting = false;
        playerMovementScript.enabled = true;
    }

    // Fungsi ini dipanggil oleh QTEEvent saat BERHASIL
    public void OnQTESuccess()
    {
        Debug.Log("QTE Success! Player dodges.");
        // Jalankan animasi dodge pada kamera player
        if (playerCameraAnimator != null)
        {
            playerCameraAnimator.SetTrigger("Dodge");
        }
    }

    // Fungsi ini dipanggil oleh QTEEvent saat GAGAL
    public void OnQTEFail()
    {
        Debug.Log("QTE Failed! Player gets hit.");
        // Di sini kamu bisa menambahkan logika jika player terkena pukulan (kurangi darah, efek layar merah, dll)
    }
}