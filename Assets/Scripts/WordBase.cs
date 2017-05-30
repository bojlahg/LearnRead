using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum WordType
{
	Common = 0,
	Names = 1
}

public class WordBase: MonoBehaviour
{
	private static WordBase wordBaseInstance;
	public static WordBase instance { get  { return wordBaseInstance; } }
	//
	public TextAsset commonSrc, namesSrc;
	//
	private Dictionary<int, List<string>> commonWordDict = new Dictionary<int, List<string>>();
	private Dictionary<int, List<string>> nameWordDict = new Dictionary<int, List<string>>();

	private void Awake()
	{
		wordBaseInstance = this;
	}

	public void Init()
	{
		ReadWords(commonSrc.text, commonWordDict);
		ReadWords(namesSrc.text, nameWordDict);
	}

	public static void ReadWords(string text, Dictionary<int, List<string>> wordDict)
	{
		StringBuilder word = new StringBuilder(20);
		char chr;
		for(int i = 0; i < text.Length; ++i)
		{
			chr = text[i];
			if(chr == '\n' || chr == '\r')
			{
				AddWord(word.ToString(), wordDict);
				word.Length = 0;
			}
			else
			{
				word.Append(chr);
			}
		}
		AddWord(word.ToString().Trim(), wordDict);
	}

	public static void AddWord(string word, Dictionary<int, List<string>> wordDict)
	{
		if(word.Length == 0)
		{
			return;
		}
#if APPDEMO
		if(word.Length > 6)
		{
			return;
		}
#endif
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

	public string GetRandomWord(WordType wt, bool[] awl, bool capital)
	{
		Dictionary<int, List<string>> wordDict = null;
		int lenidx = -1, wordidx = -1;
		List<string> words = null;

		switch(wt)
		{
		case WordType.Common: wordDict = commonWordDict; break;
		case WordType.Names: wordDict = nameWordDict; break;
		}

		List<int> allowed = new List<int>();
		int idx;
		foreach(KeyValuePair<int, List<string>> kvp in wordDict)
		{
			idx = kvp.Key - 2;
			if(idx >= 0 && idx < awl.Length && awl[idx])
			{
				allowed.Add(kvp.Key);
			}
		}

		lenidx = allowed[Random.Range(0, allowed.Count)];
		if(!wordDict.TryGetValue(lenidx, out words))
		{
			return "ERROR!";
		}
		
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

	public bool CheckSettings(WordType wt, bool[] awl)
	{
		Dictionary<int, List<string>> wordDict = null;
		switch(wt)
		{
		case WordType.Common:
			wordDict = commonWordDict;
			break;
		case WordType.Names:
			wordDict = nameWordDict;
			break;
		}

		List<int> allowed = new List<int>();
		int idx;
		foreach(KeyValuePair<int, List<string>> kvp in wordDict)
		{
			idx = kvp.Key - 2;
			if(idx >= 0 && idx < awl.Length && awl[idx])
			{
				allowed.Add(kvp.Key);
			}
		}

		return allowed.Count > 0;
	}
}
