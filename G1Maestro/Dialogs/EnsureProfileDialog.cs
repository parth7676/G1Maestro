using System;
using System.Threading.Tasks;
using G1Maestro.Entities;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace G1Maestro.Dialogs
{
    [Serializable]
    public class EnsureProfileDialog : IDialog<UserProfile>
    {
        #region Private Members
        UserProfile _userProfile;
        #endregion

        public Task StartAsync(IDialogContext context)
        {
            EnsureCompanyProfileName(context);

            return Task.CompletedTask;
        }

        private void EnsureCompanyProfileName(IDialogContext context)
        {
            if (!context.UserData.TryGetValue("profile", out _userProfile))
                _userProfile = new UserProfile();

            if (string.IsNullOrEmpty(_userProfile.Name))
                PromptDialog.Text(context, NameEntered, "What's your name?");
            else
                EnsureCompanyName(context);
        }

        private void EnsureCompanyName(IDialogContext context)
        {
            if (string.IsNullOrEmpty(_userProfile.CompanyName))
                PromptDialog.Text(context, CompanyNameEntered, "Who do you work for?");
            else
                context.Done(_userProfile);
        }

        #region Callbacks
        private async Task NameEntered(IDialogContext context, IAwaitable<string> result)
        {
            _userProfile.Name = await result;
            EnsureCompanyName(context);
        }

        private async Task CompanyNameEntered(IDialogContext context, IAwaitable<string> result)
        {
            _userProfile.CompanyName = await result;
            context.Done(_userProfile);
        }
        #endregion

    }
}