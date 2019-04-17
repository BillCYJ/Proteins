using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolymerModel.Data
{
    /// <summary>原子的共性类，原子都有类型（H,C,N,O,S,P中的一种）和各自的半径</summary>
    public class Atom
    {
        #region 各类组成氨基酸的元素

        // 这些都不是对象成员，所以需要加static，这些只需要被new一次，总不能每个原子都new一遍这些吧
        public static readonly Atom H; 
        public static readonly Atom C; //Atom.C就表示碳原子对象，该对象里包含了碳原子的type和radius
        public static readonly Atom N;
        public static readonly Atom O;
        public static readonly Atom S;
        public static readonly Atom P;

        #endregion

        /// <summary>原子类型</summary>
        public AtomType Type { get; private set; }

        /// <summary>原子半径(范德华半径)</summary>
        public float Radius { get; private set; }

        /// <summary>
        /// 私有构造函数：如果类具有一个或多个私有构造函数而没有公共构造函数，则当前这个类里可以创建该类的实例，但其他类（除嵌套类外）就不行。
        /// </summary>
        private Atom(AtomType type) : this(type, 1.0f) { }

        /// <summary>
        /// 私有构造函数的多态的实现
        /// </summary>
        private Atom(AtomType type, float radius)
        {
            this.Type = type;
            this.Radius = radius;
        }
        
        /// <summary>数据取自Bondi汇总
        /// 当我们想初始化一些静态变量的时候就需要用到静态构造函数，静态构造函数不分私有公有
        /// 这个构造函数是属于类的，而不是属于哪里实例的，就是说这个构造函数只会被执行一次。也就是在创建第一个实例或引用任何静态成员之前
        /// </summary>
        static Atom()
        {
            #region 构造各类组成氨基酸的元素

            H = new Atom(AtomType.H, 0.12f);
            C = new Atom(AtomType.C, 0.17f);
            N = new Atom(AtomType.N, 0.155f);
            O = new Atom(AtomType.O, 0.152f);
            S = new Atom(AtomType.S, 0.18f);
            P = new Atom(AtomType.P, 0.18f);

            #endregion
        }

        /// <summary>
        /// 重写了Dictionary的Equals和GetHashCode函数
        /// Atom重写这两个函数就相当于声明了：任何type相同的Atom对象都视为同一个对象，这些Atom对象都可以作为key来索引同一个value
        /// 如果不这样做，由于每个对象都有自己的地址，key又是取的对象的引用，就会导致key的不同以至于无法索引到value
        /// 如果这样做，就是实现了字典的多对一的关系，但本质上还是一对一的关系
        /// </summary>
        public override bool Equals(object obj)
        {
            Atom atom = obj as Atom;
            if (atom == null)
                return false;
            else
                return Type == atom.Type;
        }
        public override int GetHashCode()
        {
            return (int)Type;
        }
    }
}