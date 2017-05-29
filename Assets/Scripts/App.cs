using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
	private static App appInstance;
	public App instance { get { return appInstance; } }

	public int minWordLength, maxWordLength;

	private void Awake()
	{
		appInstance = this;
	}

	private void Start()
	{
		Settings.instance.ReStore();
		WordBase.instance.Init();

		SettingsScreen.instance.Show();
	}

	private void OnApplicationFocus(bool focus)
	{
#if UNITY_WP_8_1
		if(!focus)
		{
			Settings.instance.Store();
		}
#endif
	}

	private void OnApplicationPause(bool pause)
	{
#if UNITY_ANDROID || UNITY_IOS
		if(pause)
		{
			Settings.instance.Store();
		}
#endif
	}

	private void OnApplicationQuit()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		Settings.instance.Store();
#endif
	}

}
