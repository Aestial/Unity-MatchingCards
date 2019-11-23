using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    [SerializeField] User user;
    [SerializeField] InputField inputField;
    readonly Notifier notifier = new Notifier();
    public const string ON_LOGIN = "OnLogin";
    public User User
    {
        get { return user; }
        set { user = value; }
    }
    public void LogIn(string codename)
    {
        user = new User(codename);
        notifier.Notify(ON_LOGIN, user);
    }
    public void LogIn()
    {
        string codename = inputField.text;
        LogIn(codename);
    }   
}
