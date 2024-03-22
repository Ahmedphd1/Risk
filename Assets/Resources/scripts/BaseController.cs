using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BaseController : MonoBehaviour
{

    // initiating fields
    public MeshCollider meshcollider;
    public Renderer surfacerenderer;
    public GameObject worldmap;
    public GameObject building;
    private GameObject basecircle;
    private minion minion;
    private string killertag;

    // TODO: Make minion unclickable and unmoveable when inside the circle
    void Start()
    {
        basecircle = this.transform.GetChild(0).gameObject;

        minion = initminion(worldmap, basecircle, building.tag);
    }

    // Update is called once per frame
    void Update()
    {
        // finding a minion when the base circle is empty
        if (hasminion(basecircle) == false)
        {
            minion = setminion(getplayer(killertag, meshcollider));

            if (minion != null)
            {
                setbasetag(minion.getcanvas().tag);
            }
        }
        else
        {
            minion killer = minion.gethealthbar().attackedby;

            if (killer != null)
            {
                killertag = killer.getcanvas().tag;
            }
        }
    }

    private void setbasetag(string basetag)
    {
        if (basetag == playertags.player1) {
            building.tag = playertags.player1;
        } else if (basetag == playertags.player2)
        {
            building.tag = playertags.player2;
        }
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
    minion getplayer(string killedby, MeshCollider meshcollider)
    {
        minion player = null;
        // Check if the MeshCollider is not null
        if (meshcollider != null)
        {

            // getting a player inside the meshcollider

            Vector3 boxSize = meshcollider.bounds.size;
            Vector3 boxCenter = meshcollider.bounds.center;

            if (killedby != null)
            {
                player = Physics.OverlapBox(boxCenter, boxSize / 2).Where(obj => obj.transform.tag.Contains(playertags.anyplayer) && obj.transform.tag == killedby).Select(obj => obj.transform.gameObject).FirstOrDefault().GetComponent<minion>();
            }

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

    minion initminion(GameObject worldmap, GameObject basecircle, string basetag)
    {
        float minionscale = scaleminion(worldmap.transform.localScale, 5);

        minion minion = Instantiate(crab.getprefab()).GetComponent<minion>();
        minion.getcanvas().transform.localScale = new Vector3(minionscale, minionscale, minionscale);
        minion.getcanvas().name = crab.getname();

        if (NavMesh.CalculateTriangulation().vertices.Length > 0)
        {
            if (minion != null)
            {
                minion.isclickable = false;
                minion.ismoveable = false;
                minion.getcanvas().tag = basetag;
                minion.spawnminion();
                minion.getcanvas().transform.SetParent(basecircle.transform, false);
                minion.getcanvas().transform.localPosition = Vector3.zero;
                worldmap.GetComponent<NavMeshSurface>().BuildNavMesh();

                return minion;

            }

            return minion;
        }

        return minion;
    }

    float scaleminion(Vector3 worldscale, int percentageof)
    {
        return (float)((Math.Abs(worldscale.x) / 100) * percentageof);
    }
}
