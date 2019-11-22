using System;
using System.Collections.Generic;

[Serializable]
public class UserCollection
{
    public Dictionary<string, User> users;
    public UserCollection()
    {
        users = new Dictionary<string, User>();
    }    
}