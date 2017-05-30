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
	public Text textWord;
	public Color colorVowel, colorConsonant, colorOther, colorSeparator;

	public override void OnInit()
	{
		readScreenInstance = this;
	}

	public override void OnShow()
	{
		
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

	public void Button_Next()
	{
		Game.instance.Next();
	}

	public void Button_Quit()
	{
		SettingsScreen.instance.Show();
	}
}
