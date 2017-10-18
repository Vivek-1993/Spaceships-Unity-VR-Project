using UnityEngine;
using System.Collections;

public class Example_move : MonoBehaviour {
	bool isSelect = false;
	SpriteRenderer sr;


	void Start() {
		sr = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Vector2 v2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			float minX = transform.position.x - sr.bounds.size.x/2;
			float maxX = transform.position.x + sr.bounds.size.x/2;
			float minY = transform.position.y - sr.bounds.size.y/2;
			float maxY = transform.position.y + sr.bounds.size.y/2;

			if(v2.x >= minX && v2.x <= maxX && v2.y >= minY && v2.y <= maxY)
				isSelect = true;
		}
		if(Input.GetMouseButtonUp(0)) {
			isSelect = false;
		}

		if(isSelect) {
			transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}
