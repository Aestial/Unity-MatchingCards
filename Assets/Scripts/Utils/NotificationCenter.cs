using System.Collections.Generic;
using System.Collections;
using System;

public sealed class NotificationCenter
{
	public delegate void NotifyDelegate( params object[] args );

	Dictionary<string,NotifyDelegate> notifyDelegates;

	private static volatile NotificationCenter instance;
	private static object syncRoot = new Object();
	
	private NotificationCenter() {
		notifyDelegates = new Dictionary<string,NotifyDelegate>();
	}
	
	public static NotificationCenter Instance
	{
		get 
		{
			if (instance == null) 
			{
				lock (syncRoot) 
				{
					if (instance == null) 
						instance = new NotificationCenter();
				}
			}
			
			return instance;
		}
	}

	public void DefaultNotification( params object[] args){}  

	public void CreateNotificationDelegate(string name)
	{
		if(!notifyDelegates.ContainsKey(name))
		{
			NotifyDelegate notification = DefaultNotification;
			notifyDelegates.Add(name,notification);
		}
	}

	public void Notify( string name, params object[] args )
	{
		if(notifyDelegates.ContainsKey(name))
		{
			notifyDelegates[name](args);
		}
	}

	public void Subscribe( string name, NotifyDelegate del)
	{
		CreateNotificationDelegate(name);
		notifyDelegates[name] += del;
	}

	public void Unsubscribe( string name, NotifyDelegate del)
	{
		if(notifyDelegates.ContainsKey(name))
		{
			notifyDelegates[name] -= del;
		}
	}

	public void ClearAllSubscriptions()
	{
		notifyDelegates.Clear();
	}
}
