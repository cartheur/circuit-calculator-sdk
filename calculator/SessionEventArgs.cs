using System;

namespace Circuit.Calculator
{
    /// <summary>
    /// Represents event data for a session event.
    /// </summary>
    public class SessionEventArgs : EventArgs
    {
        private readonly string _sessionid;

        public SessionEventArgs(string sessionID)
        {
            _sessionid = sessionID;
        }
    }
}
