using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ChatAndToGame : MonoBehaviourPunCallbacks
{
    public Text ListText;
    public Text[] ChatText;
    public PhotonView PV;
    public InputField ChatInput;
    public Text TitleText;
    
    void Start()
    {
        RoomRenewal();
        ChatInput.text = "";
        TitleText.text = PhotonNetwork.CurrentRoom.Name;
        for(int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";
    }

    void Update()
    {
        
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
    
    [PunRPC] //플레이어 속해있는방 모든 인원에게 전함
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if(ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if(!isInput)
        {
            for(int i = 1; i < ChatText.Length; i++) ChatText[i-1].text = ChatText[i].text;
            ChatText[ChatText.Length-1].text = msg;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) 
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) 
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    public void RoomRenewal()
    {
        ListText.text = "";
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i+1  == PhotonNetwork.PlayerList.Length)?"":", ");
    }

    public void Send()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName+ " : "+ ChatInput.text);
        ChatInput.text = "";
    }


    [PunRPC]
    void GameRPC()
    {
        PhotonNetwork.LoadLevel("ElonScene");
    }

    public void clickToGame()
    {
        SoundManager.Instance.PlayBackGroundSound(2);
        PV.RPC("GameRPC", RpcTarget.All);
    }
}
