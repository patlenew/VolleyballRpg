using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Layer
{
    public static int Tile { private set; get; }

    public static int Character { private set; get; }

    public static int BoardRelated { private set; get; }

    public static void InitLayers()
    {
        Tile = 1 << LayerMask.NameToLayer("Tile");
        Character = 1 << LayerMask.NameToLayer("Tile");

        BoardRelated = Tile;
        BoardRelated += Character;
    }
}
