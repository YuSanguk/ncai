using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

// 원소 획득 오브젝트용 코드
public class GetElement : MonoBehaviour 
{
    private GetElementalObject currentInteractableItem;

    void Update()
    {
        // R키를 눌렀을 때
        if (Keyboard.current[Key.R].wasPressedThisFrame)
        {
            // 현재 상호작용 가능한 아이템이 있다면
            if (currentInteractableItem != null)
            {
                Debug.Log("R키 입력: 아이템 획득 시도");
                currentInteractableItem.PickUpItem();
            }
            else
            {
                Debug.Log("R키 입력: 주변에 획득할 수 있는 아이템이 없습니다.");
            }
        }
    }

    // ElementItem 스크립트가 호출하여 상호작용 가능 상태로 설정
    public void SetCurrentInteractable(GetElementalObject item)
    {
        currentInteractableItem = item;
    }

    // ElementItem 스크립트가 호출하여 상호작용 가능 상태 해제
    public void ClearCurrentInteractable()
    {
        currentInteractableItem = null;
    }
}