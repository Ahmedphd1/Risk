using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class minion : MonoBehaviour
{
    public bool isclickable {  get; set; } = true;
    public bool ismoveable { get; set; } = true;

    public bool isdead { get; set; } = false;

    public void spawnminion()
    {
        this.GetComponent<NetworkObject>().Spawn(true);
    }

    public GameObject getquad()
    {
        return this.getminion().transform.Cast<Transform>().ToList().FirstOrDefault(child => child.tag == playertags.selectcircle).gameObject;
    }

    public healthbar gethealthbar()
    {
        return this.getminion().transform.Cast<Transform>().ToList().FirstOrDefault(child => child.tag == playertags.healthbarsystem).GetChild(0).GetComponent<healthbar>();
    }

    public GameObject getminion()
    {
        return this.transform.GetChild(0).gameObject;
    }

    public GameObject getcanvas()
    {
        return this.gameObject;
    }

    public bool settag(string tag)
    {
        this.tag = tag;

        if (this.tag != string.Empty)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public static float scaleby(int percentageof, float targetscale)
    {
        return ((targetscale / 100) * percentageof) + targetscale;
    }
}
