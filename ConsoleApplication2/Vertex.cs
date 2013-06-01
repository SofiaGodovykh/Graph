using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Vertex<TVertex>
    {
        public Vertex()
        {
            Name = default(string);
            Data = default(TVertex);
            Index = -1;
        }

        public Vertex(TVertex data, string name)
        {
            Data = data;
            Name = name;
            Index = -1;
        }

        public string Name;


        public TVertex Data;

        public int Index;

        public string GetName()
        {
            return this.Name;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public TVertex GetData()
        {
            return this.Data;
        }

        public void SetData(TVertex data)
        {
            this.Data = data;
        }

        public override bool Equals(object obj)
        {
            Vertex<TVertex> arg = obj as Vertex<TVertex>;
            if (arg == null)
                return false;
            return ((arg.Name == this.Name) && (arg.Data.Equals(this.Data)) && (arg.Index == this.Index));
        }
    }
}
