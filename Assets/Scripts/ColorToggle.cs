using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ColorToggle : MonoBehaviour
{
	[System.Serializable]
	public class ColorToggleEvent: UnityEvent<int, bool>
	{
	}

	private Toggle toggle;
	//
	public int index;
	public Color onColor, offColor;

	public ColorToggleEvent onValueChanged;

	/*private void OnEnable()
	{
		if(!toggle)
		{
			toggle = GetComponent<Toggle>();
		}
		toggle.onValueChanged.AddListener(ValueChanged);
	}*/

	public void ValueChanged(bool val)
	{
		if(!toggle)
		{
			toggle = GetComponent<Toggle>();
		}
		if(val)
		{
			toggle.targetGraphic.color = onColor;
		}
		else
		{
			toggle.targetGraphic.color = offColor;
		}
		onValueChanged.Invoke(index, val);
		//Debug.LogFormat("{0}: {1}", gameObject.name, val);
	}
}
