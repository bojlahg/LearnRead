using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SyllabesRus
{
	private static char[] alphabet = new char[] {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я'};
	private static char[] vowel = new char[] {'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
	private static char[] voiced = new char[] {'б', 'в', 'г', 'д', 'ж', 'з', 'л', 'м', 'н', 'р', 'х', 'ц', 'ч', 'ш', 'щ' };
	private static char[] deaf = new char[] {'к', 'п', 'с', 'т', 'ф' };
	private static char[] brief = new char[] {'й' };
	private static char[] other = new char[] {'ь', 'ъ' };
	private static char[] cons = new char[] {'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };

	private static bool IsNotLastSep(string a)
	{
		for(int i = 0; i < a.Length; i++)
		{
			if(char.ToLower(a[i]).IndexOfAny(vowel) != -1)
			{
				return true;
			}
		}
		return false;
	}

	private static string AddSep(string e, List<string> r, bool a)
	{
		if(!a)
		{
			e += "|";
			return e;
		}
		if(e.ToLower().IndexOfAny(vowel) < 0)
		{
			return e;
		}
		/*if(e.IndexOf('-') > -1)
		{
			return e;
		}*/
		r.Add(e);
		return "";
	}

	public static int IndexOfAny(this char c, char[] str)
	{
		for(int i = 0; i < str.Length; ++i)
		{
			if(str[i] == c)
			{
				return i;
			}
		}
		return -1;
	}

	public static string RemoveAny(this string str, char[] chrs)
	{
		string newstr = "";
		for(int i = 0; i < str.Length; ++i)
		{
			if(str[i].IndexOfAny(chrs) == -1)
			{
				newstr += str[i];
			}
		}
		return newstr;
	}

	public static string Convert(string word)
	{
		char d = '\0';
		string e = "";

		string[] words = word.Split('-');
		List<string> result = new List<string>();

		for(int j = 0; j < words.Length; ++j)
		{
			word = words[j];
			for(int i = 0; i < word.Length; i++)
			{
				d = word[i];
				e += d;
				d = char.ToLower(d);

				if(i + 1 < word.Length)
				{
					if(char.ToLower(word[i + 1]).IndexOfAny(alphabet) > -1)
					{
						if((i != 0) &&
							(i != word.Length - 1) &&
							(d.IndexOfAny(brief) != -1) &&
							(IsNotLastSep(word.Substring(i + 1, word.Length - i - 1))))
						{
							e = AddSep(e, result, false);
						}
						else if((i < word.Length - 1) &&
							(d.IndexOfAny(vowel) != -1) &&
							(char.ToLower(word[i + 1]).IndexOfAny(vowel) != -1))
						{
							e = AddSep(e, result, false);
						}
						else if((i < word.Length - 2) &&
							(d.IndexOfAny(vowel) != -1) &&
							(char.ToLower(word[i + 1]).IndexOfAny(cons) != -1) &&
							(char.ToLower(word[i + 2]).IndexOfAny(vowel) != -1))
						{
							e = AddSep(e, result, false);
						}
						else if((i + 2 < word.Length) &&
							(d.IndexOfAny(vowel) != -1) &&
							(char.ToLower(word[i + 1]).IndexOfAny(deaf) != -1) &&
							(char.ToLower(word[i + 2]).IndexOfAny(cons) != -1) &&
							(IsNotLastSep(word.Substring(i + 1, word.Length - i - 1))))
						{
							e = AddSep(e, result, false);
						}
						else if((i > 0) &&
							(i + 1 < word.Length) &&
							(d.IndexOfAny(voiced) != -1) &&
							(char.ToLower(word[i - 1]).IndexOfAny(vowel) != -1) &&
							(char.ToLower(word[i + 1]).IndexOfAny(vowel) == -1) &&
							(char.ToLower(word[i + 1]).IndexOfAny(other) == -1) &&
							(IsNotLastSep(word.Substring(i + 1, word.Length - i - 1))))
						{
							e = AddSep(e, result, false);
						}
						else if((i < word.Length - 1) &&
							(d.IndexOfAny(other) != -1) &&
							((char.ToLower(word[i + 1]).IndexOfAny(vowel) == -1) || (IsNotLastSep(word.Substring(0, i)))))
						{
							e = AddSep(e, result, false);
						}
					}
				}
			}
			e = AddSep(e, result, true);
		}

		if(e.Length > 0)
		{
			result.Add(e);
		}

		e = "";
		for(int i = 0; i < result.Count; ++i)
		{
			if(i == result.Count - 1)
			{
				e += result[i];
			}
			else
			{
				e += result[i] + '-';
			}
		}

		return e;
	}
}
