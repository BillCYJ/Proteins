using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ZCore
{
    /// <summary>
    /// C#支持反射，其基础就是元数据（又叫中介数据，即描述其他数据的数据和信息）。
    /// 其中，Attribute是C#元数据的一个重要组成部分，Attribute用于添加元数据，赋予了用户元编程的能力。
    /// 
    /// 特性（Attribute）是用于在运行时传递程序中各种元素（比如类、方法、结构、枚举、组件等）的行为信息的声明性标签。
    /// 您可以通过使用特性向程序添加声明性信息。一个声明性标签是通过放置在它所应用的元素前面的方括号（[ ]）来描述的。
    /// 
    /// 定制特性attribute，本质上是一个类，其为目标元素提供关联附加信息，
    /// 并在运行期以反射的方式来获取附加信息（获取到特性类），相当于优雅的为元素添加了一个tag，这个tag是一个类。
    /// 
    /// Attribute类是在编译的时候被实例化的，而不是像通常的类那样在运行时候才实例化。
    /// Attribute.GetCustomAttribute（）可以获得特性类的对象（也就是会构造指定特性的新实例），IsDefined方法仅仅是判断目标有没有应用指定特性
    /// 每个特性类必须至少有一个构造函数 https://www.cnblogs.com/smileyearn/p/5808526.html
    /// 
    /// 从元数据就是元数据，特性是元数据的一部分，特性类在编译的时候确定，而调用特性类的类、方法啥的都可以在运行时动态地确定
    /// 比如用特性来定义方法，就可以直接通过特性对应的字符串来调用该方法，更加多态
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ImplementedInControllerAttribute : Attribute {

        public bool IsCustomMethodName { get; private set; }

        public string MethodName { get; private set; }

        public ImplementedInControllerAttribute() {
            IsCustomMethodName = false;
        }
        
        public ImplementedInControllerAttribute(string methodName) {
            IsCustomMethodName = true;
            this.MethodName = methodName;
        }
    }

}
