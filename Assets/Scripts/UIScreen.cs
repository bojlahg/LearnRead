using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
	public virtual void OnInit()
	{

	}

	public virtual void OnShow()
	{

	}

	public virtual void OnHide()
	{

	}

	public void Show()
	{
		UISystem.instance.Show(this);
	}
}
