using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.Managers.Events
{
    class EventManager
    {
        private EventObject[] mEvents;
        private List<CEvent> mEventList;

        public EventManager()
        {
            mEvents = new EventObject[(short)EVENT_ID.NUM_EVENTS];
            mEventList = new List<CEvent>();
        }

        /// <summary>
        /// Need to make sure our event parameters are in range and good
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="obj"></param>
        /// <returns>bool </returns>
        private bool CheckParams(EVENT_ID eventID, IListener obj)
        {
            return eventID <= EVENT_ID.BAD_EVENT || eventID >= EVENT_ID.NUM_EVENTS || obj == null;
        }

        /// <summary>
        /// ProcessEvents
        /// Goes through the list of events and dispatches to all objects listening for that event
        /// </summary>
        public void ProcessEvents()
        {
            // This should prevent errors with multithreading
            while (mEventList.Count > 0)
            {
                // Get which event needs to be called
                short index = mEventList[0].GetEventID();
                // Tell it to dispatch the event to its objects
                mEvents[index].DisptchEvent(mEventList[0]);
                // Remove the event since we've handled it
                mEventList.RemoveAt(0);
            }
        }

        /// <summary>
        /// RegisterEvent
        /// Registers an IListener object to a specific event
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="obj"></param>
        public void RegisterEvent(EVENT_ID eventID, IListener obj)
        {
            if (CheckParams(eventID, obj))
            {
                return;
            }
            
            mEvents[(short)eventID].AddObject(obj);
        }

        /// <summary>
        /// UnregisterEvent
        /// Removes an IListener object from a specific event
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="obj"></param>
        public void UnregisterEvent(EVENT_ID eventID, IListener obj)
        {
            if (CheckParams(eventID, obj))
            {
                return;
            }

            mEvents[(short)eventID].RemoveObject(obj);
        }

        /// <summary>
        /// SendEvent
        /// Allows objects to send events
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="obj"></param>
        public void SendEvent(EVENT_ID eventID, IListener obj)
        {
            if (CheckParams(eventID, obj))
            {
                return;
            }

            CEvent temp = new CEvent(obj, eventID);
            mEventList.Add(temp);
        }
    }
}
