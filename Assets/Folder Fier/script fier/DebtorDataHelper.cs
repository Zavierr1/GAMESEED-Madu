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
        debtor.secondPersuadeOption = "Bagaimana kalau Anda bayar separuh dulu hari ini, sisanya minggu depan?";
        debtor.secondIntimidateOption = "Kalau tidak bayar sekarang juga, saya akan lapor ke atasan. Siap ditagih terus-menerus?";
        debtor.secondNeutralOption = "Saya di sini cuma jalankan tugas. Ada cara yang bisa kita sepakati?";
        
        // Final Outcomes
        debtor.successDialogue = "Baiklah, saya transfer sekarang. Tapi tolong beri saya waktu minggu depan untuk sisanya.";
        debtor.failureDialogue = "Udah cukup! Pergi dari rumah saya!";
        
        // Personality traits
        debtor.intimidationResistance = 20;
        debtor.persuasionSusceptibility = 70;
        debtor.empathySusceptibility = 60;
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
        debtor.responseToPersuade = "Uang saya gak cukup… Saya cuma bisa kasih 50 ribu…";
        debtor.responseToIntimidate = "*menangis* Saya... saya gak punya uang... tolong jangan marah-marah...";
        debtor.responseToNeutral = "Uang saya gak cukup… Saya cuma bisa kasih 50 ribu…";
        
        // Second Dialogue Options
        debtor.secondNeutralOption = "Baik Bu, saya catat sebagai cicilan.";
        debtor.secondPersuadeOption = "Ibu bisa pinjam tetangga mungkin? Saya akan kembali sore.";
        debtor.secondIntimidateOption = "Kalau segini, saya harus lapor. Bisa ada konsekuensi besar lho.";
        
        // Final Outcomes
        debtor.successDialogue = "Makasih ya... Saya usahakan minggu depan bisa lebih...";
        debtor.failureDialogue = "Anak saya! Ada orang jahat mau pukul mama! Tolong!";
        
        // Personality traits
        debtor.intimidationResistance = 10;
        debtor.persuasionSusceptibility = 90;
        debtor.empathySusceptibility = 95;
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
        
        // Personality traits
        debtor.intimidationResistance = 60;
        debtor.persuasionSusceptibility = 75;
        debtor.empathySusceptibility = 30;
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
        debtor.secondPersuadeOption = "Gue kasih lo waktu 2 hari. Tapi jangan kabur.";
        debtor.secondNeutralOption = "Berapa lo bisa kasih sekarang? Sekecil apa pun.";
        debtor.secondIntimidateOption = "Kalau gak bisa bayar, lo tau risikonya.";
        
        // Final Outcomes
        debtor.successDialogue = "Nih 200 ribu, sisanya tunggu gue menang slot lagi ya bro...";
        debtor.failureDialogue = "Gue kabur dulu bro! Lo gak bakal nemu gue!";
        
        // Personality traits
        debtor.intimidationResistance = 30;
        debtor.persuasionSusceptibility = 60;
        debtor.empathySusceptibility = 70;
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
        debtor.responseToNeutral = "Saya baru bisa setor sebagian. Dagangan sepi…";
        debtor.responseToIntimidate = "*menangis* Tolong... saya benar-benar tidak punya... jangan sakiti saya...";
        debtor.responseToPersuade = "Saya baru bisa setor sebagian. Dagangan sepi…";
        
        // Second Dialogue Options
        debtor.secondPersuadeOption = "Bagaimana kalau kita buat cicilan ringan tiap minggu, Bu?";
        debtor.secondIntimidateOption = "Kalau tidak bayar hari ini, saya harus catat sebagai gagal bayar.";
        debtor.secondNeutralOption = "Kalau Ibu ada niat, saya bantu laporkan sebagai pembayaran awal.";
        
        // Final Outcomes
        debtor.successDialogue = "Makasih ya Mas... Saya usahakan tiap minggu setor sedikit.";
        debtor.failureDialogue = "Saya gak sanggup! Jangan datang lagi ya…";
        
        // Personality traits
        debtor.intimidationResistance = 15;
        debtor.persuasionSusceptibility = 85;
        debtor.empathySusceptibility = 90;
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
        debtor.responseToNeutral = "Gue bisa bayar, tapi minggu depan. Lo percaya gak?";
        debtor.responseToIntimidate = "HAH?! NGAPAIN LO BAWA-BAWA ORTU GUE?! KELUAR!";
        debtor.responseToPersuade = "*marah* ANJIR! GUE LAGI RATE UP NIH! PERGI SONO!";
        
        // Second Dialogue Options
        debtor.secondNeutralOption = "Oke, saya percaya. Tapi kasih DP dulu ya.";
        debtor.secondIntimidateOption = "Kalau minggu depan gak ada kabar, siap-siap disamperin rame-rame.";
        debtor.secondPersuadeOption = "Kalau bisa sekarang sedikit dulu, sisanya minggu depan.";
        
        // Final Outcomes
        debtor.successDialogue = "Oke, nih 50 ribu dulu. Sisanya minggu depan ya. Gue janji.";
        debtor.failureDialogue = "Ah males banget lo. Gue blokir nomer lo nanti!";
        
        // Personality traits
        debtor.intimidationResistance = 40;
        debtor.persuasionSusceptibility = 50;
        debtor.empathySusceptibility = 25;
    }
}
