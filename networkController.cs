using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class networkController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    // Start is called before the first frame update

    public GameObject roomButton;

    public List<GameObject> roomButtons;
    void Start()
    {
        //Connect();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void joinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            createRoom();
        }
    }

    void ILobbyCallbacks.OnJoinedLobby()
    {
        Debug.Log("hello");
    }
 
    void ILobbyCallbacks.OnRoomListUpdate(List<RoomInfo> roomList)
    {

        Debug.Log("seeing this");
        int y = 42;
        Vector3 position = new Vector3(108, y, 199.8774F);
        foreach(RoomInfo room in roomList)
        {

            GameObject newRoom = Instantiate(roomButton, position, Quaternion.identity);
            newRoom.transform.parent = roomButton.transform.parent;
            newRoom.transform.position = position;
            newRoom.GetComponent<roomButton>().name = room.Name;
            newRoom.transform.name = room.Name;
            newRoom.SetActive(true);
            y -= 50;
        }
    }
    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.AddCallbackTarget(this);

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
    }
    public GameObject playerPrefab;
    private void createRoom()
    {
        RoomOptions newRoom = new RoomOptions();
        string name = PhotonNetwork.LocalPlayer.NickName + "'s Room";
        Debug.Log(name);
        newRoom.MaxPlayers = 3;
        newRoom.IsVisible = true;
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 300000; // in milliseconds. any high value for debug
        PhotonNetwork.CreateRoom(name, newRoom);
    }
    public override void OnJoinedRoom()
    {
        GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        if (PhotonNetwork.CurrentRoom.PlayerCount > 2)
        {
            //newPlayer.GetComponent<player>().spectator = true;
        }
    }

    public void endGame()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log(SceneManager.GetActiveScene().name.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }


    public void setName(string name)
    {
        PhotonNetwork.LocalPlayer.NickName = name;
    }
    public void joinRoom(string room)
    {
        PhotonNetwork.JoinRoom(room);
    }

}
