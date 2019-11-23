using System;

[Serializable]
public class User
{
    public string codename;
    public string filename;
    public int bestTime;
    public int bestScore;    
    public User(string codename)
    {
        this.codename = codename;
        filename = codename + ".json";
    }
}