using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovelMate.Objects
{
    public class SimpleDataNode: SimpleNode
    {
        public Guid UserID { get; set; }
        public string Content { get; set; }

        public SimpleDataNode() : base()
        {
            this.Content = "";
        }

        public SimpleDataNode(Guid guid, string title, string content) : base(guid, title)
        {
            this.Content = content;
        }
    }
}
