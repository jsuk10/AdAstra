using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private float lodingTime = 3.0f;
    private bool isReady = false;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        StartCoroutine(Translate());

    }



    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        isReady = true;
    }

    virtual protected IEnumerator Translate()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            UIManager.Instance.Loading(time / lodingTime);
            if (time > lodingTime && isReady)
            {
                UIManager.Instance.inkSlider = null;
                SceneManager.LoadScene("Lobby");
            }
            yield return null;

        }
    }
}
