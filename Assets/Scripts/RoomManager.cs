using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Photon.Pun.Demo.PunBasics;



public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;
    public GameObject player;
    [Space]
    public Transform spawnPoint;
    [Space]
    public GameObject roomCam;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
        
    }
    public override void OnConnectedToMaster() 
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected To Server");
        PhotonNetwork.JoinLobby();
    }
     public override void OnJoinedLobby() 
    {
        base.OnJoinedLobby();
        
        Debug.Log("In a Lobby");

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        // GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        // _player.GetComponent<PlayerSetup>().IsLocalPlayer();
    }
    public override void OnJoinedRoom() 
    {
        base.OnJoinedRoom();
        // PhotonNetwork.JoinOrCreateRoom("test", null, null);
        Debug.Log("We are connected in room");

        roomCam.SetActive(false);

        spawnPlayer();
    }
    public void spawnPlayer() {

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;

    }
}
