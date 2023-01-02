using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class EngineHelper
{
    // Main Camera Cached Ref
    // Camera.main sadly is a poor ref for the engine to find a camera tagged with "Main Camera" as it's tag in the editor...
    private static Camera _camera;
    public static Camera Camera 
    {
        get 
        {
            if(_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    // Cleaner way of co-routining wait for seconds, especially for scene transitions
    // as yeild return new WaitForSeconds causes GC allocations, especially when for looped or while looped over which does a bit of stack busting
    private static readonly Dictionary<float, WaitForSeconds> WaitCache = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWaitFromCache(float time)
    {
        if(WaitCache.TryGetValue(time, out var wait)) 
            return wait;
        
        WaitCache[time] = new WaitForSeconds(time);
        return WaitCache[time];
    }

    // Ability to poll Mouse-Over-UI events
    private static PointerEventData _eventDataCurrentPos;
    private static List<RaycastResult> _results;
    public static bool isMousePointerOverUI()
    {
        _eventDataCurrentPos = new PointerEventData(EventSystem.current) 
        { 
            position = Input.mousePosition 
        };

        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPos, _results);

        return _results.Count > 0;
    }

    // Transform Extension to delete sub-object-tree without destroying the parent gameObject / node
    public static void DeleteChildren(this Transform t)
    {
        foreach(Transform child in t)
            Object.Destroy(child.gameObject);
    }
}
