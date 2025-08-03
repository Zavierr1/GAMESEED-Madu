using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor menu items to quickly create pre-configured debtor assets
/// This makes it much easier to set up all six main characters with their complete dialogue trees
/// </summary>
public class DebtorCreationMenu
{
    [MenuItem("Assets/Create/Debt Collector/Andri (Arrogant Employee)")]
    public static void CreateAndriDebtor()
    {
        Debtor debtor = ScriptableObject.CreateInstance<Debtor>();
        DebtorDataHelper.ConfigureAndriData(debtor);
        
        string path = "Assets/Debtor_Andri.asset";
        AssetDatabase.CreateAsset(debtor, AssetDatabase.GenerateUniqueAssetPath(path));
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = debtor;
        
        Debug.Log("Created Andri debtor with complete dialogue tree");
    }
    
    [MenuItem("Assets/Create/Debt Collector/Bu Wati (Gentle Housewife)")]
    public static void CreateBuWatiDebtor()
    {
        Debtor debtor = ScriptableObject.CreateInstance<Debtor>();
        DebtorDataHelper.ConfigureBuWatiData(debtor);
        
        string path = "Assets/Debtor_BuWati.asset";
        AssetDatabase.CreateAsset(debtor, AssetDatabase.GenerateUniqueAssetPath(path));
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = debtor;
        
        Debug.Log("Created Bu Wati debtor with complete dialogue tree");
    }
    
    [MenuItem("Assets/Create/Debt Collector/Pak Riko (Cunning Businessman)")]
    public static void CreatePakRikoDebtor()
    {
        Debtor debtor = ScriptableObject.CreateInstance<Debtor>();
        DebtorDataHelper.ConfigurePakRikoData(debtor);
        
        string path = "Assets/Debtor_PakRiko.asset";
        AssetDatabase.CreateAsset(debtor, AssetDatabase.GenerateUniqueAssetPath(path));
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = debtor;
        
        Debug.Log("Created Pak Riko debtor with complete dialogue tree");
    }
    
    [MenuItem("Assets/Create/Debt Collector/Yusuf (Aggressive Gambler)")]
    public static void CreateYusufDebtor()
    {
        Debtor debtor = ScriptableObject.CreateInstance<Debtor>();
        DebtorDataHelper.ConfigureYusufData(debtor);
        
        string path = "Assets/Debtor_Yusuf.asset";
        AssetDatabase.CreateAsset(debtor, AssetDatabase.GenerateUniqueAssetPath(path));
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = debtor;
        
        Debug.Log("Created Yusuf debtor with complete dialogue tree");
    }
    
    [MenuItem("Assets/Create/Debt Collector/Bu Rini (Humble Vegetable Seller)")]
    public static void CreateBuRiniDebtor()
    {
        Debtor debtor = ScriptableObject.CreateInstance<Debtor>();
        DebtorDataHelper.ConfigureBuRiniData(debtor);
        
        string path = "Assets/Debtor_BuRini.asset";
        AssetDatabase.CreateAsset(debtor, AssetDatabase.GenerateUniqueAssetPath(path));
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = debtor;
        
        Debug.Log("Created Bu Rini debtor with complete dialogue tree");
    }
    
    [MenuItem("Assets/Create/Debt Collector/Rizwan (Stubborn Gacha Addict)")]
    public static void CreateRizwanDebtor()
    {
        Debtor debtor = ScriptableObject.CreateInstance<Debtor>();
        DebtorDataHelper.ConfigureRizwanData(debtor);
        
        string path = "Assets/Debtor_Rizwan.asset";
        AssetDatabase.CreateAsset(debtor, AssetDatabase.GenerateUniqueAssetPath(path));
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = debtor;
        
        Debug.Log("Created Rizwan debtor with complete dialogue tree");
    }
    
    [MenuItem("Assets/Create/Debt Collector/Create All Main Debtors")]
    public static void CreateAllMainDebtors()
    {
        CreateAndriDebtor();
        CreateBuWatiDebtor();
        CreatePakRikoDebtor();
        CreateYusufDebtor();
        CreateBuRiniDebtor();
        CreateRizwanDebtor();
        
        Debug.Log("Created all 6 main debtors with complete dialogue trees!");
    }
}
