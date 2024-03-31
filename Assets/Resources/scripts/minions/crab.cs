using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab : minion
{
    public static GameObject getprefab()
    {
        return Resources.Load<GameObject>("3d models/prefab/crab-canvas");
    }

    public static Vector3 getscale()
    {
        return new Vector3(0.009f, 0.009f, 0.009f);
    }

    public static string getname()
    {
        return "crab";
    }
}
