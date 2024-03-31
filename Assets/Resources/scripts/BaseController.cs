using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class BaseController : NetworkBehaviour
{

    // initiating fields
    private MeshCollider meshcollider;
    private GameObject basecircle;
    private minion minion;
    private ulong killertag;
    public bool init;

    // TODO: Make minion unclickable and unmoveable when inside the circle

    // Update is called once per frame
    void Update()
    {

        if (init)
        {
            if (!IsOwner) { return; }

            meshcollider = this.transform.parent.parent.GetComponent<MeshCollider>();
            basecircle = this.transform.gameObject;

            minion = initminion(basecircle);
            init = false;
        }

        // finding a minion when the base circle is empty

        //if (hasminion(basecircle) == false)
        //{
        //    if (killertag != null) {
        //        minion = setminion(getplayer(killertag, meshcollider));
        //    }
        //}
        //else
        //{
        //    minion killer = minion.gethealthbar().attackedby;

        //    if (killer != null)
        //    {
        //        killertag = killer.getcanvas().GetComponent<NetworkObject>().OwnerClientId;
        //    }
        //}
    }

    // function to set a minion
    minion setminion(minion minion)
    {
        if (minion != null)
        {
            minion.isclickable = false;
            minion.ismoveable = false;
            minion.getcanvas().transform.SetParent(basecircle.transform, false);
            minion.getcanvas().transform.localPosition = Vector3.zero;
            minion.getcanvas().GetComponent<NavMeshAgent>().isStopped = true;
            return minion;

        } else
        {
            return minion;
        }
    }

    // function to get a player detected from the mesh controller
    minion getplayer(ulong killedby, MeshCollider meshcollider)
    {
        minion player = null;
        // Check if the MeshCollider is not null
        if (meshcollider != null)
        {

            // getting a player inside the meshcollider

            Vector3 boxSize = meshcollider.bounds.size;
            Vector3 boxCenter = meshcollider.bounds.center;

            player = Physics.OverlapBox(boxCenter, boxSize / 2).Where(obj => obj.GetComponent<NetworkObject>().OwnerClientId == killertag).Select(obj => obj.transform.gameObject).FirstOrDefault().GetComponent<minion>();

            return player;
        }

        return player;
    }

        // check if circle has a minion
        bool hasminion(GameObject basecircle)
    {
        if (basecircle.transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    minion initminion(GameObject basecircle)
    {

        minion minion = Instantiate(crab.getprefab()).GetComponent<minion>();
        minion.getcanvas().transform.localScale = crab.getscale();
        minion.getcanvas().name = crab.getname();

        if (minion != null)
        {
            minion.isclickable = false;
            minion.ismoveable = false;
            minion.getcanvas().GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
            minion.getcanvas().transform.SetParent(basecircle.transform, false);
            minion.getcanvas().transform.localPosition = Vector3.zero;

            return minion;

        }

        return minion;
    }

    float scaleminion(Vector3 worldscale, int percentageof)
    {
        return (float)((Math.Abs(worldscale.x) / 100) * percentageof);
    }
}
