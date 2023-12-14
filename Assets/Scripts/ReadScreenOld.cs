using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Whisper.Utils;
using Whisper;

public class ReadScreenOld : UIScreen
{
	private static ReadScreenOld readScreenInstance;
	public static ReadScreenOld instance { get { return readScreenInstance; } }
	//
	public TextMeshProUGUI m_WordText, m_StatText;
	public Color m_VowelColor, m_ConsonantColor, m_OtherColor, m_SeparatorColor;
	public AudioClip[] m_GoodSounds, m_BadSounds, m_SkipSounds;
	public AudioSource m_AudioSource;

	private string m_OriginalWord;
	private bool m_Recording = false;
	private float m_RecordDuration = 0;
	private float m_RecordLength = 5;
	//
	private int m_GoodCount = 0, m_BadCount = 0;

	public override void OnInit()
	{
		readScreenInstance = this;
	}

	public override void OnShow()
	{
		m_GoodCount = 0;
		m_BadCount = 0;
		UpdateStat();
	}

	public void UpdateWord(string word)
	{
		m_OriginalWord = word;
		if (Settings.instance.splitSyllables)
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
					clrword.Append(ColorUtility.ToHtmlStringRGBA(m_VowelColor));
					clrword.Append(">");
				}
				else if(chr == 'ь' || chr == 'ъ')
				{
					clrword.Append(ColorUtility.ToHtmlStringRGBA(m_OtherColor));
					clrword.Append(">");
				}
				else if(chr == '|' || chr == '-')
				{
					clrword.Append(ColorUtility.ToHtmlStringRGBA(m_SeparatorColor));
					clrword.Append(">");
				}
				else
				{
					clrword.Append(ColorUtility.ToHtmlStringRGBA(m_ConsonantColor));
					clrword.Append(">");
				}
				clrword.Append(origchr);
				clrword.Append("</color>");
			}
			m_WordText.text = clrword.ToString();
		}
		else
		{
			m_WordText.text = word;
		}
	}

	public void Button_Good()
	{
		if (Settings.instance.soundEnabled)
		{
			m_AudioSource.clip = m_GoodSounds[Random.Range(0, m_GoodSounds.Length)];
			m_AudioSource.Play();
		}
		Game.instance.Next();
		++m_GoodCount;
		UpdateStat();
	}

	public void Button_Bad()
	{
		if (Settings.instance.soundEnabled)
		{
			m_AudioSource.clip = m_BadSounds[Random.Range(0, m_BadSounds.Length)];
			m_AudioSource.Play();
		}
		Game.instance.Next();
		++m_BadCount;
		UpdateStat();
	}

	public void Button_Skip()
	{
		if (Settings.instance.soundEnabled)
		{
			m_AudioSource.clip = m_SkipSounds[Random.Range(0, m_SkipSounds.Length)];
			m_AudioSource.Play();
		}
		Game.instance.Next();
	}

	public void Button_Quit()
	{
		
		SettingsScreen.instance.Show();
	}

	private void UpdateStat()
	{
		m_StatText.text = string.Format("<color=#8EC713FF>{0}</color> / <color=#CB1D1DFF>{1}</color>", m_GoodCount, m_BadCount);
	}
}
