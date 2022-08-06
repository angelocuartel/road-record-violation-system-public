using RoadRecordViolationSystem.Utils.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoadRecordViolationSystem.ViewModels.ItextMoViewModel;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public class AccidentMessageTemplate : MessageTemplate<AccidentMessageViewModel>
    {
        public AccidentMessageTemplate(AccidentMessageViewModel model)
            :base(model)
        {

        }

        public override string GetMessage()
        {
            return $"We would like you to know that Mr/Ms {_model.FullName.ToUpper().Replace("-", " ").Trim()} got accident in {_model.Place} at {_model.Time} please contact us immediately 0930444503 ";
        }
    }
}
