

namespace shooter02.Managers.Events
{
    public enum EVENT_ID : short { BAD_EVENT = -1, 
                                   GAME_OVER = 0, 
                                   PLAYER_COMBINE,
                                   NUM_EVENTS };

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
        public EVENT_ID GetEventID() { return mID; }
    }
}
