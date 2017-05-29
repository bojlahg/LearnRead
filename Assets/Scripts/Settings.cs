using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Settings : MonoBehaviour
{
	private static Settings settingsInstance;
	public static Settings instance { get { return settingsInstance; } }
	//
	private WordType _wordType = WordType.Common;
	private IndexerArrayReadWrite<bool> _wordLengths = new IndexerArrayReadWrite<bool>(14);
	private bool _capitalLetters = true, _colorLetters = true;
	//
	public WordType wordType { get { return _wordType; } set { _wordType = value; } }
	public IndexerArrayReadWrite<bool> wordLengths { get { return _wordLengths; } }
	public bool capitalLetters { get { return _capitalLetters; } set { _capitalLetters = value; } }
	public bool colorLetters { get { return _colorLetters; } set { _colorLetters = value; } }

	private void Awake()
	{
		settingsInstance = this;
	}

	public void ReStore()
	{
		_wordType = (WordType)PlayerPrefs.GetInt("wordType", 0);

		string str = PlayerPrefs.GetString("wordLengths", "11111111111111");
		for(int i = 0; i < str.Length; ++i)
		{
			_wordLengths[i] = (str[i] == '1');
		}
		_capitalLetters = PlayerPrefs.GetInt("capitalLetters", 1) == 1;
		_colorLetters = PlayerPrefs.GetInt("colorLetters", 1) == 1;
	}

	public void Store()
	{
		PlayerPrefs.SetInt("wordType", (int)_wordType);

		StringBuilder sb = new StringBuilder(14);
		for(int i = 0; i < 14; ++i)
		{
			if(_wordLengths[i])
			{
				sb.Append('1');
			}
			else
			{
				sb.Append('0');
			}
		}
		PlayerPrefs.SetString("wordLengths", sb.ToString());
		PlayerPrefs.SetInt("capitalLetters", _capitalLetters ? 1 : 0);
		PlayerPrefs.SetInt("colorLetters", _colorLetters ? 1 : 0);
		PlayerPrefs.Save();
	}
}
