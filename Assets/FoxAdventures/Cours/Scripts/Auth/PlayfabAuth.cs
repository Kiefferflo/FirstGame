using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public static class PlayfabAuth
{
    // Const - Save email/password
    public const string PlayfabAuthPlayerPrefsKeyUsername = "playfab_auth_username";
    public const string PlayfabAuthPlayerPrefsKeyEmail = "playfab_auth_email";
    public const string PlayfabAuthPlayerPrefsKeyPassword = "playfab_auth_password";

    // Getter
    public static bool IsLoggedIn
    {
        get
        {
            return PlayFabClientAPI.IsClientLoggedIn();
        }
    }

    // Functions
    public static void TryRegisterWithEmail(string email, string password, Action<RegisterPlayFabUserResult> registerResultCallback, Action<PlayFabError> errorCallback)
    {
        PlayfabAuth.TryRegisterWithEmail(email, password, email, registerResultCallback, errorCallback);
    }

    public static void TryRegisterWithEmail(string email, string password, string username, Action<RegisterPlayFabUserResult> registerResultCallback, Action<PlayFabError> errorCallback)
    {
        var request = new RegisterPlayFabUserRequest { Email = email, Username = username, Password = password };
        PlayFabClientAPI.RegisterPlayFabUser(request, registerResultCallback, errorCallback);

    }

    public static void TryLoginWithEmail(string email, string password, Action<LoginResult> loginResultCallback, Action<PlayFabError> errorCallback)
    {
        var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
        PlayFabClientAPI.LoginWithEmailAddress(request, loginResultCallback, errorCallback);
    }

    // Logout
    public static void Logout(Action logoutResultCallback, Action errorCallback)
    {
        // Clear all keys from PlayerPrefs
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyUsername);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyEmail);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyPassword);

        // Callback
        logoutResultCallback.Invoke();
    }
}
