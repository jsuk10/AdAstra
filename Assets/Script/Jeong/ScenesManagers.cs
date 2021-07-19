using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScenesManagers : UnitySingleton<ScenesManagers>
{
    [SerializeField] private List<string> sceneNames;
    [SerializeField] private List<float> inkSize;
    private int sceneIndex = 0;

    public override void OnCreated()
    {
    }

    public override void OnInitiate()
    {
    }



    /// <summary>
    /// 신을 다시 연다
    /// </summary>
    public void ReLoadScene()
    {
        SceneManager.LoadScene(sceneNames[sceneIndex]);
    }

    public void LoadScenes()
    {
        SceneManager.LoadScene(sceneNames[sceneIndex++]);
        //SceneManager.LoadScene(sceneNames[sceneIndex++]);
        SoundManager.Instance.PlayBackGroundSound(2);
        GameManager.Instance.MaxInk = inkSize[sceneIndex];
        GameManager.Instance.useInk = 0f;
    }
}
