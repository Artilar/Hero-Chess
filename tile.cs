using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
public class tile : MonoBehaviour
{
    public PhotonView photonView;

    // Start is called before the first frame update
    public GameObject unit;

    public Material baseMat;


    public string team;
    void Start()
    {
        baseMat = GetComponent<MeshRenderer>().material;

        photonView = GetComponent<PhotonView>();
    }
    public bool setUnit(GameObject pUnit)
    {
        if(unit == null)
        {
            photonView.RPC("setUnitPUN", RpcTarget.AllBuffered, pUnit.name);
            return true;
        }
        else
        {
            return false;
        }
    }
    [PunRPC]
    public void setUnitPUN(string objectname)
    {
        GameObject pUnit = GameObject.Find(objectname);
        if(pUnit.GetComponent<unit>().myTile != null)
        {
            pUnit.GetComponent<unit>().myTile.GetComponent<tile>().unit = null;
            pUnit.GetComponent<unit>().myTile = null;

        }
        unit = pUnit;
        Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);

        //pUnit.transform.position = position;
        pUnit.GetComponent<unit>().myTile = this.gameObject;
        pUnit.GetComponent<unitController>().targetPosition = position;
    }
    public void removeUnit()
    {
        if(unit != null)
        {
            photonView.RPC("RPCRemove", RpcTarget.All);
        }
    }
    [PunRPC]
    public void RPCRemove()
    {
        unit = null;
    }

    public void setAvailability()
    {
            if(GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().localPlayer != null)
            {
                if (GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().localPlayer.name == team)
                {
                    //GetComponent<MeshRenderer>().material = available;
                }
            }
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().localPlayer != null)
        {
            if (unit != null)
            {
                if (GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().localPlayer.GetComponent<player>().Units.Contains(unit))
                {
                    if (GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().localPlayer.GetComponent<player>().selectedUnit == unit)
                    {
                        GetComponent<MeshRenderer>().material = GameObject.Find("Game Controller").GetComponent<gameController>().selected;
                    }
                    else if (unit.GetComponent<unit>().moved == true)
                    {
                        GetComponent<MeshRenderer>().material = GameObject.Find("Game Controller").GetComponent<gameController>().used;
                    }
                    else
                    {
                        GetComponent<MeshRenderer>().material = GameObject.Find("Game Controller").GetComponent<gameController>().local;
                    }
                }
                else
                {
                    GetComponent<MeshRenderer>().material = GameObject.Find("Game Controller").GetComponent<gameController>().enemy;
/*
                    if (unit.GetComponent<unit>().seen == true)
                    {
                    }
                    else
                    {
                        GetComponent<MeshRenderer>().material = baseMat;
                    }
*/

                }
            }
            else
            {
                GetComponent<MeshRenderer>().material = baseMat;

            }
        }

    }
}
