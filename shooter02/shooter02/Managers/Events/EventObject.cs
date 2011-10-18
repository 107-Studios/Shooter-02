using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.GameObjects;

namespace shooter02.Managers.Events
{
    class EventObject
    {
        List<IListener> objectList;

        public EventObject()
        {
            objectList = new List<IListener>();
        }

        public void AddObject(IListener obj)
        {
            if (obj == null || objectList.Contains(obj))
            {
                return;
            }

            objectList.Add(obj);
        }

        public int RemoveObject(IListener obj)
        {
            objectList.Remove(obj);

            return objectList.Count;
        }

        public void DisptchEvent(CEvent _event)
        {
            foreach (IListener obj in objectList)
            {
                obj.HandleEvent(_event);
            }
        }
    }
}
