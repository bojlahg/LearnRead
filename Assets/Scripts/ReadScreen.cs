using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ReadScreen : UIScreen
{
	private static ReadScreen readScreenInstance;
	public static ReadScreen instance { get { return readScreenInstance; } }
	//
	public Text textWord, textStat;
	public Color colorVowel, colorConsonant, colorOther, colorSeparator;
	private int goodCnt = 0, badCnt = 0;

	public override void OnInit()
	{
		readScreenInstance = this;
	}

	public override void OnShow()
	{
		goodCnt = 0;
		badCnt = 0;
		UpdateStat();
	}

	public void UpdateWord(string word)
	{
		if(Settings.instance.splitSyllables)
		{
			word = SyllabesRus.Convert(word);
		}

		if(Settings.instance.colorLetters)
		{
			StringBuilder clrword = new StringBuilder(1024);
			char chr, origchr;
			for(int i = 0; i < word.Length; ++i)
			{
				origchr = word[i];
				chr = char.ToLower(origchr);
				clrword.Append("<color=#");
				if(chr == 'а' || chr == 'е' || chr == 'ё' || chr == 'и' || chr == 'о' ||
					chr == 'у' || chr == 'ы' || chr == 'э' || chr == 'ю' || chr == 'я')
				{
					clrword.Append(ColorUtility.ToHtmlStringRGBA(colorVowel));
					clrword.Append(">");
				}
				else if(chr == 'ь' || chr == 'ъ')
				{
					clrword.Append(ColorUtility.ToHtmlStringRGBA(colorOther));
					clrword.Append(">");
				}
				else if(chr == '|' || chr == '-')
				{
					clrword.Append(ColorUtility.ToHtmlStringRGBA(colorSeparator));
					clrword.Append(">");
				}
				else
				{
					clrword.Append(ColorUtility.ToHtmlStringRGBA(colorConsonant));
					clrword.Append(">");
				}
				clrword.Append(origchr);
				clrword.Append("</color>");
			}
			textWord.text = clrword.ToString();
		}
		else
		{
			textWord.text = word;
		}
	}

	public void Button_Good()
	{
		Game.instance.Next();
		++goodCnt;
		UpdateStat();
	}

	public void Button_Bad()
	{
		Game.instance.Next();
		++badCnt;
		UpdateStat();
	}

	public void Button_Pass()
	{
		Game.instance.Next();
	}

	public void Button_Quit()
	{
		SettingsScreen.instance.Show();
	}

	private void UpdateStat()
	{
		textStat.text = string.Format("<color=#8EC713FF>{0}</color> / <color=#CB1D1DFF>{1}</color>", goodCnt, badCnt);
	}
}
