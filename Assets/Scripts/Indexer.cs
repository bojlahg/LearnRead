using System.Collections;
using System.Collections.Generic;

public class IndexerArrayReadWrite<T>
{
	private T[] _array;

	public IndexerArrayReadWrite(int len)
	{
		_array = new T[len];
	}

	public T[] array
	{
		get { return _array; }
	}

	public T this[int idx]
	{
		get { return _array[idx]; }
		set { _array[idx] = value; }
	}
}