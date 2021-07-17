using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

namespace Be.CoDek.Nyx
{
[RequireComponent(typeof(PhotonView))]
public class ChatManager : MonoBehaviourPunCallbacks, IPunObservable
{
public Rect GuiRect = new Rect(0, 0, 250, 300);
public bool IsVisible = true;
public bool AlignBottom = false;
public List<string> messages = new List<string>();
private string inputLine = "";
private Vector2 scrollPos = Vector2.zero;


public static readonly string ChatRPC = "Chat";

public void Start()
{
    if (this.AlignBottom)
    {
    this.GuiRect.y = Screen.height - this.GuiRect.height;
    }
}

public void OnGUI()
{
    if (!this.IsVisible || !PhotonNetwork.InRoom)
    {
    return;
    }

    if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
    {
    if (!string.IsNullOrEmpty(this.inputLine))
    {
    this.photonView.RPC("Chat", RpcTarget.AllViaServer, this.inputLine);
    this.inputLine = "";
    GUI.FocusControl("");
    return; // printing the now modified list would result in an error. to avoid this, we just skip this single frame
    }
    else
    {
    GUI.FocusControl("ChatInput");
    }
    }

    GUI.SetNextControlName("");
    GUILayout.BeginArea(this.GuiRect);

    scrollPos = GUILayout.BeginScrollView(scrollPos);
    GUILayout.FlexibleSpace();

    //for (int i = messages.Count - 1; i >= 0; i--)
    //{
    // GUILayout.Label(messages[i]);
    //}

    for (int i = 0; i < messages.Count; i++)
    {
    GUILayout.Label(messages[i]);

    if (messages.Count >= 10)
    {
    this.messages.RemoveAt(0);
    }
    }

    GUILayout.EndScrollView();

    GUILayout.BeginHorizontal();
    GUI.SetNextControlName("ChatInput");
    inputLine = GUILayout.TextField(inputLine);
    if (GUILayout.Button("Send", GUILayout.ExpandWidth(false)))
    {
    this.photonView.RPC("Chat", RpcTarget.AllViaServer, this.inputLine);
    this.inputLine = "";
    GUI.FocusControl("");
    }
    GUILayout.EndHorizontal();
    GUILayout.EndArea();
    }

    [PunRPC]
    public void Chat(string newLine, PhotonMessageInfo mi)
        {
            string senderName = "anonymous";

            if (mi.Sender != null)
            {
                if (!string.IsNullOrEmpty(mi.Sender.NickName))
                {
                senderName = mi.Sender.NickName;
                }
                else
                {
                senderName = "player " + mi.Sender.NickName;
                }
            }

            this.messages.Add(senderName + ": " + newLine);
        }

        [PunRPC]
        public void Broadcast(string broadcast)
        {
        AddLine(broadcast);
        }

        public void AddLine(string newLine)
        {
        this.messages.Add(newLine);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
        throw new System.NotImplementedException();
        }
    }
}