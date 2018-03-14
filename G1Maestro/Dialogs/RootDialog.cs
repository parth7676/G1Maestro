using System;
using System.Threading.Tasks;
using G1Maestro.Entities;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace G1Maestro.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // return our reply to the user
            context.PostAsync("Hi! Welcome to G1Maestro");
            context.Call<UserProfile>(new EnsureProfileDialog(), SaveUserProfile);
            return Task.CompletedTask;
        }

        private async Task SaveUserProfile(IDialogContext context, IAwaitable<UserProfile> result)
        {
            UserProfile profile = await result;
            context.UserData.SetValue("profile", profile);
            await context.PostAsync($"Hello {profile.Name}, I love {profile.CompanyName}");
            context.Wait(MessageReceivedAsync);
        }
    }
}