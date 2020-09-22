using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Mongo
{
    public class MongoModelMap
    {

        public MongoModelMap(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
        public int index { get; set; } = 0;
        // 一一对应
        public List<string> PropTypes { get; set; }
        public List<string> PropNames { get; set; }
    }

    public class PropType {
        public string TypeNameSpace { get; set; }
        public string MyProperty { get; set; }
    }
}
