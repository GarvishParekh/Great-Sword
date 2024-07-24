using UnityEngine;
using UnityEngine.EventSystems;

public class DragInput : EventTrigger
{
    private DragSupport ds;

    private void Awake()
    {
        ds = GetComponent<DragSupport>();  
    }

    public override void OnDrag(PointerEventData eventData)
    {
        ds.inputData.mouseDeltaX += eventData.delta.x * Time.deltaTime * 15;
        ds.inputData.mouseDeltaY -= eventData.delta.y * Time.deltaTime * 15;
        ds.inputData.mouseDeltaY = Mathf.Clamp(ds.inputData.mouseDeltaY, -20, 50);
    }
}
