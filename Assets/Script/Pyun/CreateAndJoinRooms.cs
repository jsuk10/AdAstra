using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public InputField nicknameInput;

    public void CreateRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        
        PhotonNetwork.LoadLevel("InnerRoom");
    }
}
