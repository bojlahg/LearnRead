using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	private static Settings settingsInstance;
	public static Settings instance { get { return settingsInstance; } }
	//
	private WordType _wordType = WordType.Common;
	private int _minWordLength = 2, _maxWordLength = 4;
	private bool _capitalLetters = true;
	//
	public WordType wordType { get { return _wordType; } set { _wordType = value; } }
	public int minWordLength { get { return _minWordLength; } set { _minWordLength = value; } }
	public int maxWordLength { get { return _maxWordLength; } set { _maxWordLength = value; } }
	public bool capitalLetters { get { return _capitalLetters; } set { _capitalLetters = value; } }

	private void Awake()
	{
		settingsInstance = this;
	}

	public void ReStore()
	{
		_minWordLength = PlayerPrefs.GetInt("minWordLength", 2);
		_maxWordLength = PlayerPrefs.GetInt("maxWordLength", 4);
		_capitalLetters = PlayerPrefs.GetInt("capitalLetters", 1) == 1;
	}

	public void Store()
	{
		PlayerPrefs.SetInt("minWordLength", _minWordLength);
		PlayerPrefs.SetInt("maxWordLength", _maxWordLength);
		PlayerPrefs.SetInt("capitalLetters", _capitalLetters ? 1 : 0);
	}


}
