using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    [SerializeField] User user;
    [SerializeField] InputField inputField;   
    public User User
    {
        get { return user; }
        set { user = value; }
    }
    public void LogIn(string codename)
    {
        user = new User(codename);
    }
    public void LogIn()
    {
        string codename = inputField.text;
        LogIn(codename);
    }
    void Start()
    {
        
    }   
    void Update()
    {
        
    }
}
