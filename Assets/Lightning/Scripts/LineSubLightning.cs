using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LineSubLightning : MonoBehaviour {
	public Transform StartPointSp;
	public Transform CenterPointSp;
	public Transform EndPointSp;
	public Transform StartPointSp_glow;
	public Transform CenterPointSp_glow;
	public Transform EndPointSp_glow;

	LineRenderer startRend;
	LineRenderer centerRend;
	LineRenderer endRend;
	LineRenderer startRend_glow;
	LineRenderer centerRend_glow;
	LineRenderer endRend_glow;
	Color color_glow = new Color(1, 1, 1, 1);
	Color color_center = new Color(1, 1, 1, 1);

	bool isStart = false;
	bool IsEnd = false;
	float MaxTimeLife = 0.6f;
	float t = 0;
	float start_a = 0;

	public void CreateSubLightning(List<Vector3> l_v2, Color c, float a, float sizeLine, float sizeLine_glow, float maxTimeLife, string nameLayer, int sortLayer){
		startRend = StartPointSp.GetComponent<LineRenderer>();
		centerRend = CenterPointSp.GetComponent<LineRenderer>();
		endRend = EndPointSp.GetComponent<LineRenderer>();

		startRend_glow = StartPointSp_glow.GetComponent<LineRenderer>();
		centerRend_glow = CenterPointSp_glow.GetComponent<LineRenderer>();
		endRend_glow = EndPointSp_glow.GetComponent<LineRenderer>();

		isStart = true;
		MaxTimeLife = maxTimeLife;

		centerRend.SetVertexCount(l_v2.Count);
		centerRend_glow.SetVertexCount(l_v2.Count);
		for(int i=0; i< l_v2.Count; i++){
			centerRend.SetPosition(i, l_v2[i]);
			centerRend_glow.SetPosition(i, l_v2[i]);
		}
		
		if(l_v2.Count >= 1){
			StartPointSp.position = l_v2[0] + transform.position;
			StartPointSp_glow.position = l_v2[0] + transform.position;
			EndPointSp.position = l_v2[l_v2.Count - 1] + transform.position;
			EndPointSp_glow.position = l_v2[l_v2.Count - 1] + transform.position;
			
			startRend.SetPosition(0, new Vector3(-sizeLine/2, 0, 0));
			startRend_glow.SetPosition(0, new Vector3(-sizeLine_glow/2, 0, 0));
			endRend.SetPosition(1, new Vector3(sizeLine/2, 0, 0));
			endRend_glow.SetPosition(1, new Vector3(sizeLine_glow/2, 0, 0));
		}

		if(l_v2.Count > 1){
			Vector3 v2_norm = (l_v2[1] - l_v2[0]).normalized;
			float startAngle = Vector3.Angle(Vector3.right, v2_norm) * (v2_norm.y < 0 ? -1 : 1);
			StartPointSp.Rotate(Vector3.forward, startAngle);
			StartPointSp_glow.Rotate(Vector3.forward, startAngle);

			v2_norm = (l_v2[l_v2.Count - 1] - l_v2[l_v2.Count - 2]).normalized;
			float endAngle = Vector3.Angle(Vector3.right, v2_norm) * (v2_norm.y < 0 ? -1 : 1);
			EndPointSp.Rotate(Vector3.forward, endAngle);
			EndPointSp_glow.Rotate(Vector3.forward, endAngle);
		}

		startRend.sortingLayerName = nameLayer;
		startRend.sortingOrder = sortLayer;
		startRend_glow.sortingLayerName = nameLayer;
		startRend_glow.sortingOrder = sortLayer-1;

		centerRend.sortingLayerName = nameLayer;
		centerRend.sortingOrder = sortLayer;
		centerRend_glow.sortingLayerName = nameLayer;
		centerRend_glow.sortingOrder = sortLayer-1;
		
		endRend.sortingLayerName = nameLayer;
		endRend.sortingOrder = sortLayer;
		endRend_glow.sortingLayerName = nameLayer;
		endRend_glow.sortingOrder = sortLayer-1;
		
		startRend.SetWidth(sizeLine, sizeLine);
		centerRend.SetWidth(sizeLine, sizeLine);
		endRend.SetWidth(sizeLine, sizeLine);
		startRend_glow.SetWidth(sizeLine_glow, sizeLine_glow);
		centerRend_glow.SetWidth(sizeLine_glow, sizeLine_glow);
		endRend_glow.SetWidth(sizeLine_glow, sizeLine_glow);

		MaxTimeLife = MaxTimeLife * a;
		start_a = a;
		color_glow = c;

		SetColor();
	}

	void FixedUpdate () {
		if(IsEnd){
			LineRenderer[] lr = gameObject.GetComponentsInChildren<LineRenderer>();

			for(int i = lr.Length-1; i >= 0; i--)
				DestroyImmediate(lr[i].material);

			DestroyImmediate(gameObject);
		}
		else if(isStart) {
			t += Time.deltaTime;

			if(t >= MaxTimeLife) {
				t = MaxTimeLife;
				IsEnd = true;
			}

			SetColor();
		}
	}

	void SetColor(){
		color_glow = new Color(color_glow.r, color_glow.g, color_glow.b, start_a * (MaxTimeLife - t)/MaxTimeLife);
		color_center = new Color(color_center.r, color_center.g, color_center.b, start_a * (MaxTimeLife - t)/MaxTimeLife * 2);
		
		startRend_glow.material.SetColor("_Color", color_glow);
		centerRend_glow.material.SetColor("_Color", color_glow);
		endRend_glow.material.SetColor("_Color", color_glow);
		
		startRend.material.SetColor("_Color", color_center);
		centerRend.material.SetColor("_Color", color_center);
		endRend.material.SetColor("_Color", color_center);
	}
}
