using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Crow.MVVM.Models;

public class RegisterViewModel : BaseViewModel
{
    public UserProfile UserProfile { get; set; }

    // Registration Properties
    private string _username;
    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    private string _email;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    private string _password;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    private string _confirmPassword;
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value);
    }

    private string _notificationMessage;
    public string NotificationMessage
    {
        get => _notificationMessage;
        set => SetProperty(ref _notificationMessage, value);
    }

    // Commands
    public ICommand RegisterCommand { get; }

    private readonly IUserRepository _userRepository;

    // Constructor
    public RegisterViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        // Initialize Commands
        RegisterCommand = new Command(OnRegister, CanRegister);
    }

    private bool CanRegister()
    {
        // Validate registration fields
        return !string.IsNullOrWhiteSpace(Username) &&
               !string.IsNullOrWhiteSpace(Email) &&
               !string.IsNullOrWhiteSpace(Password) &&
               Password == ConfirmPassword;
    }

    private void OnRegister()
    {
        // Attempt to register the user
        var userProfile = new UserProfile
        {
            Username = Username,
            Email = Email,
            Password = Password
        };

        var success = _userRepository.RegisterUser(userProfile);
        if (success)
        {
            NotificationMessage = "Registration successful!";
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
        else
        {
            NotificationMessage = "Registration failed. Username or email already exists.";
        }
    }
}

// Repository Interface for Dependency Injection
public interface IUserRepository
{
    bool RegisterUser(UserProfile user);
}

// Example Implementation of Repository
public class UserRepository : IUserRepository
{
    private readonly List<UserProfile> _users = new List<UserProfile>();

    public bool RegisterUser(UserProfile user)
    {
        if (_users.Any(u => u.Username == user.Username || u.Email == user.Email))
        {
            // User already exists
            return false;
        }

        _users.Add(user);
        return true;
    }
}