using UnityEngine;

public class MonsterLoad : Singleton<MonsterLoad>
{
    [Header("Directory path")]
    [SerializeField] string path;
    public Monster[] monsters;
    void Awake()
    {
        monsters = Resources.LoadAll<Monster>(path);
    }
}