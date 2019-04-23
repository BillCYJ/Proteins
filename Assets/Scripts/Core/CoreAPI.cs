using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZCore
{
    public static class CoreAPI
    {
        public static void SendCommand<TModule, TCommand>(TCommand cmd) where TModule : Module, new() where TCommand : Command
        {
            Core.SendCommand<TModule, TCommand>(cmd);
            // Type commandType = typeof(TCommand);
            // Debug.Log(commandType);
            // Result:1.加载数据完成 2.LoadDefaultPdbFileCommand 3.读取pdb完成 4.ShowProteinCommand 
            // 5.ShowInfoInBoardCommand 6.ShowInfoInBoardCommand (5和6一直循环输出)
        }

        public static void SendCommand<TModule>(Command cmd) where TModule : Module, new()
        {
            Core.SendCommand<TModule>(cmd);
        }

        public static TResult PostCommand<TModule, TCommand, TResult>(TCommand cmd) where TModule : Module, new() where TCommand : Command
        {
            return Core.PostCommand<TModule, TCommand, TResult>(cmd);
        }

        public static object PostCommand<TModule>(Command cmd) where TModule : Module, new()
        {
            return Core.PostCommand<TModule>(cmd);
        }
        //TODO: 异步的带回调函数的发送指令
    }
}