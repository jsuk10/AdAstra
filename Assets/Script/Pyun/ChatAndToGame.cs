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
    
    void Start()
    {
        RoomRenewal();
    }

    void Update()
    {
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) 
    {
        RoomRenewal();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) 
    {
        RoomRenewal();
    }

    public void RoomRenewal()
    {
        ListText.text = "";
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i+1  == PhotonNetwork.PlayerList.Length)?"":", ");
    }

    [PunRPC] //플레이어 속해있는방 모든 인원에게 전함
    void ChatPRC(string msg)
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
}
