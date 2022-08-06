using RoadRecordViolationSystem.Utils.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoadRecordViolationSystem.ViewModels;

namespace RoadRecordViolationSystem.Utils.MessageTemplates
{
    public class ApproveMessageTemplate : MessageTemplate<ItextMoViewModel.ApprovedMessageViewModel>
    {

        public ApproveMessageTemplate(ItextMoViewModel.ApprovedMessageViewModel model)
            :base(model)
        {

        }

        public override string GetMessage()
        {
            return $"Good day Mr/Ms {_model.Complainant} we would like you to know that your application for contest is Approved by the administrator." +
                $" your schedule for the hearing is on {_model.ScheduleDate} at {_model.ScheduleTime}. Mediator will be Mr/Ms {_model.Mediator} between you and the enforcer, Thank you very much";
        }
    }
}
