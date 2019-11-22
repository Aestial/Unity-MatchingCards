using UnityEngine;

public class UserCollectionController : MonoBehaviour
{
    UserCollection m_Collection;
    public UserCollection collection
    {
        get { return m_Collection; }
        set
        {
            m_Collection = value;
        }
    }
    public void Add(User user)
    {
        m_Collection.users.Add(user.codename, user);
    }
    public void OnRectTransformRemoved()
    {
        
    }
}
