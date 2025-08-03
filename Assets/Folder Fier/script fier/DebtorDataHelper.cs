using UnityEngine;

/// <summary>
/// Helper class to create pre-configured debtor data with all dialogue content
/// This makes it easier to set up the six main characters with their complete dialogue trees
/// </summary>
public static class DebtorDataHelper
{
    public static void ConfigureAndriData(Debtor debtor)
    {
        // Basic Info
        debtor.debtorName = "Andri";
        debtor.occupation = "Karyawan Swasta (Admin Finance)";
        debtor.personality = PersonalityType.Arrogant;
        debtor.debtAmount = 2000000f; // 2 million rupiah
        
        // First Dialogue
        debtor.introDialogue = "Maaf, Anda siapa ya?";
        
        debtor.firstNeutralOption = "Saya dari kantor penagihan. Boleh saya bicara sebentar soal tagihan Anda?";
        debtor.firstIntimidateOption = "Saya dari kantor penagihan. Sudah cukup main-main. Saatnya bayar!";
        debtor.firstPersuadeOption = "Pak Andri, saya paham kondisi sulit. Tapi kita bisa selesaikan ini dengan cara baik-baik.";
        
        // Responses to First Dialogue
        debtor.responseToNeutral = "Saya memang belum bayar, tapi uang saya pas-pasan. Bisa dikasih waktu?";
        debtor.responseToIntimidate = "Hah?! Siapa Anda berani ngomong kayak gitu ke saya?! Keluar dari rumah saya!";
        debtor.responseToPersuade = "Saya memang belum bayar, tapi uang saya pas-pasan. Bisa dikasih waktu?";
        
        // Second Dialogue Options
        debtor.secondPersuadeOption = "Sekarang Anda harus bayar lunas. Ini solusi terbaik untuk Anda.";
        debtor.secondIntimidateOption = "Kalau tidak bayar sekarang juga, saya akan lapor ke atasan. Siap ditagih terus-menerus?";
        debtor.secondNeutralOption = "Saya di sini cuma jalankan tugas. Ada cara yang bisa kita sepakati?";
        
        // Final Outcomes
        debtor.successDialogue = "Baiklah, saya transfer sekarang. Lunas ya, jangan ada tagihan lagi.";
        debtor.failureDialogue = "Udah cukup! Pergi dari rumah saya!";
    }
    
    public static void ConfigureBuWatiData(Debtor debtor)
    {
        // Basic Info
        debtor.debtorName = "Bu Wati";
        debtor.occupation = "Ibu Rumah Tangga";
        debtor.personality = PersonalityType.Gentle;
        debtor.debtAmount = 500000f; // 500 thousand rupiah
        
        // First Dialogue
        debtor.introDialogue = "Eh… maaf ya Nak, ini soal apa ya?";
        
        debtor.firstPersuadeOption = "Saya dari kantor penagihan. Kita cari jalan tengah, Bu.";
        debtor.firstIntimidateOption = "Ibu sudah terlalu lama menunda. Saatnya diselesaikan.";
        debtor.firstNeutralOption = "Saya tahu ini berat, tapi saya harus jalankan tugas.";
        
        // Responses to First Dialogue
        debtor.responseToPersuade = "Uang saya gak cukup… Tapi saya akan usahakan…";
        debtor.responseToIntimidate = "*menangis* Saya... saya gak punya uang... tolong jangan marah-marah...";
        debtor.responseToNeutral = "Uang saya gak cukup… Tapi saya akan usahakan…";
        
        // Second Dialogue Options
        debtor.secondNeutralOption = "Baik Bu, tolong bayar lunas hari ini.";
        debtor.secondPersuadeOption = "Ibu bisa pinjam tetangga mungkin? Harus lunas hari ini.";
        debtor.secondIntimidateOption = "Kalau gak lunas hari ini, saya harus lapor. Bisa ada konsekuensi besar lho.";
        
        // Final Outcomes
        debtor.successDialogue = "Makasih ya... Nih uangnya, lunas ya...";
        debtor.failureDialogue = "Anak saya! Ada orang jahat mau pukul mama! Tolong!";
    }
    
    public static void ConfigurePakRikoData(Debtor debtor)
    {
        // Basic Info
        debtor.debtorName = "Pak Riko";
        debtor.occupation = "Usahawan Rental Mobil";
        debtor.personality = PersonalityType.Cunning;
        debtor.debtAmount = 5000000f; // 5 million rupiah
        
        // First Dialogue
        debtor.introDialogue = "Oh, penagih hutang ya? Mau apa kamu di sini?";
        
        debtor.firstNeutralOption = "Pak, saya ingin menyelesaikan penagihan Anda dengan baik.";
        debtor.firstIntimidateOption = "Bayar sekarang, atau saya seret ke pengadilan.";
        debtor.firstPersuadeOption = "Kalau Bapak lunas hari ini, tidak ada catatan buruk untuk bisnis Anda.";
        
        // Responses to First Dialogue
        debtor.responseToNeutral = "Saya gak ada waktu. Mau saya bayar, berapa sih?";
        debtor.responseToIntimidate = "Hah?! Pengadilan? Kamu tahu gak siapa saya? Tunggu saya telepon lawyer!";
        debtor.responseToPersuade = "Saya gak ada waktu. Mau saya bayar, berapa sih?";
        
        // Second Dialogue Options
        debtor.secondNeutralOption = "Jumlahnya 5 juta. Saya bisa terima tunai atau transfer.";
        debtor.secondPersuadeOption = "Bayar sekarang, saya anggap lunas. Besok bisa beda ceritanya.";
        debtor.secondIntimidateOption = "Kalau gak sekarang, saya buat laporan. Nama Bapak bisa jelek.";
        
        // Final Outcomes
        debtor.successDialogue = "Nih transferan. Selesai kan? Jangan ganggu bisnis saya lagi.";
        debtor.failureDialogue = "Hello, lawyer? Ada debt collector abal-abal di sini. Datang sekarang!";
    }
    
    public static void ConfigureYusufData(Debtor debtor)
    {
        // Basic Info
        debtor.debtorName = "Yusuf";
        debtor.occupation = "Penjudi Slot Online";
        debtor.personality = PersonalityType.Aggressive;
        debtor.debtAmount = 1500000f; // 1.5 million rupiah
        
        // First Dialogue
        debtor.introDialogue = "Lagi apaan nih? Nanya-nanya hutang segala? Siapa lo?";
        
        debtor.firstNeutralOption = "Saya penagih hutang. Kita bisa bicara baik-baik.";
        debtor.firstIntimidateOption = "Lo pikir bisa kabur terus? Sekarang waktunya bayar.";
        debtor.firstPersuadeOption = "Gue tahu lo capek hidup kayak gini, bro. Tapi ini bisa lo benerin.";
        
        // Responses to First Dialogue
        debtor.responseToNeutral = "Gue baru kalah 1 juta, bro… Gak bisa bayar sekarang…";
        debtor.responseToIntimidate = "HAH?! MAU BERANTEM?! SINI GUE LAWAN!";
        debtor.responseToPersuade = "Gue baru kalah 1 juta, bro… Gak bisa bayar sekarang…";
        
        // Second Dialogue Options
        debtor.secondPersuadeOption = "Lo harus bayar lunas sekarang, bro. Gak ada tawar-menawar lagi.";
        debtor.secondNeutralOption = "Berapa pun cara lo, yang penting lunas hari ini.";
        debtor.secondIntimidateOption = "Kalau gak bisa bayar lunas, lo tau risikonya.";
        
        // Final Outcomes
        debtor.successDialogue = "Nih uangnya lengkap, lunas ya bro. Gue gak mau ribet lagi.";
        debtor.failureDialogue = "Gue kabur dulu bro! Lo gak bakal nemu gue!";
    }
    
    public static void ConfigureBuRiniData(Debtor debtor)
    {
        // Basic Info
        debtor.debtorName = "Bu Rini";
        debtor.occupation = "Penjual Sayur";
        debtor.personality = PersonalityType.Humble;
        debtor.debtAmount = 800000f; // 800 thousand rupiah
        
        // First Dialogue
        debtor.introDialogue = "Aduh, Mas... Ini soal utang, ya?";
        
        debtor.firstNeutralOption = "Iya, Bu. Saya cuma ingin bicara baik-baik soal pembayaran yang tertunda.";
        debtor.firstIntimidateOption = "Iya, soal utang yang Ibu belum bayar. Jangan pura-pura lupa.";
        debtor.firstPersuadeOption = "Saya tahu ini berat, Bu. Saya nggak datang buat marah, tapi cari solusi.";
        
        // Responses to First Dialogue
        debtor.responseToNeutral = "Saya akan berusaha bayar lunas. Dagangan sepi tapi saya akan cari cara…";
        debtor.responseToIntimidate = "*menangis* Tolong... saya benar-benar tidak punya... jangan sakiti saya...";
        debtor.responseToPersuade = "Saya akan berusaha bayar lunas. Dagangan sepi tapi saya akan cari cara…";
        
        // Second Dialogue Options
        debtor.secondPersuadeOption = "Tolong Bu, harus lunas hari ini. Saya yakin Ibu bisa cari cara.";
        debtor.secondIntimidateOption = "Kalau tidak bayar lunas hari ini, saya harus catat sebagai gagal bayar.";
        debtor.secondNeutralOption = "Kalau Ibu ada niat, tolong bayar lunas sekarang.";
        
        // Final Outcomes
        debtor.successDialogue = "Makasih ya Mas... Nih uangnya lengkap, lunas ya.";
        debtor.failureDialogue = "Saya gak sanggup! Jangan datang lagi ya…";
    }
    
    public static void ConfigureRizwanData(Debtor debtor)
    {
        // Basic Info
        debtor.debtorName = "Rizwan";
        debtor.occupation = "Remaja Gacha Addict";
        debtor.personality = PersonalityType.Stubborn;
        debtor.debtAmount = 1200000f; // 1.2 million rupiah
        
        // First Dialogue
        debtor.introDialogue = "Heh, utang apaan? Duit gue udah abis buat top up kemarin.";
        
        debtor.firstNeutralOption = "Saya tahu ini berat. Tapi ini soal utang lama. Bisa kita bicarakan?";
        debtor.firstIntimidateOption = "Masalahnya bukan duit lo buat top up, tapi duit orang tua lo yang harus dibalikin.";
        debtor.firstPersuadeOption = "Gacha nggak dapet SSR, utang juga numpuk? Hebat juga hidup lo.";
        
        // Responses to First Dialogue
        debtor.responseToNeutral = "Gue bisa bayar lunas sekarang. Lo percaya gak?";
        debtor.responseToIntimidate = "HAH?! NGAPAIN LO BAWA-BAWA ORTU GUE?! KELUAR!";
        debtor.responseToPersuade = "*marah* ANJIR! GUE LAGI RATE UP NIH! PERGI SONO!";
        
        // Second Dialogue Options
        debtor.secondNeutralOption = "Oke, saya percaya. Tapi harus lunas sekarang.";
        debtor.secondIntimidateOption = "Kalau sekarang gak lunas, siap-siap disamperin rame-rame.";
        debtor.secondPersuadeOption = "Gue butuh lunas sekarang, gak ada tawar-menawar lagi.";
        
        // Final Outcomes
        debtor.successDialogue = "Oke oke, nih uangnya lengkap. Lunas ya. Gue gak mau ribet.";
        debtor.failureDialogue = "Ah males banget lo. Gue blokir nomer lo nanti!";
    }
}
