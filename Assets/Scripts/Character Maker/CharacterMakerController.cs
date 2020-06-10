﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class CharacterMakerController : MonoBehaviour
{
    // Properties + Component References
    #region
    [Header("Parent Component References")]
    public GameObject mainVisualParent;
    public GameObject panelMasterParent;
    public GameObject originPanelParent;
    public GameObject appearancePanelParent;
    public GameObject presetPanelParent;

    [Header("Origin Tab Component References")]
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterRaceText;
    public TextMeshProUGUI currentBackgroundOneText;
    public TextMeshProUGUI currentBackgroundTwoText;   

    [Header("Preset Tab Component References")]    
    public TextMeshProUGUI currentClassPresetText;
    public TextMeshProUGUI currentWeaponPresetText;
    public List<TextMeshProUGUI> allTalentTextTabs;

    [Header("Model Component References")]
    public UniversalCharacterModel characterModel;

    [Header("Character Data Properties")]
    private List<MenuAbilityTab> allAbilityTabs;
    private List<StatusPairing> allPassiveTabs;
    private WeaponPresetDataSO currentWeaponPreset;
    private ClassPresetDataSO currentClassPreset;
    private CharacterData.Background currentBackgroundOne;
    private CharacterData.Background currentBackgroundTwo;
    private List<TalentPairing> allTalentPairings;

    #endregion

    // Singleton Pattern
    #region
    public static CharacterMakerController Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    // On Button Click Events
    #region

    // Main Buttons
    #region
    public void OnCharacterMakerMainMenuButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnCharacterMakerButtonClicked() called...");
        SetMainWindowViewState(true);
        SetCharacterModelDefaultStartingState();
        SetCharacterBackgroundDefaultState();
        BuildCharacterFromClassPresetData(CharacterPresetLibrary.Instance.allClassPresets[0]);
    }
    public void OnOriginButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnOriginButtonClicked() called...");
        DisabelAllPanelViews();
        SetOriginPanelViewState(true);
    }
    public void OnAppearanceButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnAppearanceButtonClicked() called...");
        DisabelAllPanelViews();
        SetAppearancePanelViewState(true);
    }
    public void OnPresetButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPresetButtonClicked() called...");
        DisabelAllPanelViews();
        SetPresetPanelViewState(true);
    }
    public void OnSaveCharacterButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnSaveCharacterButtonClicked() called...");
        StartCharacterSaveProcess();
    }
    #endregion

    // Appearance Page
    #region
    public void OnNextHeadButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnNextHeadButtonClicked() called...");

        if(characterModel.myModelRace == UniversalCharacterModel.ModelRace.Human)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.humanHeads));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Orc)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.orcHeads));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Undead)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.undeadHeads));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Elf)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.elfHeads));
        }
    }
    public void OnPreviousHeadButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPreviousHeadButtonClicked() called...");

        if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Human)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.humanHeads));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Orc)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.orcHeads));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Undead)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.undeadHeads));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Elf)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.elfHeads));
        }
    }
    public void OnNextFaceButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnNextFaceButtonClicked() called...");

        if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Human)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.humanFaces));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Orc)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.orcFaces));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Undead)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.undeadFaces));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Elf)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.elfFaces));
        }
    }
    public void OnPreviousFaceButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPreviousFaceButtonClicked() called...");

        if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Human)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.humanFaces));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Orc)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.orcFaces));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Undead)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.undeadFaces));
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Elf)
        {
            CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.elfFaces));
        }
    }
    public void OnNextHeadWearButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnNextHeadWearButtonClicked() called...");
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allHeadWear));
    }
    public void OnPreviousHeadWearButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPreviousHeadWearButtonClicked() called...");
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetPreviousElementInList(characterModel.allHeadWear));
    }
    public void OnNextChestWearButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnNextChestWearButtonClicked() called...");
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allChestWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allRightArmWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allLeftArmWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allRightHandWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allLeftHandWear));
    }
    public void OnPreviousChestWearButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPreviousHeadWearButtonClicked() called...");
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
               CharacterModelController.GetNextElementInList(characterModel.allChestWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allRightArmWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allLeftArmWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allRightHandWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allLeftHandWear));
    }
    public void OnNextLegWearButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnNextLegWearButtonClicked() called...");
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allLeftLegWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allRightLegWear));
    }
    public void OnPreviousLegWearButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPreviousLegWearButtonClicked() called...");
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
               CharacterModelController.GetNextElementInList(characterModel.allLeftLegWear));
        CharacterModelController.EnableAndSetElementOnModel(characterModel,
                CharacterModelController.GetNextElementInList(characterModel.allRightLegWear));
    }
    #endregion

    // Origin Page
    #region
    public void OnNextRaceButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnNextRaceButtonClicked() called...");

        if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Human)
        {            
            CharacterModelController.SetBaseOrcView(characterModel);
            characterRaceText.text = "Orc";
        }
        else if(characterModel.myModelRace == UniversalCharacterModel.ModelRace.Orc)
        {
            CharacterModelController.SetBaseUndeadView(characterModel);
            characterRaceText.text = "Undead";
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Undead)
        {
            CharacterModelController.SetBaseElfView(characterModel);
            characterRaceText.text = "Elf";
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Elf)
        {
            CharacterModelController.SetBaseHumanView(characterModel);
            characterRaceText.text = "Human";
        }

        BuildWeaponTabFromWeaponPresetData(currentWeaponPreset);

    }
    public void OnPreviousRaceButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPreviousRaceButtonClicked() called...");

        if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Orc)
        {
            CharacterModelController.SetBaseHumanView(characterModel);
            characterRaceText.text = "Human";
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Human)
        {
            CharacterModelController.SetBaseUndeadView(characterModel);
            characterRaceText.text = "Undead";
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Undead)
        {
            CharacterModelController.SetBaseElfView(characterModel);
            characterRaceText.text = "Elf";
        }
        else if (characterModel.myModelRace == UniversalCharacterModel.ModelRace.Elf)
        {
            CharacterModelController.SetBaseOrcView(characterModel);
            characterRaceText.text = "Orc";
        }

        BuildWeaponTabFromWeaponPresetData(currentWeaponPreset);
    }
    public void OnNextBackgroundOneButtonClicked()
    {
        SetCharacterBackgroundOne(GetNextBackground(currentBackgroundOne));
        if(currentBackgroundOne == currentBackgroundTwo)
        {
            SetCharacterBackgroundOne(GetNextBackground(currentBackgroundOne));
        }
    }
    public void OnPreviousBackgroundOneButtonClicked()
    {
        SetCharacterBackgroundOne(GetPreviousBackground(currentBackgroundOne));
        if (currentBackgroundOne == currentBackgroundTwo)
        {
            SetCharacterBackgroundOne(GetPreviousBackground(currentBackgroundOne));
        }
    }
    public void OnNextBackgroundTwoButtonClicked()
    {
        SetCharacterBackgroundTwo(GetNextBackground(currentBackgroundTwo));
        if (currentBackgroundTwo == currentBackgroundOne)
        {
            SetCharacterBackgroundTwo(GetNextBackground(currentBackgroundTwo));
        }
    }
    public void OnPreviousBackgroundTwoButtonClicked()
    {
        SetCharacterBackgroundTwo(GetPreviousBackground(currentBackgroundTwo));
        if (currentBackgroundTwo == currentBackgroundOne)
        {
            SetCharacterBackgroundTwo(GetPreviousBackground(currentBackgroundTwo));
        }
    }
    #endregion

    // Preset Page
    #region
    public void OnNextClassPresetButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnNextClassPresetButtonClicked() called...");
        BuildCharacterFromClassPresetData(GetNextClassPreset());
    }
    public void OnPreviousClassPresetButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPreviousClassPresetButtonClicked() called...");
        BuildCharacterFromClassPresetData(GetPreviousClassPreset());
    }
    public void OnNextWeaponPresetButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnNextWeaponPresetButtonClicked() called...");
        BuildWeaponTabFromWeaponPresetData(GetNextWeaponPreset());
    }
    public void OnPreviousWeaponPresetButtonClicked()
    {
        Debug.Log("CharacterMakerController.OnPreviousWeaponPresetButtonClicked() called...");
        BuildWeaponTabFromWeaponPresetData(GetPreviousWeaponPreset());
    }
    #endregion
    #endregion

    // Enable + Disable Views
    #region
    public void SetMainWindowViewState(bool onOrOff)
    {
        Debug.Log("CharacterMakerController.SetMainWindowViewState() called, set state: " + onOrOff.ToString());
        mainVisualParent.SetActive(onOrOff);
    }
    public void SetPanelMasterViewState(bool onOrOff)
    {
        Debug.Log("CharacterMakerController.SetPanelMasterViewState() called, set state: " + onOrOff.ToString());
        panelMasterParent.SetActive(onOrOff);
    }
    public void SetOriginPanelViewState(bool onOrOff)
    {
        Debug.Log("CharacterMakerController.SetOriginPanelViewState() called, set state: " + onOrOff.ToString());
        originPanelParent.SetActive(onOrOff);
    }
    public void SetAppearancePanelViewState(bool onOrOff)
    {
        Debug.Log("CharacterMakerController.SetAppearancePanelViewState() called, set state: " + onOrOff.ToString());
        appearancePanelParent.SetActive(onOrOff);
    }
    public void SetPresetPanelViewState(bool onOrOff)
    {
        Debug.Log("CharacterMakerController.SetPresetPanelViewState() called, set state: " + onOrOff.ToString());
        presetPanelParent.SetActive(onOrOff);
    }
    public void DisabelAllPanelViews()
    {
        Debug.Log("CharacterMakerController.DisabelAllPanelViews() called...");
        SetOriginPanelViewState(false);
        SetAppearancePanelViewState(false);
        SetPresetPanelViewState(false);
    }
    public void DisableAllAbilityTabs()
    {
        foreach(MenuAbilityTab tab in allAbilityTabs)
        {
            tab.gameObject.SetActive(false);
        }
    }
    public void DisableAllTalentTextTabs()
    {
        foreach (TextMeshProUGUI tab in allTalentTextTabs)
        {
            tab.gameObject.SetActive(false);
        }
    }
    #endregion

    // Set Default + Start State Logic
    #region
    private void SetCharacterModelDefaultStartingState()
    {
        Debug.Log("CharacterMakerController.SetCharacterModelDefaultStartingState() called...");
        SetCharacterModelIdleAnim();
        SetCharacterModelDefaultView();
    }
    private void SetCharacterModelIdleAnim()
    {
        Debug.Log("CharacterMakerController.SetCharacterModelIdleAnim() called...");
        characterModel.SetIdleAnim();
    }
    private void SetCharacterModelDefaultView()
    {
        Debug.Log("CharacterMakerController.SetCharacterModelDefaultView() called...");
        CharacterModelController.CompletelyDisableAllViews(characterModel);
        CharacterModelController.SetBaseHumanView(characterModel);
        characterRaceText.text = "Human";
    }
    private void SetCharacterBackgroundDefaultState()
    {
        SetCharacterBackgroundOne(CharacterData.Background.Unknown);
        SetCharacterBackgroundTwo(CharacterData.Background.Unknown);
    }
    #endregion

    // Conditional Checks + Bools
    #region
    public static bool IsCharacterSaveActionValid()
    {
        return true;
    }
    #endregion

    // Save Character Preset Data Logic
    #region
    public void StartCharacterSaveProcess()
    {
        Debug.Log("CharacterMakerController.StartCharacterSaveProcess() called...");

        if (IsCharacterSaveActionValid())
        {
            // Save action is valid, start save process
            CharacterPresetData newData = new CharacterPresetData();

            // Set Origin Data
            SaveOriginDataToCharacterPresetFile(newData);            

            // Set up model data
            SaveModelDataToCharacterPresetFile(newData, characterModel);

            // Set up combat data
            SaveCombatDataToCharacterPresetFile(newData);

            // Set up weapon data
            SaveWeaponDataToCharacterPresetFile(newData);

            // Print info (for testing, remove later)
            CharacterPresetLibrary.Instance.PrintPresetData(newData);

            // Add new data to persistency
            CharacterPresetLibrary.Instance.AddCharacterPresetToPlayerMadeList(newData);
        }
    }
    private void SaveModelDataToCharacterPresetFile(CharacterPresetData charData, UniversalCharacterModel model)
    {
        Debug.Log("CharacterMakerController.SaveModelDataToCharacterPresetFile() called...");

        // Get all active model elements
        List<UniversalCharacterModelElement> allActiveModelElements = new List<UniversalCharacterModelElement>
        {
            // Body Parts
            model.activeHead,
            model.activeFace,
            model.activeLeftLeg ,
            model.activeRightLeg,
            model.activeRightHand,
            model.activeRightArm,
            model.activeLeftHand,
            model.activeLeftArm,
            model.activeChest,

            // Clothing 
            model.activeHeadWear,
            model.activeChestWear,
            model.activeRightLegWear,
            model.activeLeftLegWear,
            model.activeRightArmWear,
            model.activeRightHandWear,
            model.activeLeftArmWear,
            model.activeLeftHandWear,

            // Weapons
            model.activeMainHandWeapon,
            model.activeOffHandWeapon,
        };

        // Add names of each element to preset data list
        foreach(UniversalCharacterModelElement ele in allActiveModelElements)
        {
            if(ele != null)
            {
                charData.activeModelElements.Add(new ModelElementData(ele));
            }
            else if(ele == null)
            {
                Debug.Log("CharacterMakerController.SaveModelDataToCharacterPresetFile() detected a " +
                    "UCM element with a null gameObject parent, skipping...");
            }            
        }
    }
    private void SaveCombatDataToCharacterPresetFile(CharacterPresetData charData)
    {
        Debug.Log("CharacterMakerController.SaveCombatDataToCharacterPresetFile() called...");

        // Add abilities
        foreach (MenuAbilityTab ability in allAbilityTabs)
        {
            if (ability.isAbility)
            {
                charData.knownAbilities.Add(ability.myAbilityData);
            }
            if (ability.isPassive)
            {
                allPassiveTabs.Add(new StatusPairing(ability.myPassiveData, ability.passiveStacks));
            }
        }

        // Add passives
        foreach (StatusPairing passive in allPassiveTabs)
        {
            charData.knownPassives.Add(passive);
        }

        // Add talent data
        foreach(TalentPairing talentPairing in allTalentPairings)
        {
            charData.knownTalents.Add(talentPairing);
        }
    }
    private void SaveOriginDataToCharacterPresetFile(CharacterPresetData charData)
    {
        Debug.Log("CharacterMakerController.SaveOriginDataToCharacterPresetFile() called...");

        // Set name
        charData.characterName = characterNameText.text;

        // Set backgrounds
        charData.backgrounds.Add(currentBackgroundOne);
        charData.backgrounds.Add(currentBackgroundTwo);
    }
    private void SaveWeaponDataToCharacterPresetFile(CharacterPresetData charData)
    {
        Debug.Log("CharacterMakerController.SaveWeaponDataToCharacterPresetFile() called...");

        // Main hand weapon
        if (currentWeaponPreset.mainHandWeapon)
        {
            charData.mhWeapon = currentWeaponPreset.mainHandWeapon;
        }

        // Off hand weapon
        if (currentWeaponPreset.offHandWeapon)
        {
            charData.ohWeapon = currentWeaponPreset.offHandWeapon;
        }

    }
    #endregion

    // Core Logic
    #region
    public void BuildCharacterFromClassPresetData(ClassPresetDataSO data)
    {
        Debug.Log("CharacterMakerController.BuildCharacterFromClassPresetData() called, building from " + data.classPresetName);
        // Flush old data and views
        DisableAllAbilityTabs();
        DisableAllTalentTextTabs();
        ClearAllTalentPairings();

        // Build new data + views
        currentClassPreset = data;
        currentClassPresetText.text = data.classPresetName;
        BuildAllAbilityTabsFromClassPresetData(data);
        BuildAllTalentTextTabsFromClassPresetData(data);
        BuildWeaponTabFromWeaponPresetData(data.weaponPreset);

    }
    private void BuildAllAbilityTabsFromClassPresetData(ClassPresetDataSO data)
    {
        Debug.Log("CharacterMakerController.BuildAbilityTabsFromClassPresetData() called, building from " + data.classPresetName);

        // Build abilities first
        foreach(AbilityDataSO abilityData in data.abilities)
        {
            BuildAbilityTabFromAbilityData(abilityData);
        }

        // Build passives second
        foreach (StatusPairing passiveData in data.passives)
        {
            BuildAbilityTabFromPassiveData(passiveData.statusData, passiveData.statusStacks);
        }
    }
    private void BuildTalentTextTabFromTalentPairing(TalentPairing talentPair)
    {
        // Get next available text tab
        TextMeshProUGUI textTab = GetNextAvailableTalentTextTab();

        // Enable view
        textTab.gameObject.SetActive(true);

        // Set text
        textTab.text = talentPair.talentType.ToString() + " +" + talentPair.talentStacks.ToString();
        
    }
    private void BuildWeaponTabFromWeaponPresetData(WeaponPresetDataSO data)
    {
        Debug.Log("CharacterMakerController.BuildWeaponTabFromWeaponPresetData() called...");

        // Cancel build if character preset does not have a weapon preset
        if (!data)
        {
            Debug.Log("CharacterMakerController.BuildWeaponTabFromWeaponPresetData() given null data, cancelling weapon build...");
            return;
        }

        // Set text panel
        currentWeaponPresetText.text = data.weaponPresetName;

        // cache data
        currentWeaponPreset = data;

        // disable model weapon views and cache refs
        if (characterModel.activeMainHandWeapon)
        {
            characterModel.activeMainHandWeapon.gameObject.SetActive(false);
            characterModel.activeMainHandWeapon = null;
        }
        if (characterModel.activeOffHandWeapon)
        {
            characterModel.activeOffHandWeapon.gameObject.SetActive(false);
            characterModel.activeOffHandWeapon = null;
        }

        // set MH weapon model view
        foreach (UniversalCharacterModelElement ucme in characterModel.allMainHandWeapons)
        {
            if (ucme.weaponsWithMyView.Contains(data.mainHandWeapon))
            {
                CharacterModelController.EnableAndSetElementOnModel(characterModel, ucme);
                break;
            }
        }
         
        // Set OH weapon model view
        if(data.offHandWeapon != null)
        {
            foreach (UniversalCharacterModelElement ucme in characterModel.allOffHandWeapons)
            {
                if (ucme.weaponsWithMyView.Contains(data.offHandWeapon))
                {
                    CharacterModelController.EnableAndSetElementOnModel(characterModel, ucme);
                    break;
                }
            }
        }

    }
    private void BuildAbilityTabFromAbilityData(AbilityDataSO data)
    {
        Debug.Log("CharacterMakerController.BuildAbilityTabFromAbilityData() called, building from: " + data.abilityName);

        // Get slot
        MenuAbilityTab nextSlot = GetNextAvailableMenuTabSlot();

        // Enable view
        nextSlot.gameObject.SetActive(true);

        // Build views and data
        nextSlot.SetUpAbilityTabAsAbility(data);
    }
    private void BuildAbilityTabFromPassiveData(StatusIconDataSO data, int stacks)
    {
        Debug.Log("CharacterMakerController.BuildAbilityTabFromAbilityData() called, building from: " + data.statusName);

        // Get slot
        MenuAbilityTab nextSlot = GetNextAvailableMenuTabSlot();

        // Enable view
        nextSlot.gameObject.SetActive(true);

        // Build views and data
        nextSlot.SetUpAbilityTabAsPassive(data, stacks);
    }
    private void SetCharacterBackgroundOne(CharacterData.Background background)
    {
        // cache BG ref
        currentBackgroundOne = background;

        // set text
        currentBackgroundOneText.text = background.ToString();
    }
    private void SetCharacterBackgroundTwo(CharacterData.Background background)
    {
        // cache BG ref
        currentBackgroundTwo = background;

        // set text
        currentBackgroundTwoText.text = background.ToString();
    }    
    private void BuildAllTalentTextTabsFromClassPresetData(ClassPresetDataSO data)
    {
        Debug.Log("CharacterMakerController.BuildAbilityTabsFromClassPresetData() called, building from " + data.classPresetName);

        foreach(TalentPairing talentPair in data.talents)
        {
            AddTalentPairingToPersistentList(new TalentPairing(talentPair.talentType, talentPair.talentStacks));
            BuildTalentTextTabFromTalentPairing(talentPair);
        }

    }
    private void ClearAllTalentPairings()
    {
        allTalentPairings.Clear();
    }
    private void AddTalentPairingToPersistentList(TalentPairing talentPairing)
    {
        allTalentPairings.Add(talentPairing);
    }

    #endregion

    // Get Next + Previous Data
    #region
    private MenuAbilityTab GetNextAvailableMenuTabSlot()
    {
        Debug.Log("CharacterMakerController.GetNextAvailbleMenuTabSlot() called...");

        MenuAbilityTab tabReturned = null;

        foreach (MenuAbilityTab tab in allAbilityTabs)
        {
            if (tab.gameObject.activeSelf == false)
            {
                tabReturned = tab;
                break;
            }
        }
        if (tabReturned == null)
        {
            Debug.Log("CharacterMakerController.GetNextAvailbleMenuTabSlot() could not find an availble menu tab, returning null...");
        }
        return tabReturned;

    }
    private TextMeshProUGUI GetNextAvailableTalentTextTab()
    {
        Debug.Log("CharacterMakerController.GetNextAvailableTalentTextSlot() called...");

        TextMeshProUGUI tabReturned = null;

        foreach (TextMeshProUGUI tab in allTalentTextTabs)
        {
            if (tab.gameObject.activeSelf == false)
            {
                tabReturned = tab;
                break;
            }
        }
        if (tabReturned == null)
        {
            Debug.Log("CharacterMakerController.TextMeshProUGUI() could not find an availble menu tab, returning null...");
        }
        return tabReturned;

    }
    private ClassPresetDataSO GetNextClassPreset()
    {
        Debug.Log("CharacterMakerController.GetNextClassPreset() called...");

        int currentIndex = CharacterPresetLibrary.Instance.allClassPresets.IndexOf(currentClassPreset);
        int nextIndex = 0;

        if (currentClassPreset == CharacterPresetLibrary.Instance.allClassPresets.Last())
        {
            nextIndex = 0;
        }
        else
        {
            nextIndex = currentIndex + 1;
        }

        return CharacterPresetLibrary.Instance.allClassPresets[nextIndex];
    }
    private ClassPresetDataSO GetPreviousClassPreset()
    {
        Debug.Log("CharacterMakerController.GetPreviousClassPreset() called...");

        int currentIndex = CharacterPresetLibrary.Instance.allClassPresets.IndexOf(currentClassPreset);
        int previousIndex = 0;

        if (currentClassPreset == CharacterPresetLibrary.Instance.allClassPresets.First())
        {
            previousIndex = CharacterPresetLibrary.Instance.allClassPresets.Count - 1;
        }
        else
        {
            previousIndex = currentIndex - 1;
        }

        return CharacterPresetLibrary.Instance.allClassPresets[previousIndex];
    }
    private WeaponPresetDataSO GetNextWeaponPreset()
    {
        Debug.Log("CharacterMakerController.GetNextWeaponPreset() called...");

        int currentIndex = CharacterPresetLibrary.Instance.allWeaponPresets.IndexOf(currentWeaponPreset);
        int nextIndex = 0;

        if (currentWeaponPreset == CharacterPresetLibrary.Instance.allWeaponPresets.Last())
        {
            nextIndex = 0;
        }
        else
        {
            nextIndex = currentIndex + 1;
        }

        return CharacterPresetLibrary.Instance.allWeaponPresets[nextIndex];
    }
    private WeaponPresetDataSO GetPreviousWeaponPreset()
    {
        Debug.Log("CharacterMakerController.GetPreviousWeaponPreset() called...");

        int currentIndex = CharacterPresetLibrary.Instance.allWeaponPresets.IndexOf(currentWeaponPreset);
        int previousIndex = 0;

        if (currentWeaponPreset == CharacterPresetLibrary.Instance.allWeaponPresets.First())
        {
            previousIndex = CharacterPresetLibrary.Instance.allWeaponPresets.Count - 1;
        }
        else
        {
            previousIndex = currentIndex - 1;
        }

        return CharacterPresetLibrary.Instance.allWeaponPresets[previousIndex];
    }
    private CharacterData.Background GetNextBackground(CharacterData.Background currentBackground)
    {
        Debug.Log("CharacterMakerController.GetNextBackground() called...");

        CharacterData.Background bgReturned = CharacterData.Background.None;

        int currentEnumIndex = (int)currentBackground;
        int enumCount = Enum.GetNames(typeof(CharacterData.Background)).Length;
        Debug.Log("CharacterMakerController.GetNextBackground() found " + enumCount.ToString() + " elements in the background enum");
        int nextIndex = 0;

        if (currentEnumIndex == enumCount - 1)
        {
            nextIndex = 0;
        }
        else
        {
            nextIndex = currentEnumIndex + 1;
        }

        bgReturned = (CharacterData.Background)nextIndex;

        return bgReturned;
    }
    private CharacterData.Background GetPreviousBackground(CharacterData.Background currentBackground)
    {
        Debug.Log("CharacterMakerController.GetPreviousBackground() called...");

        CharacterData.Background bgReturned = CharacterData.Background.None;

        int currentEnumIndex = (int)currentBackground;
        int enumCount = Enum.GetNames(typeof(CharacterData.Background)).Length;
        Debug.Log("CharacterMakerController.GetPreviousBackground() found " + enumCount.ToString() + " elements in the background enum");
        int previousIndex = 0;

        if (currentEnumIndex == 0)
        {
            previousIndex = enumCount - 1;
        }
        else
        {
            previousIndex = currentEnumIndex - 1;
        }

        bgReturned = (CharacterData.Background)previousIndex;

        return bgReturned;
    }
    #endregion

}
