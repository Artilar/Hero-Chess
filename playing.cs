using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playing : MonoBehaviour
{
    // Start is called before the first frame update
    public bool activateAbility;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if(Input.GetKeyDown("q"))
            {
                if (GetComponent<player>().selectedUnit != null)
                {
                    if(GetComponent<player>().selectedUnit.GetComponent<unit>().myAbility != null)
                    {
                        activateAbility = true;
                        runAbility();
                    }
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = GetComponent<player>().camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "unit")
                    {
                        GetComponent<player>().selectingUnit(hit.transform.gameObject);
                    }
                    if (hit.transform.tag == "tile")
                    {
                        moveUnit(hit.transform.gameObject);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = GetComponent<player>().camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "unit")
                    {
                        selectSecondUnit(hit.transform.gameObject);
                    }
                   
                }
            }
        }
    }

    public void moveUnit(GameObject tile)
    {
        if (GetComponent<player>().selectedUnit != null)
        {
            if (tile.transform.GetComponent<tile>().unit == null)
            {
                if (GetComponent<movement>().ruleCheckMovement(GetComponent<player>().selectedUnit) == true)
                {
                    if (GetComponent<movement>().moving(tile.transform.gameObject) == true)
                    {
                        GetComponent<movement>().forceFogOfWar();
                    }
                }
            }
        }
    }

    public void selectSecondUnit(GameObject unit)
    {
        if (GetComponent<player>().Units.Contains(unit.transform.gameObject))
        {
            GetComponent<player>().secondaryUnit = unit.transform.gameObject;
        }
        else if (GetComponent<player>().controller.enemyUnits().Contains(unit.transform.gameObject))
        {
            if(activateAbility == true)
            {
                runAbility(unit.transform.gameObject);
            }
            else if (GetComponent<player>().selectedUnit != null)
            {
                attack(unit);
            }
        }
    }

    public void attack(GameObject target)
    {
        if (GetComponent<movement>().checkAttackRange(target))
        {
            if (GetComponent<movement>().ruleCheckAttack(GetComponent<player>().selectedUnit))
            {
                GetComponent<player>().selectedUnit.transform.gameObject.GetComponent<unitController>().attack(target);
            }
        }
    }
    public void runAbility()
    {
        if(GetComponent<player>().selectedUnit.GetComponent<unit>().myAbility.targeted == false)
        {
            GetComponent<player>().selectedUnit.GetComponent<AbilityController>().runAbility(GetComponent<player>().selectedUnit);
        }
        else if (GetComponent<player>().selectedUnit.GetComponent<unit>().myAbility.targeted == true)
        {
            GetComponent<player>().selectedUnit.GetComponent<AbilityController>().runAbility(GetComponent<player>().selectedUnit);
        }
    }
    public void runAbility(GameObject unit)
    {
        if (GetComponent<player>().selectedUnit.GetComponent<unit>().myAbility.targeted == false)
        {
            GetComponent<player>().selectedUnit.GetComponent<AbilityController>().runAbility(GetComponent<player>().selectedUnit);
        }
        else if (GetComponent<player>().selectedUnit.GetComponent<unit>().myAbility.targeted == true)
        {
            GetComponent<player>().selectedUnit.GetComponent<AbilityController>().runAbility(unit);
        }
    }
}
