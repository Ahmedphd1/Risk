using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class attackarea : MonoBehaviour
{
    private float timetoattack = 2f;
    private float timer = 0;
    public Transform player;
    private bool startanimation = false;
    private minion enemy = default;

    void Update()
    {
        if (startanimation == false) {

            BoxCollider[] colliders = Physics.OverlapSphere(transform.position, transform.GetComponent<SphereCollider>().radius).Where(c => c.GetType() == typeof(BoxCollider) && c.transform.tag != player.GetComponent<minion>().getcanvas().tag)
            .Select(c => (BoxCollider)c)
            .ToArray();

            if (colliders.Length > 0)
            {
                enemy = colliders.OrderBy(c => Vector3.Distance(transform.position, c.transform.position)).First().gameObject.GetComponentInParent<minion>();
                player.GetComponent<minion>().getcanvas().transform.LookAt(enemy.getcanvas().transform);
                startanimation = true;
            }
        }

        if (startanimation && enemy.isdead == false) 
        {
            attack(enemy, player.GetComponent<minion>());
        } else
        {
            startanimation = false;
            enemy = null;
        }
    }

    void attack(minion enemy, minion player)
    {
        if (startanimation)
        {
            timer += Time.deltaTime;

            if (timer >= timetoattack)
            {
                enemy.gethealthbar().attackedby = player;

                enemy.gethealthbar().damage(10);
                timer = 0;
                startanimation = false;
            }
        }
    }
}
