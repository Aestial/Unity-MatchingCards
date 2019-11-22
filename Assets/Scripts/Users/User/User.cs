using System;
using System.Collections.Generic;

[Serializable]
public class User
{
    public string codename;
    public int bestTime;
    public int bestScore;
    public string file;
    public User(string codename)
    {
        this.codename = codename;
    }
}