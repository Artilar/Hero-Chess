using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject localPlayer;
    public PhotonView pView;
    public List<GameObject> champs;
    // Start is called before the first frame update

    public Material used;
    public Material local;
    public Material enemy;
    public Material selected;

    public int turn;
    public List<player> allPlayer;
    public player playerOne;
    public player playerTwo;
    public List<GameObject> tiles;
    public GameObject[,] tilesArray;
    public bool FOWActive;

    void Start()
    {
        FOWActive = false;
        turn = 1;
        tilesArray = new GameObject[8, 8];
        setTile();
    }
    public void setTile()
    {
        int amount = 0;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                tilesArray[y, x] = tiles[amount];
                amount++;
            }
        }
    }
    public bool difference(Vector3 tile, Vector3 moveTile)
    {
        Debug.Log(Vector3.Distance(tile, moveTile));
        if (Vector3.Distance(tile, moveTile) <= 1.5f)
        {
            return true;
        }
        return false;
    }
    public void changeTurn(player incoming)
    {
        pView.RPC("changeTurnPun", RpcTarget.All);
    }
    [PunRPC]
    public void changeTurnPun()
    {
        foreach (player p in allPlayer)
        {
            p.turnStarted = false;
            p.GetComponent<movement>().forceFogOfWar();

        }

        turn++;
        if(turn >= allPlayer.Count)
        {
            turn = 0;
        }
        //playerOne.GetComponent<movement>().forceFogOfWar();
    }
    // Update is called once per frame
    void Update()
    {
    }

    public GameObject getTileCoOrd(int x, int y)
    {
        foreach (GameObject n in tiles)
        {
            if (n.transform.position.x == x)
            {
                if (n.transform.position.z == y)
                {
                    return n;
                }
            }
        }
        return null;
    }

    public void addPlayer(string local)
    {
        pView.RPC("addPlayerPun", RpcTarget.AllBuffered, local);

    }
    [PunRPC]
    public void addPlayerPun(string local)
    {
        allPlayer.Add(GameObject.Find(local).GetComponent<player>());
    }

    public List<GameObject> enemyUnits()
    {
        List<GameObject> enemyUnits = new List<GameObject>();

        foreach(player p in allPlayer)
        {
            if (localPlayer != p.gameObject)
            {
                foreach(GameObject unit in p.Units)
                {
                    enemyUnits.Add(unit);

                }
            }
        }

        return enemyUnits;
    }

    void assignTeams()
    {
        pView.RPC("assignTeam", RpcTarget.AllBuffered);

    }
    [PunRPC]
    public void assignTeam()
    {
        for(int i = 0; i < allPlayer.Count; i++)
        {
            allPlayer[i].team = i;
        }
    }
}
