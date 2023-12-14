using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class AnimatorToggle : MonoBehaviour
{
	private Toggle m_Toggle;
	private Animator m_Animator;
	//
	public RuntimeAnimatorController m_OnAnim, m_OffAnim;
		

	private void OnEnable()
	{
		if(!m_Toggle)
		{
			m_Toggle = GetComponent<Toggle>();
		}
		if (!m_Animator)
        {
			m_Animator = GetComponent<Animator>();
		}
		ValueChanged(m_Toggle.isOn);
		m_Toggle.onValueChanged.AddListener(ValueChanged);
	}

    private void OnDisable()
    {
		m_Toggle.onValueChanged.RemoveListener(ValueChanged);
	}

    public void ValueChanged(bool val)
	{
		if(!m_Toggle)
		{
			m_Toggle = GetComponent<Toggle>();
		}
		if(val)
		{
			m_Animator.runtimeAnimatorController = m_OnAnim;
		}
		else
		{
			m_Animator.runtimeAnimatorController = m_OffAnim;
		}
	}
}
