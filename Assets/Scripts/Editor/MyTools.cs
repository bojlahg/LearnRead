using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class MyTools
{
	[MenuItem("Tools/Process dictionary")]
	private static void ProcessDictionary()
	{
		string[] lines = File.ReadAllLines(string.Format("{0}/Text/dict.txt", Application.dataPath));

		Dictionary<int, List<string>> wordDict = new Dictionary<int, List<string>>();

		foreach(string word in lines)
		{
			if(word.Length > 0 && word.Length <= 15)
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

		string dir = string.Format("{0}/Text/Processed/", Application.dataPath);
		if(!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
		}

		List<string> allcap = new List<string>(),
				firstcap = new List<string>(),
				mixcap = new List<string>(),
				nocap = new List<string>(),
				firstcapother = new List<string>(),
				other = new List<string>();
		foreach(KeyValuePair<int, List<string>> kvp in wordDict)
		{
			foreach(string word in kvp.Value)
			{
				if(word.IndexOfAny(new char[] { '.', '-', ',', '!', '@', '#', ':', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=', '{', '}', ';', '"', '\'', '?', '<', '>', '~', '`' }) >= 0)
				{
					if(word[0] == char.ToUpper(word[0]))
					{
						firstcapother.Add(word);
					}
					else
					{
						other.Add(word);
					}
				}
				else if(word == word.ToUpper())
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
		}

		allcap.Sort(compare);
		firstcap.Sort(compare);
		mixcap.Sort(compare);
		nocap.Sort(compare);
		firstcapother.Sort(compare);
		other.Sort(compare);

		if(allcap.Count > 0)
		{
			File.WriteAllLines(string.Format("{0}/allcap.txt", dir), allcap.ToArray());
		}
		if(firstcap.Count > 0)
		{
			File.WriteAllLines(string.Format("{0}/firstcap.txt", dir), firstcap.ToArray());
		}
		if(mixcap.Count > 0)
		{
			File.WriteAllLines(string.Format("{0}/mixcap.txt", dir), mixcap.ToArray());
		}
		if(nocap.Count > 0)
		{
			File.WriteAllLines(string.Format("{0}/nocapc.txt", dir), nocap.ToArray());
		}
		if(firstcapother.Count > 0)
		{
			File.WriteAllLines(string.Format("{0}/firstcapother.txt", dir), firstcapother.ToArray());
		}
		if(other.Count > 0)
		{
			File.WriteAllLines(string.Format("{0}/other.txt", dir), other.ToArray());
		}
	}

	private static int compare(string a, string b)
	{
		if(a.Length == b.Length)
		{
			return a.CompareTo(b);
		}
		return a.Length.CompareTo(b.Length);
	}
}
