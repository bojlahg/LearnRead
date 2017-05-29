using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : UIScreen
{
	private static SettingsScreen settingsScreenInstance;
	public static SettingsScreen instance { get { return settingsScreenInstance; } }
	//
	public Text textMinWordLength, textMaxWordLength;
	public Toggle toggleCapitalLetters;

	public override void OnInit()
	{
		settingsScreenInstance = this;
	}

	public override void OnShow()
	{
		textMinWordLength.text = Settings.instance.minWordLength.ToString();
		textMaxWordLength.text = Settings.instance.maxWordLength.ToString();
		toggleCapitalLetters.isOn = Settings.instance.capitalLetters;
	}

	public void Button_Start()
	{
		ReadScreen.instance.Show();
		Game.instance.StartGame();
	}
	
	public void Button_IncreaseMin()
	{
		if(Settings.instance.minWordLength + 1 <= WordBase.instance.maxWordLength)
		{
			++Settings.instance.minWordLength;
			textMinWordLength.text = Settings.instance.minWordLength.ToString();
		}
	}

	public void Button_DecreaseMin()
	{
		if(Settings.instance.minWordLength - 1 >= WordBase.instance.minWordLength)
		{
			--Settings.instance.minWordLength;
			textMinWordLength.text = Settings.instance.minWordLength.ToString();
		}
	}

	public void Button_IncreaseMax()
	{
		if(Settings.instance.maxWordLength + 1 <= WordBase.instance.maxWordLength)
		{
			++Settings.instance.maxWordLength;
			textMaxWordLength.text = Settings.instance.maxWordLength.ToString();
		}
	}

	public void Button_DecreaseMax()
	{
		if(Settings.instance.maxWordLength - 1 >= WordBase.instance.minWordLength)
		{
			--Settings.instance.maxWordLength;
			textMaxWordLength.text = Settings.instance.maxWordLength.ToString();
		}
	}

	public void Toggle_Caps(bool val)
	{
		Settings.instance.capitalLetters = val;
	}
}
