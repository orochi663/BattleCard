using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MapUtility{
	public class MapHelper
	{
		public MapHelper(){
		}
		
		public static void SetFX(Transform chess, Transform fx, float duration){
			Transform fxObj = Object.Instantiate(fx,chess.transform.position, Quaternion.identity) as Transform;
			Object.Destroy(GameObject.Find(fxObj.name).gameObject, duration);
		}
		
		public static bool CheckPassive(PassiveType pt, Transform chess){
			CharacterPassive chessPassive = chess.GetComponent<CharacterPassive>();
			return chessPassive.PassiveDict[pt];
		}
		
		public static void SetObjTransparent(Transform obj, Color col, float alpha){	
			Shader opaShader = Shader.Find("Transparent/Diffuse");
			obj.renderer.material.shader = opaShader;
			col.a = alpha;
			obj.renderer.material.color = col;
			
			Transform model = obj.FindChild("Models");
			List<Transform> models = new List<Transform>();
			if(model.childCount>0){
				for(int i=0; i<model.childCount; i++){
					if(model.GetChild(i).GetComponent<SkinnedMeshRenderer>()!=null){
						models.Add(model.GetChild(i));
					}
				}
			}
			if(models.Count>0){
				foreach(Transform m in models){
					m.renderer.material.shader = opaShader;
					m.renderer.material.color = col;
				}
			}
		}
		
		public static bool SetObjOldShader(Transform obj, float alpha){
			Shader diffShader = Shader.Find("Diffuse");
			obj.renderer.material.shader = diffShader;
			Color currentCol = obj.renderer.material.color; 
			currentCol.a = alpha;
			obj.renderer.material.color = currentCol;
			List<Transform> models = new List<Transform>();
			Transform model = obj.FindChild("Models");
			if(model.childCount>0){
				for(int i=0; i<model.childCount; i++){
					if(model.GetChild(i).GetComponent<SkinnedMeshRenderer>()!=null){
						models.Add(model.GetChild(i));
					}
				}
			}
			if(models.Count>0){
				foreach(Transform m in models){
					m.renderer.material.shader = diffShader;
					m.renderer.material.color = currentCol;
				}
			}
			return true;
		}
		
		public static bool Success(int rate){
			bool ifSucceed = false;
			int realNum = Random.Range(0,100);
			if(realNum < rate){
				ifSucceed = true;
			}else{
				ifSucceed = false;
			}
			return ifSucceed;
		}
		
		public static bool IsMapOccupied(Transform map){
			bool occupied = true;
			if(map!=null){
				Vector3 rayDir = -map.up;
				Vector3 startPos = map.position;
				startPos.y = map.position.y+15.0f;
				Ray rayUp = new Ray(startPos, rayDir);
				RaycastHit hit;
				if(Physics.Raycast(rayUp,out hit,13.0f)){
					occupied = true;
				}else{
					occupied = false;
				}
			}else{
				occupied = true;
			}
			return occupied;
		}
		
		public static Transform GetMapOccupiedObj(Transform map){
			Transform obj = null;
			if(map!=null){
				Vector3 rayDir = -map.up;
				Vector3 startPos = map.position;
				startPos.y = map.position.y+15.0f;
				Ray rayUp = new Ray(startPos, rayDir);
				RaycastHit hit;
				if(Physics.Raycast(rayUp,out hit,20.0f)){
					obj = hit.transform;
				}
			}
			return obj;
		}
		
		
		
	}
}

