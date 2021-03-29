using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    GameObject selectedUnit;
    private void Update()
    {
        selectedUnit = GetComponent<player>().selectedUnit;
    }
    public bool checkAttackRange(GameObject attackingObject)
    {
        GameObject.Find("Game Controller").GetComponent<Pathfinding>().FindPath(selectedUnit.transform.position, attackingObject.transform.position);
        if (GameObject.Find("Game Controller").GetComponent<Grid>().path.Count <=3)
        {
            return true;
        }
        return false;
    }
    public bool moving(GameObject tile)
    {
        GetComponent<player>().controller.GetComponent<Pathfinding>().FindPath(selectedUnit.transform.position, tile.transform.position);
        foreach (Node n in GetComponent<player>().controller.GetComponent<Grid>().path)
        {
            Debug.Log(n.worldPosition);

            if (selectedUnit.GetComponent<unit>().moveScore >= 1)
            {
                //GetComponent<player>().selectedUnit.GetComponent<tile>().removeUnit();
                GetComponent<player>().controller.getTileCoOrd(n.gridX, n.gridY).GetComponent<tile>().setUnit(selectedUnit);

                selectedUnit.GetComponent<unit>().moveScore--;
            }
        }
        //controller.getTileCoOrd(controller.GetComponent<Grid>().path[0].gridX, controller.GetComponent<Grid>().path[0].gridY).GetComponent<tile>().setUnit(selectedUnit);
        return true;
    }
    public void forceFogOfWar()
    {
        
        GetComponent<PhotonView>().RPC("createFogOfWar", RpcTarget.All);
        
    }
    [PunRPC]
    void createFogOfWar()
    {         
        if (GetComponent<ruleSet>().FOWActive == true)
        {
            if (GetComponent<player>().pView.IsMine)
            {
                List<GameObject> enemyUnits = GetComponent<player>().controller.enemyUnits();

                foreach (GameObject n in enemyUnits)
                {
                    n.GetComponent<unit>().seen = false;
                    n.SetActive(false);

                    foreach (GameObject m in GetComponent<player>().Units)
                    {
                        GameObject.Find("Game Controller").GetComponent<Pathfinding>().FindPath(m.GetComponent<unit>().myTile.transform.position, n.GetComponent<unit>().myTile.transform.position);
                        m.GetComponent<unit>().myAnimator.SetBool("canSEE", false);
                        if (GameObject.Find("Game Controller").GetComponent<Grid>().path.Count <= 3)
                        {
                            n.SetActive(true);
                            m.GetComponent<unit>().myAnimator.SetBool("canSEE", true);
                            n.GetComponent<unit>().seen = true;
                        }
                    }
                }
            }
        }
        
    }

    public bool ruleCheckMovement(GameObject unit)
    {
        if(GetComponent<ruleSet>().multiMoves == false)
        {
            if (unit.GetComponent<unit>().moved == false)
            {
                    unit.GetComponent<unit>().moved = true;
                    return true;
            }
        }
        else
        {
            unit.GetComponent<unit>().moveScore--;
            return true;
        }
        return false;
    }

    public bool ruleCheckAttack(GameObject unit)
    {
        if (unit.GetComponent<unit>().attacked == true)
        {
            return false;
        }
        else
        {
            if (GetComponent<ruleSet>().attackAswell == true)
            {
                Debug.Log("this far");
                if (unit.GetComponent<unit>().moveScore > 1)
                {
                    Debug.Log("also this");
                    unit.GetComponent<unit>().moveScore--;
                    unit.GetComponent<unit>().attacked = true;
                    return true;
                }
            }
            else
            {
                unit.GetComponent<unit>().attacked = true;
                return true;

            }
        }
        return false;
    }
}