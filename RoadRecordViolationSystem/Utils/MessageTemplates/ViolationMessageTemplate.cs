using RoadRecordViolationSystem.Utils.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoadRecordViolationSystem.ViewModels.ItextMoViewModel;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public class ViolationMessageTemplate : MessageTemplate<ViolationMessageViewmodel>
    {
        public ViolationMessageTemplate(ViolationMessageViewmodel model)
            :base(model)
        {

        }

        public override string GetMessage()
        {
            return $"Good day Mr/Ms {_model.FullName.Replace("-","")} this is from Department of public order and safety and we would like you to know that you have an existing violation/s in our system, please visit us at " +
                $"dposportal.somee.com and go to violations section and use your ticket number {_model.TicketNumber} to search your violations, Thank you very much";
        }
    }
}
