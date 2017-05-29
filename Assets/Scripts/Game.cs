using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	private static Game gameInstance;
	public static Game instance { get { return gameInstance; } }
	//
	private string _word = "", lastWord = "";
	public string word { get { return _word; } }

	private void Awake()
	{
		gameInstance = this;
	}

	public void StartGame()
	{
		_word = WordBase.instance.GetRandomWord(Settings.instance.minWordLength, Settings.instance.maxWordLength, Settings.instance.capitalLetters);
		lastWord = _word;
		ReadScreen.instance.UpdateWord(_word);
	}

	public void Next()
	{
		do
		{
			_word = WordBase.instance.GetRandomWord(Settings.instance.minWordLength, Settings.instance.maxWordLength, Settings.instance.capitalLetters);
		}
		while(lastWord == _word);
		lastWord = _word;
		ReadScreen.instance.UpdateWord(_word);
	}
}
