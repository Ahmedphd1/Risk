using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class unitclick : MonoBehaviour
{
    // setting up fields
    private LayerMask clickable;
    void Start()
    {
        clickable.value = 1 << LayerMask.NameToLayer(layers.unitclickable); // getting the unit layer
    }

    // Update is called once per frame
    void Update()
    {
        // if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift)) // if shift is pressed. select characters by shift
                {

                    print("shift click pressed");

                    minion minion = hit.collider.gameObject.GetComponent<minion>();

                    print(minion);

                    if (minion != null && minion.isclickable)
                    {
                        unitselections.instance.shiftclickselect(minion);
                    }
                }
                else // else slect by click
                {
                    minion minion = hit.collider.gameObject.GetComponent<minion>();

                    if (minion != null && minion.isclickable)
                    {
                        unitselections.instance.shiftclickselect(minion);
                    }
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift)) // if left clicked. deselect characters
                {

                    unitselections.instance.deselectall();
                }
            }
        }
    }
}
