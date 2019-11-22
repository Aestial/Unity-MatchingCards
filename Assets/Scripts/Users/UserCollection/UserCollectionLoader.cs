using UnityEngine;

public class UserCollectionLoader : Loader<UserCollectionLoader>
{
    UserCollection collection;
    UserCollectionController controller;
    void Start()
    {
        controller = GetComponent<UserCollectionController>();
        collection = Get(Create);
        Debug.Log(collection);
        notifier.Notify(ON_LOADED, collection);
        controller.collection = collection;
    }
    void OnApplicationQuit()
    {
        Save(collection);
    }
    private UserCollection Create()
    {
        return new UserCollection();
    }    
}