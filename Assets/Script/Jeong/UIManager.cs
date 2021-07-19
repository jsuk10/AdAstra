﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : UnitySingleton<UIManager>
{
    private Dictionary<string, GameObject> UIList;
    [SerializeField] public Slider inkSlider;


    public override void OnCreated()
    {
    }

    public override void OnInitiate()
    {
        UIList = new Dictionary<string, GameObject>();
    }

    public void SetDirctionary(string name, GameObject gameObject) {
        UIList.Add(name, gameObject);
    }
    public GameObject GetDirctionary(string name) {
        return UIList[name];
    }

    public void SettingOnOFf() {
        UIList["Setting"].SetActive(!UIList["Setting"].gameObject.activeSelf);
    }

    public void AddChange() {
        if (inkSlider == null)
        {
            inkSlider = UIList["InkSlider"].GetComponent<Slider>();
        }
        inkSlider.value = 1- ((GameManager.Instance.usedInk + GameManager.Instance.useInk) / GameManager.Instance.MaxInk);

        //if (inkSlider.onValueChanged == null)
        //{
        //    inkSlider.onValueChanged.AddListener( (float value )=> {
        //    });
        //}
    }

}
