using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum WordType
{
	Common = 0,
	Names = 1,
	Abbrevs = 2
}

public class WordBase: MonoBehaviour
{
	private static WordBase wordBaseInstance;
	public static WordBase instance { get  { return wordBaseInstance; } }
	//
	public TextAsset commonSrc, namesSrc, AbbrevSrc;
	//
	private Dictionary<int, List<string>> wordDict = new Dictionary<int, List<string>>();
	private int _minWordLength, _maxWordLength;

	public int minWordLength { get { return _minWordLength; } }
	public int maxWordLength { get { return _maxWordLength; } }

	private void Awake()
	{
		wordBaseInstance = this;
	}

	public void Init()
	{
		ReadWords(commonSrc.text);

		_minWordLength = 1000;
		_maxWordLength = 0;
		int totalCnt = 0;
		foreach(KeyValuePair<int, List<string>> kvp in wordDict)
		{
			if(_minWordLength > kvp.Key)
			{
				_minWordLength = kvp.Key;
			}
			if(_maxWordLength < kvp.Key)
			{
				_maxWordLength = kvp.Key;
			}

			Debug.LogFormat("Length: {0} Count: {1}", kvp.Key, kvp.Value.Count);
			totalCnt += kvp.Value.Count;
		}
		Debug.LogFormat("Total count: {0} Lengths: {1} to {2}", totalCnt, _minWordLength, _maxWordLength);

		//
		//SaveDictByLen();
	}

	private void SaveDictByLen()
	{
		foreach(KeyValuePair<int, List<string>> kvp in wordDict)
		{
			List<string> allcap = new List<string>(), firstcap = new List<string>(), mixcap = new List<string>(), nocap = new List<string>();

			foreach(string word in kvp.Value)
			{
				if(word == word.ToUpper())
				{
					allcap.Add(word);
				}
				else if(word == word.ToLower())
				{
					nocap.Add(word);
				}
				else if(word[0] == char.ToUpper(word[0]))
				{
					firstcap.Add(word);
				}
				else
				{
					mixcap.Add(word);
				}
			}

			allcap.Sort();
			firstcap.Sort();
			mixcap.Sort();
			nocap.Sort();

			System.IO.File.WriteAllLines(string.Format("{0}/Text/{1}_ac.txt", Application.dataPath, kvp.Key), allcap.ToArray());
			System.IO.File.WriteAllLines(string.Format("{0}/Text/{1}_fc.txt", Application.dataPath, kvp.Key), firstcap.ToArray());
			System.IO.File.WriteAllLines(string.Format("{0}/Text/{1}_mc.txt", Application.dataPath, kvp.Key), mixcap.ToArray());
			System.IO.File.WriteAllLines(string.Format("{0}/Text/{1}_nc.txt", Application.dataPath, kvp.Key), nocap.ToArray());
		}
	}

	public void ReadWords(string text)
	{
		StringBuilder word = new StringBuilder(20);
		char chr;
		for(int i = 0; i < text.Length; ++i)
		{
			chr = text[i];
			if(chr == '\n' || chr == '\r')
			{
				AddWord(word.ToString());
				word.Length = 0;
			}
			else
			{
				word.Append(chr);
			}
		}
		AddWord(word.ToString());

		commonSrc = null;
	}

	public void AddWord(string word)
	{
		if(word.Length > 0)
		{
			List<string> words;
			if(wordDict.TryGetValue(word.Length, out words))
			{
				words.Add(word);
			}
			else
			{
				words = new List<string>();
				words.Add(word);
				wordDict.Add(word.Length, words);
			}
		}
	}

	public string GetRandomWord(int minlen, int maxlen, bool capital)
	{
		int lenidx = -1, wordidx = -1;
		List<string> words = null;
		do
		{
			lenidx = Random.Range(Settings.instance.minWordLength, Settings.instance.maxWordLength);
			if(!wordDict.TryGetValue(lenidx, out words))
			{
				words = null;
			}
		}
		while(words == null);

		wordidx = Random.Range(0, words.Count);

		if(capital)
		{
			return words[wordidx].ToUpper();
		}
		else
		{
			return words[wordidx];
		}
	}
}
