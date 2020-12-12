using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemEditor : BaseEditor {

    public override void SetStats(ScriptableObject assetData, ScriptableObject asset)
    {
        Item itemData = assetData as Item;
        Item newItem = asset as Item;

        newItem.myName = itemData.myName;
        newItem.myIcon = itemData.myIcon;
        newItem.myQuality = itemData.myQuality;
        newItem.maxStackSize = itemData.maxStackSize;
        newItem.myDescription = itemData.myDescription;

    }


}
