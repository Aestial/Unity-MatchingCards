using System.Collections.Generic;

public class Notifier
{
	readonly Dictionary<string, NotificationCenter.NotifyDelegate> subscriptions;

	public Notifier() {
		subscriptions = new Dictionary<string, NotificationCenter.NotifyDelegate>();
	}

	~Notifier() 
	{
		UnsubcribeAll();
	}

	public void Notify(string name, params object[] args) {
		NotificationCenter.Instance.Notify(name,args);
	}

	public void Subscribe(string name, NotificationCenter.NotifyDelegate slot){
		subscriptions.Add(name,slot);
		NotificationCenter.Instance.Subscribe( name, slot );
	}

	public void UnsubcribeAll()
	{
		foreach (string key in subscriptions.Keys)
		{
			NotificationCenter.Instance.Unsubscribe(key,subscriptions[key]);
		}

		subscriptions.Clear();
	}
}
