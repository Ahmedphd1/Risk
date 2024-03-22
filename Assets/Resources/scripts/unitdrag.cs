using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitdrag : MonoBehaviour
{

    // setting fields
    Camera mycam;
    [SerializeField]
    RectTransform box;

    Rect selectionbox;
    Vector2 startposition;
    Vector2 endposition;
    void Start()
    {
        // setting fields
        mycam = Camera.main;
        startposition = Vector2.zero;
        endposition = Vector2.zero;
        drawvisual(); // setting up the draw visuals
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if mouse is clicked
        {

            // gettign the start position of the sprite
            startposition = Input.mousePosition;
            selectionbox = new Rect();
        }

        // getting the end position of the sprite
        if (Input.GetMouseButton(0)) 
        {
            endposition = Input.mousePosition;
            drawvisual();
            drawselection();
        }

        // removing the draw visual
        if (Input.GetMouseButtonUp(0))
        {
            selectunit();
            startposition = Vector2.zero;
            endposition = Vector2.zero;
            drawvisual();
        }
    }
    
    // setting the draw visual
    void drawvisual()
    {
        Vector2 boxstart = startposition;
        Vector2 boxend = endposition;

        Vector2 boxcenter = (boxstart + boxend) / 2;
        box.position = boxcenter;

        Vector2 boxsize = new Vector2(Mathf.Abs(boxstart.x - boxend.x), Mathf.Abs(boxstart.y - boxend.y));

        box.sizeDelta = boxsize;
    }

    // drawing the rectangle selection box
    void drawselection()
    {
        if (Input.mousePosition.x < startposition.x)
        {
            selectionbox.xMin = Input.mousePosition.x;
            selectionbox.xMax = startposition.x;
        } else
        {
            selectionbox.xMin = startposition.x;
            selectionbox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < startposition.y)
        {
            selectionbox.yMin = Input.mousePosition.y;
            selectionbox.yMax = startposition.y;
        }
        else
        {
            selectionbox.yMin = startposition.y;
            selectionbox.yMax = Input.mousePosition.y;
        }
    }

    // selecting the units inside the visual box
    void selectunit()
    {
        foreach (minion minion in unitselections.instance.unitlist)
        {
            if (minion != null && minion.isclickable)
            {
                if (selectionbox.Contains(mycam.WorldToScreenPoint(minion.getcanvas().transform.position)))
                {
                    unitselections.instance.dragselect(minion);
                }
            }
        }
    }
}
