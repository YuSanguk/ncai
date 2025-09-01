using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq; // Dictionary를 사용하기 위해 필요
using UnityEngine.UI; // UI를 사용한다면 추가

public class ElementManager : MonoBehaviour
{
    public InputActionReference interaction;
    
    // 다양한 원소 데이터를 관리할 딕셔너리
    // Key는 원소의 이름(string), Value는 ElementData 객체
    private Dictionary<string, ElementDefine> elements = new Dictionary<string, ElementDefine>();

    // UI에 잔여 원소 개수를 표시할 Text 컴포넌트 (선택 사항)
    public Text elementCountText;
    private List<string> orderedElementNames = new List<string>(); // 순서 관리를 위한 리스트
    private int selectedElementIndex = 0;
    public string selectedElementName = "";

    void Start()
    {
        // 게임 시작 시 초기 원소 데이터 설정
        AddElement("Earth", 0);
        AddElement("Water", 0);
        AddElement("Fire", 0);

        UpdateUI();
    }
    
    void OnEnable()
    {
        interaction.action.Enable();
    }

    void OnDisable()
    {
        interaction.action.Disable();
    }

    void Update()
    {
        
        ElementSpin();
    }

    // 새로운 원소를 추가하는 메서드
    public void AddElement(string name, int initialCount)
    {
        if (elements.ContainsKey(name))
        {
            Debug.LogWarning("Element with name " + name + " already exists.");
            return;
        }

        elements.Add(name, new ElementDefine(name, initialCount));
        orderedElementNames.Add(name); // 추가된 원소 이름을 리스트에 저장
    }

    public List<string> GetOrderedElementNames()
    {
        return orderedElementNames;
    }

    // 특정 원소의 개수를 증가시키는 메서드
    public void IncreaseElementCount(string name, int amount)
    {
        if (elements.ContainsKey(name))
        {
            elements[name].count += amount;
            UpdateUI();
        }
        else
        {
            Debug.LogError("Element " + name + " not found!");
        }
    }

    // 특정 원소의 개수를 감소시키는 메서드
    public void DecreaseElementCount(string name, int amount)
    {
        if (elements.ContainsKey(name))
        {
            elements[name].count -= amount;
            // 개수가 0 이하가 되지 않도록 처리
            if (elements[name].count < 0)
            {
                elements[name].count = 0;
            }

            UpdateUI();
        }
        else
        {
            Debug.Log("Element " + name + " not found!");
        }
    }

    // 특정 원소의 데이터를 가져오는 메서드
    public ElementDefine GetElementData(string name)
    {
        if (elements.ContainsKey(name))
        {
            return elements[name];
        }

        Debug.LogError("Element " + name + " not found!");
        return null;
    }

    // 현재 모든 원소의 상태를 UI에 표시 (UI가 없는 경우 디버그 로그로 대체)
    private void UpdateUI()
    {
        // 원소 관리

        string text1 = elements.Keys.ElementAt(selectedElementIndex);
        string text2 = elements.Values.ElementAt(selectedElementIndex).count.ToString();
        string uiText = text1 + ":" + text2 + "\n";

        if (elementCountText != null)
        {
            elementCountText.text = uiText;
        }
        else
        {
            Debug.Log(uiText);
        }
    }

    private void ElementSpin()
    {
        // 원소 스핀 with E키.
        if(interaction.action.WasPressedThisFrame())
        {
            selectedElementIndex += 1;
            if (selectedElementIndex >= orderedElementNames.Count)
            {
                selectedElementIndex = 0;
            }
            // Debug.Log(selectedElementIndex);
            
            selectedElementName = orderedElementNames[selectedElementIndex];
            UpdateUI();
        }
    }
}