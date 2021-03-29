using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droppingUnits : MonoBehaviour
{
    public int team;
    public GameObject createPiece(GameObject name, GameObject tile)
    {
        GameObject newUnit;
        Vector3 position = new Vector3(tile.transform.position.x, tile.transform.position.y + 1f, tile.transform.position.z);
        newUnit = PhotonNetwork.Instantiate(name.name, position, Quaternion.identity);
        newUnit.GetComponent<unit>().setName(PhotonNetwork.LocalPlayer.NickName + "Main" + GetComponent<player>().Units.Count, team);
        //newUnit.GetComponent<unit>().myPlayer = this.gameObject;

        tile.GetComponent<tile>().setUnit(newUnit);
        addingUnit(newUnit);

        return newUnit;
    }
    public GameObject dropPiece(GameObject piece, GameObject tile)
    {
        return createPiece(piece, tile);
    }
    public void addingUnit(GameObject newUnit)
    {
        GetComponent<PhotonView>().RPC("addingUnitPhoton", RpcTarget.All, newUnit.name);
    }
    [PunRPC]
    public void addingUnitPhoton(string name)
    {
        GameObject newUnit = GameObject.Find(name);
        GetComponent<player>().Units.Add(newUnit);
    }
}
