using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.GameObjects;

namespace shooter02.Managers.Events
{
    enum EVENT_ID : short { BAD_EVENT = -1, GAME_OVER = 0, COMBINE_PLAYER, NUM_EVENTS };

    class CEvent
    {
        private IListener mParam;
        private EVENT_ID mID;

        public CEvent()
        {
            mParam = null;
            mID = EVENT_ID.BAD_EVENT;
        }

        public CEvent(IListener _param, EVENT_ID _id)
        {
            mParam = _param;
            mID = _id;
        }

        public IListener GetParam() { return mParam; }
        public short GetEventID() { return (short)mID; }
    }
}
