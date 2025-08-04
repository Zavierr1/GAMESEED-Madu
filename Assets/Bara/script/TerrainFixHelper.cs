using UnityEngine;

public class TerrainFixHelper : MonoBehaviour
{
    [Header("Debug Tools")]
    public bool fixWindIssues = false;
    public bool fixLODIssues = false;
    public bool fixCullingIssues = false;
    public bool showTerrainInfo = false;
    
    [Header("Manual Settings")]
    [Range(0f, 5f)]
    public float windStrength = 0.1f;
    [Range(50f, 1000f)]
    public float treeDistance = 250f;
    [Range(50f, 500f)]
    public float detailDistance = 100f;

    void Start()
    {
        if (fixWindIssues) FixWindProblems();
        if (fixLODIssues) FixLODProblems();
        if (fixCullingIssues) FixCullingProblems();
        if (showTerrainInfo) ShowTerrainDebugInfo();
    }

    void Update()
    {
        // Apply settings in real-time when changed in inspector
        if (Application.isEditor)
        {
            UpdateWindSettings();
            UpdateTerrainSettings();
        }
    }

    void FixWindProblems()
    {
        // Find and fix wind zones
        WindZone[] windZones = FindObjectsOfType<WindZone>();
        foreach (WindZone wind in windZones)
        {
            Debug.Log($"Found WindZone: Main={wind.windMain}, Turbulence={wind.windTurbulence}");
            
            // Reduce wind to minimal values
            wind.windMain = windStrength;
            wind.windTurbulence = windStrength * 0.5f;
            wind.windPulseMagnitude = 0.1f;
            wind.windPulseFrequency = 0.01f;
            
            Debug.Log($"Fixed WindZone: Main={wind.windMain}, Turbulence={wind.windTurbulence}");
        }
        
        if (windZones.Length == 0)
        {
            Debug.Log("No WindZones found - wind is not the issue");
        }
    }

    void FixLODProblems()
    {
        // Find all LOD Groups in the scene (including on trees)
        LODGroup[] lodGroups = FindObjectsOfType<LODGroup>();
        foreach (LODGroup lodGroup in lodGroups)
        {
            Debug.Log($"Found LODGroup on: {lodGroup.gameObject.name}");
            
            // Get current LOD levels
            LOD[] lods = lodGroup.GetLODs();
            
            // Adjust LOD distances to be less aggressive
            for (int i = 0; i < lods.Length; i++)
            {
                if (i == 0) lods[i].screenRelativeTransitionHeight = 0.6f; // Keep high detail longer
                else if (i == 1) lods[i].screenRelativeTransitionHeight = 0.3f;
                else if (i == 2) lods[i].screenRelativeTransitionHeight = 0.1f;
                else lods[i].screenRelativeTransitionHeight = 0.01f;
            }
            
            lodGroup.SetLODs(lods);
            Debug.Log($"Fixed LOD distances for: {lodGroup.gameObject.name}");
        }
    }

    void FixCullingProblems()
    {
        // Find and fix terrain culling distances
        Terrain[] terrains = FindObjectsOfType<Terrain>();
        foreach (Terrain terrain in terrains)
        {
            Debug.Log($"Found Terrain: {terrain.name}");
            Debug.Log($"Current Tree Distance: {terrain.treeDistance}");
            Debug.Log($"Current Detail Distance: {terrain.detailObjectDistance}");
            
            // Increase culling distances
            terrain.treeDistance = treeDistance;
            terrain.detailObjectDistance = detailDistance;
            terrain.treeBillboardDistance = treeDistance * 0.8f;
            terrain.treeCrossFadeLength = 25f;
            
            Debug.Log($"Fixed Tree Distance: {terrain.treeDistance}");
            Debug.Log($"Fixed Detail Distance: {terrain.detailObjectDistance}");
        }
    }

    void UpdateWindSettings()
    {
        WindZone[] windZones = FindObjectsOfType<WindZone>();
        foreach (WindZone wind in windZones)
        {
            wind.windMain = windStrength;
            wind.windTurbulence = windStrength * 0.5f;
        }
    }

    void UpdateTerrainSettings()
    {
        Terrain[] terrains = FindObjectsOfType<Terrain>();
        foreach (Terrain terrain in terrains)
        {
            terrain.treeDistance = treeDistance;
            terrain.detailObjectDistance = detailDistance;
            terrain.treeBillboardDistance = treeDistance * 0.8f;
        }
    }

    void ShowTerrainDebugInfo()
    {
        Debug.Log("=== TERRAIN DEBUG INFO ===");
        
        Terrain[] terrains = FindObjectsOfType<Terrain>();
        foreach (Terrain terrain in terrains)
        {
            Debug.Log($"Terrain: {terrain.name}");
            Debug.Log($"  Tree Distance: {terrain.treeDistance}");
            Debug.Log($"  Detail Distance: {terrain.detailObjectDistance}");
            Debug.Log($"  Billboard Distance: {terrain.treeBillboardDistance}");
            Debug.Log($"  Cross Fade Length: {terrain.treeCrossFadeLength}");
            Debug.Log($"  Tree Count: {terrain.terrainData.treeInstanceCount}");
        }
        
        WindZone[] windZones = FindObjectsOfType<WindZone>();
        foreach (WindZone wind in windZones)
        {
            Debug.Log($"WindZone: {wind.name}");
            Debug.Log($"  Main: {wind.windMain}");
            Debug.Log($"  Turbulence: {wind.windTurbulence}");
            Debug.Log($"  Pulse Magnitude: {wind.windPulseMagnitude}");
            Debug.Log($"  Pulse Frequency: {wind.windPulseFrequency}");
        }
    }

    [ContextMenu("Force Fix All Issues")]
    public void ForceFixAll()
    {
        Debug.Log("=== FIXING ALL TERRAIN ISSUES ===");
        FixWindProblems();
        FixLODProblems();
        FixCullingProblems();
        Debug.Log("=== ALL FIXES APPLIED ===");
    }
}
