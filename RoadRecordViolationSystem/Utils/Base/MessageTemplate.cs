using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.Base
{
    public abstract class MessageTemplate<T>
    {
        protected T _model;

        public MessageTemplate(T model)
        {
            _model = model;
        }

        public abstract string GetMessage();
    }
}
