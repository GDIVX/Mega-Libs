using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabs;
    public Sprite idleSprite, hoverSprite, activeSprite;
    public List<GameObject> itemsToSwitch;
    [HideInInspector] public TabButton selectedTab;

    public void Subscribe(TabButton tab)
    {
        if (tabs == null)
        {
            tabs = new List<TabButton>();
        }

        tabs.Add(tab);
    }

    public void OnTabEnter(TabButton tab)
    {
        ResetTabs();
        if (selectedTab == null || tab != selectedTab)
            tab.background.sprite = hoverSprite;
    }

    public void OnTabExit(TabButton tab)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton tab)
    {
        if(selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = tab;

        selectedTab.Select();

        ResetTabs();
        tab.background.sprite = activeSprite;
        int index = tab.transform.GetSiblingIndex();

        for (int i = 0; i < itemsToSwitch.Count; i++)
        {
            if (i == index)
            {
                itemsToSwitch[i].SetActive(true);
            }
            else
            {
                itemsToSwitch[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton tab in tabs)
        {
            if (selectedTab != null && tab == selectedTab) { continue; }
            tab.background.sprite = idleSprite;
        }
    }


}
