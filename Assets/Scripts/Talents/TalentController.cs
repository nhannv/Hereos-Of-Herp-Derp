﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentController : MonoBehaviour
{
    // Initialization + Setup Singleton
    #region
    public static TalentController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Conditional checks + bools
    #region
    public bool IsTalentPurchaseable(CharacterData character, Talent talent)
    {
        Debug.Log("TalentController.IsTalentPurchaseable() called, checking talent: " + talent.talentName);

        if (!character.HasEnoughAbilityPoints(1))
        {
            Debug.Log("Unable to purchase talent: Not enough ability points");
            return false;
        }

        else if (!DoesCharacterMeetTalentTierRequirment(character, talent))
        {
            Debug.Log("Unable to purchase " + talent.talentName + ": " + character + " does not meet talent tier requirment");
            return false;
        }

        else if (talent.purchased)
        {
            Debug.Log("Unable to purchase " + talent.talentName + ": " + character.myName + " has already purchased this talent");
            return false;
        }

        else
        {
            Debug.Log(character.myName + " meets all requirments to purchase " + talent.talentName);
            return true;
        }
    }
    public bool DoesCharacterMeetTalentTierRequirment(CharacterData character, Talent talent)
    {
        Debug.Log("TalentController.DoesCharacterMeetTalentTierRequirment() called...");

        if (talent.talentPool == Talent.TalentPool.Guardian && 
            character.guardianPoints >= talent.talentTier) 
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }

        else if (talent.talentPool == Talent.TalentPool.Duelist &&
            character.duelistPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }

        else if (talent.talentPool == Talent.TalentPool.Brawler &&
            character.brawlerPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }

        else if (talent.talentPool == Talent.TalentPool.Assassination &&
            character.assassinationPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else if (talent.talentPool == Talent.TalentPool.Pyromania &&
            character.pyromaniaPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else if (talent.talentPool == Talent.TalentPool.Cyromancy &&
            character.cyromancyPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else if (talent.talentPool == Talent.TalentPool.Ranger &&
            character.rangerPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else if (talent.talentPool == Talent.TalentPool.Manipulation &&
            character.manipulationPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else if (talent.talentPool == Talent.TalentPool.Divinity &&
            character.divinityPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else if (talent.talentPool == Talent.TalentPool.Shadowcraft &&
            character.shadowcraftPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else if (talent.talentPool == Talent.TalentPool.Corruption &&
            character.corruptionPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else if (talent.talentPool == Talent.TalentPool.Naturalism &&
            character.naturalismPoints >= talent.talentTier)
        {
            Debug.Log(character.myName + " meets the talent tier requirments of " + talent.talentName);
            return true;
        }
        else
        {
            Debug.Log(character.myName + " does NOT meet the talent tier requirments of " + talent.talentName);
            return false;
        }
    }
    #endregion

    // Mouse + Input Events
    #region
    public void OnTalentButtonClicked(CharacterData character, Talent talent)
    {
        Debug.Log("TalentController.OnTalentButtonClicked() called...");

        // Try buy talent
        if (IsTalentPurchaseable(character, talent))
        {
            PurchaseTalent(character, talent);
            CardRewardScreenManager.Instance.CreateTalentUnlockedExplosion(talent);
        }
    }
    #endregion

    // Talent Purchase Related
    #region
    public void PurchaseTalent(CharacterData character, Talent talent, bool requiresPayemnt = true)
    {
        Debug.Log("TalentController.PurchaseTalent() called...");

        // Unlock talent to prevent further purchase
        talent.purchased = true;

        // Pay ability points
        if (requiresPayemnt)
        {
            character.ModifyAbilityPoints(-1);
        }        

        // Apply benefits of the talent
        if (talent.isPassive)
        {
            ApplyTalentPassiveEffectToCharacter(character, talent);
        }
        else if (talent.isAbility)
        {
            ApplyTalentAbilityToCharacter(character, talent);
        }

        RefreshAllTalentButtonViewStates(character, true, talent.talentPool);
        
    }
    public void ApplyTalentPassiveEffectToCharacter(CharacterData character, Talent talent)
    {
        Debug.Log("TalentController.ApplyTalentPassiveEffectToCharacter() called...");

        if (talent.talentName == "Tenacious")
        {
            character.ModifyTenacious(talent.passiveStacks);
        }

        else if (talent.talentName == "Masochist")
        {
            character.ModifyMasochist(talent.passiveStacks);
        }

        else if (talent.talentName == "Last Stand")
        {
            character.ModifyLastStand(talent.passiveStacks);
        }

        else if (talent.talentName == "Slippery")
        {
            character.ModifySlippery(talent.passiveStacks);
        }

        else if (talent.talentName == "Riposte")
        {
            character.ModifyRiposte(talent.passiveStacks);
        }

        else if (talent.talentName == "Perfect Reflexes")
        {
            character.ModifyPerfectReflexes(talent.passiveStacks);
        }

        else if (talent.talentName == "Opportunist")
        {
            character.ModifyOpportunist(talent.passiveStacks);
        }

        else if (talent.talentName == "Patient Stalker")
        {
            character.ModifyPatientStalker(talent.passiveStacks);
        }

        else if (talent.talentName == "Stealth")
        {
            character.ModifyStealth(talent.passiveStacks);
        }

        else if (talent.talentName == "Cautious")
        {
            character.ModifyCautious(talent.passiveStacks);
        }

        else if (talent.talentName == "Guardian Aura")
        {
            character.ModifyGuardianAura(talent.passiveStacks);
        }

        else if (talent.talentName == "Unwavering")
        {
            character.ModifyUnwavering(talent.passiveStacks);
        }

        else if (talent.talentName == "Fiery Aura")
        {
            character.ModifyFieryAura(talent.passiveStacks);
        }

        else if (talent.talentName == "Immolation")
        {
            character.ModifyImmolation(talent.passiveStacks);
        }

        else if (talent.talentName == "Demon")
        {
            character.ModifyDemon(talent.passiveStacks);
        }

        else if (talent.talentName == "Shatter")
        {
            character.ModifyShatter(talent.passiveStacks);
        }

        else if (talent.talentName == "Frozen Heart")
        {
            character.ModifyFrozenHeart(talent.passiveStacks);
        }

        else if (talent.talentName == "Predator")
        {
            character.ModifyPredator(talent.passiveStacks);
        }

        else if (talent.talentName == "Hawk Eye")
        {
            character.ModifyHawkEye(talent.passiveStacks);
        }

        else if (talent.talentName == "Flux")
        {
            character.ModifyFlux(talent.passiveStacks);
        }

        else if (talent.talentName == "Coup De Grace")
        {
            character.ModifyCoupDeGrace(talent.passiveStacks);
        }

        else if (talent.talentName == "Quick Draw")
        {
            character.ModifyQuickDraw(talent.passiveStacks);
        }

        else if (talent.talentName == "Phasing")
        {
            character.ModifyPhasing(talent.passiveStacks);
        }

        else if (talent.talentName == "Ethereal Being")
        {
            character.ModifyEtherealBeing(talent.passiveStacks);
        }

        else if (talent.talentName == "Encouraging Aura")
        {
            character.ModifyEncouragingAura(talent.passiveStacks);
        }

        else if (talent.talentName == "Radiance")
        {
            character.ModifyRadiance(talent.passiveStacks);
        }

        else if (talent.talentName == "Sacred Aura")
        {
            character.ModifySacredAura(talent.passiveStacks);
        }

        else if (talent.talentName == "Shadow Aura")
        {
            character.ModifyShadowAura(talent.passiveStacks);
        }

        else if (talent.talentName == "Shadow Form")
        {
            character.ModifyShadowForm(talent.passiveStacks);
        }

        else if (talent.talentName == "Poisonous")
        {
            character.ModifyPoisonous(talent.passiveStacks);
        }

        else if (talent.talentName == "Venomous")
        {
            character.ModifyVenomous(talent.passiveStacks);
        }

        else if (talent.talentName == "Toxicity")
        {
            character.ModifyToxicity(talent.passiveStacks);
        }

        else if (talent.talentName == "Toxic Aura")
        {
            character.ModifyToxicAura(talent.passiveStacks);
        }

        else if (talent.talentName == "Storm Aura")
        {
            character.ModifyStormAura(talent.passiveStacks);
        }

        else if (talent.talentName == "Storm Lord")
        {
            character.ModifyStormLord(talent.passiveStacks);
        }
       
    }    
    public void ApplyTalentAbilityToCharacter(CharacterData character, Talent talent)
    {
        Debug.Log("TalentController.ApplyTalentAbilityToCharacter() called...");

        AbilityDataSO data = talent.myAbilityData;

        if(data == null)
        {
            Debug.Log("ApplyTalentAbilityToCharacter.ApplyTalentAbilityToCharacter() revieved null talent argument, searching for clone in Ability Library instead.");
            data = AbilityLibrary.Instance.GetAbilityByName(talent.talentName);
        }

        // Modify character data
        character.HandleLearnAbility(data);
    }
    #endregion

    // Build Talent Info Panels + Get Data
    #region
    public Talent GetTalentByName(CharacterData character, string talentName)
    {
        Debug.Log("TalentController.GetTalentByName() called, searching for " + talentName);
        Talent talentReturned = null;

        foreach (Talent talent in character.allTalentButtons)
        {
            if (talent.name == talentName)
            {
                talentReturned = talent;
                break;
            }
        }

        if (talentReturned == null)
        {
            Debug.Log("TalentController.GetTalentByName() could not find a talent with the name " + talentName + ", returning null");
        }

        return talentReturned;

    }
    public void BuildTalentInfoPanelFromData(Talent talent)
    {
        if (talent.isAbility)
        {
            BuildTalentInfoPanelFromAbilityData(talent);
        }
        else
        {
            BuildTalentInfoPanelFromPassiveData(talent);
        }

    }
    public void BuildTalentInfoPanelFromAbilityData(Talent talent)
    {
        // Get data
        AbilityDataSO data = AbilityLibrary.Instance.GetAbilityByName(talent.talentName);

        // Set button image
        talent.talentImage.sprite = data.sprite;

        // build text and images assets
        AbilityInfoSheetController.Instance.BuildSheetFromData(talent.abilityInfoSheet, data, AbilityInfoSheet.PivotDirection.Upwards);
        TextLogic.SetAbilityDescriptionText(data, talent.abilityInfoSheet.descriptionText);       

    }
    public void BuildTalentInfoPanelFromPassiveData(Talent talent)
    {
        // Set button image
        StatusIconDataSO data = StatusIconLibrary.Instance.GetStatusIconByName(talent.talentName);
        talent.talentImage.sprite = data.statusSprite;

        PassiveInfoSheetController.Instance.BuildSheetFromData(talent.passiveInfoSheet, data, talent.passiveStacks, PassiveInfoSheet.PivotDirection.Upwards);
        TextLogic.SetStatusIconDescriptionText(talent.talentName, talent.passiveInfoSheet.descriptionText, talent.passiveStacks);
    }
    #endregion

    // View Logic
    #region
    public void RefreshAllTalentButtonViewStates(CharacterData character, bool refreshSpecificPageOnly = false, Talent.TalentPool specificPage = Talent.TalentPool.Guardian)
    {
        // only refresh a single talent page (helps with performance)
        if (refreshSpecificPageOnly)
        {
            foreach (Talent talent in character.allTalentButtons)
            {
                if(talent.talentPool == specificPage)
                {
                    if (!IsTalentPurchaseable(character, talent) && talent.purchased == false)
                    {
                        talent.purchasedOverlay.SetActive(false);
                        talent.blackTintOverlay.SetActive(true);
                    }
                    else if (talent.purchased)
                    {
                        talent.blackTintOverlay.SetActive(false);
                        talent.purchasedOverlay.SetActive(true);
                    }
                    else
                    {
                        talent.blackTintOverlay.SetActive(false);
                        talent.purchasedOverlay.SetActive(false);
                    }
                }
            }
        }

        // refresh all talent buttons
        else
        {
            foreach (Talent talent in character.allTalentButtons)
            {

                if (!IsTalentPurchaseable(character, talent) && talent.purchased == false)
                {
                    talent.purchasedOverlay.SetActive(false);
                    talent.blackTintOverlay.SetActive(true);
                }
                else if (talent.purchased)
                {
                    talent.blackTintOverlay.SetActive(false);
                    talent.purchasedOverlay.SetActive(true);
                }
                else
                {
                    talent.blackTintOverlay.SetActive(false);
                    talent.purchasedOverlay.SetActive(false);
                }
            }

        }
      
    }
    #endregion
}
