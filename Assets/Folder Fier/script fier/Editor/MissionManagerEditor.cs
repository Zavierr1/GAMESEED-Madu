using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MissionManager))]
public class MissionManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector
        DrawDefaultInspector();
        
        MissionManager missionManager = (MissionManager)target;
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Character Mapping Setup", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Assign prefabs from Assets/Bara/NPC/ folder to match characters:\n" +
                               "0. Andri (Arrogant) - Suggested: man.prefab or mas jenggot.prefab\n" +
                               "1. Bu Wati (Gentle) - Suggested: lady.prefab or maam.prefab\n" +
                               "2. Pak Riko (Cunning) - Suggested: om botak.prefab\n" +
                               "3. Yusuf (Aggressive) - Suggested: bocah punk.prefab\n" +
                               "4. Bu Rini (Humble) - Suggested: maam.prefab or lady.prefab\n" +
                               "5. Rizwan (Stubborn) - Suggested: man.prefab or mas jenggot.prefab", 
                               MessageType.Info);
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Auto-Setup Character Mappings"))
        {
            AutoSetupCharacterMappings(missionManager);
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Runtime Debug", EditorStyles.boldLabel);
        
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Test Setup Daily Missions"))
            {
                missionManager.SetupDailyMissions();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Test buttons available during Play mode", MessageType.Info);
        }
    }
    
    private void AutoSetupCharacterMappings(MissionManager missionManager)
    {
        // Try to find the NPC prefabs automatically
        string[] characterNames = { "Andri", "Bu Wati", "Pak Riko", "Yusuf", "Bu Rini", "Rizwan" };
        string[] suggestedPrefabs = { "man", "lady", "om botak", "bocah punk", "maam", "mas jenggot" };
        
        SerializedObject serializedObject = new SerializedObject(missionManager);
        SerializedProperty characterMappingsProperty = serializedObject.FindProperty("characterMappings");
        
        // Ensure array size is 6
        characterMappingsProperty.arraySize = 6;
        
        for (int i = 0; i < 6; i++)
        {
            SerializedProperty mappingProperty = characterMappingsProperty.GetArrayElementAtIndex(i);
            SerializedProperty characterNameProperty = mappingProperty.FindPropertyRelative("characterName");
            SerializedProperty prefabProperty = mappingProperty.FindPropertyRelative("prefab");
            SerializedProperty personalityProperty = mappingProperty.FindPropertyRelative("personality");
            
            // Set character name
            characterNameProperty.stringValue = characterNames[i];
            
            // Set personality
            personalityProperty.enumValueIndex = i; // Matches the enum order
            
            // Try to find and assign prefab
            string prefabPath = $"Assets/Bara/NPC/{suggestedPrefabs[i]}.prefab";
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            
            if (prefab != null)
            {
                prefabProperty.objectReferenceValue = prefab;
                Debug.Log($"Auto-assigned {suggestedPrefabs[i]}.prefab to {characterNames[i]}");
            }
            else
            {
                Debug.LogWarning($"Could not find prefab at {prefabPath}");
            }
        }
        
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(missionManager);
        
        Debug.Log("Character mappings auto-setup completed! Check the assignments and adjust if needed.");
    }
}
