using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponEditor : ItemEditor {

    [MenuItem("DevBird/Weapon Editor")]
    public static void ShowWindow()
    {
        GetWindow<WeaponEditor>();
    }

    private void OnGUI()
    {
        DrawGUI<Weapon>();
    }

    public override void SetStats(ScriptableObject assetData, ScriptableObject asset)
    {
        Weapon itemData = assetData as Weapon;
        Weapon newItem = asset as Weapon;

        newItem.power = itemData.power;

        base.SetStats(assetData, asset);
    }

}
