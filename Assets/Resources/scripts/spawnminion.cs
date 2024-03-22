using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class spawnminion : MonoBehaviour
{
    private Dictionary<string, float> spawndata = new Dictionary<string, float>(); // dictionary with spawn data of minion
    private float targettime; // the time when the minion is spawned
    public int spawntime = 1; // dynamic spawn timer in minutes
    public float radiusmargin = 0.01f; // the radius margin for next cycle
    public Vector3 worldscale;

    // Start is called before the first frame update
    void Start()
    {
        worldscale = this.gameObject.transform.root.localScale;
        float radius = transform.GetComponent<NavMeshObstacle>().radius; // getting the radius of the capsule box
        Vector3 center = transform.GetComponent<NavMeshObstacle>().center; // getting the center of the capsule box
        spawndata = getcirclespaces(getdistance(Math.Cos(1) * radius + center.x, Math.Cos(2) * radius + center.x), scaleminion(worldscale, 5), 360f, radius); // getting the spawn data of the cycle
        targettime = 5f; // getting the time when the minion is spawned
    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.tag != playertags.neutral)
        {
            targettime -= Time.deltaTime; // calculating the timer

            // if time is up. spawn minion
            if (targettime <= 0.0f)
            {

                if (spawndata["minionspawn"] < spawndata["numberofspawns"]) // if not enough minions are spawned. spawn more
                {
                    // function to spawn minion at given coordinate
                    spawnminions(spawndata["sizetoangle"], spawndata["radius"], spawndata["minionspawn"], transform.GetComponent<NavMeshObstacle>().height);
                    spawndata["minionspawn"]++; // keeping track of minions spawned
                }
                else // if enough minions. get ready for next spawn cycle
                {
                    spawndata["radius"] = getradiusmargin(radiusmargin, spawndata["radius"]);
                    spawndata["minionspawn"] = 0;
                }

                targettime = 5f; // updating the timer
            }
        }
    }

    float getdistance(double angle1, double angle2)
    {
        var list = new List<double>();

        list.Add(Math.Abs(angle1));
        list.Add(Math.Abs(angle2));

        return (float)(list.Max() - list.Min());
    }

    // function to get the radius margin
    float getradiusmargin(float margin, float radius)
    {
        return radius + margin;
    }

    // function to get the spawn data
    Dictionary<string, float> getcirclespaces(float deltaareal, float minionareal, float maxangle, float radius)
    {
        float sizetoangle = minionareal / deltaareal;

        float numberofspawns = maxangle / sizetoangle;

        return new Dictionary<string, float>()
        {
            {"sizetoangle", sizetoangle },
            {"numberofspawns", numberofspawns },
            {"radius", radius },
            {"minionspawn", 0 }
        };
    }

    // function to spawn minions
    bool spawnminions(float sizetoangle, float radius, float nextspawn, float height)
    {
        Vector3 center = transform.GetComponent<NavMeshObstacle>().center;

        float angle = nextspawn * sizetoangle;
        double x = Math.Cos(angle) * radius + center.x;
        double y = Math.Sin(angle) * radius + center.z;

        createminion(new Vector3(((float)x), -0.00055f, (float)y));

        return true;
    }

    float scaleminion(Vector3 worldscale, int percentageof)
    {
        return (float)((Math.Abs(worldscale.x) / 100) * percentageof);
    }


    // function to create the minion
    void createminion(Vector3 position)
    {
        float minionscale = scaleminion(worldscale, 5);

        minion minion = Instantiate(crab.getprefab(), position, Quaternion.identity).GetComponent<minion>();
        minion.getcanvas().transform.localScale = new Vector3(minionscale, minionscale, minionscale);
        minion.getcanvas().name = crab.getname();
        minion.spawnminion();
        minion.getcanvas().transform.parent = this.transform;
        // find a way to set the scale at a universal scale

        if (NavMesh.CalculateTriangulation().vertices.Length > 0)
        {
            if (minion != null)
            {

                minion.getcanvas().tag = this.transform.tag;
                minion.getcanvas().transform.SetParent(this.transform, false);

                adjustnavmeshminion(minion.getcanvas(), position);

            }
        }
    }

    void adjustnavmeshminion(GameObject minion, Vector3 position)
    {
        GameObject map = this.transform.root.gameObject;

        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        // Get the center of the navmesh bounding box
        Bounds navMeshBounds = new Bounds(triangulation.vertices[0], Vector3.zero);
        foreach (Vector3 vertex in triangulation.vertices)
        {
            navMeshBounds.Encapsulate(vertex);
        }

        Vector3 offset = navMeshBounds.center - map.transform.position;

        // Apply the new position to the minion
        minion.transform.localPosition = new Vector3(position.x, offset.y, position.y);
        map.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();

    }
}
