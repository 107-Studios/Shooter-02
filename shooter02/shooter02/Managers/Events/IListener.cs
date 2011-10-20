
namespace shooter02.Managers.Events
{
    interface IListener
    {
        /// <summary>
        /// HandleEvent
        /// All Objects that derive from IListener must override this function
        /// Each object will need to handle the events differently
        /// </summary>
        /// <param name="_event"></param>
        void HandleEvent(CEvent _event);
    }
}
