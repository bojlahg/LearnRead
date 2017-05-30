using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : UIScreen
{
	private static SettingsScreen settingsScreenInstance;
	public static SettingsScreen instance { get { return settingsScreenInstance; } }
	//
	public Toggle toggleCommonWords, toggleNames, toggleCapitalLetters, toggleColorLetters, toggleSplitSyllables;
	public Toggle[] toggleLengths;
	public Text textTitle;

	public override void OnInit()
	{
		settingsScreenInstance = this;

#if APPDEMO
		textTitle.text = "Родители учат читать\n(демо)";
#endif
	}

	public override void OnShow()
	{
		toggleCommonWords.isOn = Settings.instance.wordType == WordType.Common;
		toggleNames.isOn = Settings.instance.wordType == WordType.Names;
		toggleCapitalLetters.isOn = Settings.instance.capitalLetters;
		toggleColorLetters.isOn = Settings.instance.colorLetters;
		toggleSplitSyllables.isOn = Settings.instance.splitSyllables;

		for(int i = 0; i < toggleLengths.Length; ++i)
		{
			toggleLengths[i].isOn = Settings.instance.wordLengths[i];
#if APPDEMO
			if(i > 4)
			{
				toggleLengths[i].gameObject.SetActive(false);
			}
#endif
		}


	}

	public void Button_Start()
	{
		if(WordBase.instance.CheckSettings(Settings.instance.wordType, Settings.instance.wordLengths.array))
		{
			ReadScreen.instance.Show();
			Game.instance.StartGame();
		}
	}

	public void Toggle_Length(int idx, bool val)
	{
		Settings.instance.wordLengths[idx] = val;
	}

	/*public void Button_IncreaseLength()
	{
		if(Settings.instance.wordLength + 1 <= WordBase.instance.maxWordLength)
		{
			++Settings.instance.wordLength;
			textWordLength.text = Settings.instance.wordLength.ToString();
		}
	}

	public void Button_DecreaseLength()
	{
		if(Settings.instance.wordLength - 1 >= WordBase.instance.minWordLength)
		{
			--Settings.instance.wordLength;
			textWordLength.text = Settings.instance.wordLength.ToString();
		}
	}*/

	public void Toggle_Common(bool val)
	{
		if(val)
		{
			Settings.instance.wordType = WordType.Common;
		}
	}

	public void Toggle_Names(bool val)
	{
		if(val)
		{
			Settings.instance.wordType = WordType.Names;
		}
	}

	public void Toggle_Caps(bool val)
	{
		Settings.instance.capitalLetters = val;
	}

	public void Toggle_SplitSyllables(bool val)
	{
		Settings.instance.splitSyllables = val;
	}

	public void Toggle_ColorLetters(bool val)
	{
		Settings.instance.colorLetters = val;
	}
}
