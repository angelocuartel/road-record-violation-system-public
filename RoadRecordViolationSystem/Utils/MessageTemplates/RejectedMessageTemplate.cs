using RoadRecordViolationSystem.Utils.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoadRecordViolationSystem.ViewModels;

namespace RoadRecordViolationSystem.Utils.MessageTemplates
{
    public class RejectedMessageTemplate : MessageTemplate<ItextMoViewModel.RejectedMessageViewModel>
    {

        public RejectedMessageTemplate(ItextMoViewModel.RejectedMessageViewModel model)
            :base(model)
        {

        }

        public override string GetMessage()
        {
            return $"Good day Mr/Ms {_model.Complainant} we would like you to know that your application for contest is rejected by the administrator." +
                $"reason for rejection {_model.ReasonForRejection.ToUpper()}. Thank you very much";
        }
    }
}
