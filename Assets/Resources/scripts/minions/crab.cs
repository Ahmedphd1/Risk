using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab : minion
{
    public static GameObject getprefab()
    {
        return Resources.Load<GameObject>("3d models/characters/prefabs/crab-canvas");
    }

    public static string getname()
    {
        return "crab";
    }
}
