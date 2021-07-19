using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(this.transform.position.x + Random.Range(minX, maxX), this.transform.position.y + Random.Range(minY, maxY));
        var player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        CameraController.Instance.ChangeCameraTarget(player);
        // player.name = PhotonNetwork.LocalPlayer.NickName;
        // GameManager.Instance.SetPlayer(player);
    }
}
