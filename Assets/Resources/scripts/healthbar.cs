using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    private Slider healthslider;
    public Transform player;
    public int health;
    public minion attackedby { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        healthslider = this.GetComponent<Slider>();
        this.GetComponent<Slider>().minValue = 0;
        this.GetComponent<Slider>().maxValue = health;
        this.GetComponent<Slider>().value = health;
    }

    // Update is called once per frame
    void Update()
    {

        if (healthslider.value <= 0) {
            death(player.GetComponent<minion>());
        }
    }

    public void damage(int dmage)
    {
       healthslider.value -= dmage;
    }

    public void heal(int heal)
    {
        healthslider.value += heal;
    }

    private void death(minion minion)
    {
        unitselections.instance.removeunit(minion);
        minion.isdead = true;
        Destroy(minion.getcanvas());
    }
}
