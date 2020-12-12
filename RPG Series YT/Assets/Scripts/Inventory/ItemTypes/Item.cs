using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

    public string myName;
    public string myDescription;
    public Sprite myIcon;
    public int maxStackSize = 1;

    public Quality myQuality;

    public enum Quality
    {
        common,
        rare,
        epic,
        legendary
    }

    public Color GetQualityColor()
    {
        var myColor = new Color();

        switch (myQuality)
        {
            case Item.Quality.common:
                myColor = Color.grey;
                break;
            case Item.Quality.rare:
                myColor = Color.blue;
                break;
            case Item.Quality.epic:
                myColor = Color.magenta;
                break;
            case Item.Quality.legendary:
                myColor = Color.red;
                break;
        }

        return myColor;
    }
}
