using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Linq;

public class movement : MonoBehaviour
{
    Customactions input; // creating a custom action for left click
    [Header("Movement")]
    public ParticleSystem clickEffect; // the click effect sprite
    public LayerMask clickableLayers; // the layers that is clickable
    public int speed = 5; // the speed of the minion to travel at
    [SerializeField] Transform minion;

    void Awake()
    {
        // getting the fields
        input = new Customactions();
    }

    private void Start()
    {
        minion.GetComponent<NavMeshAgent>().speed = speed;
        clickableLayers.value = 1 << LayerMask.NameToLayer(layers.floor);
        AssignInputs(); // trigger for when left button is clicked

        unitselections.instance.unitlist.Add(minion.GetComponent<minion>());
    }

    // function to detect when left click is clicked
    void AssignInputs()
    {
        input.rightclick.clickmove.performed += ctx => ClickToMove();
    }

    // function to move the character when button is clicked
    void ClickToMove()
    {
        RaycastHit hit; // getting the raycast

        if (minion != null)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers) && minion.GetComponent<minion>().getquad().active && minion.GetComponent<minion>().ismoveable)
            {
                minion.GetComponent<minion>().GetComponent<NavMeshAgent>().destination = hit.point; // setting the destination by calculating the raycast coordinates
            }
        }
    }

    private void OnDestroy()
    {
        unitselections.instance.unitlist.Remove(minion.GetComponent<minion>());
    }

    void OnEnable()
    { input.Enable(); }

    void OnDisable()
    { input.Disable(); }

}
