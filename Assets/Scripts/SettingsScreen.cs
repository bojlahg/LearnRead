using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsScreen : UIScreen
{
	private static SettingsScreen settingsScreenInstance;
	public static SettingsScreen instance { get { return settingsScreenInstance; } }
	//
	public Toggle m_CommonWordsToggle, m_NamesToggle, m_CapitalLettersToggle, m_ColorLettersToggle, m_SplitSyllablesToggle, m_SoundToggle;
	public GameObject m_WordLengthPrefab;
	//
	private Toggle[] m_LengthToggles;

	public override void OnInit()
	{
		settingsScreenInstance = this;

		m_WordLengthPrefab.SetActive(false);
	}

	public override void OnShow()
	{
		m_CommonWordsToggle.isOn = Settings.instance.wordType == WordType.Common;
		m_NamesToggle.isOn = Settings.instance.wordType == WordType.Names;
		m_CapitalLettersToggle.isOn = Settings.instance.capitalLetters;
		m_ColorLettersToggle.isOn = Settings.instance.colorLetters;
		m_SplitSyllablesToggle.isOn = Settings.instance.splitSyllables;
		m_SoundToggle.isOn = Settings.instance.soundEnabled;

		m_LengthToggles = new Toggle[Settings.instance.wordLengths.array.Length];
		for (int i = 0; i < m_LengthToggles.Length; ++i)
		{
			GameObject newgo = GameObject.Instantiate(m_WordLengthPrefab, m_WordLengthPrefab.transform.parent);
			newgo.SetActive(true);
			m_LengthToggles[i] = newgo.GetComponent<Toggle>();
			m_LengthToggles[i].isOn = Settings.instance.wordLengths[i];
			TextMeshProUGUI text = m_LengthToggles[i].transform.GetComponentInChildren<TextMeshProUGUI>();
			text.text = (2 + i).ToString();
		}
	}

	public override void OnHide()
	{
		for (int i = 0; i < m_LengthToggles.Length; ++i)
		{
			GameObject.Destroy(m_LengthToggles[i].gameObject);
		}
		m_LengthToggles = null;
	}

	public void Button_Start()
	{
		if(WordBase.instance.CheckSettings(Settings.instance.wordType, Settings.instance.wordLengths.array))
		{
			for (int i = 0; i < m_LengthToggles.Length; ++i)
			{
				Settings.instance.wordLengths[i] = m_LengthToggles[i];
			}
			if (m_CommonWordsToggle.isOn)
			{
				Settings.instance.wordType = WordType.Common;
			}
            else 
			{
				Settings.instance.wordType = WordType.Names;
			}
			
			Settings.instance.capitalLetters = m_CapitalLettersToggle.isOn;
			Settings.instance.splitSyllables = m_SplitSyllablesToggle.isOn;
			Settings.instance.colorLetters = m_ColorLettersToggle.isOn;
			Settings.instance.soundEnabled = m_SoundToggle.isOn;

			ReadScreen.instance.Show();
			Game.instance.StartGame();
		}
	}

}
