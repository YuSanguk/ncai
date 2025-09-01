using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider))]
    public class GetElementalObject : MonoBehaviour
    {
        public string hadElementName;
        public int elementCount = 1; // 지급되는 원소의 개수

        private GetElement player;
        private ElementManager elementManager;

        // 플레이어가 아이템 획득 가능한 범위 내에 있는지 확인
        private bool playerInRange = false;

        private GetElement playerController;

        void Start()
        {
            // Collider를 Trigger로 설정
            GetComponent<Collider>().isTrigger = true;

            // 플레이어 컨트롤러를 찾아 참조
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerController = playerObject.GetComponent<GetElement>();
            }
        }

        // 플레이어가 트리거 범위에 들어오면 호출
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // Player 태그 넣어주기. 이동 객체에.
            {
                playerInRange = true;
                Debug.Log($"플레이어가 '{hadElementName}' 원소 아이템 옆에 있습니다. R키로 획득 가능.");
                // 플레이어에게 현재 상호작용 가능한 아이템을 알림
                if (playerController != null)
                {
                    playerController.SetCurrentInteractable(this);
                }
            }
        }

        // 플레이어가 트리거 범위에서 나가면 호출
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
                Debug.Log($"플레이어가 '{hadElementName}' 원소 아이템 범위에서 벗어났습니다.");
                // 플레이어의 상호작용 상태 초기화
                if (playerController != null)
                {
                    playerController.ClearCurrentInteractable();
                }
            }
        }

        // 플레이어가 상호작용 키(R)를 눌렀을 때 호출될 메서드
        public void PickUpItem()
        {
            if (playerInRange)
            {
                // ElementManager를 찾아 원소 개수 증가
                GameObject gameManager = GameObject.Find("ElementalManager");
                if (gameManager != null)
                {
                    ElementManager elementManager = gameManager.GetComponent<ElementManager>();
                    if (elementManager != null)
                    {
                        elementManager.IncreaseElementCount(hadElementName, elementCount);
                        Debug.Log($"{hadElementName} 원소 {elementCount}개를 획득했습니다!");

                        // 아이템 획득 후 오브젝트 파괴
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}