using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
	private static UISystem uiSystemInstance;
	public static UISystem instance { get { return uiSystemInstance; } }
	//
	private UIScreen curScreen = null;

	private void Awake()
	{
		uiSystemInstance = this;

		UIScreen[] screens = GetComponentsInChildren<UIScreen>(true);

		foreach(UIScreen scr in screens)
		{
			scr.gameObject.SetActive(false);
			scr.OnInit();
		}
	}

	public void Show(UIScreen scr)
	{
		if(curScreen != null)
		{
			curScreen.gameObject.SetActive(false);
			curScreen.OnHide();
		}
		curScreen = scr;
		curScreen.OnShow();
		curScreen.gameObject.SetActive(true);
	}
}
