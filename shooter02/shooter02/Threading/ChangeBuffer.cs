using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.Threading
{
    class ChangeBuffer
    {
        public List<ChangeMessage> Messages { get; set; }

        public ChangeBuffer()
        {
            Messages = new List<ChangeMessage>();
        }

        public void Add(ChangeMessage msg)
        {
            Messages.Add(msg);
        }

        public void Clear()
        {
            Messages.Clear();
        }

    }
}
