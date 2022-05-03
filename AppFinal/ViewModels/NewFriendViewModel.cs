using System;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFinal.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;

namespace AppFinal.ViewModels
{
    public class NewFriendViewModel : BaseViewModel
    {

        private string name;
        private string description;

        public NewFriendViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                   && !String.IsNullOrWhiteSpace(description);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Friend newFriend = new Friend()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Description = Description
            };

            await DataStore.AddFriendAsync(newFriend);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}