using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadScreen : UIScreen
{
	private static ReadScreen readScreenInstance;
	public static ReadScreen instance { get { return readScreenInstance; } }
	//
	public Text textWord;

	public override void OnInit()
	{
		readScreenInstance = this;
	}

	public override void OnShow()
	{
		
	}

	public void UpdateWord(string word)
	{
		textWord.text = word;
	}

	public void Button_Next()
	{
		Game.instance.Next();
	}

	public void Button_Quit()
	{
		SettingsScreen.instance.Show();
	}
}
