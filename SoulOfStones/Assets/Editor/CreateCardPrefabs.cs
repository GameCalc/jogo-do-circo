using UnityEditor;
using UnityEngine;

class CreatePrefabFromSelected
{
    //fonte = http://wiki.unity3d.com/index.php?title=CreatePrefabFromSelected 
    const string menuTitle = "Cards/Create Card From Selected";
    const string menuTitle2 = "Cards/Update cards";

    /// <summary>
    /// Creates a prefab from the selected game object.
    /// </summary>
    [MenuItem(menuTitle)]
    static void CreatePrefab(GameObject o = null )
    {
        GameObject obj;
        if (o == null)
        {
            obj = Selection.activeGameObject;
        }
        else
        {
            obj = o;
        }
        
        string name = obj.name;

        Object prefab = EditorUtility.CreateEmptyPrefab("Assets/Prefabs/" + name + ".prefab");
        EditorUtility.ReplacePrefab(obj, prefab);
        AssetDatabase.Refresh();
    }
    [MenuItem(menuTitle2)]
    static void UpdataCards()
    {
        GameObject[] objs = Selection.gameObjects;
        foreach (GameObject o in objs){
            CreatePrefab(o);
        }
    }

    /// <summary>
    /// Validates the menu.
    /// </summary>
    /// <remarks>The item will be disabled if no game object is selected.</remarks>
    [MenuItem(menuTitle, true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null;
    }
    [MenuItem(menuTitle2, true)]
    static bool ValidateUpdataCards()
    {
        return Selection.activeGameObject != null;
    }
}
