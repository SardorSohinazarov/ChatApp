using ChatApp.Web.Models.DTOs;
using System.ComponentModel;

namespace ChatApp.Web.States
{
    public class AuthenticationState : INotifyPropertyChanged
    {
        public const string AuthStoreKey = "authKey";

        public event PropertyChangedEventHandler? PropertyChanged;
        public string? Name { get; set; }
        public string? Token { get; set; }

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAuthenticated)));
                }
            }
        }

        public void LoadState(AuthResponseDTO authResponseDTO)
        {
            Name = authResponseDTO.Name;
            Token = authResponseDTO.Token;
            IsAuthenticated = true;
        }

        public void UnLoadState()
        {
            Name = null;
            Token = null;
            IsAuthenticated = false;
        }
    }
}
