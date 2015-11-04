using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Common.General.Util
{
    public class Try
    {
        protected Action Action {get; set;}

        private int maxTries = 1;
        private Action onFailure;

        protected Try(Action action)
        {
            Action = action;
        }

        public static Try To(Action action)
        {
            return new Try(action);
        }

        public Try OnFailedAttempt(Action action)
        {
            onFailure = action;
            return this;
        }

        protected virtual TryResult Attempt()
        {
            for (int attempt = 1; attempt <= maxTries; attempt++)
            {
                try
                {
                    Action();
                    break;
                }
                catch (Exception ex)
                {
                    if (onFailure != null) onFailure();
                    if (attempt < maxTries) continue;

                    return new TryResult(ex);
                }
            }

            return new TryResult(true);
        }

        public Try UpTo(int maxAttempts)
        {
            maxTries = maxAttempts;
            return this;
        }

        public TryResult Times()
        {
            return Attempt();
        }
    }

    public class TryResult
    {
        public TryResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public TryResult(Exception exception)
        {
            Succeeded = false;
            Exception = exception;
        }

        public bool Succeeded { get; private set; }
        public Exception Exception { get; private set; }
    }
}
