using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace PolymerModel.Data
{
    public enum BondType
    {
        Single = 1,
        Double = 2,
    }

    /// <summary>
    /// 氨基酸标准残基
    /// 额外补充：按氨基酸种类着色，不同颜色则不同材质
    /// </summary>
    public class Aminoacid
    {
        internal static Dictionary<AminoacidType, Aminoacid> Aminoacids;

        /// <summary>氨基酸类型</summary>
        public AminoacidType Type { get; private set; }

        /// <summary>是否是标准残基</summary>
        public bool IsStandard { get; private set; }

        /// <summary>氨基酸类型(中文)</summary>
        public string Chinese { get; private set; }

        /// <summary>该氨基酸包含的原子</summary>
        public ReadOnlyCollection<AtomInAminoacid> Atoms { get; private set; }

        /// <summary>使用字典进行O(1)时间复杂度的查询，根据原子的名字string（含后缀的）来得到对应的原子实例</summary>
        private Dictionary<string, AtomInAminoacid> atomDic;
        /// <summary>索引器</summary>
        public AtomInAminoacid this[string key]
        {
            get
            {
                return atomDic[key];
            }
        }

        /// <summary>原子间的化学键连接</summary>
        public ReadOnlyDictionary<KeyValuePair<AtomInAminoacid, AtomInAminoacid>, BondType> Connections { get; private set; }

        static Aminoacid()
        {
            Aminoacids = new Dictionary<AminoacidType, Aminoacid>();
        }

        /// <summary>
        /// 公有构造函数
        /// </summary>
        /// <param name="type">氨基酸类型</param>
        /// <param name="isStandard">是否是标准残基</param>
        /// <param name="atoms">构成氨基酸得原子</param>
        /// <param name="connection">原子间的连接关系</param>
        internal Aminoacid(AminoacidType type, string chinese, bool isStandard, IList<string> atomNames, IDictionary<KeyValuePair<string, string>, BondType> connection)
        {
            this.Type = type;
            this.Chinese = chinese;

            List<AtomInAminoacid> atoms = new List<AtomInAminoacid>();
            foreach (string name in atomNames)
            {
                AtomInAminoacid atomInAminoacid = new AtomInAminoacid(name)
                {
                    Aminoacid = this,
                };
                atoms.Add(atomInAminoacid);
            }
            Atoms = new ReadOnlyCollection<AtomInAminoacid>(atoms);

            atomDic = new Dictionary<string, AtomInAminoacid>();
            foreach (var child in atoms)
            {
                atomDic.Add(child.Name, child);
            }

            Dictionary<KeyValuePair<AtomInAminoacid, AtomInAminoacid>, BondType> connectDic = new Dictionary<KeyValuePair<AtomInAminoacid, AtomInAminoacid>, BondType>();
            foreach (var child in connection)
            {
                // this是38行的索引器的调用，即输入原子名字string(含后缀的)，返回对应的原子实例
                connectDic.Add(new KeyValuePair<AtomInAminoacid, AtomInAminoacid>(this[child.Key.Key], this[child.Key.Value]), child.Value);
            }
            Connections = new ReadOnlyDictionary<KeyValuePair<AtomInAminoacid, AtomInAminoacid>, BondType>(connectDic);
        }

        public override bool Equals(object obj)
        {
            Aminoacid aminoacid = obj as Aminoacid;
            if (aminoacid == null)
                return false;
            else
                return Type == aminoacid.Type;
        }
        
        public override int GetHashCode()
        {
            return (int)Type;
        }

        /// <summary>根据氨基酸的type来获取某个氨基酸实例 </summary>
        public static Aminoacid Generate(string type)
        {
            return Generate((AminoacidType)Enum.Parse(typeof(AminoacidType), type));
        }

        public static Aminoacid Generate(AminoacidType type)
        {
            Aminoacid aminoacid = null;
            if(!Aminoacids.TryGetValue(type, out aminoacid))
            {
                throw new ArgumentException("Unhandled AminoacidType:" + type.ToString());
            }
            return Aminoacids[type];
        }
    }
}
