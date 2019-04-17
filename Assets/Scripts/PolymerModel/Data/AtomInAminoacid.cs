using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace PolymerModel.Data
{
    /// <summary>氨基酸中的原子实例，原子的非共性类</summary>
    public class AtomInAminoacid
    {
        /// <summary>所属氨基酸</summary>
        public Aminoacid Aminoacid { get; internal set; }

        /// <summary>在该氨基酸中的名字(pdb文件中某些特定的行（首单词为ATOM的行）的ATOM[13-16]列 对应的就是 原子在氨基酸中的名字（是带后缀的）)</summary>
        public string Name { get; private set; }

        /// <summary>所属原子类型</summary>
        public Atom Atom { get; private set; }

        public AtomInAminoacid(string name)
        {
            this.Name = name;
            this.Atom = GetAtomByName(name);
        }

        /// <summary>根据名字规则返回原子类型</summary>
        private Atom GetAtomByName(string name)
        {
            //即首字母为原子类型
            switch (char.ToUpper(name.First()))
            {
                // 规定：不处理H，也不显示H
                case 'C': return Atom.C; //Atom.C就表示碳原子对象
                case 'N': return Atom.N;
                case 'O': return Atom.O;
                case 'S': return Atom.S;
                case 'P': return Atom.P;
                default: throw new ArgumentException("Unhandled Atom Name:" + name);
            }
        }

        public override bool Equals(object obj)
        {
            AtomInAminoacid atom = obj as AtomInAminoacid;
            if (atom == null)
                return false;
            else
                return this.Aminoacid == atom.Aminoacid && this.Name == atom.Name;
        }

        /// <summary>直接使用名字作为HashCode(保证在同一个氨基酸中作为Key存储哈希表) </summary>
        public override int GetHashCode()
        {
            return Name.GetHashCode() + Aminoacid.GetHashCode();
        }
    }
}
