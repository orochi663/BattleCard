using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MapUtility;

public class CtrlStealing : MonoBehaviour, CommonSkill{
	
	int Player;
	IList aTerritory, bTerritory;
	int skillRate;
	RoundCounter chessStorage;
	Transform selectedMap, target;
	SkillSlidingUI sUI;
	
	// Use this for initialization
	void Start () {
		chessStorage = Camera.main.GetComponent<RoundCounter>();
	}
	
	public void InsertSelection(Transform map){
		selectedMap = map;
		target = MapHelper.GetMapOccupiedObj(map);
	}
	
	public IList GetSelectionRange(){
		IList selectionRange = new List<Transform>();
		Player = transform.parent.parent.GetComponent<CharacterProperty>().Player;
		if(Player == 1){
			foreach(Transform chess in chessStorage.PlayerBChesses){
				CharacterProperty property = chess.GetComponent<CharacterProperty>();
				if(!property.death && !property.Summoner){
					selectionRange.Add(chess.GetComponent<CharacterSelect>().getMapPosition());
				}
			}
		}else if(Player == 2){
			foreach(Transform chess in chessStorage.PlayerAChesses){
				CharacterProperty property = chess.GetComponent<CharacterProperty>();
				if(!property.death && !property.Summoner){
					selectionRange.Add(chess.GetComponent<CharacterSelect>().getMapPosition());
				}
			}
		}
		
		return selectionRange;
	}
	
	public void Execute(){
		sUI = Camera.mainCamera.GetComponent<SkillSlidingUI>();
		Player = transform.parent.parent.GetComponent<CharacterProperty>().Player;
		if(transform.GetComponent<SkillProperty>().PassSkillRate){
			CharacterProperty targetProperty = target.GetComponent<CharacterProperty>();
			targetProperty.Moved = false;
			targetProperty.Activated = false;
			targetProperty.Attacked = false;
			targetProperty.TurnFinished = false;
			SkillUI sui = new SkillUI(target, true, "Controlled");
			sUI.UIItems.Add(sui);
			sUI.FadeInUI = true;
			if(Player == 1){
				chessStorage.PlayerAChesses.Add(target);
				chessStorage.PlayerBChesses.Remove(target);
				targetProperty.Player = 1;
			}else if(Player == 2){
				chessStorage.PlayerBChesses.Add(target);
				chessStorage.PlayerAChesses.Remove(target);
				targetProperty.Player = 2;
			}
		}else{
			SkillUI sui = new SkillUI(transform.parent.parent, false, "");
			sUI.UIItems.Add(sui);
			sUI.FadeInUI = true;
			print("Stealing failed");
		}
	}
}
