using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class tabs : MonoBehaviour
{
    UIDocument tabsUI;
    Button storage;
    Button buy;
    Button sell;
    Button repair;
    public GameObject storageShow;
    public GameObject sellShow;
    public GameObject buyShow;
    public GameObject repairShow;
    private void OnEnable()
    {
        tabsUI = GetComponent<UIDocument>();
        storage = tabsUI.rootVisualElement.Q("tabs").Q<Button>("storage");
        buy = tabsUI.rootVisualElement.Q("tabs").Q<Button>("buy");
        sell = tabsUI.rootVisualElement.Q("tabs").Q<Button>("sell");
        repair = tabsUI.rootVisualElement.Q("tabs").Q<Button>("repair");
        storage.RegisterCallback<ClickEvent>(storageTab);
        sell.RegisterCallback<ClickEvent>(sellTab);
        buy.RegisterCallback<ClickEvent>(buyTab);
        repair.RegisterCallback<ClickEvent>(repairTab);
        storageShow.SetActive(false);
        buyShow.SetActive(false);
        sellShow.SetActive(false);
        repairShow.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void storageTab(ClickEvent c)
    {
        storageShow.SetActive(true);
        buyShow.SetActive(false);
        sellShow.SetActive(false);
        repairShow.SetActive(false);
    }
    void sellTab(ClickEvent c)
    {
        storageShow.SetActive(false);
        buyShow.SetActive(false);
        sellShow.SetActive(true);
        repairShow.SetActive(false);
    }
    void buyTab(ClickEvent c)
    {
        storageShow.SetActive(false);
        buyShow.SetActive(true);
        sellShow.SetActive(false);
        repairShow.SetActive(false);
    }
    void repairTab(ClickEvent c)
    {
        storageShow.SetActive(false);
        buyShow.SetActive(false);
        sellShow.SetActive(false);
        repairShow.SetActive(true);
    }
}
