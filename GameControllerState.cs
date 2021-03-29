using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerState : MonoBehaviour
{
    // Start is called before the first frame update
    public PhotonView pView;
    public enum gameState { waiting, picking, dropping, readyForPlay, playing}
    public gameState myState;

    public List<GameObject> playersPicked;
    public List<GameObject> playersDropped;
    public List<GameObject> playersReady;

    void Start()
    {
        playersPicked = new List<GameObject>();
        playersDropped = new List<GameObject>();
        playersReady = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myState == gameState.waiting)
        {
            if (PhotonNetwork.PlayerList.Length >= 2)
            {
                myState = gameState.picking;
            }
        }
        if (myState == gameState.picking)
        {
            if (playersPicked.Count == GetComponent<gameController>().allPlayer.Count)
            {
                GetComponent<gameController>().assignTeam();

                foreach (GameObject tile in GetComponent<gameController>().tiles)
                {
                    tile.GetComponent<tile>().setAvailability();
                }
                myState = gameState.dropping;
            }
        }
        if (myState == gameState.dropping)
        {
            if (playersDropped.Count == GetComponent<gameController>().allPlayer.Count)
            {
                myState = gameState.readyForPlay;
            }
        }
        if(myState == gameState.readyForPlay)
        {
            if (playersReady.Count == GetComponent<gameController>().allPlayer.Count)
            {
                myState = gameState.playing;
            }
        }
        if (myState == gameState.playing)
        {
            /*
            if (GetComponent<gameController>().playerOne.Units.Count == 0 || GetComponent<gameController>().playerTwo.Units.Count == 0)
            {
                GameObject.Find("networkController").GetComponent<networkController>().endGame();
            }
            */
        }
    }

    public void playerPicked(GameObject incoming)
    {
        pView.RPC("RPCPicked", RpcTarget.All, incoming.name);
    }
    [PunRPC]
    void RPCPicked(string incoming)
    {
        GameObject inc = GameObject.Find(incoming);
        playersPicked.Add(inc);
    }
    public void playerDropped(GameObject incoming)
    {
        pView.RPC("RPCDropped", RpcTarget.All, incoming.name);
    }
    [PunRPC]
    void RPCDropped(string incoming)
    {
        GameObject inc = GameObject.Find(incoming);
        playersDropped.Add(inc);
    }
    public void playerReady(GameObject incoming)
    {
            pView.RPC("RPCReady", RpcTarget.All, incoming.name);
    }
    [PunRPC]
    void RPCReady(string incoming)
    {
        GameObject inc = GameObject.Find(incoming);
        playersReady.Add(inc);
    }
}
