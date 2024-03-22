using System.Collections.Generic;
using UnityEngine;

public class unitselections : MonoBehaviour
{

    // setting up fields
    public List<minion> unitlist = new List<minion>();
    public List<minion> unitselected = new List<minion>();



    // creating unique instance
    private static unitselections _instance;
    public static unitselections instance { get { return _instance; } }

    void Awake()
    {
       if (_instance == null && _instance != this)
        {
            _instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    // selecting by click
    public void clickselect(minion unit)
    {
        if (unit != null)
        {
            deselectall();
            unitselected.Add(unit);
            unit.getquad().gameObject.SetActive(true);
        }
    }

    // selecting by shift
    public void shiftclickselect(minion unit)
    {
        if (unit != null)
        {
            print("unit is not null");
            if (!unitselected.Contains(unit))
            {
                unitselected.Add(unit);
                unit.getquad().gameObject.SetActive(true);
                print("quad is active");
            }
            else
            {
                unitselected.Remove(unit);
                unit.getquad().gameObject.SetActive(false);
            }
        }
    }

    // selecting by drag
    public void dragselect(minion unit)
    {
        if (unit != null)
        {
            if (!unitselected.Contains(unit))
            {
                unitselected.Add(unit);
                unit.getquad().gameObject.SetActive(true);
            }
        }
    }


    // de select characters
    public void deselectall()
    {
        foreach (var unit in unitselected)
        {
            if (unit != null)
            {
                unit.getquad().gameObject.SetActive(false);
            }
        }
        unitselected.Clear();
    }

    // de select one
    public void deselect(minion unit)
    {
        if (unit != null)
        {
            unit.getquad().gameObject.SetActive(false);
        }
    }

    public void removeunit(minion unit)
    {
        if (unit != null)
        {
            unitlist.Remove(unit);
            unitselected.Remove(unit);
        }
    }
}
