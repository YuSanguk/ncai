using UnityEngine;

// 이 스크립트를 가진 오브젝트는 항상 Collider 컴포넌트가 필요
[RequireComponent(typeof(Collider))]
public class UseElementalObject : MonoBehaviour
{
    // 이 오브젝트가 상호작용하기 위해 필요한 원소의 이름
    public string requiredElementName;

    private UseElement player;
    private ElementManager elementManager;

    void Start()
    {
        // Collider를 Trigger로 설정
        GetComponent<Collider>().isTrigger = true;

        // 원소 매니저와 플레이어 컨트롤러를 찾아 참조
        GameObject gameManagerObject = GameObject.Find("ElementalManager");
        if (gameManagerObject != null)
        {
            elementManager = gameManagerObject.GetComponent<ElementManager>();
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<UseElement>();
        }
    }

    // 플레이어가 트리거 범위에 들어오면 호출
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player != null)
        {
            Debug.Log($"플레이어가 {requiredElementName} 원소가 필요한 오브젝트에 접근했습니다.");
            // 플레이어에게 현재 상호작용 가능한 오브젝트를 알림
            player.SetCurrentInteractiveObject(this, requiredElementName);
        }
    }

    // 플레이어가 트리거 범위에서 나가면 호출
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && player != null)
        {
            Debug.Log("플레이어가 오브젝트 범위에서 벗어났습니다.");
            // 플레이어의 상호작용 상태 초기화
            player.ClearCurrentInteractiveObject();
        }
    }

    // 플레이어로부터 상호작용 요청을 받으면 실행되는 로직
    public void Interact()
    {
        if (elementManager != null)
        {
            // 필요한 원소의 개수가 1개 이상인지 확인
            ElementDefine requiredElement = elementManager.GetElementData(requiredElementName);
            Debug.Log(requiredElement);
            if (requiredElement != null && requiredElement.count > 0)
            {
                // 원소 1개 사용
                elementManager.DecreaseElementCount(requiredElementName, 1);
                Debug.Log($"상호작용 성공! {requiredElementName} 원소 1개를 사용했습니다. 남은 개수: {requiredElement.count}");
                // TODO: 여기에 상호작용 성공 시 필요한 게임 로직 추가 (문이 열리거나, 효과가 발생하는 등)
            }
            else
            {
                Debug.LogWarning($"상호작용 실패: {requiredElementName} 원소가 부족합니다!");
            }
        }
    }
}