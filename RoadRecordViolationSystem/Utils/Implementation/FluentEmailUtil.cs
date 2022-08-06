using FluentEmail.Core;
using RoadRecordViolationSystem.Utils.Interface;
using System.Threading.Tasks;


namespace RoadRecordViolationSystem.Utils.Implementation
{
    public class FluentEmailUtil : IEmail
    {
        private readonly IFluentEmail _email;
        public FluentEmailUtil(IFluentEmail email)
        {
            _email = email;
        }

        public void Send<T>(string to, string subject, T model)
        {
            var emailProvider = _email
                .To(to)
                .Subject(subject)
                .Body("Good day this is from Dpos ");
        }

        public async Task SendWithTemplateAsync<T>(string to, string subject, T model, IEmailTemplate template)
        {
            var emailProvider = _email
               .To(to)
               .Subject(subject)
               .UsingTemplateFromFile(template.GetRazorPath(), model, true);

            await emailProvider.SendAsync();
        }
    }

}
