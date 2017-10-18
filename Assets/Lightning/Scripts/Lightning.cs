using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public enum TypeMode {
	_2D,
	_3D
}

public class ListLineSubLightning{
	public Vector3 startP;
	public Vector3 endP;
	public float a;
	public float widthLightning;
	public float widthLightning_glow;
}

public class Lightning : MonoBehaviour {
	public GameObject MolniyaLych;
	public TypeMode TypeModeLightning = TypeMode._2D;
	public string SortingLayer;
	public int OrderLayer;
	[Range(0.05f, 5.0f)]
	public float MaxTimeLifeLightning = 1.0f;
	[Range(0.05f, 5.0f)]
	public float DeltaTimeNextSubLightning = 0.11f;
	[Range(0.01f, 5.0f)]
	public float MaxTimeLifeSubLightning = 0.6f;
	public bool HasLoop = false;
	[Space(20)]
	public Color ColorLightning = Color.blue;
	[Range(1, 6)]
	public int QuantityIterations = 5;
	[Range(0.0f, 1.0f)]
	public float OffsetLine = 0.8f;
	[Range(0.0f, 10.0f)]
	public float OffsetPlusDistanseLine = 0.0f;
	[Range(0.0f, 180.0f)]
	public float AngleAdditionalLightning = 10.0f;
	[Range(0.0f, 1.0f)]
	public float LengthScaleAdditionalLightning = 0.7f;
	[Range(0.0f, 1.0f)]
	public float ProbabilityAdditionalLightning = 0.25f;
	[Range(0.01f, 1.0f)]
	public float WidthLightning = 0.148f;
	[Range(0.01f, 1.0f)]
	public float WidthLightningGlow = 0.148f;
	
	bool HasStart = false;
	bool HasEnd = false;
	float time = 0;
	float deltaTime = 0;

	Vector3 StartPos = Vector3.zero;
	Vector3 EndPos = Vector3.zero;

	List<List<ListLineSubLightning>> molniya2 = new List<List<ListLineSubLightning>>();


	public void Create (Vector3 _StartPos, Vector3 _EndPos) {
		StartPos = _StartPos;
		EndPos = _EndPos;
		
		HasStart = true;
		HasEnd = false;
		time = 0;
		deltaTime = 0;
		
		CreateM(StartPos, EndPos);
	}

	public void SetStartPos(Vector3 _StartPos) {
		StartPos = _StartPos;
	}
	
	public void SetEndPos(Vector3 _EndPos) {
		EndPos = _EndPos;
	}

	void FixedUpdate () {
		if(HasStart){
			time += Time.deltaTime;
			deltaTime += Time.deltaTime;

			if(time >= MaxTimeLifeLightning) {
				if(!HasLoop)
					DestroyImmediate(gameObject);
				else {
					HasStart = true;
					HasEnd = false;
					time = 0;
				}
			}
			if(!HasEnd && deltaTime >= DeltaTimeNextSubLightning){
				CreateM(StartPos, EndPos);
			}
		}
	}


	void CreateM(Vector3 StartPos, Vector3 EndPos) {
		float a = 1.0f;
		float _OffsetLine = OffsetLine * (Vector3.Distance(StartPos, EndPos) / 2 + OffsetPlusDistanseLine);
		float widthLightning = WidthLightning;
		float widthLightning_glow = WidthLightningGlow;

		deltaTime = 0;

		molniya2 = new List<List<ListLineSubLightning>>();

		List<ListLineSubLightning> molniya0 = new List<ListLineSubLightning>();
		molniya0.Add(new ListLineSubLightning() {
			startP = StartPos,
			endP = EndPos,
			a = a,
			widthLightning = widthLightning,
			widthLightning_glow = widthLightning_glow
		});
		molniya2.Add(molniya0);

		for(int i = 0; i < QuantityIterations; i++) {
			int k_p = molniya2.Count;
			for(int p = 0; p < k_p; p++)  {
				int k = molniya2[p].Count;
				for(int j = 0; j < k; j++)  { 
					Vector3 sp = molniya2[p][j].startP;
					Vector3 ep = molniya2[p][j].endP;
					a = molniya2[p][j].a;
					widthLightning = molniya2[p][j].widthLightning;
					widthLightning_glow = molniya2[p][j].widthLightning_glow;

					Vector3 midP = sp + (ep - sp)/2;
					Vector3 norm = (ep - sp).normalized;


					if(TypeModeLightning == TypeMode._2D)
						midP += new Vector3(norm.y, -norm.x, 0) * Random.Range(-_OffsetLine, _OffsetLine);
					else 
						midP += (Quaternion.Euler(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360)) * (Quaternion.Euler(0, 0, 90) * norm)) * Random.Range(-_OffsetLine, _OffsetLine);


					molniya2[p][j].endP = midP;
					molniya2[p].Insert(j+1, new ListLineSubLightning() { startP = midP, endP = ep, a = a, widthLightning = widthLightning, widthLightning_glow = widthLightning_glow });
					j++;
					k++;

					if(Random.Range(0.0f, 1.0f) <= ProbabilityAdditionalLightning) {
						Vector3 direction = midP - sp;
						float randomAngleLine = Random.Range(-AngleAdditionalLightning, AngleAdditionalLightning);
						if(TypeModeLightning == TypeMode._2D)
							direction = Quaternion.Euler(0, 0, randomAngleLine) * direction;
						else 
						direction = Quaternion.Euler(randomAngleLine, randomAngleLine, randomAngleLine) * direction;
						Vector3 splitEnd = direction*LengthScaleAdditionalLightning + midP; 
						molniya0 = new List<ListLineSubLightning>();
						molniya0.Add(new ListLineSubLightning() { startP = midP, endP = splitEnd, a = a/1.5f, widthLightning = widthLightning/2.5f, widthLightning_glow = widthLightning_glow/1.5f });
						molniya2.Add(molniya0);
					}
				}
			}
			_OffsetLine /= 2;
		}

		foreach(List<ListLineSubLightning> lms in molniya2) {
			List<Vector3> l_v2 = new List<Vector3>();

			float _a = lms[0].a;
			float _widthLightning = lms[0].widthLightning;
			float _widthLightning_glow = lms[0].widthLightning_glow;

			l_v2.Add(lms[0].startP);
			foreach(ListLineSubLightning lm in lms) {
				l_v2.Add(lm.endP);
			}

			GameObject go = Instantiate(MolniyaLych);
			go.transform.SetParent(gameObject.transform, false);
			LineSubLightning mol = go.GetComponent<LineSubLightning>();
			mol.CreateSubLightning(l_v2, ColorLightning, _a, _widthLightning, _widthLightning_glow, MaxTimeLifeSubLightning, SortingLayer, OrderLayer);
		}
	}

}
