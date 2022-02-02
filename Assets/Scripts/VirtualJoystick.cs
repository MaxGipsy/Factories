using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
{

	public static Vector3 direction { get; private set; }

	void Awake()
	{
		direction = Vector3.zero;
	}

	
	public void OnDrag(PointerEventData eventData)
	{
		Vector2 curDir = new Vector2(Input.touches[0].position.x - transform.position.x, Input.touches[0].position.y - transform.position.y).normalized;
		// Строка ниже для управления с помощью мыши
		//Vector2 curDir = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y).normalized;

		direction = new Vector3(curDir.x, 0, curDir.y);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		direction = Vector3.zero;
	}

}