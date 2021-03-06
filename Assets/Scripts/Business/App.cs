﻿using PolymerModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Util;
using System.Reflection;
using ZCore;
using UnityEngine.Events;

public class App : MonoBehaviour
{
    public static App Instance
    {
        get;
        private set;
    }

    public Transform GameObjectPoolRoot
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        if(GameObjectPoolRoot == null)
        {
            GameObject go = new GameObject("GameObjectPoolRoot");
            GameObjectPoolRoot = go.transform;
            GameObjectPoolRoot.SetParent(this.gameObject.transform);
        }
        LoadData();
    }

    private async void LoadData()
    {
        await PolymerModelAPI.LoadDataAsync();
        Debug.Log("加载数据完成");
        CoreAPI.SendCommand<PdbLoaderModule, LoadDefaultPdbFileCommand>(new LoadDefaultPdbFileCommand("6dce", () => {
            CoreAPI.SendCommand<ProteinDisplayModule, ShowDisplayViewCommand>(new ShowDisplayViewCommand());
            CoreAPI.SendCommand<ProteinDisplayModule, ShowProteinCommand>(new ShowProteinCommand());
        }));
    }

    public void OnButtonClick()
    {
        // 调用PdbLoaderModule里的OnLoadLocalPdbFileCommand方法，反射实现的
        CoreAPI.SendCommand<PdbLoaderModule, LoadLocalPdbFileCommand>(new LoadLocalPdbFileCommand(() =>
        {
            CoreAPI.SendCommand<ProteinDisplayModule, ShowProteinCommand>(new ShowProteinCommand());
        }));
    }
}
