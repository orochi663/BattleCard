using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MapUtility;
using BuffUtility;

public class GainSelfPower : MonoBehaviour, CommonSkill{
	
	Transform aider;
	public PowerType[] Mode;
	public int[] Value;
	public Dictionary<PowerType, int> PowerList;
	Transform fxBuffAtk, fxBuffDef, fxBuffRange, fxBuffCritiq, fxBuffMove, fxBuffSkill;
	
	// Use this for initialization
	void Start () {
		aider = transform.parent.parent;
		PowerList = new Dictionary<PowerType, int>();
		for(int i=0;i<Mode.Length;i++){
			PowerList.Add(Mode[i],Value[i]);
		}
		CommonFX cFX  = Camera.main.GetComponent<CommonFX>();
		fxBuffAtk = cFX.BuffAtk;
		fxBuffCritiq = cFX.BuffCritiq;
		fxBuffDef = cFX.BuffDef;
		fxBuffMove = cFX.BuffMove;
		fxBuffRange = cFX.BuffRange;
		fxBuffSkill = cFX.BuffSkill;
	}
	
	public void InsertSelection (Transform map)
	{
		throw new System.NotImplementedException ();
	}
	
	public IList GetSelectionRange ()
	{
		throw new System.NotImplementedException ();
	}
	
	void BuffVisualUI(BuffType type, int val, BuffSlidingUI bSUI){
		Dictionary<BuffType,int> dict = new Dictionary<BuffType, int>();
		dict.Add(type, val);
		BuffUI bUI = new BuffUI(aider,dict);
		bSUI.UIItems.Add(bUI);
		bSUI.FadeInUI = true;
	}
	
	public void Execute ()
	{
		BuffCalculation bCal = new BuffCalculation(aider);
		BuffSlidingUI bSUI = Camera.mainCamera.GetComponent<BuffSlidingUI>();
		foreach(var pair in PowerList){
			switch(pair.Key){
				case PowerType.Critical:
					aider.GetComponent<BuffList>().ExtraDict[BuffType.CriticalHit] += pair.Value;
					BuffVisualUI(BuffType.CriticalHit, pair.Value, bSUI);
					MapHelper.SetFX(aider,fxBuffCritiq,4.0f);
					bCal.UpdateBuffValue();
					break;
				case PowerType.Damage:
					aider.GetComponent<BuffList>().ExtraDict[BuffType.Attack] += pair.Value;
					BuffVisualUI(BuffType.Attack, pair.Value, bSUI);
					MapHelper.SetFX(aider,fxBuffAtk,4.0f);
					bCal.UpdateBuffValue();
					break;
				case PowerType.Hp:
					aider.GetComponent<BuffList>().ExtraDict[BuffType.Defense] += pair.Value;
					BuffVisualUI(BuffType.Defense, pair.Value, bSUI);
					MapHelper.SetFX(aider,fxBuffDef,4.0f);
					bCal.UpdateBuffValue();
					break;
				case PowerType.SkillRate:
					aider.GetComponent<BuffList>().ExtraDict[BuffType.SkillRate] += pair.Value;
					BuffVisualUI(BuffType.SkillRate, pair.Value, bSUI);
					MapHelper.SetFX(aider,fxBuffSkill,4.0f);
					bCal.UpdateBuffValue();
					break;
				case PowerType.MoveRange:
					aider.GetComponent<BuffList>().ExtraDict[BuffType.MoveRange] += pair.Value;
					BuffVisualUI(BuffType.MoveRange, pair.Value, bSUI);
					MapHelper.SetFX(aider,fxBuffMove,4.0f);
					bCal.UpdateBuffValue();
					break;
				case PowerType.AttackRange:
					aider.GetComponent<BuffList>().ExtraDict[BuffType.AttackRange] += pair.Value;
					BuffVisualUI(BuffType.AttackRange, pair.Value, bSUI);
					MapHelper.SetFX(aider,fxBuffRange,4.0f);
					bCal.UpdateBuffValue();
					break;
			}
		}
	}
}
