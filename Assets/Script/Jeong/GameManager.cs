using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임을 관리해주는 매니저 다음 스테이지로 넘어가고 count를 올리고 내려줌
/// </summary>
public class GameManager : UnitySingleton<GameManager>
{
    private int maxPlayerCount;
    private int playerCount;
    [SerializeField] private List<string> sceneNames;
    [SerializeField] private List<GameObject> playerList;
    private int stageIndex;
    
    public override void OnCreated()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInitiate()
    {
        stageIndex = 0;
        playerCount = 0;
        if (sceneNames == null || sceneNames.Count == 0)
            sceneNames = new List<string>();
    }

    /// <summary>
    /// 플레이어가 출구에 들어갈 경우 카운트를 증가 시켜줌
    /// 만약 Max에 도달하면 다음 스테이지로 이동함.
    /// </summary>
    public void EnterPlayer() {
        playerCount++;
        if (playerList.Count => playerCount) {
            playerCount = 0;
            SceneManager.LoadScene(sceneNames[++stageIndex]);
        }
    }

    /// <summary>
    /// 플레이어가 출구엑서 나올 경우 적용ㄷ
    /// </summary>
    public void ExitPlayer() {
        playerCount--;
    }

}
