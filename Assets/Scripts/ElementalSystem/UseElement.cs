using UnityEngine;
using UnityEngine.InputSystem;

public class UseElement : MonoBehaviour
{
    private ElementManager elementManager;
    private UseElementalObject currentInteractiveObject;
    private string selectedElementName; // 현재 상호작용 오브젝트가 요구하는 원소

    void Start()
    {
        // GameManager를 찾아 ElementManager 컴포넌트 가져오기
        GameObject gameManagerObject = GameObject.Find("ElementalManager");
        if (gameManagerObject != null)
        {
            elementManager = gameManagerObject.GetComponent<ElementManager>();
        }
    }

    void Update()
    {
        // 'R' 키 입력 감지
        if (Keyboard.current[Key.R].wasPressedThisFrame)
        {
            if (currentInteractiveObject != null && selectedElementName != null)
            {
                Debug.Log($"J 키 입력: {selectedElementName} 원소를 사용 시도합니다.");
                // 상호작용 오브젝트의 상호작용 메서드 호출
                currentInteractiveObject.Interact();
            }
            else
            {
                Debug.Log("J 키 입력: 상호작용할 오브젝트가 없거나 원소가 선택되지 않았습니다.");
            }
        }
    }

    // InteractiveObject 스크립트가 호출하는 메서드
    public void SetCurrentInteractiveObject(UseElementalObject obj, string elementName)
    {
        currentInteractiveObject = obj;
        selectedElementName = elementName;
    }

    // InteractiveObject 스크립트가 호출하는 메서드
    public void ClearCurrentInteractiveObject()
    {
        currentInteractiveObject = null;
        selectedElementName = null;
    }
}