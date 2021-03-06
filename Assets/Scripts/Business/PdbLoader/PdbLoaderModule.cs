﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;
using PolymerModel.Data;

public class PdbLoaderModule : Module
{
    /// <summary>读取本地pdb文件 </summary>
    public void OnLoadLocalPdbFileCommand(LoadLocalPdbFileCommand cmd)
    {
        GetController<PdbLoaderController>().LoadLocalPdbFileAsync(cmd.CompleteCallback);
    }
    
    /// <summary>从网络加载pdb文件 </summary>
    public void OnLoadNetworkPdbFileCommand(LoadNetworkPdbFileCommand cmd)
    {
        GetController<PdbLoaderController>().LoadNetworkPdbFile(cmd.IDCode, cmd.CompleteCallback);
    }
    
    /// <summary>获取当前读取的蛋白质数据</summary>
    public Protein OnGetProteinDataCommand(GetProteinDataCommand cmd)
    {
        return GetController<PdbLoaderController>().GetProteinData();
    }

    public void OnLoadDefaultPdbFileCommand(LoadDefaultPdbFileCommand cmd)
    {
        GetController<PdbLoaderController>().LoadDefaultPdbFileCommand(cmd.IDCode, cmd.CompleteCallback);
    }
}
