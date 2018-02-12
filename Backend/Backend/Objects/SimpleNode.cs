using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovelMate.Objects
{
    public class SimpleNode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public SimpleNode()
        {
            this.Id = Guid.NewGuid();
            this.Name = "";
        }

        public SimpleNode(Guid guid, string title)
        {
            this.Id = guid;
            this.Name = title;
        }
    }
}
