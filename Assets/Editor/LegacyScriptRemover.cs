using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class LegacyScriptRemover : EditorWindow
{
    // Replace string with the exact name of your script class
    private const string TargetScriptName = "MaterialCorroder";

    [MenuItem("Tools/Clean Remove Legacy Script")]
    public static void RemoveScriptFromScene()
    {
        // Find all GameObjects in the current scene, including inactive ones
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        int removalCount = 0;

        foreach (GameObject go in allObjects)
        {
            // Skip assets/prefabs in the project window; only modify scene objects
            if (EditorUtility.IsPersistent(go.transform.root.gameObject)) 
                continue;

            // Get the component dynamically using its class string name
            Component legacyComponent = go.GetComponent(TargetScriptName);

            if (legacyComponent != null)
            {
                // Registers the deletion with Unity's Undo system
                Undo.DestroyObjectImmediate(legacyComponent);
                removalCount++;
                
                // Marks the scene as dirty so Unity knows it needs to be saved
                EditorSceneManager.MarkSceneDirty(go.scene);
            }
        }

        Debug.Log($"Successfully removed '{TargetScriptName}' from {removalCount} GameObjects.");
        
        // Save the modified scene automatically
        EditorSceneManager.SaveOpenScenes();
    }
}
