using System.Collections.Generic;

namespace shooter02.Managers.Events
{
    class EventObject
    {
        List<IListener> mObjectList;

        public EventObject()
        {
            mObjectList = new List<IListener>();
        }

        /// <summary>
        /// AddObject
        /// Adds the object to the specific event
        /// Allows the object to listen for specific events
        /// </summary>
        /// <param name="obj"></param>
        public void AddObject(IListener obj)
        {
            if (obj == null || mObjectList.Contains(obj))
            {
                return;
            }

            mObjectList.Add(obj);
        }

        /// <summary>
        /// RemoveObject
        /// Removes the object from listening to the event
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int RemoveObject(IListener obj)
        {
            mObjectList.Remove(obj);

            return mObjectList.Count;
        }

        /// <summary>
        /// DispaceEvent
        /// Alerts all objects that the event has been thrown
        /// </summary>
        /// <param name="_event"></param>
        public void DisptchEvent(CEvent _event)
        {
            foreach (IListener obj in mObjectList)
            {
                obj.HandleEvent(_event);
            }
        }

        /// <summary>
        /// RemoveAll()
        /// Clears the list of objects listening for this event
        /// </summary>
        public void RemoveAll()
        {
            mObjectList.Clear();
        }
    }
}
