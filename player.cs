using ExitGames.Client.Photon;
using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public bool spectator;
    public GameObject camera;
    public PhotonView pView;
    public gameController controller;
    public GameControllerState stateController;
    // Start is called before the first frame update
    public GameObject heroOne;
    public GameObject heroTwo;
    public GameObject heroThree;

    public GameObject unitSelection;
    public GameObject startButton;
    public List<GameObject> Units;
    public GameObject pawn;
    public GameObject selectedUnit;
    public GameObject secondaryUnit;
    public int team;
    public player otherPlayer;

    public bool pawnMove;
    public bool heroMove;

    public GameObject nextTurnButton;
    public void setHeroes(GameObject one, GameObject two, GameObject three)
    {
        heroOne = one;
        heroTwo = two;
        heroThree = three;
        stateController.playerPicked(this.gameObject);
        Destroy(unitSelection);
    }
    [PunRPC]
    void addUnit(string name)
    {
        GameObject newUnit = GameObject.Find(name);
        Units.Add(newUnit);
    }

    [PunRPC]
    void setName(string nickname, int pteam)
    {
        gameObject.name = nickname;
        pteam = team;
    }
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        stateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerState>();
        
        pView = GetComponent<PhotonView>();
        if (pView.IsMine == false)
        {
            Destroy(camera);
            Destroy(unitSelection);
        }
        else
        {
            pView.RPC("setName", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, PhotonNetwork.CountOfPlayersInRooms);

            controller.localPlayer = this.gameObject;
            controller.addPlayer(gameObject.name);
            //controller.getStats();
            GameObject.Find("Main Camera").SetActive(false);
        }
        
    }
   
    private void dropping()
    {
        if (pView.IsMine == true)
        {
            if(Units.Count < 8)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "tile")
                        {
                           
                                if (hit.transform.GetComponent<tile>().unit == null)
                                {
                                    if (Units.Count == 0)
                                    {
                                        GetComponent<droppingUnits>().dropPiece(heroOne, hit.transform.gameObject);
                                    }
                                    else if (Units.Count == 1)
                                    {
                                        GetComponent<droppingUnits>().dropPiece(heroTwo, hit.transform.gameObject);
                                    }
                                    else if (Units.Count == 2)
                                    {
                                        GetComponent<droppingUnits>().dropPiece(heroThree, hit.transform.gameObject);
                                    }
                                    else if (Units.Count > 2)
                                    {
                                        GetComponent<droppingUnits>().dropPiece(pawn, hit.transform.gameObject);
                                        if (Units.Count == 8)
                                        {
                                            startButton.SetActive(true);
                                            pView.RPC("setplayerDropped", RpcTarget.All);

                                        }
                                    }
                                }
                            
                        }
                    }
                }
            }
        }
    }
    public void setplayerReady()
    {
        pView.RPC("RPCplayerReady", RpcTarget.All);
    }
    [PunRPC]
    void RPCplayerReady()
    {
        if (pView.IsMine)
        {
            startButton.SetActive(false);
            stateController.playerReady(this.gameObject);
        }
    }
    [PunRPC]
    void setplayerDropped()
    {
        if(pView.IsMine)
        {
            stateController.playerDropped(this.gameObject);
        }
    }
    public void selectingUnit(GameObject incoming)
    {
        if (Units.Contains(incoming))
        {
            if (selectedUnit != null)
            {
                selectedUnit.GetComponent<unit>().selectedBool = false;
            }
            selectedUnit = incoming;
            selectedUnit.GetComponent<unit>().selectedBool = true;
        }
    }
    private void playing()
    {
       
    }
    // Update is called once per frame
    void Update()
         {
        if(spectator == true)
        {
            unitSelection.SetActive(false);

        }

        pView.RPC("stayActive", RpcTarget.All);
        if(spectator == false)
        {
            if (unitSelection != null)
            {
                if (stateController.myState == GameControllerState.gameState.picking)
                {
                    unitSelection.SetActive(true);
                }
                else
                {
                    unitSelection.SetActive(false);
                }
            }
            if (stateController.myState == GameControllerState.gameState.dropping)
            {
                dropping();
            }
            if (stateController.myState == GameControllerState.gameState.playing)
            {
                //GetComponent<movement>().forceFogOfWar();
                if (controller.turn == team)
                {
                    startTurn();

                    GetComponent<playing>().play();
                    //nextTurnButton.SetActive(true);
                }
                else
                {
                    //nextTurnButton.SetActive(false);
                }
            }
        }
    }
     public bool turnStarted;
    private void startTurn()
    {
        if(turnStarted == false)
        {
            foreach (GameObject unit in Units)
            {
                unit.GetComponent<unit>().moveScore = unit.GetComponent<unit>().baseMoveScore;
                unit.GetComponent<unit>().moved = false;
                unit.GetComponent<unit>().attacked = false;
            }
        }
        turnStarted = true;
    }
    public void moveCheck()
    {
        controller.changeTurn(this);
    }
    [PunRPC]
    void stayActive()
    {
        //Debug.Log("am active");
    }
}
