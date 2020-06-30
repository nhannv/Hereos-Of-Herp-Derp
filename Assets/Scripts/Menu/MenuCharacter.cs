﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Spriter2UnityDX;
using UnityEngine.UI;

public class MenuCharacter : MonoBehaviour
{
    // Variables + Properties
    #region
    [Header("Object References")]
    public GameObject infoPanelParent;
    public UniversalCharacterModel myModel;
    public EntityRenderer myER;

    [Header("Text References")]
    public TextMeshProUGUI presetNameText;
    public TextMeshProUGUI presetDescriptionText;
    public TextMeshProUGUI attributeOneText;
    public TextMeshProUGUI attributeTwoText;
    public TextMeshProUGUI attributeThreeText;
    public TextMeshProUGUI backgroundTextOne;
    public TextMeshProUGUI backgroundTextTwo;
    public TextMeshProUGUI currentRaceText;
    public TextMeshProUGUI raceDescriptionText;

    [Header("GUI References")]
    public Button previousPresetButton;
    public Button nextPresetButton;

    [Header("Properties")]
    public CharacterPresetData myPreset;
    public string myPresetName;
    public CharacterPresetData currentCharacterPreset;
    public MenuAbilityTab tabOne;
    public MenuAbilityTab tabTwo;
    public MenuAbilityTab tabThree;
    public MenuAbilityTab tabFour;

    public MenuAbilityTab racialAbilityTab;
    public MenuAbilityTab racialPassiveTab;

    #endregion

    // Initialization + Set up
    #region
    private void Start()
    {
        BuildMyViewsFromCharacterPresetData(CharacterPresetLibrary.Instance.GetOriginCharacterPresetByName("Random"));
        myModel.SetIdleAnim();
    }
    public void BuildMyViewsFromCharacterPresetData(CharacterPresetData data)
    {
        // Cache current preset
        currentCharacterPreset = data;

        // Set name text
        SetPresetName(data.characterName);

        // Set background texts
        MainMenuManager.Instance.BuildBackgroundTextsFromCharacterPresetData(this, data);

        // Set data
        SetMyCharacterData(data);

        // Build model
        CharacterModelController.BuildModelFromCharacterPresetData(myModel, data);

        // Set Scaling on model
        //CharacterModelController.AutoSetModelScaleFromRace(myModel);

        // Modify abilities/passives on panel
        MainMenuManager.Instance.BuildCharacterAbilityTabsFromPresetData(this, data);

        // modify attribute texts
        MainMenuManager.Instance.BuildTalentTextsFromCharacterPresetData(this, data);

        // Set Description text
        MainMenuManager.Instance.BuildDescriptionTextCharacterPresetData(this, data);

        // Build Race Tab
        MainMenuManager.Instance.BuildRaceTabFromCharacterPresetData(this, data.modelRace);

        // Build Racial Ability + Passive tabs
        MainMenuManager.Instance.BuildRacialAbilityAndPassiveTabsFromPresetData(this, data);
    }
   
    #endregion

    // Mouse + Button Click Events
    #region
    public void OnMouseEnter()
    {
        Debug.Log("MenuCharacter.OnMouseEnter() called...");

        if(myPresetName != "Random")
        {
            myER.Color = MainMenuManager.Instance.highlightColour;
        }        

    }
    public void OnMouseDown()
    {
        Debug.Log("MenuCharacter.OnMouseDown() called...");
        MainMenuManager.Instance.SetSelectedCharacter(this);
    }
    public void OnMouseExit()
    {
        Debug.Log("MenuCharacter.OnMouseExit() called...");

        if (myPresetName != "Random")
        {
            myER.Color = MainMenuManager.Instance.normalColour;
        }            
    }
    public void OnPreviousPresetButtonClicked()
    {
        BuildMyViewsFromCharacterPresetData(CharacterPresetLibrary.Instance.GetPreviousOriginPreset(currentCharacterPreset));

        // refresh button highlight
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void OnNextPresetButtonClicked()
    {
        BuildMyViewsFromCharacterPresetData(CharacterPresetLibrary.Instance.GetNextOriginPreset(currentCharacterPreset));
        // refresh button highlight
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void OnLoadCustomPresetButtonClicked()
    {
        Debug.Log("MenuCharacter.OnLoadCustomPresetButtonClicked() called...");
        MainMenuManager.Instance.EnableLoadPresetWindow();
        MainMenuManager.Instance.PopulateLoadPresetWindow();
    }
    #endregion

    // View Logic
    #region
    public void EnableInfoPanel()
    {
        infoPanelParent.SetActive(true);
    }
    public void DisableInfoPanel()
    {
        infoPanelParent.SetActive(false);
    }
    #endregion

    // Misc Logic
    #region
    public void SetPresetName(string newName)
    {
        presetNameText.text = newName;
    }
    public void SetMyCharacterData(CharacterPresetData data)
    {
        myPreset = data;
    }
    #endregion
}
