using UnityEngine;
using UnityEngine.EventSystems;
//INDEPENDENT SCRIPT
public class UserInputManager : MonoBehaviour
{
    private float lastClickTime = 0f;
    private const float doubleClickTimeThreshold = 0.2f;
    
    public bool ClickObj(GameObject Obj)
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == Obj)
                {
                    if (Time.time - lastClickTime < doubleClickTimeThreshold)
                    {
                        lastClickTime = 0f;
                        return false;
                    }
                    else
                    {
                        lastClickTime = Time.time;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool ClickObjWTag(string tag)
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag(tag))
                {
                    return true;
                }
                return false;
            }
        }
        return false;
    }
    public bool ClickObjWOTag(string tag)
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.gameObject.CompareTag(tag))
                {
                    return true;
                }
                return false;
            }
        }
        return false;
    }
    public bool DoubleClickObj(GameObject Obj)
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == Obj)
                {
                    if (Time.time - lastClickTime < doubleClickTimeThreshold)
                    {
                        lastClickTime = 0f;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool ClickNone()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return true;
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Floor"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public GameObject OtherClickedObj()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    public GameObject ClickUI()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                if (results.Count > 0)
                {
                    return results[0].gameObject;
                }
            }
        }
        return null;
    }
}
