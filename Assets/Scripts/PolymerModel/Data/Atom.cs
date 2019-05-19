using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolymerModel.Data
{
    /// <summary>原子的共性类，原子都有类型（H,C,N,O,S,P中的一种）和各自的半径</summary>
    public class Atom
    {
        #region 各类组成氨基酸的元素

        // 加了static的成员，就不属于某个具体的对象了，而是属于Atom这个类，也就是静态成员的地址空间在类的地址空间里，而不在每个对象的地址空间里
		// 并且这些只会被new一次（下方的静态构造函数会在 创建第一个实例或引用任何静态成员之前 被自动调用）
		// 这样做的意义是：减少了这些原子实例化的次数，也就是说，总不能每调用某个原子就new一遍该原子吧，要用的时候，直接到这儿来取不就好了。
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
		/// 自定义的构造函数，有了自定义的构造函数，就不会自动生成默认的无参的构造函数了
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
        /// 当我们想初始化一些静态变量的时候就需要用到静态构造函数
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
		///
		/// 在Object类中可以看见Equals的原本实现，原本是比较两个obj的引用，也就是指向的地址是否一致，但是这里的需求是，只要两个obj的type相同，
		/// 就判断为同一个obj了，所以需要重写Equals函数。
        /// </summary>
        public override bool Equals(object obj)
        {
            Atom atom = obj as Atom; // 这里只声明了一个Atom的对象，并没new，只是单纯的复制，所以不属于在类里new自身类的对象
            if (atom == null)
                return false;
            else
                return Type == atom.Type;
        }
		// 因为每个对象都会有一个独一无二的哈希码标识，如果Equals的比较规则修改了，也必须要修改GetHashCode的规则
		// 不然，由于修改了Equals的比较规则，假设两个obj的type相同，那就说明这两个obj相同，但这两个obj的哈希值可能会不同（比如：new了两次一个类的对象，这两个类的地址的确不同）
        public override int GetHashCode() 
        {
            return (int)Type;
        }
    }
}