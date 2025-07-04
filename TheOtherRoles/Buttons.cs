using HarmonyLib;
using Hazel;
using System;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using TheOtherRoles.Objects;
using System.Linq;
using System.Collections.Generic;
using TheOtherRoles.Utilities;
using TheOtherRoles.CustomGameModes;
using TheOtherRoles.Patches;
using TheOtherRoles.Modules;
using TheOtherRoles.MetaContext;
using Reactor.Utilities;
using BepInEx.Unity.IL2CPP.Utils.Collections;

namespace TheOtherRoles
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    static class HudManagerStartPatch
    {
        private static bool initialized = false;

        private static CustomButton engineerRepairButton;
        private static CustomButton janitorCleanButton;
        public static CustomButton sheriffKillButton;
        private static CustomButton deputyHandcuffButton;
        private static CustomButton timeMasterShieldButton;
        private static CustomButton medicShieldButton;
        private static CustomButton shifterShiftButton;
        private static CustomButton morphlingButton;
        private static CustomButton camouflagerButton;
        private static CustomButton portalmakerPlacePortalButton;
        private static CustomButton usePortalButton;
        private static CustomButton portalmakerMoveToPortalButton;
        private static CustomButton hackerButton;
        public static CustomButton hackerVitalsButton;
        public static CustomButton hackerAdminTableButton;
        private static CustomButton trackerTrackPlayerButton;
        private static CustomButton trackerTrackCorpsesButton;
        public static CustomButton vampireKillButton;
        public static CustomButton garlicButton;
        public static CustomButton jackalKillButton;
        public static CustomButton sidekickKillButton;
        private static CustomButton jackalSidekickButton;
        private static CustomButton eraserButton;
        private static CustomButton placeJackInTheBoxButton;        
        private static CustomButton lightsOutButton;
        public static CustomButton cleanerCleanButton;
        public static CustomButton warlockCurseButton;
        public static CustomButton securityGuardButton;
        public static CustomButton securityGuardCamButton;
        public static CustomButton arsonistButton;
        public static CustomButton serialKillerButton;
        public static CustomButton vultureEatButton;
        public static CustomButton mediumButton;
        public static CustomButton pursuerButton;
        public static CustomButton witchSpellButton;
        public static CustomButton assassinButton;
        public static CustomButton mayorMeetingButton;
        public static CustomButton thiefKillButton;
        public static CustomButton ninjaButton;
        public static CustomButton evilTrackerButton;
        public static CustomButton sprintButton;
        public static List<CustomButton> fortuneTellerButtons;
        public static CustomButton fortuneTellerLeftButton;
        public static CustomButton fortuneTellerRightButton;
        public static CustomButton veteranAlertButton;
        public static CustomButton undertakerDragButton;
        public static CustomButton mimicAAdminButton;
        public static CustomButton mimicAMorphButton;
        public static CustomButton sherlockInvestigateButton;
        public static CustomButton sherlockWatchButton;
        public static CustomButton bomberAPlantBombButton;
        public static CustomButton bomberAReleaseBombButton;
        public static CustomButton bomberBPlantBombButton;
        public static CustomButton bomberBReleaseBombButton;
        public static CustomButton jackalAndSidekickSabotageLightsButton;
        public static CustomButton evilHackerButton;
        public static CustomButton evilHackerCreatesMadmateButton;
        public static CustomButton trapperSetTrapButton;
        public static CustomButton blackmailerButton;
        public static CustomButton moriartyBrainwashButton;
        public static CustomButton moriartyKillButton;
        public static CustomButton akujoHonmeiButton;
        public static CustomButton akujoBackupButton;
        public static CustomButton plagueDoctorButton;
        public static CustomButton jekyllAndHydeKillButton;
        public static CustomButton jekyllAndHydeDrugButton;
        public static CustomButton jekyllAndHydeSuicideButton;
        public static CustomButton teleporterTeleportButton;
        public static CustomButton detectiveButton;
        public static CustomButton kataomoiButton;
        public static CustomButton kataomoiStalkingButton;
        public static CustomButton kataomoiSearchButton;
        public static CustomButton cupidArrowButton;
        public static CustomButton cupidShieldButton;
        public static CustomButton foxStealthButton;
        public static CustomButton foxRepairButton;
        public static CustomButton foxImmoralistButton;
        public static CustomButton immoralistButton;
        public static CustomButton buskerButton;
        public static CustomButton noisemakerButton;
        public static CustomButton schrodingersCatKillButton;
        public static CustomButton schrodingersCatSwitchButton;
        public static CustomButton yoyoButton;
        public static CustomButton archaeologistDetectButton;
        public static CustomButton archaeologistExcavateButton;
        public static CustomButton doomsayerButton;
        public static CustomButton operateButton;
        public static CustomButton freePlaySuicideButton;
        public static CustomButton freePlayReviveButton;
        public static CustomButton eventKickButton;
        //public static CustomButton trapperButton;
        //public static CustomButton bomberButton;
        //public static CustomButton defuseButton;
        public static CustomButton zoomOutButton;
        //public static CustomButton roleSummaryButton;
        public static CustomButton accelAttributeButton;
        public static CustomButton decelAttributeButton;
        private static CustomButton hunterLighterButton;
        private static CustomButton hunterAdminTableButton;
        private static CustomButton hunterArrowButton;
        private static CustomButton huntedShieldButton;

        public static Dictionary<byte, List<CustomButton>> deputyHandcuffedButtons = null;
        public static PoolablePlayer targetDisplay;

        public static TMPro.TMP_Text securityGuardButtonScrewsText;
        public static TMPro.TMP_Text securityGuardChargesText;
        public static TMPro.TMP_Text deputyButtonHandcuffsText;
        public static TMPro.TMP_Text tricksterBoxesText;
        public static TMPro.TMP_Text engineerRepairText;
        public static TMPro.TMP_Text foxRepairText;
        public static TMPro.TMP_Text vultureRemainingText;
        public static TMPro.TMP_Text pursuerButtonBlanksText;
        public static TMPro.TMP_Text veteranButtonAlertText;
        public static TMPro.TMP_Text hackerAdminTableChargesText;
        public static TMPro.TMP_Text hackerVitalsChargesText;
        public static TMPro.TMP_Text sherlockNumInvestigateText;
        public static TMPro.TMP_Text sherlockNumKillTimerText;
        public static TMPro.TMP_Text moriartyKillCounterText;
        public static TMPro.TMP_Text jekyllAndHydeKillCounterText;
        public static TMPro.TMP_Text jekyllAndHydeDrugText;
        public static TMPro.TMP_Text doomsayerUsesText;
        public static TMPro.TMP_Text akujoTimeRemainingText;
        public static TMPro.TMP_Text akujoHonmeiText;
        public static TMPro.TMP_Text akujoBackupLeftText;
        public static TMPro.TMP_Text cupidTimeRemainingText;
        public static TMPro.TMP_Text cupidLoversText;
        public static TMPro.TMP_Text plagueDoctornumInfectionsText;
        public static TMPro.TMP_Text teleporterNumLeftText;
        public static TMPro.TMP_Text noisemakerButtonText;
        //public static TMPro.TMP_Text trapperChargesText;
        public static TMPro.TMP_Text portalmakerButtonText1;
        public static TMPro.TMP_Text portalmakerButtonText2;
        public static TMPro.TMP_Text portalmakerButtonNumText;
        public static TMPro.TMP_Text huntedShieldCountText;

        public static Props.Proptip accelAttributePropTip;
        public static Props.Proptip decelAttributePropTip;

        public static void setCustomButtonCooldowns() {
            if (!initialized) {
                try {
                    createButtonsPostfix(HudManager.Instance);
                } 
                catch {
                    TheOtherRolesPlugin.Logger.LogWarning("Button cooldowns not set, either the gamemode does not require them or there's something wrong.");
                    return;
                }
            }
            engineerRepairButton.MaxTimer = 0f;
            janitorCleanButton.MaxTimer = Janitor.cooldown;
            sheriffKillButton.MaxTimer = Sheriff.cooldown;
            deputyHandcuffButton.MaxTimer = Deputy.handcuffCooldown;
            timeMasterShieldButton.MaxTimer = TimeMaster.cooldown;
            medicShieldButton.MaxTimer = 0f;
            shifterShiftButton.MaxTimer = 0f;
            morphlingButton.MaxTimer = Morphling.cooldown;
            camouflagerButton.MaxTimer = Camouflager.cooldown;
            portalmakerPlacePortalButton.MaxTimer = Portalmaker.cooldown;
            usePortalButton.MaxTimer = Portalmaker.usePortalCooldown;
            portalmakerMoveToPortalButton.MaxTimer = Portalmaker.usePortalCooldown;
            hackerButton.MaxTimer = Hacker.cooldown;
            hackerVitalsButton.MaxTimer = Hacker.cooldown;
            hackerAdminTableButton.MaxTimer = Hacker.cooldown;
            vampireKillButton.MaxTimer = Vampire.cooldown;
            trackerTrackPlayerButton.MaxTimer = 0f;
            garlicButton.MaxTimer = 0f;
            jackalKillButton.MaxTimer = Jackal.cooldown;
            sidekickKillButton.MaxTimer = Sidekick.cooldown;
            jackalSidekickButton.MaxTimer = Jackal.createSidekickCooldown;
            eraserButton.MaxTimer = Eraser.cooldown;
            placeJackInTheBoxButton.MaxTimer = Trickster.placeBoxCooldown;
            lightsOutButton.MaxTimer = Trickster.lightsOutCooldown;
            cleanerCleanButton.MaxTimer = Cleaner.cooldown;
            warlockCurseButton.MaxTimer = Warlock.cooldown;
            yoyoButton.MaxTimer = Yoyo.markCooldown;
            securityGuardButton.MaxTimer = SecurityGuard.cooldown;
            securityGuardCamButton.MaxTimer = SecurityGuard.cooldown;
            arsonistButton.MaxTimer = Arsonist.cooldown;
            kataomoiButton.MaxTimer = Kataomoi.stareCooldown;
            kataomoiStalkingButton.MaxTimer = Kataomoi.stalkingCooldown;
            kataomoiSearchButton.MaxTimer = Kataomoi.searchCooldown;
            vultureEatButton.MaxTimer = Vulture.cooldown;
            detectiveButton.MaxTimer = Detective.inspectCooldown;
            mediumButton.MaxTimer = Medium.cooldown;
            pursuerButton.MaxTimer = Pursuer.cooldown;
            trackerTrackCorpsesButton.MaxTimer = Tracker.corpsesTrackingCooldown;
            witchSpellButton.MaxTimer = Witch.cooldown;
            assassinButton.MaxTimer = Assassin.cooldown;
            thiefKillButton.MaxTimer = Thief.cooldown;
            mayorMeetingButton.MaxTimer = GameManager.Instance.LogicOptions.GetEmergencyCooldown();
            ninjaButton.MaxTimer = Ninja.stealthCooldown;
            serialKillerButton.MaxTimer = SerialKiller.suicideTimer;
            archaeologistDetectButton.MaxTimer = Archaeologist.cooldown;
            archaeologistExcavateButton.MaxTimer = Archaeologist.cooldown;
            doomsayerButton.MaxTimer = Doomsayer.cooldown;
            foreach (var button in fortuneTellerButtons)
            {
                button.MaxTimer = 0f;
                button.Timer = 0f;
            }
            fortuneTellerLeftButton.MaxTimer = 0f;
            fortuneTellerLeftButton.Timer = 0f;
            fortuneTellerRightButton.MaxTimer = 0f;
            fortuneTellerRightButton.Timer = 0f;
            //serialKillerButton.MaxTimer = 0f;
            evilTrackerButton.MaxTimer = EvilTracker.cooldown;
            trapperSetTrapButton.MaxTimer = Trapper.cooldown;
            sprintButton.MaxTimer = Sprinter.sprintCooldown;
            veteranAlertButton.MaxTimer = Veteran.cooldown;
            plagueDoctorButton.MaxTimer = PlagueDoctor.infectCooldown;
            teleporterTeleportButton.MaxTimer = Teleporter.teleportCooldown;
            undertakerDragButton.MaxTimer = 0f;
            mimicAAdminButton.MaxTimer = 0f;
            mimicAMorphButton.MaxTimer = 0f;
            sherlockInvestigateButton.MaxTimer = Sherlock.cooldown;
            sherlockWatchButton.MaxTimer = GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown;
            bomberAPlantBombButton.MaxTimer = BomberA.cooldown;
            bomberBPlantBombButton.MaxTimer = BomberA.cooldown;
            bomberAReleaseBombButton.MaxTimer = 0f;
            bomberBReleaseBombButton.MaxTimer = 0f;
            evilHackerButton.MaxTimer = 0f;
            evilHackerCreatesMadmateButton.MaxTimer = 0f;
            moriartyBrainwashButton.MaxTimer = Moriarty.brainwashCooldown;
            moriartyKillButton.MaxTimer = 0f;
            noisemakerButton.MaxTimer = Noisemaker.cooldown;
            schrodingersCatKillButton.MaxTimer = SchrodingersCat.killCooldown;
            schrodingersCatSwitchButton.MaxTimer = 0f;
            schrodingersCatSwitchButton.Timer = 0f;
            //roleSummaryButton.Timer = 0f;
            //roleSummaryButton.MaxTimer = 0f;
            cupidArrowButton.MaxTimer = 0f;
            cupidShieldButton.MaxTimer = 0f;
            accelAttributeButton.MaxTimer = 0f;
            accelAttributeButton.Timer = 0f;
            decelAttributeButton.MaxTimer = 0f;
            decelAttributeButton.Timer = 0f;
            blackmailerButton.MaxTimer = Blackmailer.cooldown;
            jekyllAndHydeKillButton.MaxTimer = JekyllAndHyde.cooldown;
            jekyllAndHydeSuicideButton.MaxTimer = JekyllAndHyde.suicideTimer;
            jekyllAndHydeDrugButton.MaxTimer = 0f;
            jekyllAndHydeDrugButton.Timer = 0f;
            akujoHonmeiButton.MaxTimer = 0f;
            akujoBackupButton.MaxTimer = 0f;
            foxStealthButton.MaxTimer = Fox.stealthCooldown;
            foxRepairButton.MaxTimer = 0f;
            foxImmoralistButton.MaxTimer = 20f;
            immoralistButton.MaxTimer = 20f;
            buskerButton.MaxTimer = Busker.cooldown;
            jackalAndSidekickSabotageLightsButton.MaxTimer = 0f;
            operateButton.MaxTimer = 0f;
            operateButton.Timer = 0f;
            freePlayReviveButton.MaxTimer = 0f;
            freePlayReviveButton.Timer = 0f;
            freePlaySuicideButton.MaxTimer = 0f;
            freePlaySuicideButton.Timer = 0f;
            //trapperButton.MaxTimer = Trapper.cooldown;
            //bomberButton.MaxTimer = Bomber.bombCooldown;
            hunterLighterButton.MaxTimer = Hunter.lightCooldown;
            hunterAdminTableButton.MaxTimer = Hunter.AdminCooldown;
            hunterArrowButton.MaxTimer = Hunter.ArrowCooldown;
            huntedShieldButton.MaxTimer = Hunted.shieldCooldown;
            //defuseButton.MaxTimer = 0f;
            //defuseButton.Timer = 0f;

            timeMasterShieldButton.EffectDuration = TimeMaster.shieldDuration;
            hackerButton.EffectDuration = Hacker.duration;
            hackerVitalsButton.EffectDuration = Hacker.duration;
            hackerAdminTableButton.EffectDuration = Hacker.duration;
            vampireKillButton.EffectDuration = Vampire.delay;
            camouflagerButton.EffectDuration = Camouflager.duration;
            morphlingButton.EffectDuration = Morphling.duration;
            lightsOutButton.EffectDuration = Trickster.lightsOutDuration;
            arsonistButton.EffectDuration = Arsonist.duration;
            mediumButton.EffectDuration = Medium.duration;
            trackerTrackCorpsesButton.EffectDuration = Tracker.corpsesTrackingDuration;
            witchSpellButton.EffectDuration = Witch.spellCastingDuration;
            securityGuardCamButton.EffectDuration = SecurityGuard.duration;
            ninjaButton.EffectDuration = Ninja.stealthDuration;
            sprintButton.EffectDuration = Sprinter.sprintDuration;
            kataomoiButton.EffectDuration = Kataomoi.stareDuration;
            kataomoiStalkingButton.EffectDuration = Kataomoi.stalkingDuration;
            kataomoiSearchButton.EffectDuration = Kataomoi.searchDuration;
            bomberAPlantBombButton.EffectDuration = BomberA.duration;
            bomberBPlantBombButton.EffectDuration = BomberA.duration;
            archaeologistDetectButton.EffectDuration = Archaeologist.arrowDuration;
            archaeologistExcavateButton.EffectDuration = Archaeologist.detectDuration;
            //serialKillerButton.EffectDuration = SerialKiller.suicideTimer;
            veteranAlertButton.EffectDuration = Veteran.alertDuration;
            foxStealthButton.EffectDuration = Fox.stealthDuration;
            detectiveButton.EffectDuration = Detective.inspectDuration;
            buskerButton.EffectDuration = Busker.duration;
            hunterLighterButton.EffectDuration = Hunter.lightDuration;
            hunterArrowButton.EffectDuration = Hunter.ArrowDuration;
            huntedShieldButton.EffectDuration = Hunted.shieldDuration;
            //defuseButton.EffectDuration = Bomber.defuseDuration;
            //bomberButton.EffectDuration = Bomber.destructionTime + Bomber.bombActiveAfter;
            // Already set the timer to the max, as the button is enabled during the game and not available at the start
            lightsOutButton.Timer = lightsOutButton.MaxTimer;
            zoomOutButton.MaxTimer = 0f;
        }

        public static void resetTimeMasterButton() {
            timeMasterShieldButton.Timer = timeMasterShieldButton.MaxTimer;
            timeMasterShieldButton.isEffectActive = false;
            timeMasterShieldButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
            SoundEffectsManager.stop("timemasterShield");
        }

        public static void resetHuntedRewindButton() {
            huntedShieldButton.Timer = huntedShieldButton.MaxTimer;
            huntedShieldButton.isEffectActive = false;
            huntedShieldButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
            SoundEffectsManager.stop("timemasterShield");
        }

        private static void addReplacementHandcuffedButton(CustomButton button, Vector3? positionOffset = null, Func<bool> couldUse = null)
        {
            Vector3 positionOffsetValue = positionOffset ?? button.PositionOffset;  // For non custom buttons, we can set these manually.
            positionOffsetValue.z = -0.1f;
            couldUse ??= button.CouldUse;
            CustomButton replacementHandcuffedButton = new(() => { }, () => { return true; }, couldUse, () => { }, Deputy.getHandcuffedButtonSprite(), positionOffsetValue, button.hudManager, button.hotkey,
                true, Deputy.handcuffDuration, () => { }, button.mirror);
            replacementHandcuffedButton.actionButtonGameObject.ForEachChild((Il2CppSystem.Action<GameObject>)((c) => { if (c.name.Equals("HotKeyGuide")) GameObject.Destroy(c); }));
            ButtonEffect.SetMouseActionIcon(replacementHandcuffedButton.actionButtonGameObject, true, ModTranslation.getString("buttonsHandcuffed"), false);
            replacementHandcuffedButton.Timer = replacementHandcuffedButton.EffectDuration;
            replacementHandcuffedButton.actionButton.cooldownTimerText.color = new Color(0F, 0.8F, 0F);
            replacementHandcuffedButton.isEffectActive = true;
            if (deputyHandcuffedButtons.ContainsKey(PlayerControl.LocalPlayer.PlayerId))
                deputyHandcuffedButtons[PlayerControl.LocalPlayer.PlayerId].Add(replacementHandcuffedButton);
            else
                deputyHandcuffedButtons.Add(PlayerControl.LocalPlayer.PlayerId, new List<CustomButton> { replacementHandcuffedButton });
        }
        
        // Disables / Enables all Buttons (except the ones disabled in the Deputy class), and replaces them with new buttons.
        public static void setAllButtonsHandcuffedStatus(bool handcuffed, bool reset = false)
        {
            if (reset) {
                deputyHandcuffedButtons = new Dictionary<byte, List<CustomButton>>();
                return;
            }
            if (handcuffed && !deputyHandcuffedButtons.ContainsKey(PlayerControl.LocalPlayer.PlayerId))
            {
                int maxI = CustomButton.buttons.Count;
                for (int i = 0; i < maxI; i++)
                {
                    try
                    {
                        if (CustomButton.buttons[i].HasButton() && !CustomButton.buttons[i].isSuicide)  // For each custombutton the player has
                        {
                            addReplacementHandcuffedButton(CustomButton.buttons[i]);  // The new buttons are the only non-handcuffed buttons now!
                        }
                        if (!CustomButton.buttons[i].isSuicide) CustomButton.buttons[i].isHandcuffed = true;
                    }
                    catch (NullReferenceException)
                    {
                        System.Console.WriteLine("[WARNING] NullReferenceException from MeetingEndedUpdate().HasButton(), if theres only one warning its fine");  // Note: idk what this is good for, but i copied it from above /gendelo
                    }
                }
                // Non Custom (Vanilla) Buttons. The Originals are disabled / hidden in UpdatePatch.cs already, just need to replace them. Can use any button, as we replace onclick etc anyways.
                // Kill Button if enabled for the Role
                if (FastDestroyableSingleton<HudManager>.Instance.KillButton.isActiveAndEnabled) addReplacementHandcuffedButton(arsonistButton, CustomButton.ButtonPositions.upperRowRight, couldUse: () => { return FastDestroyableSingleton<HudManager>.Instance.KillButton.currentTarget != null; });
                // Vent Button if enabled
                if (PlayerControl.LocalPlayer.roleCanUseVents()) addReplacementHandcuffedButton(arsonistButton, FastDestroyableSingleton<HudManager>.Instance.ImpostorVentButton.transform.localPosition, couldUse: () => { return FastDestroyableSingleton<HudManager>.Instance.ImpostorVentButton.currentTarget != null; });
                // Report Button
                addReplacementHandcuffedButton(arsonistButton, (!PlayerControl.LocalPlayer.Data.Role.IsImpostor) ? new Vector3(-1f, -0.06f, 0): CustomButton.ButtonPositions.lowerRowRight, () => { return FastDestroyableSingleton<HudManager>.Instance.ReportButton.graphic.color == Palette.EnabledColor; });
            }
            else if (!handcuffed && deputyHandcuffedButtons.ContainsKey(PlayerControl.LocalPlayer.PlayerId))  // Reset to original. Disables the replacements, enables the original buttons.
            {
                foreach (CustomButton replacementButton in deputyHandcuffedButtons[PlayerControl.LocalPlayer.PlayerId])
                {
                    replacementButton.HasButton = () => { return false; };
                    replacementButton.Update(); // To make it disappear properly.
                    ButtonEffect.SetMouseActionIcon(replacementButton.actionButtonGameObject, false);
                    CustomButton.buttons.Remove(replacementButton);
                }
                deputyHandcuffedButtons.Remove(PlayerControl.LocalPlayer.PlayerId);

                foreach (CustomButton button in CustomButton.buttons)
                {
                    button.isHandcuffed = false;
                }
            }
        }

        private static void setButtonTargetDisplay(PlayerControl target, CustomButton button = null, Vector3? offset=null) {
            if (target == null || button == null) {
                if (targetDisplay != null) {  // Reset the poolable player
                    targetDisplay.gameObject.SetActive(false);
                    GameObject.Destroy(targetDisplay.gameObject);
                    targetDisplay = null;
                }
                return;
            }
            // Add poolable player to the button so that the target outfit is shown
            button.actionButton.cooldownTimerText.transform.localPosition = new Vector3(0, 0, -1f);  // Before the poolable player
            targetDisplay = UnityEngine.Object.Instantiate<PoolablePlayer>(Patches.IntroCutsceneOnDestroyPatch.playerPrefab, button.actionButton.transform);
            NetworkedPlayerInfo data = target.Data;
            target.SetPlayerMaterialColors(targetDisplay.cosmetics.currentBodySprite.BodySprite);
            targetDisplay.SetSkin(data.DefaultOutfit.SkinId, data.DefaultOutfit.ColorId);
            targetDisplay.SetHat(data.DefaultOutfit.HatId, data.DefaultOutfit.ColorId);
            targetDisplay.cosmetics.nameText.text = "";  // Hide the name!
            targetDisplay.transform.localPosition = new Vector3(0f, 0.22f, -0.01f);
            if (offset != null) targetDisplay.transform.localPosition += (Vector3)offset;
            targetDisplay.transform.localScale = Vector3.one * 0.33f;
            targetDisplay.setSemiTransparent(false);
            targetDisplay.gameObject.SetActive(true);
        }

        /*public static void createRoleSummaryButton(HudManager __instance)
        {
            roleSummaryButton = new CustomButton(
            () => {
                if (LobbyRoleInfo.RolesSummaryUI == null)
                {
                    LobbyRoleInfo.RoleSummaryOnClick();
                }
                else
                {
                    UnityEngine.Object.Destroy(LobbyRoleInfo.RolesSummaryUI);
                    LobbyRoleInfo.RolesSummaryUI = null;
                }
            },
            () => { return PlayerControl.LocalPlayer != null && LobbyBehaviour.Instance; },
            () => {
                if (PlayerCustomizationMenu.Instance || GameSettingMenu.Instance)
                {
                    if (LobbyRoleInfo.RolesSummaryUI != null)
                    {
                        UnityEngine.Object.Destroy(LobbyRoleInfo.RolesSummaryUI);
                    }
                }
                return true;
            },
            () => { },
            Helpers.loadSpriteFromResources("TheOtherRoles.Resources.HelpButton.png", 150f),
            new Vector3(-1.075f, 0.0f, 0.0f),
            __instance,
            null
            );
        }*/

        public static void Postfix(HudManager __instance) {
            initialized = false;

            try {
                createButtonsPostfix(__instance);
            } catch { }
        }
         
        public static void createButtonsPostfix(HudManager __instance) {
            // get map id, or raise error to wait...
            var mapId = GameOptionsManager.Instance.currentNormalGameOptions.MapId;

            // Engineer Repair
            engineerRepairButton = new CustomButton(
                () => {
                    engineerRepairButton.Timer = 0f;
                    _ = new StaticAchievementToken("engineer.common1");
                    MessageWriter usedRepairWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.EngineerUsedRepair, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(usedRepairWriter);
                    RPCProcedure.engineerUsedRepair();
                    SoundEffectsManager.play("engineerRepair");
                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks.GetFastEnumerator()) {
                        if (task.TaskType == TaskTypes.FixLights) {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.EngineerFixLights, Hazel.SendOption.Reliable, -1);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.engineerFixLights();
                        } else if (task.TaskType == TaskTypes.RestoreOxy) {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.LifeSupp, 0 | 64);
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.LifeSupp, 1 | 64);
                        } else if (task.TaskType == TaskTypes.ResetReactor) {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Reactor, 16);
                        } else if (task.TaskType == TaskTypes.ResetSeismic) {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Laboratory, 16);
                        } else if (task.TaskType == TaskTypes.FixComms) {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Comms, 16 | 0);
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Comms, 16 | 1);
                        } else if (task.TaskType == TaskTypes.StopCharles) {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Reactor, 0 | 16);
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Reactor, 1 | 16);
                        } else if (SubmergedCompatibility.IsSubmerged && task.TaskType == SubmergedCompatibility.RetrieveOxygenMask) {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.EngineerFixSubmergedOxygen, Hazel.SendOption.Reliable, -1);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.engineerFixSubmergedOxygen();
                        }

                    }
                },
                () => { return Engineer.engineer != null && Engineer.engineer == PlayerControl.LocalPlayer && Engineer.remainingFixes > 0 && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (engineerRepairText != null) engineerRepairText.text = Engineer.remainingFixes.ToString();
                    bool sabotageActive = false;
                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks.GetFastEnumerator())
                        if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles
                            || (SubmergedCompatibility.IsSubmerged && task.TaskType == SubmergedCompatibility.RetrieveOxygenMask))
                            sabotageActive = true;
                    return sabotageActive && Engineer.remainingFixes > 0 && PlayerControl.LocalPlayer.CanMove;
                },
                () => {},
                Engineer.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("RepairText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            engineerRepairText = engineerRepairButton.ShowUsesIcon(3);

            // Janitor Clean
            janitorCleanButton = new CustomButton(
                () => {
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer.MaxReportDistance, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody")
                        {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component && !component.Reported)
                            {
                                Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                Vector2 truePosition2 = component.TruePosition;
                                if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false))
                                {
                                    NetworkedPlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);

                                    _ = new StaticAchievementToken("janitor.common1");
                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CleanBody, Hazel.SendOption.Reliable, -1);
                                    writer.Write(playerInfo.PlayerId);
                                    writer.Write(Janitor.janitor.PlayerId);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.cleanBody(playerInfo.PlayerId, Janitor.janitor.PlayerId);
                                    janitorCleanButton.Timer = janitorCleanButton.MaxTimer;
                                    SoundEffectsManager.play("cleanerClean");

                                    break;
                                }
                            }
                        }
                    }
                },
                () => { return Janitor.janitor != null && Janitor.janitor == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return __instance.ReportButton.graphic.color == Palette.EnabledColor && PlayerControl.LocalPlayer.CanMove; },
                () => { janitorCleanButton.Timer = janitorCleanButton.MaxTimer; },
                Janitor.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("CleanText")
            );

            // Sheriff Kill
            sheriffKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMuderAttempt(Sheriff.sheriff, Sheriff.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.SuppressKill) return;

                    if (murderAttemptResult is MurderAttemptResult.PerformKill or MurderAttemptResult.ReverseKill) {
                        byte targetId = 0;
                        if (((Sheriff.currentTarget.Data.Role.IsImpostor && (Sheriff.currentTarget != Mini.mini || Mini.isGrownUp())) ||
                            (Sheriff.spyCanDieToSheriff && Spy.spy == Sheriff.currentTarget) ||
                            (Sheriff.canKillNeutrals && Helpers.isNeutral(Sheriff.currentTarget)) ||
                            Jackal.jackal == Sheriff.currentTarget || Sidekick.sidekick == Sheriff.currentTarget ||
                            (CreatedMadmate.createdMadmate == Sheriff.currentTarget && CreatedMadmate.canDieToSheriff) ||
                            (Madmate.canDieToSheriff && Madmate.madmate.Any(x => x.PlayerId == Sheriff.currentTarget.PlayerId))) &&
                            !Madmate.madmate.Any(y => y.PlayerId == Sheriff.sheriff.PlayerId)) 
                        {
                            _ = new StaticAchievementToken("sheriff.common1");
                            targetId = Sheriff.currentTarget.PlayerId;
                        }
                        else {
                            _= new StaticAchievementToken("sheriff.another1");
                            targetId = PlayerControl.LocalPlayer.PlayerId;
                        }

                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(Sheriff.sheriff.Data.PlayerId);
                        killWriter.Write(targetId);
                        killWriter.Write(byte.MaxValue);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.uncheckedMurderPlayer(Sheriff.sheriff.Data.PlayerId, targetId, Byte.MaxValue);
                    }

                    sheriffKillButton.Timer = sheriffKillButton.MaxTimer;
                    Sheriff.currentTarget = null;
                },
                () => { return Sheriff.sheriff != null && Sheriff.sheriff == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Sheriff.currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => { sheriffKillButton.Timer = sheriffKillButton.MaxTimer;},
                __instance.KillButton.graphic.sprite,
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.Q
            );

            // Deputy Handcuff
            deputyHandcuffButton = new CustomButton(
                () => {
                    byte targetId = 0;
                    targetId = Sheriff.sheriff == PlayerControl.LocalPlayer ? Sheriff.currentTarget.PlayerId : Deputy.currentTarget.PlayerId;  // If the deputy is now the sheriff, sheriffs target, else deputies target

                    if (Helpers.isEvil(Helpers.playerById(targetId)))
                        _ = new StaticAchievementToken("deputy.common1");

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.DeputyUsedHandcuffs, Hazel.SendOption.Reliable, -1);
                    writer.Write(targetId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.deputyUsedHandcuffs(targetId);
                    Deputy.currentTarget = null;
                    deputyHandcuffButton.Timer = deputyHandcuffButton.MaxTimer;

                    SoundEffectsManager.play("deputyHandcuff");
                },
                () => { return ((Deputy.deputy != null && Deputy.deputy == PlayerControl.LocalPlayer) || (Sheriff.sheriff != null && Sheriff.sheriff == PlayerControl.LocalPlayer && Sheriff.sheriff == Sheriff.formerDeputy && Deputy.keepsHandcuffsOnPromotion)) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (deputyButtonHandcuffsText != null) deputyButtonHandcuffsText.text = $"{Deputy.remainingHandcuffs}";
                    return ((Deputy.deputy != null && Deputy.deputy == PlayerControl.LocalPlayer && Deputy.currentTarget) || (Sheriff.sheriff != null && Sheriff.sheriff == PlayerControl.LocalPlayer && Sheriff.sheriff == Sheriff.formerDeputy && Sheriff.currentTarget)) && Deputy.remainingHandcuffs > 0 && PlayerControl.LocalPlayer.CanMove;
                },
                () => { deputyHandcuffButton.Timer = deputyHandcuffButton.MaxTimer; },
                Deputy.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("HandcuffText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            // Deputy Handcuff button handcuff counter
            deputyButtonHandcuffsText = deputyHandcuffButton.ShowUsesIcon(3);

            jackalAndSidekickSabotageLightsButton = new CustomButton(
                () => {
                    ShipStatus.Instance.RpcUpdateSystem(SystemTypes.Sabotage, (byte)SystemTypes.Electrical);
                },
                () => {
                    return ((Jackal.jackal != null && Jackal.jackal == PlayerControl.LocalPlayer && Jackal.canSabotageLights) ||
                            (Sidekick.sidekick != null && Sidekick.sidekick == PlayerControl.LocalPlayer && Sidekick.canSabotageLights)) && !PlayerControl.LocalPlayer.Data.IsDead
                             && (!Helpers.isFungle() || CustomOptionHolder.fungleElectrical.getBool());
                },
                () => {
                    if (Helpers.sabotageTimer() > jackalAndSidekickSabotageLightsButton.Timer || Helpers.sabotageActive())
                        jackalAndSidekickSabotageLightsButton.Timer = Helpers.sabotageTimer() + 5f;  // this will give imps time to do another sabotage.
                    return Helpers.canUseSabotage();
                },
                () => {
                    jackalAndSidekickSabotageLightsButton.Timer = Helpers.sabotageTimer() + 5f;
                },
                Trickster.getLightsOutButtonSprite(),
                CustomButton.ButtonPositions.upperRowCenter,
                __instance,
                KeyCode.G,
                buttonText: ModTranslation.getString("LightsOutText")
            );

            // Time Master Rewind Time
            timeMasterShieldButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.TimeMasterShield, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.timeMasterShield();
                    SoundEffectsManager.play("timemasterShield");
                },
                () => { return TimeMaster.timeMaster != null && TimeMaster.timeMaster == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove; },
                () => {
                    timeMasterShieldButton.Timer = timeMasterShieldButton.MaxTimer;
                    timeMasterShieldButton.isEffectActive = false;
                    timeMasterShieldButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                TimeMaster.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F, 
                true,
                TimeMaster.shieldDuration,
                () => {
                    timeMasterShieldButton.Timer = timeMasterShieldButton.MaxTimer;
                    SoundEffectsManager.stop("timemasterShield");

                },
                buttonText: ModTranslation.getString("TimeShieldText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            // Medic Shield
            medicShieldButton = new CustomButton(
                () => {
                    medicShieldButton.Timer = 0f;

                    if (!Helpers.isEvil(Medic.currentTarget))
                        _ = new StaticAchievementToken("medic.common1");

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, Medic.setShieldAfterMeeting ? (byte)CustomRPC.SetFutureShielded : (byte)CustomRPC.MedicSetShielded, Hazel.SendOption.Reliable, -1);
                    writer.Write(Medic.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    if (Medic.setShieldAfterMeeting)
                        RPCProcedure.setFutureShielded(Medic.currentTarget.PlayerId);
                    else
                        RPCProcedure.medicSetShielded(Medic.currentTarget.PlayerId);
                    Medic.meetingAfterShielding = false;

                    SoundEffectsManager.play("medicShield");
                    },
                () => { return Medic.medic != null && Medic.medic == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return !Medic.usedShield && Medic.currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => {},
                Medic.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("ShieldText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );


            // Shifter shift
            shifterShiftButton = new CustomButton(
                () => {
                    if (Helpers.checkSuspendAction(Shifter.shifter, Shifter.currentTarget)) return;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetFutureShifted, Hazel.SendOption.Reliable, -1);
                    writer.Write(Shifter.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.setFutureShifted(Shifter.currentTarget.PlayerId);
                    SoundEffectsManager.play("shifterShift");
                },
                () => { return Shifter.shifter != null && Shifter.shifter == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Shifter.currentTarget && Shifter.futureShift == null && PlayerControl.LocalPlayer.CanMove; },
                () => { },
                Shifter.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("ShiftText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            // Morphling morph

            morphlingButton = new CustomButton(
                () => {
                    if (Morphling.sampledTarget != null) {
                         _ = new StaticAchievementToken("morphling.common1");
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MorphlingMorph, Hazel.SendOption.Reliable, -1);
                        writer.Write(Morphling.sampledTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.morphlingMorph(Morphling.sampledTarget.PlayerId);
                        Morphling.sampledTarget = null;
                        morphlingButton.EffectDuration = Morphling.duration;
                        morphlingButton.shakeOnEnd = true;
                        SoundEffectsManager.play("morphlingMorph");
                    } else if (Morphling.currentTarget != null) {
                        if (Helpers.checkSuspendAction(Morphling.morphling, Morphling.currentTarget)) return;
                        Morphling.acTokenChallenge.Value.playerId = Morphling.currentTarget.PlayerId;
                        Morphling.acTokenChallenge.Value.kill = false;
                        Morphling.sampledTarget = Morphling.currentTarget;
                        morphlingButton.Sprite = Morphling.getMorphSprite();
                        morphlingButton.buttonText = ModTranslation.getString("MorphText");
                        morphlingButton.shakeOnEnd = false;
                        morphlingButton.EffectDuration = 1f;
                        SoundEffectsManager.play("morphlingSample");

                        // Add poolable player to the button so that the target outfit is shown
                        setButtonTargetDisplay(Morphling.sampledTarget, morphlingButton);
                    }
                },
                () => { return Morphling.morphling != null && Morphling.morphling == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return (Morphling.currentTarget || Morphling.sampledTarget) && PlayerControl.LocalPlayer.CanMove && !Helpers.MushroomSabotageActive(); },
                () => { 
                    morphlingButton.Timer = morphlingButton.MaxTimer;
                    morphlingButton.Sprite = Morphling.getSampleSprite();
                    morphlingButton.buttonText = ModTranslation.getString("SampleText");
                    morphlingButton.isEffectActive = false;
                    morphlingButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    Morphling.sampledTarget = null;
                    setButtonTargetDisplay(null);
                },
                Morphling.getSampleSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                true,
                Morphling.duration,
                () => {
                    if (Morphling.sampledTarget == null) {
                        morphlingButton.Timer = morphlingButton.MaxTimer;
                        morphlingButton.Sprite = Morphling.getSampleSprite();
                        morphlingButton.buttonText = ModTranslation.getString("SampleText");
                        SoundEffectsManager.play("morphlingMorph");

                        // Reset the poolable player
                        setButtonTargetDisplay(null);
                    }
                },
                buttonText: ModTranslation.getString("SampleText"),
                actionName: ModTranslation.getString("morphlingButton")
            );

            // Camouflager camouflage
            camouflagerButton = new CustomButton(
                () => {
                    _ = new StaticAchievementToken("camouflager.common1");
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CamouflagerCamouflage, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.camouflagerCamouflage();
                    SoundEffectsManager.play("morphlingMorph");
                },
                () => { return Camouflager.camouflager != null && Camouflager.camouflager == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove; },
                () => {
                    camouflagerButton.Timer = camouflagerButton.MaxTimer;
                    camouflagerButton.isEffectActive = false;
                    camouflagerButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Camouflager.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                true,
                Camouflager.duration,
                () => {
                    camouflagerButton.Timer = camouflagerButton.MaxTimer;
                    SoundEffectsManager.play("morphlingMorph");
                },
                buttonText: ModTranslation.getString("CamoText")
            );

            // Hacker button
            hackerButton = new CustomButton(
                () => {
                    Hacker.hackerTimer = Hacker.duration;
                    SoundEffectsManager.play("hackerHack");
                },
                () => { return Hacker.hacker != null && Hacker.hacker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return true; },
                () => {
                    hackerButton.Timer = hackerButton.MaxTimer;
                    hackerButton.isEffectActive = false;
                    hackerButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Hacker.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.F,
                true,
                0f,
                () => { hackerButton.Timer = hackerButton.MaxTimer;},
                buttonText: ModTranslation.getString("HackerText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            hackerAdminTableButton = new CustomButton(
               () => {
                   _ = new StaticAchievementToken("hacker.common1");
                   if (!MapBehaviour.Instance || !MapBehaviour.Instance.isActiveAndEnabled) {
                       HudManager __instance = FastDestroyableSingleton<HudManager>.Instance;
                       __instance.InitMap();
                       MapBehaviour.Instance.ShowCountOverlay(allowedToMove: true, showLivePlayerPosition: true, includeDeadBodies: true);
                   }
                   if (Hacker.cantMove) PlayerControl.LocalPlayer.moveable = false;
                   PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement 
                   Hacker.chargesAdminTable--;
               },
               () => { return Hacker.hacker != null && Hacker.hacker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;},
               () => {
                   if (hackerAdminTableChargesText != null) hackerAdminTableChargesText.text = $"{Hacker.chargesAdminTable} / {Hacker.toolsNumber}";
                   return Hacker.chargesAdminTable > 0; 
               },
               () => {
                   hackerAdminTableButton.Timer = hackerAdminTableButton.MaxTimer;
                   hackerAdminTableButton.isEffectActive = false;
                   hackerAdminTableButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
               },
               Hacker.getAdminSprite(),
               CustomButton.ButtonPositions.lowerRowRight,
               __instance,
               KeyCode.Q,
               true,
               0f,
               () => { 
                   hackerAdminTableButton.Timer = hackerAdminTableButton.MaxTimer;
                   if (!hackerVitalsButton.isEffectActive) PlayerControl.LocalPlayer.moveable = true;
                   if (MapBehaviour.Instance && MapBehaviour.Instance.isActiveAndEnabled) MapBehaviour.Instance.Close();
               },
               GameOptionsManager.Instance.currentNormalGameOptions.MapId == 3,
               FastDestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Admin),
               abilityTexture: CustomButton.ButtonLabelType.AdminButton
           );

            // Hacker Admin Table Charges
            hackerAdminTableChargesText = GameObject.Instantiate(hackerAdminTableButton.actionButton.cooldownTimerText, hackerAdminTableButton.actionButton.cooldownTimerText.transform.parent);
            hackerAdminTableChargesText.text = "";
            hackerAdminTableChargesText.enableWordWrapping = false;
            hackerAdminTableChargesText.transform.localScale = Vector3.one * 0.5f;
            hackerAdminTableChargesText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            hackerVitalsButton = new CustomButton(
               () => {
                   if (GameOptionsManager.Instance.currentNormalGameOptions.MapId != 1) {
                       _ = new StaticAchievementToken("hacker.common2");
                       if (Hacker.vitals == null) {
                           var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("panel_vitals") || x.gameObject.name.Contains("Vitals"));
                           if (e == null || Camera.main == null) return;
                           Hacker.vitals = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                       }
                       Hacker.vitals.transform.SetParent(Camera.main.transform, false);
                       Hacker.vitals.transform.localPosition = new Vector3(0.0f, 0.0f, -50f);
                       Hacker.vitals.Begin(null);
                   } else {
                       if (Hacker.doorLog == null) {
                           var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("SurvLogConsole"));
                           if (e == null || Camera.main == null) return;
                           Hacker.doorLog = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                       }
                       Hacker.doorLog.transform.SetParent(Camera.main.transform, false);
                       Hacker.doorLog.transform.localPosition = new Vector3(0.0f, 0.0f, -50f);
                       Hacker.doorLog.Begin(null);
                   }

                   if (Hacker.cantMove) PlayerControl.LocalPlayer.moveable = false;
                   PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement 

                   Hacker.chargesVitals--;
               },
               () => { return Hacker.hacker != null && Hacker.hacker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && GameOptionsManager.Instance.currentGameOptions.MapId != 0 && GameOptionsManager.Instance.currentNormalGameOptions.MapId != 3; },
               () => {
                   if (hackerVitalsChargesText != null) hackerVitalsChargesText.text = $"{Hacker.chargesVitals} / {Hacker.toolsNumber}";
                   hackerVitalsButton.actionButton.graphic.sprite = Helpers.isMira() ? Hacker.getLogSprite() : Hacker.getVitalsSprite();
                   hackerVitalsButton.actionButton.OverrideText(Helpers.isMira() ?
                       TranslationController.Instance.GetString(StringNames.DoorlogLabel) :
                       TranslationController.Instance.GetString(StringNames.VitalsLabel));
                   return Hacker.chargesVitals > 0;
               },
               () => {
                   hackerVitalsButton.Timer = hackerVitalsButton.MaxTimer;
                   hackerVitalsButton.isEffectActive = false;
                   hackerVitalsButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
               },
               Hacker.getVitalsSprite(),
               CustomButton.ButtonPositions.lowerRowCenter,
               __instance,
               KeyCode.Q,
               true,
               0f,
               () => { 
                   hackerVitalsButton.Timer = hackerVitalsButton.MaxTimer;
                   if(!hackerAdminTableButton.isEffectActive) PlayerControl.LocalPlayer.moveable = true;
                   if (Minigame.Instance) {
                       if (Helpers.isMira()) Hacker.doorLog.ForceClose();
                       else Hacker.vitals.ForceClose();
                   }
               },
               false,
              Helpers.isMira() ?
              TranslationController.Instance.GetString(StringNames.DoorlogLabel) :
              TranslationController.Instance.GetString(StringNames.VitalsLabel),
              abilityTexture: CustomButton.ButtonLabelType.AdminButton
           );

            // Hacker Vitals Charges
            hackerVitalsChargesText = GameObject.Instantiate(hackerVitalsButton.actionButton.cooldownTimerText, hackerVitalsButton.actionButton.cooldownTimerText.transform.parent);
            hackerVitalsChargesText.text = "";
            hackerVitalsChargesText.enableWordWrapping = false;
            hackerVitalsChargesText.transform.localScale = Vector3.one * 0.5f;
            hackerVitalsChargesText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            // Tracker button
            trackerTrackPlayerButton = new CustomButton(
                () => {
                    if (Tracker.currentTarget.Data.Role.IsImpostor)
                        _ = new StaticAchievementToken("tracker.common1");

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.TrackerUsedTracker, Hazel.SendOption.Reliable, -1);
                    writer.Write(Tracker.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.trackerUsedTracker(Tracker.currentTarget.PlayerId);
                    SoundEffectsManager.play("trackerTrackPlayer");
                },
                () => { return Tracker.tracker != null && Tracker.tracker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && Tracker.currentTarget != null && !Tracker.usedTracker; },
                () => { if(Tracker.resetTargetAfterMeeting) Tracker.resetTracked(); },
                Tracker.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("TrackerText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            trackerTrackCorpsesButton = new CustomButton(
                () => { Tracker.corpsesTrackingTimer = Tracker.corpsesTrackingDuration;
                            SoundEffectsManager.play("trackerTrackCorpses"); },
                () => { return Tracker.tracker != null && Tracker.tracker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Tracker.canTrackCorpses; },
                () => { return PlayerControl.LocalPlayer.CanMove; },
                () => {
                    trackerTrackCorpsesButton.Timer = trackerTrackCorpsesButton.MaxTimer;
                    trackerTrackCorpsesButton.isEffectActive = false;
                    trackerTrackCorpsesButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Tracker.getTrackCorpsesButtonSprite(),
                CustomButton.ButtonPositions.lowerRowCenter,
                __instance,
                KeyCode.G,
                true,
                Tracker.corpsesTrackingDuration,
                () => {
                    trackerTrackCorpsesButton.Timer = trackerTrackCorpsesButton.MaxTimer;
                },
                buttonText: ModTranslation.getString("PathfindText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            detectiveButton = new CustomButton(
                () =>
                {
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer.MaxReportDistance, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody") {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component && !component.Reported) {
                                Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                Vector2 truePosition2 = component.TruePosition;
                                if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false)) {
                                    NetworkedPlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);
                                    var dp = GameHistory.deadPlayers?.Where(x => x.player?.PlayerId == playerInfo.PlayerId)?.FirstOrDefault();
                                    if (dp != null && dp.killerIfExisting != null)
                                        TORGUIManager.Instance.StartCoroutine(Helpers.CoAnimIcon(dp.killerIfExisting).WrapToIl2Cpp());
                                    Detective.inspectCooldown = detectiveButton.Timer = detectiveButton.MaxTimer;
                                    break;
                                }
                            }
                        }
                    }
                },
                () => { return Detective.detective && PlayerControl.LocalPlayer == Detective.detective && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && __instance.ReportButton.graphic.color == Palette.EnabledColor; },
                () =>
                {
                    detectiveButton.Timer = detectiveButton.MaxTimer;
                    detectiveButton.isEffectActive = false;
                    detectiveButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Detective.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                true,
                Detective.inspectDuration,
                () => { detectiveButton.Timer = detectiveButton.MaxTimer; },
                buttonText: ModTranslation.getString("InspectText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
    
            vampireKillButton = new CustomButton(
                () => {
                    if (Helpers.checkSuspendAction(Vampire.vampire, Vampire.currentTarget)) return;
                    MurderAttemptResult murder = Helpers.checkMuderAttempt(Vampire.vampire, Vampire.currentTarget);
                    if (murder == MurderAttemptResult.PerformKill) {
                        if (Vampire.targetNearGarlic) {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                            writer.Write(Vampire.vampire.PlayerId);
                            writer.Write(Vampire.currentTarget.PlayerId);
                            writer.Write(Byte.MaxValue);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.uncheckedMurderPlayer(Vampire.vampire.PlayerId, Vampire.currentTarget.PlayerId, Byte.MaxValue);

                            vampireKillButton.HasEffect = false; // Block effect on this click
                            vampireKillButton.Timer = vampireKillButton.MaxTimer;
                        } else {
                            _ = new StaticAchievementToken("vampire.common1");
                            Vampire.bitten = Vampire.currentTarget;
                            // Notify players about bitten
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.VampireSetBitten, Hazel.SendOption.Reliable, -1);
                            writer.Write(Vampire.bitten.PlayerId);
                            writer.Write((byte)0);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.vampireSetBitten(Vampire.bitten.PlayerId, 0);

                            byte lastTimer = (byte)Vampire.delay;
                            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Vampire.delay, new Action<float>((p) => { // Delayed action
                                if (p <= 1f) {
                                    byte timer = (byte)vampireKillButton.Timer;
                                    if (timer != lastTimer) {
                                        lastTimer = timer;
                                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareGhostInfo, Hazel.SendOption.Reliable, -1);
                                        writer.Write(PlayerControl.LocalPlayer.PlayerId);
                                        writer.Write((byte)RPCProcedure.GhostInfoTypes.VampireTimer);
                                        writer.Write(timer);
                                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    }
                                }
                                if (p == 1f) {
                                    // Perform kill if possible and reset bitten (regardless whether the kill was successful or not)
                                    var res = Helpers.checkMurderAttemptAndKill(Vampire.vampire, Vampire.bitten, showAnimation: false);
                                    if (res == MurderAttemptResult.PerformKill)
                                    {
                                        Vampire.acTokenChallenge.Value.deathTime = DateTime.UtcNow;
                                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.VampireSetBitten, Hazel.SendOption.Reliable, -1);
                                        writer.Write(byte.MaxValue);
                                        writer.Write(byte.MaxValue);
                                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                                        RPCProcedure.vampireSetBitten(byte.MaxValue, byte.MaxValue);
                                    }
                                }
                            })));
                            SoundEffectsManager.play("vampireBite");

                            vampireKillButton.HasEffect = true; // Trigger effect on this click
                        }
                    } else if (murder == MurderAttemptResult.BlankKill) {
                        vampireKillButton.Timer = vampireKillButton.MaxTimer;
                        vampireKillButton.HasEffect = false;
                    } else {
                        vampireKillButton.HasEffect = false;
                    }
                },
                () => { return Vampire.vampire != null && Vampire.vampire == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (Vampire.targetNearGarlic && Vampire.canKillNearGarlics) {
                        vampireKillButton.actionButton.graphic.sprite = __instance.KillButton.graphic.sprite;
                        vampireKillButton.showButtonText = true;
                        vampireKillButton.buttonText = TranslationController.Instance.GetString(StringNames.KillLabel);
                    }
                    else {
                        vampireKillButton.actionButton.graphic.sprite = Vampire.getButtonSprite();
                        vampireKillButton.showButtonText = ModTranslation.getString("VampireText") != "";
                        vampireKillButton.buttonText = ModTranslation.getString("VampireText");
                    }
                    return Vampire.currentTarget != null && PlayerControl.LocalPlayer.CanMove && (!Vampire.targetNearGarlic || Vampire.canKillNearGarlics);
                },
                () => {
                    vampireKillButton.Timer = vampireKillButton.MaxTimer;
                    vampireKillButton.isEffectActive = false;
                    vampireKillButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Vampire.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.Q,
                false,
                0f,
                () => {
                    vampireKillButton.Timer = vampireKillButton.MaxTimer;
                },
                buttonText: ModTranslation.getString("VampireText")
            );

            garlicButton = new CustomButton(
                () => {
                    Vampire.localPlacedGarlic = true;
                    var pos = PlayerControl.LocalPlayer.transform.position;
                    byte[] buff = new byte[sizeof(float) * 2];
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0*sizeof(float), sizeof(float));
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1*sizeof(float), sizeof(float));

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceGarlic, Hazel.SendOption.Reliable);
                    writer.WriteBytesAndSize(buff);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.placeGarlic(buff);
                    SoundEffectsManager.play("garlic");
                },
                () => { return !Vampire.localPlacedGarlic && !PlayerControl.LocalPlayer.Data.IsDead && Vampire.garlicsActive && !HideNSeek.isHideNSeekGM; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Vampire.localPlacedGarlic; },
                () => { },
                Vampire.getGarlicButtonSprite(),
                new Vector3(0, -0.06f, 0),
                __instance,
                null,
                true,
                ModTranslation.getString("GarlicText"),
                CustomButton.ButtonLabelType.UseButton
            );

            portalmakerPlacePortalButton = new CustomButton(
                () => {
                    portalmakerPlacePortalButton.Timer = portalmakerPlacePortalButton.MaxTimer;

                    var pos = PlayerControl.LocalPlayer.transform.position;
                    byte[] buff = new byte[sizeof(float) * 2];
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlacePortal, Hazel.SendOption.Reliable);
                    writer.WriteBytesAndSize(buff);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.placePortal(buff);
                    SoundEffectsManager.play("tricksterPlaceBox");
                },
                () => { return Portalmaker.portalmaker != null && Portalmaker.portalmaker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Portal.secondPortal == null; },
                () =>
                {
                    if (portalmakerButtonNumText != null) portalmakerButtonNumText.text = $"{(Portal.firstPortal == null ? 2 : 1)}";
                    return PlayerControl.LocalPlayer.CanMove && Portal.secondPortal == null;
                },
                () => { portalmakerPlacePortalButton.Timer = portalmakerPlacePortalButton.MaxTimer; },
                Portalmaker.getPlacePortalButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("PlacePortalText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            portalmakerButtonNumText = portalmakerPlacePortalButton.ShowUsesIcon(3);

            usePortalButton = new CustomButton(
                () => {
                    bool didTeleport = false;
                    Vector3 exit = Portal.findExit(PlayerControl.LocalPlayer.transform.position).portalGameObject.transform.position;
                    Vector3 entry = Portal.findEntry(PlayerControl.LocalPlayer.transform.position);

                    bool portalMakerSoloTeleport = !Portal.locationNearEntry(PlayerControl.LocalPlayer.transform.position);
                    if (portalMakerSoloTeleport) {
                        _ = new StaticAchievementToken("portalmaker.another1");
                        exit = Portal.firstPortal.portalGameObject.transform.position;
                        entry = PlayerControl.LocalPlayer.transform.position;
                    }

                    PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(entry);

                    if (!PlayerControl.LocalPlayer.Data.IsDead) {  // Ghosts can portal too, but non-blocking and only with a local animation
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UsePortal, Hazel.SendOption.Reliable, -1);
                        writer.Write((byte)PlayerControl.LocalPlayer.PlayerId);
                        writer.Write(portalMakerSoloTeleport ? (byte)1 : (byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                    }
                    RPCProcedure.usePortal(PlayerControl.LocalPlayer.PlayerId, portalMakerSoloTeleport ? (byte)1 : (byte)0);
                    usePortalButton.Timer = usePortalButton.MaxTimer;
                    portalmakerMoveToPortalButton.Timer = usePortalButton.MaxTimer;
                    SoundEffectsManager.play("portalUse");
                    FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Portal.teleportDuration, new Action<float>((p) => { // Delayed action
                        PlayerControl.LocalPlayer.moveable = false;
                        PlayerControl.LocalPlayer.NetTransform.Halt();
                        if (p >= 0.5f && p <= 0.53f && !didTeleport && !MeetingHud.Instance) {
                            if (SubmergedCompatibility.IsSubmerged) {
                                SubmergedCompatibility.ChangeFloor(exit.y > -7);
                            }
                            PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(exit);
                            didTeleport = true;
                        }
                        if (p == 1f) {
                            PlayerControl.LocalPlayer.moveable = true;
                        }
                    })));
                    },
                () => {
                    if (PlayerControl.LocalPlayer == Portalmaker.portalmaker && Portal.bothPlacedAndEnabled)
                        portalmakerButtonText1.text = Portal.locationNearEntry(PlayerControl.LocalPlayer.transform.position) || !Portalmaker.canPortalFromAnywhere ? "" : "1. " + Portal.firstPortal.room;
                    return Portal.bothPlacedAndEnabled; },
                () => { return PlayerControl.LocalPlayer.CanMove && (Portal.locationNearEntry(PlayerControl.LocalPlayer.transform.position) || (Portalmaker.canPortalFromAnywhere && PlayerControl.LocalPlayer == Portalmaker.portalmaker)) && !Portal.isTeleporting; },
                () => { usePortalButton.Timer = usePortalButton.MaxTimer; },
                Portalmaker.getUsePortalButtonSprite(),
                new Vector3(0.9f, -0.06f, 0),
                __instance,
                KeyCode.J,
                mirror: true,
                ModTranslation.getString("PortalTeleportText"),
                CustomButton.ButtonLabelType.UseButton
            );

            portalmakerMoveToPortalButton = new CustomButton(
                () => {
                    _ = new StaticAchievementToken("portalmaker.another1");

                    bool didTeleport = false;
                    Vector3 exit = Portal.secondPortal.portalGameObject.transform.position;

                    if (!PlayerControl.LocalPlayer.Data.IsDead) {  // Ghosts can portal too, but non-blocking and only with a local animation
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UsePortal, Hazel.SendOption.Reliable, -1);
                        writer.Write((byte)PlayerControl.LocalPlayer.PlayerId);
                        writer.Write((byte)2);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                    }
                    RPCProcedure.usePortal(PlayerControl.LocalPlayer.PlayerId, 2);
                    usePortalButton.Timer = usePortalButton.MaxTimer;
                    portalmakerMoveToPortalButton.Timer = usePortalButton.MaxTimer;
                    SoundEffectsManager.play("portalUse");
                    FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Portal.teleportDuration, new Action<float>((p) => { // Delayed action
                        PlayerControl.LocalPlayer.moveable = false;
                        PlayerControl.LocalPlayer.NetTransform.Halt();
                        if (p >= 0.5f && p <= 0.53f && !didTeleport && !MeetingHud.Instance) {
                            if (SubmergedCompatibility.IsSubmerged) {
                                SubmergedCompatibility.ChangeFloor(exit.y > -7);
                            }
                            PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(exit);
                            didTeleport = true;
                        }
                        if (p == 1f) {
                            PlayerControl.LocalPlayer.moveable = true;
                        }
                    })));
                },
                () => { return Portalmaker.canPortalFromAnywhere && Portal.bothPlacedAndEnabled && PlayerControl.LocalPlayer == Portalmaker.portalmaker; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Portal.locationNearEntry(PlayerControl.LocalPlayer.transform.position) && !Portal.isTeleporting; },
                () => { portalmakerMoveToPortalButton.Timer = usePortalButton.MaxTimer; },
                Portalmaker.getUsePortalButtonSprite(),
                new Vector3(0.9f, 1f, 0),
                __instance,
                KeyCode.G,
                mirror: true,
                ModTranslation.getString("PortalTeleportText"),
                CustomButton.ButtonLabelType.UseButton
            );


            portalmakerButtonText1 = GameObject.Instantiate(usePortalButton.actionButton.cooldownTimerText, usePortalButton.actionButton.cooldownTimerText.transform.parent);
            portalmakerButtonText1.text = "";
            portalmakerButtonText1.enableWordWrapping = false;
            portalmakerButtonText1.transform.localScale = Vector3.one * 0.5f;
            portalmakerButtonText1.transform.localPosition += new Vector3(-0.05f, 0.55f, -1f);

            portalmakerButtonText2 = GameObject.Instantiate(portalmakerMoveToPortalButton.actionButton.cooldownTimerText, portalmakerMoveToPortalButton.actionButton.cooldownTimerText.transform.parent);
            portalmakerButtonText2.text = "";
            portalmakerButtonText2.enableWordWrapping = false;
            portalmakerButtonText2.transform.localScale = Vector3.one * 0.5f;
            portalmakerButtonText2.transform.localPosition += new Vector3(-0.05f, 0.55f, -1f);



            // Jackal Sidekick Button
            jackalSidekickButton = new CustomButton(
                () => {
                    if (Helpers.checkSuspendAction(Jackal.jackal, Jackal.currentTarget)) return;
                    _ = new StaticAchievementToken("jackal.common1");
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.JackalCreatesSidekick, Hazel.SendOption.Reliable, -1);
                    writer.Write(Jackal.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.jackalCreatesSidekick(Jackal.currentTarget.PlayerId);
                    SoundEffectsManager.play("jackalSidekick");
                },
                () => { return Jackal.canCreateSidekick && Jackal.jackal != null && Jackal.jackal == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Jackal.canCreateSidekick && Jackal.currentTarget != null && PlayerControl.LocalPlayer.CanMove; },
                () => { jackalSidekickButton.Timer = jackalSidekickButton.MaxTimer;},
                Jackal.getSidekickButtonSprite(),
                CustomButton.ButtonPositions.lowerRowCenter,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("SidekickText")
            );

            // Jackal Kill
            jackalKillButton = new CustomButton(
                () => {
                    if (Helpers.checkMurderAttemptAndKill(Jackal.jackal, Jackal.currentTarget) == MurderAttemptResult.SuppressKill) return;

                    jackalKillButton.Timer = jackalKillButton.MaxTimer; 
                    Jackal.currentTarget = null;
                },
                () => { return Jackal.jackal != null && Jackal.jackal == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Jackal.currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => { jackalKillButton.Timer = jackalKillButton.MaxTimer;},
                __instance.KillButton.graphic.sprite,
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.Q
            );

            jekyllAndHydeKillButton = new CustomButton(
                // OnClick
                () =>
                {
                    if (Helpers.checkMurderAttemptAndKill(PlayerControl.LocalPlayer, JekyllAndHyde.currentTarget) == MurderAttemptResult.SuppressKill) return;

                    jekyllAndHydeKillButton.Timer = jekyllAndHydeKillButton.MaxTimer;
                    JekyllAndHyde.currentTarget = null;
                },
                // HasButton
                () => { return PlayerControl.LocalPlayer == JekyllAndHyde.jekyllAndHyde && !JekyllAndHyde.isJekyll() && !PlayerControl.LocalPlayer.Data.IsDead; },
                // CouldUse
                () =>
                {
                    if (jekyllAndHydeKillCounterText != null)
                    {
                        jekyllAndHydeKillCounterText.text = $"{JekyllAndHyde.counter}/{JekyllAndHyde.numberToWin}";
                    }
                    return JekyllAndHyde.currentTarget != null && PlayerControl.LocalPlayer.CanMove;
                },
                // OnMeetingEnds
                () =>
                {
                    jekyllAndHydeKillButton.Timer = jekyllAndHydeKillButton.MaxTimer = JekyllAndHyde.cooldown;
                },
                __instance.KillButton.graphic.sprite,
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.Q,
                false
            );
            jekyllAndHydeKillCounterText = GameObject.Instantiate(jekyllAndHydeKillButton.actionButton.cooldownTimerText, jekyllAndHydeKillButton.actionButton.cooldownTimerText.transform.parent);
            jekyllAndHydeKillCounterText.text = "";
            jekyllAndHydeKillCounterText.enableWordWrapping = false;
            jekyllAndHydeKillCounterText.transform.localScale = Vector3.one * 0.5f;
            jekyllAndHydeKillCounterText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            jekyllAndHydeSuicideButton = new CustomButton(
                () => { },
                () => { return PlayerControl.LocalPlayer == JekyllAndHyde.jekyllAndHyde && !JekyllAndHyde.isJekyll() && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return true; },
                () =>
                {
                    if (JekyllAndHyde.reset) jekyllAndHydeSuicideButton.Timer = JekyllAndHyde.suicideTimer;
                },
                SerialKiller.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowCenter,
                __instance,
                KeyCode.None,
                true,
                JekyllAndHyde.suicideTimer,
                () =>
                {
                    byte targetId = PlayerControl.LocalPlayer.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SerialKillerSuicide, Hazel.SendOption.Reliable, -1); killWriter.Write(targetId);
                    killWriter.Write(targetId);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.serialKillerSuicide(targetId);
                },
                buttonText: ModTranslation.getString("serialKillerSuicideText"),
                isSuicide: true,
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            )
            {
                isEffectActive = true
            };
            ButtonEffect.SetMouseActionIcon(jekyllAndHydeSuicideButton.actionButtonGameObject, true, ModTranslation.getString("buttonsNormalSuicide"), false, ButtonEffect.ActionIconType.InfoAction);

            jekyllAndHydeDrugButton = new CustomButton(
                // OnClick
                () =>
                {
                    _ = new StaticAchievementToken("jekyllAndHyde.common1");
                    JekyllAndHyde.oddIsJekyll = !JekyllAndHyde.oddIsJekyll;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetOddIsJekyll, Hazel.SendOption.Reliable, -1);
                    writer.Write(JekyllAndHyde.oddIsJekyll);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    jekyllAndHydeSuicideButton.Timer = jekyllAndHydeSuicideButton.MaxTimer;
                    jekyllAndHydeKillButton.Timer = jekyllAndHydeKillButton.MaxTimer;
                    JekyllAndHyde.numUsed += 1;
                    SoundEffectsManager.play("jekyllAndHydeDrug");
                },
                // HasButton
                () => { return PlayerControl.LocalPlayer == JekyllAndHyde.jekyllAndHyde && !PlayerControl.LocalPlayer.Data.IsDead && JekyllAndHyde.numUsed < JekyllAndHyde.getNumDrugs(); },
                // CouldUse
                () =>
                {
                    if (jekyllAndHydeDrugText != null)
                    {
                        jekyllAndHydeDrugText.text = $"{JekyllAndHyde.numUsed} / {JekyllAndHyde.getNumDrugs()}";
                    }
                    return PlayerControl.LocalPlayer.CanMove;
                },
                // OnMeetingEnds
                () =>
                {
                    jekyllAndHydeDrugButton.Timer = jekyllAndHydeDrugButton.MaxTimer;
                },
                PlagueDoctor.getSyringeIcon(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                false,
                ModTranslation.getString("DrugText"),
                CustomButton.ButtonLabelType.UseButton,
                actionName: ModTranslation.getString("drugButtonSwitch")
            );
            jekyllAndHydeDrugText = GameObject.Instantiate(jekyllAndHydeDrugButton.actionButton.cooldownTimerText, jekyllAndHydeDrugButton.actionButton.cooldownTimerText.transform.parent); 
            jekyllAndHydeDrugText.text = "";
            jekyllAndHydeDrugText.enableWordWrapping = false;
            jekyllAndHydeDrugText.transform.localScale = Vector3.one * 0.5f;
            jekyllAndHydeDrugText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            teleporterTeleportButton = new CustomButton(
                () =>
                {
                    List<byte> transportTargets = new();
                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        if (!player.Data.Disconnected)
                        {
                            if (!player.Data.IsDead) transportTargets.Add(player.PlayerId);
                            else
                            {
                                foreach (var body in UnityEngine.Object.FindObjectsOfType<DeadBody>())
                                {
                                    if (body.ParentId == player.PlayerId) transportTargets.Add(player.PlayerId);
                                }
                            }
                        }
                    }
                    byte[] transporttargetIDs = transportTargets.ToArray();
                    var pk = new Teleporter.PlayerMenu((x) =>
                    {
                        Teleporter.target1 = x;
                        Teleporter.SwappingMenus = true;
                        Coroutines.Start(Teleporter.OpenSecondMenu());
                    }, (y) =>
                    {
                        return transporttargetIDs.Contains(y.PlayerId);
                    });
                    Coroutines.Start(pk.Open(0f, true));
                },
                () => { return Teleporter.teleporter != null && PlayerControl.LocalPlayer == Teleporter.teleporter && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (teleporterNumLeftText != null)
                    {
                        teleporterNumLeftText.text = $"{Teleporter.teleportNumber}";
                    }
                    return Teleporter.teleportNumber > 0 && PlayerControl.LocalPlayer.CanMove && !Teleporter.SwappingMenus;
                },
                () => { teleporterTeleportButton.Timer = teleporterTeleportButton.MaxTimer; },
                Teleporter.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                false,
                ModTranslation.getString("TeleporterTeleportText"),
                CustomButton.ButtonLabelType.UseButton
            );
            teleporterNumLeftText = teleporterTeleportButton.ShowUsesIcon(3);

            plagueDoctorButton = new CustomButton(
                () =>
                {
                    if (Helpers.checkSuspendAction(PlagueDoctor.plagueDoctor, PlagueDoctor.currentTarget)) return;

                    byte targetId = PlagueDoctor.currentTarget.PlayerId;

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlagueDoctorSetInfected, Hazel.SendOption.Reliable, -1);
                    writer.Write(targetId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.plagueDoctorInfected(targetId);
                    PlagueDoctor.numInfections--;
                    _ = new StaticAchievementToken("plagueDoctor.common1");

                    plagueDoctorButton.Timer = plagueDoctorButton.MaxTimer;
                    PlagueDoctor.currentTarget = null;
                    SoundEffectsManager.play("plagueDoctorSyringe");
                },
                () => { return PlagueDoctor.plagueDoctor != null && PlayerControl.LocalPlayer == PlagueDoctor.plagueDoctor && PlagueDoctor.numInfections > 0 && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (plagueDoctornumInfectionsText != null)
                    {
                        if (PlagueDoctor.numInfections > 0)
                            plagueDoctornumInfectionsText.text = $"{PlagueDoctor.numInfections}";
                        else
                            plagueDoctornumInfectionsText.text = "";
                    }
                    return PlagueDoctor.currentTarget != null && PlagueDoctor.numInfections > 0 && PlayerControl.LocalPlayer.CanMove;
                },
                () => { plagueDoctorButton.Timer = plagueDoctorButton.MaxTimer; },
                PlagueDoctor.getSyringeIcon(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("InfectText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            plagueDoctornumInfectionsText = plagueDoctorButton.ShowUsesIcon(2);

            // EvilHacker creates madmate button
            evilHackerCreatesMadmateButton = new CustomButton(
                () =>
                {
                    /*
                     * creates madmate
                     */
                    if (Helpers.checkSuspendAction(EvilHacker.evilHacker, EvilHacker.currentTarget)) return;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.EvilHackerCreatesMadmate, Hazel.SendOption.Reliable, -1);
                    writer.Write(EvilHacker.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.evilHackerCreatesMadmate(EvilHacker.currentTarget.PlayerId);
                    _ = new StaticAchievementToken("evilHacker.common1");

                },
                () =>
                {
                    return EvilHacker.evilHacker != null &&
                      EvilHacker.evilHacker == PlayerControl.LocalPlayer &&
                      EvilHacker.canCreateMadmate &&
                      !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { return EvilHacker.currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => { },
                EvilHacker.getMadmateButtonSprite(),
                CustomButton.ButtonPositions.lowerRowCenter,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("MadmateText")
            );

            eventKickButton = new CustomButton(
                () => {
                    EventUtility.kickTarget();
                },
                () => { return EventUtility.isEnabled && Mini.mini != null && !Mini.mini.Data.IsDead && PlayerControl.LocalPlayer != Mini.mini; },
                () => { return EventUtility.currentTarget != null; },
                () => { },
                EventUtility.getKickButtonSprite(),
                CustomButton.ButtonPositions.highRowRight,
                __instance,
                KeyCode.K,
                true,
                3f,
                () => {
                    // onEffectEnds
                    eventKickButton.Timer = 69;
                },
                buttonText: "KICK"
                );

            blackmailerButton = new CustomButton(
               () => { // Action when Pressed
                   if (Blackmailer.currentTarget != null)
                   {
                       if (Helpers.checkSuspendAction(Blackmailer.blackmailer, Blackmailer.currentTarget)) return;
                       _ = new StaticAchievementToken("blackmailer.common1");
                       Blackmailer.acTokenChallenge.Value.cleared |= Blackmailer.acTokenChallenge.Value.witness.Any(x => x == Blackmailer.currentTarget.PlayerId);

                       MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.BlackmailPlayer, Hazel.SendOption.Reliable, -1);
                       writer.Write(Blackmailer.currentTarget.PlayerId);
                       AmongUsClient.Instance.FinishRpcImmediately(writer);
                       RPCProcedure.blackmailPlayer(Blackmailer.currentTarget.PlayerId);
                       blackmailerButton.Timer = blackmailerButton.MaxTimer;
                       SoundEffectsManager.play("blackmailerSilence");
                   }
               },
               () => { return Blackmailer.blackmailer != null && Blackmailer.blackmailer == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
               () => { return Blackmailer.currentTarget != null && PlayerControl.LocalPlayer.CanMove; },
               () =>
               {
                   Blackmailer.acTokenChallenge.Value.witness = new();
                   blackmailerButton.Timer = blackmailerButton.MaxTimer;
               },
               Blackmailer.getBlackmailButtonSprite(),
               CustomButton.ButtonPositions.upperRowLeft, //brb
               __instance,
               KeyCode.F,
               buttonText: ModTranslation.getString("BlackmailerText")
           );

            // Sidekick Kill
            sidekickKillButton = new CustomButton(
                () => {
                    if (Helpers.checkMurderAttemptAndKill(Sidekick.sidekick, Sidekick.currentTarget) == MurderAttemptResult.SuppressKill) return;
                    sidekickKillButton.Timer = sidekickKillButton.MaxTimer; 
                    Sidekick.currentTarget = null;
                },
                () => { return Sidekick.canKill && Sidekick.sidekick != null && Sidekick.sidekick == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Sidekick.currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => { sidekickKillButton.Timer = sidekickKillButton.MaxTimer;},
                __instance.KillButton.graphic.sprite,
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.Q
            );

            // Eraser erase button
            eraserButton = new CustomButton(
                () => {
                    if (Helpers.checkSuspendAction(Eraser.eraser, Eraser.currentTarget)) return;
                    eraserButton.MaxTimer += Eraser.cooldownIncrease;
                    eraserButton.Timer = eraserButton.MaxTimer;

                    _ = new StaticAchievementToken("eraser.common1");
                    if (Eraser.currentTarget.Data.Role.IsImpostor) _ = new StaticAchievementToken("eraser.another1");
                    Eraser.acTokenChallenge.Value++;

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetFutureErased, Hazel.SendOption.Reliable, -1);
                    writer.Write(Eraser.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.setFutureErased(Eraser.currentTarget.PlayerId);
                    SoundEffectsManager.play("eraserErase");
                },
                () => { return Eraser.eraser != null && Eraser.eraser == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && Eraser.currentTarget != null; },
                () => { eraserButton.Timer = eraserButton.MaxTimer;},
                Eraser.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("EraserText")
            );

            trapperSetTrapButton = new CustomButton(
                () =>
                { // ボタンが押された時に実行
                    if (!PlayerControl.LocalPlayer.CanMove || Trap.hasTrappedPlayer()) return;
                    Trapper.setTrap();
                    trapperSetTrapButton.Timer = trapperSetTrapButton.MaxTimer;
                },
                () =>
                { /*ボタン有効になる条件*/
                    return PlayerControl.LocalPlayer == Trapper.trapper && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () =>
                { /*ボタンが使える条件*/
                    return PlayerControl.LocalPlayer.CanMove && !Trap.hasTrappedPlayer();
                },
                () =>
                { /*ミーティング終了時*/
                    trapperSetTrapButton.Timer = trapperSetTrapButton.MaxTimer;
                },
                Trapper.getTrapButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("PlaceTrapText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            moriartyBrainwashButton = new CustomButton(
                () =>
                {
                    if (Moriarty.currentTarget != null)
                    {
                        if (Helpers.checkSuspendAction(Moriarty.moriarty, Moriarty.currentTarget)) return;
                        _ = new StaticAchievementToken("moriarty.common1");
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetBrainwash, Hazel.SendOption.Reliable, -1);
                        writer.Write(Moriarty.currentTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.setBrainwash(Moriarty.currentTarget.PlayerId);
                        SoundEffectsManager.play("moriartyBrainwash");

                        // 洗脳終了までのカウントダウン
                        Moriarty.generateBrainwashText();
                    }
                    Moriarty.tmpTarget = null;
                    moriartyBrainwashButton.Timer = moriartyBrainwashButton.MaxTimer;
                },
                () => { return PlayerControl.LocalPlayer == Moriarty.moriarty && !PlayerControl.LocalPlayer.Data.IsDead && Moriarty.target == null; },
                () => { return PlayerControl.LocalPlayer.CanMove && Moriarty.currentTarget != null; },
                () => { moriartyBrainwashButton.Timer = moriartyBrainwashButton.MaxTimer; },
                Moriarty.getBrainwashIcon(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("BrainwashText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            moriartyKillButton = new CustomButton(
                () =>
                {

                    MurderAttemptResult murder = Helpers.checkMurderAttemptAndKill(PlayerControl.LocalPlayer, Moriarty.killTarget, showAnimation: false);
                    if (murder == MurderAttemptResult.PerformKill)
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MoriartyKill, Hazel.SendOption.Reliable, -1);
                        writer.Write(Moriarty.killTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.moriartyKill(Moriarty.killTarget.PlayerId);
                        Moriarty.target = null;
                        moriartyBrainwashButton.Timer = moriartyBrainwashButton.MaxTimer;
                    }
                },
                // HasButton
                () => { return PlayerControl.LocalPlayer == Moriarty.moriarty && !PlayerControl.LocalPlayer.Data.IsDead; },
                // CouldUse
                () =>
                {
                    if (moriartyKillCounterText != null)
                    {
                        moriartyKillCounterText.text = $"{Moriarty.counter}/{Moriarty.numberToWin}";
                    }
                    moriartyKillButton.buttonText = Moriarty.killTarget ? Moriarty.killTarget.Data.PlayerName : ModTranslation.getString("moriartyTargetNone");
                    return Moriarty.killTarget != null && PlayerControl.LocalPlayer.CanMove;
                },
                // OnMeetingEnds
                () =>
                {
                    moriartyKillButton.Timer = moriartyKillButton.MaxTimer;
                    Moriarty.brainwashed.Clear();
                    Moriarty.target = null;
                },
                __instance.KillButton.graphic.sprite,
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.Q,
                buttonText: ModTranslation.getString("moriartyTargetNone"),
                actionName: ModTranslation.getString("moriartyKillTarget")
            );
            moriartyKillCounterText = GameObject.Instantiate(moriartyKillButton.actionButton.cooldownTimerText, moriartyKillButton.actionButton.cooldownTimerText.transform.parent);
            moriartyKillCounterText.text = "";
            moriartyKillCounterText.enableWordWrapping = false;
            moriartyKillCounterText.transform.localScale = Vector3.one * 0.5f;
            moriartyKillCounterText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            placeJackInTheBoxButton = new CustomButton(
                () => {
                    placeJackInTheBoxButton.Timer = placeJackInTheBoxButton.MaxTimer;

                    var pos = PlayerControl.LocalPlayer.transform.position;
                    byte[] buff = new byte[sizeof(float) * 2];
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0*sizeof(float), sizeof(float));
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1*sizeof(float), sizeof(float));

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceJackInTheBox, Hazel.SendOption.Reliable);
                    writer.WriteBytesAndSize(buff);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.placeJackInTheBox(buff);
                    SoundEffectsManager.play("tricksterPlaceBox");
                },
                () => { return Trickster.trickster != null && Trickster.trickster == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && !JackInTheBox.hasJackInTheBoxLimitReached(); },
                () =>
                {
                    if (tricksterBoxesText != null) tricksterBoxesText.text = (JackInTheBox.JackInTheBoxLimit - JackInTheBox.AllJackInTheBoxes.Count).ToString();
                    return PlayerControl.LocalPlayer.CanMove && !JackInTheBox.hasJackInTheBoxLimitReached();
                },
                () => { placeJackInTheBoxButton.Timer = placeJackInTheBoxButton.MaxTimer;},
                Trickster.getPlaceBoxButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("PlaceJackInTheBoxText")
            );
            tricksterBoxesText = placeJackInTheBoxButton.ShowUsesIcon(0);

            lightsOutButton = new CustomButton(
                () => {
                    _ = new StaticAchievementToken("trickster.common2");
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.LightsOut, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.lightsOut();
                    SoundEffectsManager.play("lighterLight");
                },
                () => { return Trickster.trickster != null && Trickster.trickster == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && JackInTheBox.hasJackInTheBoxLimitReached() && JackInTheBox.boxesConvertedToVents; },
                () => { return PlayerControl.LocalPlayer.CanMove && JackInTheBox.hasJackInTheBoxLimitReached() && JackInTheBox.boxesConvertedToVents; },
                () => { 
                    lightsOutButton.Timer = lightsOutButton.MaxTimer;
                    lightsOutButton.isEffectActive = false;
                    lightsOutButton.actionButton.graphic.color = Palette.EnabledColor;
                    Trickster.acTokenChallenge.Value.kills = 0;
                },
                Trickster.getLightsOutButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                true,
                Trickster.lightsOutDuration,
                () => {
                    lightsOutButton.Timer = lightsOutButton.MaxTimer;
                    SoundEffectsManager.play("lighterLight");
                    Trickster.acTokenChallenge.Value.kills = 0;
                },
                buttonText: ModTranslation.getString("LightsOutText")
            );

            // Cleaner Clean
            cleanerCleanButton = new CustomButton(
                () => {
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer.MaxReportDistance, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody")
                        {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component && !component.Reported)
                            {
                                Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                Vector2 truePosition2 = component.TruePosition;
                                if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false))
                                {
                                    _ = new StaticAchievementToken("cleaner.common1");
                                    Cleaner.acTokenChallenge.Value++;

                                    NetworkedPlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);
                                    
                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CleanBody, Hazel.SendOption.Reliable, -1);
                                    writer.Write(playerInfo.PlayerId);
                                    writer.Write(Cleaner.cleaner.PlayerId);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.cleanBody(playerInfo.PlayerId, Cleaner.cleaner.PlayerId);

                                    Cleaner.cleaner.killTimer = cleanerCleanButton.Timer = cleanerCleanButton.MaxTimer;
                                    SoundEffectsManager.play("cleanerClean");
                                    break;
                                }
                            }
                        }
                    }
                },
                () => { return Cleaner.cleaner != null && Cleaner.cleaner == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return __instance.ReportButton.graphic.color == Palette.EnabledColor && PlayerControl.LocalPlayer.CanMove; },
                () => { cleanerCleanButton.Timer = cleanerCleanButton.MaxTimer; },
                Cleaner.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("CleanText")
            );

            // Warlock curse
            warlockCurseButton = new CustomButton(
                () => {
                    if (Warlock.curseVictim == null) {
                        if (Helpers.checkSuspendAction(Warlock.warlock, Warlock.currentTarget)) return;
                        // Apply Curse
                        Warlock.curseVictim = Warlock.currentTarget;
                        warlockCurseButton.Sprite = Warlock.getCurseKillButtonSprite();
                        warlockCurseButton.Timer = 1f;
                        warlockCurseButton.buttonText = ModTranslation.getString("CurseKillText");
                        SoundEffectsManager.play("warlockCurse");

                        // Ghost Info
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareGhostInfo, Hazel.SendOption.Reliable, -1);
                        writer.Write(PlayerControl.LocalPlayer.PlayerId);
                        writer.Write((byte)RPCProcedure.GhostInfoTypes.WarlockTarget);
                        writer.Write(Warlock.curseVictim.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);

                    } else if (Warlock.curseVictim != null && Warlock.curseVictimTarget != null) {
                        MurderAttemptResult murder = Helpers.checkMurderAttemptAndKill(Warlock.warlock, Warlock.curseVictimTarget, showAnimation: false);
                        if (murder == MurderAttemptResult.SuppressKill) return;
                        if (murder == MurderAttemptResult.PerformKill) {
                            _ = new StaticAchievementToken("warlock.common1");
                            Warlock.acTokenChallenge.Value++;
                            if (Warlock.curseVictimTarget == PlayerControl.LocalPlayer)
                                _ = new StaticAchievementToken("warlock.another1");
                        }

                        warlockCurseButton.buttonText = ModTranslation.getString("CurseText");
                        // If blanked or killed
                        if (Warlock.rootTime > 0) {
                            AntiTeleport.position = PlayerControl.LocalPlayer.transform.position;
                            PlayerControl.LocalPlayer.moveable = false;
                            PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement so the warlock is not just running straight into the next object
                            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Warlock.rootTime, new Action<float>((p) => { // Delayed action
                                if (p == 1f) {
                                    PlayerControl.LocalPlayer.moveable = true;
                                }
                            })));
                        }

                        Warlock.curseVictim = null;
                        Warlock.curseVictimTarget = null;
                        warlockCurseButton.Sprite = Warlock.getCurseButtonSprite();
                        Warlock.warlock.killTimer = warlockCurseButton.Timer = warlockCurseButton.MaxTimer;

                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareGhostInfo, Hazel.SendOption.Reliable, -1);
                        writer.Write(PlayerControl.LocalPlayer.PlayerId);
                        writer.Write((byte)RPCProcedure.GhostInfoTypes.WarlockTarget);
                        writer.Write(Byte.MaxValue); // This will set it to null!
                        AmongUsClient.Instance.FinishRpcImmediately(writer);

                    }
                },
                () => { return Warlock.warlock != null && Warlock.warlock == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return ((Warlock.curseVictim == null && Warlock.currentTarget != null) || (Warlock.curseVictim != null && Warlock.curseVictimTarget != null)) && PlayerControl.LocalPlayer.CanMove; },
                () => {
                    warlockCurseButton.Timer = warlockCurseButton.MaxTimer;
                    warlockCurseButton.Sprite = Warlock.getCurseButtonSprite();
                    warlockCurseButton.buttonText = ModTranslation.getString("CurseText");
                    Warlock.curseVictim = null;
                    Warlock.curseVictimTarget = null;
                },
                Warlock.getCurseButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("CurseText"),
                actionName: ModTranslation.getString("warlockButton")
            );

            // Security Guard button
            securityGuardButton = new CustomButton(
                () => {
                    if (SecurityGuard.ventTarget != null) { // Seal vent
                        SecurityGuard.acTokenCommon.Value.vent = true;

                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SealVent, Hazel.SendOption.Reliable);
                        writer.WritePacked(SecurityGuard.ventTarget.Id);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.sealVent(SecurityGuard.ventTarget.Id);
                        SecurityGuard.ventTarget = null;
                        
                    } else if (GameOptionsManager.Instance.currentNormalGameOptions.MapId != 1 && !SubmergedCompatibility.IsSubmerged) { // Place camera if there's no vent and it's not MiraHQ or Submerged
                        SecurityGuard.acTokenCommon.Value.camera = true;
                        
                        var pos = PlayerControl.LocalPlayer.transform.position;
                        byte[] buff = new byte[sizeof(float) * 2];
                        Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0*sizeof(float), sizeof(float));
                        Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1*sizeof(float), sizeof(float));

                        byte roomId;
                        try {
                            roomId = (byte)FastDestroyableSingleton<HudManager>.Instance.roomTracker.LastRoom.RoomId;
                        } catch {
                            roomId = (byte)SystemTypes.Outside;
                        }

                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceCamera, Hazel.SendOption.Reliable);
                        writer.WriteBytesAndSize(buff);
                        writer.Write(roomId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.placeCamera(buff, roomId); 
                    }
                    SoundEffectsManager.play("securityGuardPlaceCam");  // Same sound used for both types (cam or vent)!
                    securityGuardButton.Timer = securityGuardButton.MaxTimer;
                },
                () => { return SecurityGuard.securityGuard != null && SecurityGuard.securityGuard == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && SecurityGuard.remainingScrews >= Mathf.Min(SecurityGuard.ventPrice, SecurityGuard.camPrice); },
                () => {
                    securityGuardButton.actionButton.graphic.sprite = (SecurityGuard.ventTarget == null && !Helpers.isMira() && !Helpers.isFungle() && GameOptionsManager.Instance.currentNormalGameOptions.MapId != 1 && !SubmergedCompatibility.IsSubmerged) ? SecurityGuard.getPlaceCameraButtonSprite() : SecurityGuard.getCloseVentButtonSprite(); 
                    if (SecurityGuard.ventTarget == null && !Helpers.isMira() && !Helpers.isFungle() && GameOptionsManager.Instance.currentNormalGameOptions.MapId != 1 && !SubmergedCompatibility.IsSubmerged)
                    {
                        securityGuardButton.buttonText = ModTranslation.getString("PlaceCameraText");
                    }
                    else
                    {
                        securityGuardButton.buttonText = ModTranslation.getString("CloseVentText");
                    }
                    if (securityGuardButtonScrewsText != null) securityGuardButtonScrewsText.text = $"{SecurityGuard.remainingScrews}/{SecurityGuard.totalScrews}";

                    if (SecurityGuard.ventTarget != null)
                        return SecurityGuard.remainingScrews >= SecurityGuard.ventPrice && PlayerControl.LocalPlayer.CanMove;
                    return !Helpers.isMira() && !Helpers.isFungle() && !SubmergedCompatibility.IsSubmerged && SecurityGuard.remainingScrews >= SecurityGuard.camPrice && PlayerControl.LocalPlayer.CanMove;
                },
                () => { securityGuardButton.Timer = securityGuardButton.MaxTimer; },
                SecurityGuard.getPlaceCameraButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                abilityTexture: CustomButton.ButtonLabelType.UseButton,
                buttonText: ModTranslation.getString("PlaceCameraText"),
                actionName: ModTranslation.getString("securityGuardButton")
            );

            // Security Guard button screws counter
            securityGuardButtonScrewsText = GameObject.Instantiate(securityGuardButton.actionButton.cooldownTimerText, securityGuardButton.actionButton.cooldownTimerText.transform.parent);
            securityGuardButtonScrewsText.text = "";
            securityGuardButtonScrewsText.enableWordWrapping = false;
            securityGuardButtonScrewsText.transform.localScale = Vector3.one * 0.5f;
            securityGuardButtonScrewsText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            securityGuardCamButton = new CustomButton(
                () => {
                    if (!Helpers.isMira()) {
                        if (SecurityGuard.minigame == null) {
                            SecurityGuard.acTokenChallenge.Value++;

                            byte mapId = GameOptionsManager.Instance.currentNormalGameOptions.MapId;
                            UnityEngine.Object.FindObjectsOfType<SystemConsole>().ToList().ForEach(x => TheOtherRolesPlugin.Logger.LogMessage($"{x.name} {x.GetType()}, {x.MinigamePrefab.TaskType}"));
                            var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("Surv_Panel") || x.name.Contains("Cam") || x.name.Contains("BinocularsSecurityConsole"));
                            if (Helpers.isSkeld() || mapId == 3) e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("SurvConsole"));
                            else if (Helpers.isAirship()) e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("task_cams"));
                            if (e == null || Camera.main == null) return;
                            SecurityGuard.minigame = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                        }
                        SecurityGuard.minigame.transform.SetParent(Camera.main.transform, false);
                        SecurityGuard.minigame.transform.localPosition = new Vector3(0.0f, 0.0f, -50f);
                        SecurityGuard.minigame.Begin(null);
                    } else {
                        if (SecurityGuard.minigame == null) {
                            var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("SurvLogConsole"));
                            if (e == null || Camera.main == null) return;
                            SecurityGuard.minigame = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                        }
                        SecurityGuard.minigame.transform.SetParent(Camera.main.transform, false);
                        SecurityGuard.minigame.transform.localPosition = new Vector3(0.0f, 0.0f, -50f);
                        SecurityGuard.minigame.Begin(null);
                    }
                    SecurityGuard.charges--;

                    if (SecurityGuard.cantMove) PlayerControl.LocalPlayer.moveable = false;
                    PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement 
                },
                () => { return SecurityGuard.securityGuard != null && SecurityGuard.securityGuard == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && SecurityGuard.remainingScrews < Mathf.Min(SecurityGuard.ventPrice, SecurityGuard.camPrice)
                               && !SubmergedCompatibility.IsSubmerged; },
                () => {
                    if (securityGuardChargesText != null) securityGuardChargesText.text = $"{SecurityGuard.charges} / {SecurityGuard.maxCharges}";
                    securityGuardCamButton.actionButton.graphic.sprite = Helpers.isMira() ? SecurityGuard.getLogSprite() : SecurityGuard.getCamSprite();
                    securityGuardCamButton.actionButton.OverrideText(Helpers.isMira() ?
                        TranslationController.Instance.GetString(StringNames.SecurityLogsSystem) :
                        ModTranslation.getString("securityGuardCamButton"));
                    return PlayerControl.LocalPlayer.CanMove && SecurityGuard.charges > 0;
                },
                () => {
                    securityGuardCamButton.Timer = securityGuardCamButton.MaxTimer;
                    securityGuardCamButton.isEffectActive = false;
                    securityGuardCamButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                SecurityGuard.getCamSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.G,
                true,
                0f,
                () => {
                    securityGuardCamButton.Timer = securityGuardCamButton.MaxTimer;
                    if (Minigame.Instance) {
                        SecurityGuard.minigame.ForceClose();
                    }
                    PlayerControl.LocalPlayer.moveable = true;
                },
                false,
                Helpers.isMira() ?
                TranslationController.Instance.GetString(StringNames.SecurityLogsSystem) :
                ModTranslation.getString("securityGuardCamButton"),
                abilityTexture: CustomButton.ButtonLabelType.AdminButton
            );

            // Security Guard cam button charges
            securityGuardChargesText = GameObject.Instantiate(securityGuardCamButton.actionButton.cooldownTimerText, securityGuardCamButton.actionButton.cooldownTimerText.transform.parent);
            securityGuardChargesText.text = "";
            securityGuardChargesText.enableWordWrapping = false;
            securityGuardChargesText.transform.localScale = Vector3.one * 0.5f;
            securityGuardChargesText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            // Arsonist button
            arsonistButton = new CustomButton(
                () => {
                    bool dousedEveryoneAlive = Arsonist.dousedEveryoneAlive();
                    if (dousedEveryoneAlive) {
                        _ = new StaticAchievementToken("arsonist.challenge");
                        MessageWriter winWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ArsonistWin, Hazel.SendOption.Reliable, -1);
                        AmongUsClient.Instance.FinishRpcImmediately(winWriter);
                        RPCProcedure.arsonistWin();
                        arsonistButton.HasEffect = false;
                    } else if (Arsonist.currentTarget != null) {
                        if (Helpers.checkSuspendAction(Arsonist.arsonist, Arsonist.currentTarget)) return;
                        Arsonist.douseTarget = Arsonist.currentTarget;
                        arsonistButton.HasEffect = true;
                        SoundEffectsManager.play("arsonistDouse");
                    }
                },
                () => { return Arsonist.arsonist != null && Arsonist.arsonist == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool dousedEveryoneAlive = Arsonist.dousedEveryoneAlive();
                    if (dousedEveryoneAlive)
                    {
                        arsonistButton.actionButton.graphic.sprite = Arsonist.getIgniteSprite();
                        arsonistButton.buttonText = ModTranslation.getString("IgniteText");
                    }
                    
                    if (arsonistButton.isEffectActive && Arsonist.douseTarget != Arsonist.currentTarget) {
                        Arsonist.douseTarget = null;
                        arsonistButton.Timer = 0f;
                        arsonistButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && (dousedEveryoneAlive || Arsonist.currentTarget != null);
                },
                () => {
                    arsonistButton.Timer = arsonistButton.MaxTimer;
                    arsonistButton.isEffectActive = false;
                    Arsonist.douseTarget = null;
                },
                Arsonist.getDouseSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                true,
                Arsonist.duration,
                () => {
                    if (Arsonist.douseTarget != null) Arsonist.dousedPlayers.Add(Arsonist.douseTarget);
                    _ = new StaticAchievementToken("arsonist.common1");
                    arsonistButton.Timer = Arsonist.dousedEveryoneAlive() ? 0 : arsonistButton.MaxTimer;

                    foreach (PlayerControl p in Arsonist.dousedPlayers) {
                        if (TORMapOptions.playerIcons.ContainsKey(p.PlayerId)) {
                            TORMapOptions.playerIcons[p.PlayerId].setSemiTransparent(false);
                        }
                    }

                    // Ghost Info
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareGhostInfo, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write((byte)RPCProcedure.GhostInfoTypes.ArsonistDouse);
                    writer.Write(Arsonist.douseTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    Arsonist.douseTarget = null;
                },
                buttonText: ModTranslation.getString("DouseText"),
                shakeOnEnd: false
            );

            // Veteran Alert
            veteranAlertButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.VeteranAlert, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.veteranAlert();

                    Veteran.remainingAlerts--;
                    SoundEffectsManager.play("veteranAlert");
                },
                () => { return Veteran.veteran != null && Veteran.veteran == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (veteranButtonAlertText != null) veteranButtonAlertText.text = $"{Veteran.remainingAlerts}";
                    return PlayerControl.LocalPlayer.CanMove && Veteran.remainingAlerts > 0; 
                },
                () => {
                    veteranAlertButton.Timer = veteranAlertButton.MaxTimer;
                    veteranAlertButton.isEffectActive = false;
                    veteranAlertButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Veteran.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight, //brb
                __instance,
                KeyCode.F,
                true,
                Veteran.alertDuration,
                () => { veteranAlertButton.Timer = veteranAlertButton.MaxTimer; },
                buttonText: ModTranslation.getString("AlertText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            veteranButtonAlertText = veteranAlertButton.ShowUsesIcon(3);

            archaeologistDetectButton = new CustomButton(
                () =>
                {
                    var remainingList = Antique.antiques.Where(x => !x.isBroken).ToList();
                    var id = rnd.Next(remainingList.Count);
                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ArchaeologistDetect, SendOption.Reliable);
                    writer.Write(id);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.archaeologistDetect((byte)id);
                    _ = new StaticAchievementToken("archaeologist.common1");
                },
                () => { return Archaeologist.archaeologist != null && PlayerControl.LocalPlayer == Archaeologist.archaeologist && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && Archaeologist.hasRemainingAntique(); },
                () => { archaeologistDetectButton.Timer = archaeologistDetectButton.MaxTimer = Archaeologist.cooldown; },
                Archaeologist.getDetectSprite(),
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.F,
                true,
                Archaeologist.arrowDuration,
                () =>
                {
                    archaeologistDetectButton.Timer = archaeologistDetectButton.MaxTimer;
                    Archaeologist.arrowActive = false;
                },
                buttonText: ModTranslation.getString("DetectText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            archaeologistExcavateButton = new CustomButton(
                () =>
                {
                    if (Archaeologist.target != null)
                    {
                        Archaeologist.antiqueTarget = Archaeologist.target;
                        archaeologistExcavateButton.HasEffect = true;
                    }
                },
                () => { return Archaeologist.archaeologist != null && PlayerControl.LocalPlayer == Archaeologist.archaeologist && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (archaeologistExcavateButton.isEffectActive && Archaeologist.target != Archaeologist.antiqueTarget)
                    {
                        Archaeologist.antiqueTarget = null;
                        archaeologistExcavateButton.Timer = 0f;
                        archaeologistExcavateButton.isEffectActive = false;
                    }
                    return Archaeologist.target != null && PlayerControl.LocalPlayer.CanMove && Archaeologist.hasRemainingAntique();
                },
                () =>
                {
                    archaeologistExcavateButton.Timer = archaeologistExcavateButton.MaxTimer = Archaeologist.cooldown;
                    archaeologistExcavateButton.isEffectActive = false;
                    archaeologistExcavateButton.actionButton.graphic.color = Palette.EnabledColor;
                    Archaeologist.antiqueTarget = null;
                },
                Archaeologist.getExcavateSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.G,
                true,
                Archaeologist.detectDuration,
                () =>
                {
                    byte index = (byte)Archaeologist.antiqueTarget.id;
                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ArchaeologistExcavate, SendOption.Reliable);
                    writer.Write(index);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.archaeologistExcavate(index);
                    Archaeologist.revealed.RemoveAll(x => x.isBroken);
                    archaeologistExcavateButton.Timer = archaeologistExcavateButton.MaxTimer;
                },
                buttonText: ModTranslation.getString("ExcavateText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton,
                shakeOnEnd: false
            );

            //createRoleSummaryButton(__instance);

            accelAttributeButton = new CustomButton(
                () => { },
                () => { return Props.AccelTrap.acceled.ContainsKey(PlayerControl.LocalPlayer) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (accelAttributePropTip != null)
                        accelAttributePropTip.ProptipText = string.Format(ModTranslation.getString("accelTrapPropTip"), Math.Abs(Math.Round(CustomOptionHolder.accelerationDuration.getFloat() - DateTime.UtcNow.Subtract(Props.AccelTrap.acceled[PlayerControl.LocalPlayer]).TotalSeconds)));
                    return true;
                },
                () => { },
                Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AccelAttribute.png", 250f),
                new Vector3(-0.5f, 1f, 0f),
                __instance,
                null,
                true,
                isSuicide: true
            );

            accelAttributePropTip = accelAttributeButton.actionButtonGameObject.AddComponent<Props.Proptip>();

            decelAttributeButton = new CustomButton(
                () => { },
                () => { return Props.DecelTrap.deceled.ContainsKey(PlayerControl.LocalPlayer) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (decelAttributePropTip != null)
                        decelAttributePropTip.ProptipText = string.Format(ModTranslation.getString("decelTrapPropTip"), Math.Abs(Math.Round(CustomOptionHolder.decelerationDuration.getFloat() - DateTime.UtcNow.Subtract(Props.DecelTrap.deceled[PlayerControl.LocalPlayer]).TotalSeconds)));
                    return true;
                },
                () => { },
                Helpers.loadSpriteFromResources("TheOtherRoles.Resources.DecelAttribute.png", 250f),
                new Vector3(0.1f, 1f, 0),
                __instance,
                null,
                true,
                isSuicide: true
            );

            decelAttributePropTip = decelAttributeButton.actionButtonGameObject.AddComponent<Props.Proptip>();

            bomberAPlantBombButton = new CustomButton(
                // OnClick
                () =>
                {
                    if (Helpers.checkSuspendAction(BomberA.bomberA, BomberA.currentTarget)) return;

                    if (BomberA.currentTarget != null)
                    {
                        BomberA.tmpTarget = BomberA.currentTarget;
                        bomberAPlantBombButton.HasEffect = true;
                    }
                },
                // HasButton
                () => { return PlayerControl.LocalPlayer == BomberA.bomberA && !PlayerControl.LocalPlayer.Data.IsDead && BomberB.bomberB != null && !BomberB.bomberB.Data.IsDead; },
                // CouldUse
                () =>
                {
                    if (bomberAPlantBombButton.isEffectActive && BomberA.tmpTarget != BomberA.currentTarget)
                    {
                        BomberA.tmpTarget = null;
                        bomberAPlantBombButton.Timer = 0f;
                        bomberAPlantBombButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && BomberA.currentTarget != null;
                },
                // OnMeetingEnds
                () =>
                {
                    bomberAPlantBombButton.Timer = bomberAPlantBombButton.MaxTimer;
                    bomberAPlantBombButton.isEffectActive = false;
                    BomberA.tmpTarget = null;                    
                },
                BomberA.getBomberButtonSprite(),
                CustomButton.ButtonPositions.upperRowCenter,
                __instance,
                KeyCode.F,
                true,
                BomberA.duration,
                // OnEffectsEnd
                () =>
                {
                    if (BomberA.tmpTarget != null)
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlantBomb, Hazel.SendOption.Reliable, -1);
                        writer.Write(BomberA.tmpTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        BomberA.bombTarget = BomberA.tmpTarget;
                    }
                    _ = new StaticAchievementToken("bomber.common1");
                    BomberA.tmpTarget = null;
                    bomberAPlantBombButton.Timer = bomberAPlantBombButton.MaxTimer;
                    SoundEffectsManager.play("bomberPlantBomb");
                },
                buttonText: ModTranslation.getString("PlantBombText"),
                shakeOnEnd: false
            );

            bomberBPlantBombButton = new CustomButton(
                // OnClick
                () =>
                {
                    if (Helpers.checkSuspendAction(BomberB.bomberB, BomberB.currentTarget)) return;

                    if (BomberB.currentTarget != null)
                    {
                        BomberB.tmpTarget = BomberB.currentTarget;
                        bomberBPlantBombButton.HasEffect = true;
                    }
                },
                // HasButton
                () => { return PlayerControl.LocalPlayer == BomberB.bomberB && !PlayerControl.LocalPlayer.Data.IsDead && BomberA.bomberA != null && !BomberA.bomberA.Data.IsDead; },
                // CouldUse
                () =>
                {
                    if (bomberBPlantBombButton.isEffectActive && BomberB.tmpTarget != BomberB.currentTarget)
                    {
                        BomberB.tmpTarget = null;
                        bomberBPlantBombButton.Timer = 0f;
                        bomberBPlantBombButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && BomberB.currentTarget != null;
                },
                // OnMeetingEnds
                () =>
                {
                    bomberBPlantBombButton.Timer = bomberBPlantBombButton.MaxTimer;
                    bomberBPlantBombButton.isEffectActive = false;
                    BomberB.tmpTarget = null;                    
                },
                BomberB.getBomberButtonSprite(),
                CustomButton.ButtonPositions.upperRowCenter,
                __instance,
                KeyCode.F,
                true,
                BomberA.duration,
                // OnEffectsEnd
                () =>
                {
                    if (BomberB.tmpTarget != null)
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlantBomb, Hazel.SendOption.Reliable, -1);
                        writer.Write(BomberB.tmpTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        BomberB.bombTarget = BomberB.tmpTarget;
                    }
                    _ = new StaticAchievementToken("bomber.common1");
                    BomberB.tmpTarget = null;
                    bomberBPlantBombButton.Timer = bomberBPlantBombButton.MaxTimer;
                    SoundEffectsManager.play("bomberPlantBomb");
                },
                buttonText: ModTranslation.getString("PlantBombText"),
                shakeOnEnd: false
            );

            bomberAReleaseBombButton = new CustomButton(
                // OnClick
                () =>
                {
                    if (Helpers.checkSuspendAction(BomberA.bomberA, BomberA.currentTarget)) return;

                    // Use MurderAttempt to exclude eg.Medic shielded
                    MurderAttemptResult attempt = Helpers.checkMuderAttempt(BomberA.bomberA, BomberA.bombTarget);

                    var bomberB = BomberB.bomberB;
                    float distance = Vector2.Distance(PlayerControl.LocalPlayer.transform.localPosition, bomberB.transform.localPosition);

                    if (attempt == MurderAttemptResult.PerformKill)
                    {
                        if (PlayerControl.LocalPlayer.CanMove && BomberA.bombTarget != null && BomberB.bombTarget != null && bomberB != null && !bomberB.Data.IsDead && distance < 1)
                        {
                            var target = BomberA.bombTarget;
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ReleaseBomb, Hazel.SendOption.Reliable, -1);
                            writer.Write(PlayerControl.LocalPlayer.PlayerId);
                            writer.Write(target.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.releaseBomb(PlayerControl.LocalPlayer.PlayerId, target.PlayerId);
                        }
                        _ = new StaticAchievementToken("bomber.challenge");
                    }
                    else if (attempt == MurderAttemptResult.BlankKill)
                    {
                        bomberAPlantBombButton.Timer = bomberAPlantBombButton.MaxTimer;
                        return;
                    }
                    else if (attempt == MurderAttemptResult.SuppressKill) return;
                },
                // HasButton
                () => { return PlayerControl.LocalPlayer == BomberA.bomberA && !PlayerControl.LocalPlayer.Data.IsDead && BomberB.bomberB != null && !BomberB.bomberB.Data.IsDead; },
                // CouldUse
                () =>
                {
                    var bomberB = BomberB.bomberB;
                    float distance = Vector2.Distance(PlayerControl.LocalPlayer.transform.localPosition, bomberB.transform.localPosition);

                    return PlayerControl.LocalPlayer.CanMove && BomberA.bombTarget != null && BomberB.bombTarget != null && bomberB != null && !bomberB.Data.IsDead && distance < 1;
                },
                // OnMeetingEnds
                () =>
                {
                    bomberAReleaseBombButton.Timer = bomberAReleaseBombButton.MaxTimer;
                },
                BomberA.getReleaseButtonSprite(),
                CustomButton.ButtonPositions.lowerRowCenter,
                __instance,
                KeyCode.Q,
                false,
                ModTranslation.getString("ReleaseBombText")
            );

            bomberBReleaseBombButton = new CustomButton(
                // OnClick
                () =>
                {
                    if (Helpers.checkSuspendAction(BomberB.bomberB, BomberB.currentTarget)) return;
                    var bomberA = BomberA.bomberA;
                    float distance = Vector2.Distance(PlayerControl.LocalPlayer.transform.localPosition, bomberA.transform.localPosition);

                    MurderAttemptResult attempt = Helpers.checkMuderAttempt(BomberB.bomberB, BomberB.bombTarget);

                    if (attempt == MurderAttemptResult.PerformKill)
                    {
                        if (PlayerControl.LocalPlayer.CanMove && BomberA.bombTarget != null && BomberB.bombTarget != null && bomberA != null && !bomberA.Data.IsDead && distance < 1)
                        {
                            var target = BomberB.bombTarget;
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ReleaseBomb, Hazel.SendOption.Reliable, -1);
                            writer.Write(PlayerControl.LocalPlayer.PlayerId);
                            writer.Write(target.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.releaseBomb(PlayerControl.LocalPlayer.PlayerId, target.PlayerId);
                        }
                        _ = new StaticAchievementToken("bomber.challenge");
                    }
                    else if (attempt == MurderAttemptResult.BlankKill)
                    {
                        bomberBPlantBombButton.Timer = bomberBPlantBombButton.MaxTimer;
                        return;
                    }
                    else if (attempt == MurderAttemptResult.SuppressKill) return;
                },
                // HasButton
                () => { return PlayerControl.LocalPlayer == BomberB.bomberB && !PlayerControl.LocalPlayer.Data.IsDead && BomberA.bomberA != null && !BomberA.bomberA.Data.IsDead; },
                // CouldUse
                () =>
                {
                    var bomberA = BomberA.bomberA;
                    float distance = Vector2.Distance(PlayerControl.LocalPlayer.transform.localPosition, bomberA.transform.localPosition);

                    return PlayerControl.LocalPlayer.CanMove && BomberA.bombTarget != null && BomberB.bombTarget != null && !bomberA.Data.IsDead && bomberA != null && distance < 1;
                },
                // OnMeetingEnds
                () =>
                {
                    bomberBReleaseBombButton.Timer = bomberBReleaseBombButton.MaxTimer;
                },
                BomberB.getReleaseButtonSprite(),
                CustomButton.ButtonPositions.lowerRowCenter,
                __instance,
                KeyCode.Q,
                false,
                ModTranslation.getString("ReleaseBombText")
            );

            undertakerDragButton = new CustomButton(
                () =>
                {
                    _ = new StaticAchievementToken("undertaker.common1");
                    var bodyComponent = Undertaker.TargetBody;
                    if (Undertaker.DraggedBody == null && bodyComponent != null)
                    {
                        Undertaker.RpcDragBody(bodyComponent.ParentId);
                    }
                    else if (Undertaker.DraggedBody != null)
                    {
                        var position = Undertaker.DraggedBody.transform.position;
                        Undertaker.RpcDropBody(position);
                    }
                }, // Action OnClick
                () =>
                {
                    return Undertaker.undertaker != null &&
                           Undertaker.undertaker == PlayerControl.LocalPlayer &&
                           !PlayerControl.LocalPlayer.Data.IsDead;
                }, // Bool HasButton
                () =>
                {
                    if (Undertaker.DraggedBody != null)
                    {
                        undertakerDragButton.Sprite = Undertaker.getDropButtonSprite();
                        undertakerDragButton.buttonText = ModTranslation.getString("DropBodyText");
                    }
                    else
                    {
                        undertakerDragButton.Sprite = Undertaker.getDragButtonSprite();
                        undertakerDragButton.buttonText = ModTranslation.getString("DragBodyText");
                    }
                    return ((Undertaker.TargetBody != null && Undertaker.DraggedBody == null)
                            || (Undertaker.DraggedBody != null && Undertaker.CanDropBody))
                           && PlayerControl.LocalPlayer.CanMove;
                }, // Bool CouldUse
                () => { }, // Action OnMeetingEnds
                Undertaker.getDragButtonSprite(), // Sprite sprite,
                CustomButton.ButtonPositions.upperRowLeft, // Vector3 PositionOffset
                __instance, // HudManager hudManager
                KeyCode.F
            )
            {
                showButtonText = ModTranslation.getString("DropBodyText") != ""
            };

            buskerButton = new CustomButton(
                () =>
                {
                    if (buskerButton.isEffectActive)
                    {
                        Busker.buttonInterrupted = true;
                        buskerButton.Timer = 0f;
                        return;
                    }

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.BuskerPseudocide, SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(false);
                    writer.Write(false);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.buskerPseudocide(PlayerControl.LocalPlayer.PlayerId, false, false);
                },
                () => { return Busker.busker != null && PlayerControl.LocalPlayer == Busker.busker && (!PlayerControl.LocalPlayer.Data.IsDead || Busker.checkPseudocide()); },
                () =>
                {
                    if (buskerButton.isEffectActive)
                    {
                        buskerButton.buttonText = ModTranslation.getString("ReviveText");
                    }
                    else
                    {
                        buskerButton.buttonText = ModTranslation.getString("PseudocideText");
                    }
                    return PlayerControl.LocalPlayer.CanMove && (!Busker.pseudocideFlag || MapData.GetCurrentMapData().CheckMapArea(PlayerControl.LocalPlayer.transform.position));
                },
                () =>
                {
                    buskerButton.Timer = buskerButton.MaxTimer = Busker.cooldown;
                },
                Busker.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                true,
                Busker.duration,
                () =>
                {
                    if (Busker.buttonInterrupted)
                    {
                        Busker.buttonInterrupted = false;
                        _ = new StaticAchievementToken("busker.common1");
                        Busker.acTokenChallenge.Value.pseudocide = DateTime.UtcNow;
                        MessageWriter writer1 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.BuskerRevive, SendOption.Reliable, -1);
                        writer1.Write(PlayerControl.LocalPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer1);
                        RPCProcedure.buskerRevive(PlayerControl.LocalPlayer.PlayerId);
                        buskerButton.Timer = buskerButton.MaxTimer = Busker.cooldown;
                    }
                    else
                    {
                        _ = new StaticAchievementToken("busker.another1");
                        Busker.dieBusker();
                    }
                },
                buttonText: ModTranslation.getString("PseudocideText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            )
            {
                effectCancellable = true
            };

            noisemakerButton = new CustomButton(
                () =>
                {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.NoisemakerSetSounded, SendOption.Reliable, -1);
                    writer.Write(Noisemaker.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.noisemakerSetSounded(Noisemaker.currentTarget.PlayerId);
                    Noisemaker.numSound--;
                    noisemakerButton.Timer = noisemakerButton.MaxTimer;
                },
                () => { return Noisemaker.noisemaker != null && PlayerControl.LocalPlayer == Noisemaker.noisemaker && Noisemaker.numSound > 0 && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (noisemakerButtonText != null)
                        noisemakerButtonText.text = Noisemaker.numSound > 0 ? $"{Noisemaker.numSound}" : "";
                    return PlayerControl.LocalPlayer.CanMove && Noisemaker.currentTarget != null && Noisemaker.target == null;
                },
                () =>
                {
                    noisemakerButton.Timer = noisemakerButton.MaxTimer = Noisemaker.cooldown;
                    Noisemaker.target = null;
                },
                Noisemaker.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("NoisemakerText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            noisemakerButtonText = noisemakerButton.ShowUsesIcon(3);

            schrodingersCatKillButton = new CustomButton(
                () =>
                {
                    if (Helpers.checkMurderAttemptAndKill(PlayerControl.LocalPlayer, SchrodingersCat.currentTarget) == MurderAttemptResult.SuppressKill) return;

                    schrodingersCatKillButton.Timer = schrodingersCatKillButton.MaxTimer;
                    SchrodingersCat.currentTarget = null;
                },
                () => { return SchrodingersCat.isJackalButtonEnable() || SchrodingersCat.isJekyllAndHydeButtonEnable() || SchrodingersCat.isMoriartyButtonEnable(); },
                () => { return SchrodingersCat.currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => { schrodingersCatKillButton.Timer = schrodingersCatKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.Q
            );

            schrodingersCatSwitchButton = new CustomButton(
                () =>
                {
                    SchrodingersCat.showMenu();
                },
                () => { return SchrodingersCat.team == SchrodingersCat.Team.None && SchrodingersCat.canChooseImpostor && PlayerControl.LocalPlayer == SchrodingersCat.schrodingersCat && SchrodingersCat.tasksComplete(PlayerControl.LocalPlayer) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove; },
                () => { schrodingersCatSwitchButton.Timer = 0; },
                Morphling.getMorphSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("SwitchTeamText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            // Sherlock Investigate
            sherlockInvestigateButton = new CustomButton(
                () =>
                {
                    if (Sherlock.killPopup == null) Sherlock.killPopup = GameManagerCreator.Instance.HideAndSeekManagerPrefab.DeathPopupPrefab;
                    int count = 0;
                    string msg = "";
                    foreach (var item in Sherlock.killLog)
                    {
                        float distance = Vector3.Distance(item.Item2.Item2, PlayerControl.LocalPlayer.transform.position);
                        if (distance < Sherlock.investigateDistance)
                        {
                            var newPopUp = UnityEngine.Object.Instantiate(Sherlock.killPopup, __instance.transform.parent);
                            PlayerControl killer = Helpers.getPlayerById(item.Item1);
                            PlayerControl target = Helpers.getPlayerById(item.Item2.Item1);
                            string killerTeam = RoleInfo.GetRolesString(killer, useColors: true, showModifier: false, includeHidden: true);
                            newPopUp.gameObject.transform.GetChild(0).GetComponent<TextTranslatorTMP>().enabled = false;
                            newPopUp.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text = string.Format(ModTranslation.getString("sherlockMessage2"), killerTeam);
                            newPopUp.gameObject.transform.position += count % 4 * new Vector3(0, -1.2f, 0) + new Vector3(3f - (int)(count / 4) * 3.8f, -0.25f, __instance.MapButton.transform.localPosition.z - 600f);
                            newPopUp.ModShow(target, 0);
                            count++;
                            msg += $"{string.Format(ModTranslation.getString("sherlockMeeting"), target?.Data?.PlayerName, killerTeam)}\n";
                        }
                    }
                    if (!string.IsNullOrEmpty(msg))
                    {
                        msg = msg.TrimEnd('\n');
                        MeetingOverlayHolder.RegisterOverlay(TORGUIContextEngine.API.VerticalHolder(GUIAlignment.Left,
                        new TORGUIText(GUIAlignment.Left, TORGUIContextEngine.API.GetAttribute(AttributeAsset.OverlayTitle), new TranslateTextComponent("sherlockInfo")),
                        new TORGUIText(GUIAlignment.Left, TORGUIContextEngine.API.GetAttribute(AttributeAsset.OverlayContent), new RawTextComponent(msg)))
                        , MeetingOverlayHolder.IconsSprite[2], Sherlock.color);
                    }
                    if (count == 0)
                    {
                        Sherlock.investigateMessage(ModTranslation.getString("sherlockMessage1"), 7f, Color.white);
                    }
                    if (count >= 3)
                        _ = new StaticAchievementToken("sherlock.common1");
                    Sherlock.numUsed += 1;
                    sherlockInvestigateButton.Timer = sherlockInvestigateButton.MaxTimer;
                    SoundEffectsManager.play("sherlockInvestigate");
                },
                () => { return PlayerControl.LocalPlayer == Sherlock.sherlock && !Sherlock.sherlock.Data.IsDead; },
                () =>
                {
                    if (sherlockNumInvestigateText != null)
                    {
                        sherlockNumInvestigateText.text = $"{Sherlock.numUsed} / {Sherlock.getNumInvestigate()}";
                    }

                    return PlayerControl.LocalPlayer.CanMove && Sherlock.numUsed < Sherlock.getNumInvestigate();
                },
                () =>
                {
                    sherlockInvestigateButton.Timer = sherlockInvestigateButton.MaxTimer;
                    Sherlock.killLog.Clear();
                },
                Sherlock.getInvestigateIcon(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("InvestigateText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            sherlockNumInvestigateText = GameObject.Instantiate(sherlockInvestigateButton.actionButton.cooldownTimerText, sherlockInvestigateButton.actionButton.cooldownTimerText.transform.parent);
            sherlockNumInvestigateText.text = "";
            sherlockNumInvestigateText.enableWordWrapping = false;
            sherlockNumInvestigateText.transform.localScale = Vector3.one * 0.5f;
            sherlockNumInvestigateText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            // Sherlock Watch
            sherlockWatchButton = new CustomButton(
                () => { },
                () => { return PlayerControl.LocalPlayer == Sherlock.sherlock && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (sherlockNumKillTimerText != null)
                    {
                        sherlockNumKillTimerText.text = $"{Sherlock.killTimerCounter}";
                    }
                    if (sherlockWatchButton.Timer <= 0)
                    {
                        Sherlock.killTimerCounter += 1;
                        sherlockWatchButton.Timer = GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown;
                        Sherlock.acTokenChallenge.Value |= Sherlock.killTimerCounter >= 4 && !MeetingHud.Instance;
                    }

                    return PlayerControl.LocalPlayer.CanMove && Sherlock.numUsed < Sherlock.getNumInvestigate();
                },
                () => {
                    sherlockWatchButton.Timer = sherlockWatchButton.MaxTimer;
                    Sherlock.killTimerCounter = 0;
                },
                Sherlock.getWatchIcon(),
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.H,
                actionName: ModTranslation.getString("sherlockWatchButton")
            );
            sherlockNumKillTimerText = GameObject.Instantiate(sherlockWatchButton.actionButton.cooldownTimerText, sherlockWatchButton.actionButton.cooldownTimerText.transform.parent);
            sherlockNumKillTimerText.text = "";
            sherlockNumKillTimerText.enableWordWrapping = false;
            sherlockNumKillTimerText.transform.localScale = Vector3.one * 0.5f;
            sherlockNumKillTimerText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            cupidArrowButton = new CustomButton(
                () =>
                {
                    if (Helpers.checkSuspendAction(Cupid.cupid, Cupid.currentTarget)) return;

                    if (Cupid.lovers1 == null)
                    {
                        Cupid.lovers1 = Cupid.currentTarget;
                    }
                    else
                    {
                        if (Cupid.currentTarget != Cupid.lovers1)
                        {
                            Cupid.lovers2 = Cupid.currentTarget;
                        }
                    }
                    if (Cupid.lovers1 != null && Cupid.lovers2 != null)
                    {
                        _ = new StaticAchievementToken("cupid.common1");
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetCupidLovers, Hazel.SendOption.Reliable, -1);
                        writer.Write(Cupid.lovers1.PlayerId);
                        writer.Write(Cupid.lovers2.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.setCupidLovers(Cupid.lovers1.PlayerId, Cupid.lovers2.PlayerId);
                    }
                },
                () => { return Cupid.cupid != null && PlayerControl.LocalPlayer == Cupid.cupid && !PlayerControl.LocalPlayer.Data.IsDead && Cupid.lovers2 == null && Cupid.timeLeft > 0; },
                () =>
                {
                    if (cupidLoversText != null) cupidLoversText.text = Cupid.lovers1 == null ? "2" : "1";
                    return PlayerControl.LocalPlayer.CanMove && Cupid.currentTarget != null;
                },
                () => { cupidArrowButton.Timer = cupidArrowButton.MaxTimer; },
                Cupid.getArrowSprite(),
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("CupidArrowText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            cupidTimeRemainingText = GameObject.Instantiate(cupidArrowButton.actionButton.cooldownTimerText, __instance.transform);
            cupidTimeRemainingText.text = "";
            cupidTimeRemainingText.enableWordWrapping = false;
            cupidTimeRemainingText.transform.localScale = Vector3.one * 0.45f;
            cupidTimeRemainingText.transform.localPosition = cupidArrowButton.actionButton.cooldownTimerText.transform.parent.localPosition + new Vector3(-0.1f, 0.35f, 0f);
            ButtonEffect.SetMouseActionIcon(cupidArrowButton.actionButtonGameObject, true, ModTranslation.getString("buttonsCupidSuicide"), true, ButtonEffect.ActionIconType.InfoAction);
            cupidLoversText = cupidArrowButton.ShowUsesIcon(4);

            cupidShieldButton = new CustomButton(
                () =>
                {
                    if (Helpers.checkSuspendAction(Cupid.cupid, Cupid.currentTarget)) return;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetCupidShield, Hazel.SendOption.Reliable, -1);
                    writer.Write(Cupid.shieldTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.setCupidShield(Cupid.shieldTarget.PlayerId);
                },
                () => { return Cupid.isShieldOn && Cupid.cupid != null && PlayerControl.LocalPlayer == Cupid.cupid && !PlayerControl.LocalPlayer.Data.IsDead && Cupid.shielded == null; },
                () => { return Cupid.isShieldOn && Cupid.cupid != null && PlayerControl.LocalPlayer == Cupid.cupid && !PlayerControl.LocalPlayer.Data.IsDead && Cupid.shielded == null && Cupid.shieldTarget != null; },

                () => { cupidShieldButton.Timer = cupidShieldButton.MaxTimer; },
                Medic.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.G,
                buttonText: ModTranslation.getString("ShieldText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            // Akujo Honmei
            akujoHonmeiButton = new CustomButton(
                () =>
                {
                    if (Helpers.checkSuspendAction(Akujo.akujo, Akujo.currentTarget)) return;
                    _ = new StaticAchievementToken("akujo.common2");
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.AkujoSetHonmei, Hazel.SendOption.Reliable, -1);
                    writer.Write(Akujo.akujo.PlayerId);
                    writer.Write(Akujo.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.akujoSetHonmei(PlayerControl.LocalPlayer.PlayerId, Akujo.currentTarget.PlayerId);
                },
                () => { return PlayerControl.LocalPlayer == Akujo.akujo && !PlayerControl.LocalPlayer.Data.IsDead && Akujo.honmei == null && Akujo.timeLeft > 0; },
                () =>
                {                    
                    return PlayerControl.LocalPlayer == Akujo.akujo && !PlayerControl.LocalPlayer.Data.IsDead && Akujo.currentTarget != null && Akujo.honmei == null && Akujo.timeLeft > 0;
                },
                () => { akujoHonmeiButton.Timer = akujoHonmeiButton.MaxTimer; },
                Akujo.getHonmeiSprite(),
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("AkujoHonmeiText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            akujoHonmeiText = akujoHonmeiButton.ShowUsesIcon(4);
            akujoHonmeiText.text = "1";
            akujoTimeRemainingText = GameObject.Instantiate(akujoHonmeiButton.actionButton.cooldownTimerText, __instance.transform);
            akujoTimeRemainingText.text = "";
            akujoTimeRemainingText.enableWordWrapping = false;
            akujoTimeRemainingText.transform.localScale = Vector3.one * 0.45f;
            akujoTimeRemainingText.transform.localPosition = akujoHonmeiButton.actionButton.cooldownTimerText.transform.parent.localPosition + new Vector3(-0.1f, 0.35f, 0f);
            ButtonEffect.SetMouseActionIcon(akujoHonmeiButton.actionButtonGameObject, true, ModTranslation.getString("buttonsAkujoSuicide"), true, ButtonEffect.ActionIconType.InfoAction);

            // Akujo Keep
            akujoBackupButton = new CustomButton(
                () =>
                {
                    if (Helpers.checkSuspendAction(Akujo.akujo, Akujo.currentTarget)) return;
                    _ = new StaticAchievementToken("akujo.common1");
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.AkujoSetKeep, Hazel.SendOption.Reliable, -1);
                    writer.Write(Akujo.akujo.PlayerId);
                    writer.Write(Akujo.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.akujoSetKeep(PlayerControl.LocalPlayer.PlayerId, Akujo.currentTarget.PlayerId);
                },
                () => { return PlayerControl.LocalPlayer == Akujo.akujo && !PlayerControl.LocalPlayer.Data.IsDead && Akujo.keepsLeft > 0 && Akujo.timeLeft > 0; },
                () =>
                {
                    if (akujoBackupLeftText != null)
                    {
                        if (Akujo.keepsLeft > 0)
                            akujoBackupLeftText.text = Akujo.keepsLeft.ToString();
                        else
                            akujoBackupLeftText.text = "";
                    }
                    return PlayerControl.LocalPlayer == Akujo.akujo && !PlayerControl.LocalPlayer.Data.IsDead && Akujo.currentTarget != null && Akujo.keepsLeft > 0 && Akujo.timeLeft > 0;
                },
                () => { akujoBackupButton.Timer = akujoBackupButton.MaxTimer; },
                Akujo.getKeepSprite(),
                CustomButton.ButtonPositions.upperRowCenter,
                __instance,
                KeyCode.K,
                buttonText: ModTranslation.getString("AkujoBackupText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            akujoBackupLeftText = akujoBackupButton.ShowUsesIcon(4);

            // Mimic(Assistant) Morph
            mimicAMorphButton = new CustomButton(
                () =>
                {
                    if (!MimicA.isMorph)
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MimicMorph, Hazel.SendOption.Reliable, -1);
                        writer.Write(PlayerControl.LocalPlayer.PlayerId);
                        writer.Write(MimicK.mimicK.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.mimicMorph(PlayerControl.LocalPlayer.PlayerId, MimicK.mimicK.PlayerId);
                    }
                    else
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MimicResetMorph, Hazel.SendOption.Reliable, -1);
                        writer.Write(PlayerControl.LocalPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.mimicResetMorph(PlayerControl.LocalPlayer.PlayerId);
                    }
                    MimicA.acTokenCommon.Value++;
                },
                () => { return MimicA.mimicA != null && PlayerControl.LocalPlayer == MimicA.mimicA && !PlayerControl.LocalPlayer.Data.IsDead && MimicK.mimicK != null && !MimicK.mimicK.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Helpers.MushroomSabotageActive(); },
                () => { MimicA.acTokenCommon.Value = 0; },
                MimicA.getMorphSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                false
            );

            // Mimic(Assistant) Admin
            mimicAAdminButton = new CustomButton(
                () =>
                {
                    if (!MapBehaviour.Instance || !MapBehaviour.Instance.isActiveAndEnabled)
                    {
                        HudManager __instance = FastDestroyableSingleton<HudManager>.Instance;
                        __instance.InitMap();
                        MapBehaviour.Instance.ShowCountOverlay(allowedToMove: true, showLivePlayerPosition: true, includeDeadBodies: true);
                    }
                    _ = new StaticAchievementToken("mimicA.common1");
                },
                () => {
                    return MimicA.mimicA != null && PlayerControl.LocalPlayer == MimicA.mimicA && !PlayerControl.LocalPlayer.Data.IsDead
                    && MimicK.mimicK != null && !MimicK.mimicK.Data.IsDead;
                },
                () => { return PlayerControl.LocalPlayer.CanMove; },
                () => { },
                MimicA.getAdminSprite(),
                CustomButton.ButtonPositions.upperRowCenter,
                __instance,
                KeyCode.H,
                false,
                FastDestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Admin),
                abilityTexture: CustomButton.ButtonLabelType.AdminButton
            );

            // Ninja Stealth
            ninjaButton = new CustomButton(
                () =>
                {
                    if (ninjaButton.isEffectActive)
                    {
                        ninjaButton.Timer = 0;
                        return;
                    }

                    _ = new StaticAchievementToken("ninja.common1");
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.NinjaStealth, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(true);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.ninjaStealth(PlayerControl.LocalPlayer.PlayerId, true);
                    SoundEffectsManager.play("ninjaStealth");
                },
                () => { return Ninja.ninja != null && Ninja.ninja == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (ninjaButton.isEffectActive)
                    {
                        ninjaButton.buttonText = ModTranslation.getString("NinjaUnstealthText");
                    }
                    else
                    {
                        ninjaButton.buttonText = ModTranslation.getString("NinjaText");
                    }
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () =>
                {
                    ninjaButton.Timer = ninjaButton.MaxTimer = Ninja.stealthCooldown;
                    ninjaButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    ninjaButton.isEffectActive = false;
                    Ninja.ninja.SetKillTimer(GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown + (Ninja.penalized ? Ninja.killPenalty : 0f));
                },
                Ninja.getButtonSprite(),
                //new Vector3(-1.8f, -0.06f, 0),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                true,
                Ninja.stealthDuration,
                () =>
                {
                    ninjaButton.Timer = ninjaButton.MaxTimer = Ninja.stealthCooldown;

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.NinjaStealth, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(false);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.ninjaStealth(PlayerControl.LocalPlayer.PlayerId, false);

                    PlayerControl.LocalPlayer.SetKillTimer(Math.Max(PlayerControl.LocalPlayer.killTimer, Ninja.killPenalty));
                },
                buttonText: ModTranslation.getString("NinjaText")
            )
            {
                effectCancellable = true
            };

            // Kataomoi button
            kataomoiButton = new CustomButton(
                () => {
                    if (Kataomoi.canLove())
                    {
                        var murderAttemptResult = Helpers.checkMuderAttempt(Kataomoi.kataomoi, Kataomoi.currentTarget);
                        if (murderAttemptResult == MurderAttemptResult.SuppressKill) return;

                        MessageWriter winWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KataomoiWin, Hazel.SendOption.Reliable, -1);
                        AmongUsClient.Instance.FinishRpcImmediately(winWriter);
                        RPCProcedure.kataomoiWin();

                        if (murderAttemptResult == MurderAttemptResult.PerformKill)
                        {
                            _ = new StaticAchievementToken("kataomoi.challenge");
                            byte targetId = Kataomoi.currentTarget.PlayerId;
                            MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                            killWriter.Write(Kataomoi.kataomoi.Data.PlayerId);
                            killWriter.Write(targetId);
                            killWriter.Write(byte.MaxValue);
                            AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                            RPCProcedure.uncheckedMurderPlayer(Kataomoi.kataomoi.Data.PlayerId, targetId, Byte.MaxValue);
                        }

                        kataomoiButton.HasEffect = false;
                    }
                    else if (Kataomoi.currentTarget != null)
                    {
                        kataomoiButton.HasEffect = true;
                    }
                },
                () => { return Kataomoi.kataomoi != null && Kataomoi.kataomoi == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (Kataomoi.canLove()) {
                        kataomoiButton.actionButton.graphic.sprite = Kataomoi.getLoveSprite();
                        kataomoiButton.buttonText = ModTranslation.getString("KataomoiLoveText");
                    }

                    if (kataomoiButton.isEffectActive && Kataomoi.target != null && Kataomoi.target != Kataomoi.currentTarget)
                    {
                        kataomoiButton.Timer = 0f;
                        kataomoiButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && Kataomoi.currentTarget != null;
                },
                () => {
                    kataomoiButton.Timer = kataomoiButton.MaxTimer;
                    kataomoiButton.isEffectActive = false;
                    kataomoiButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Kataomoi.getStareSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.H,
                true,
                Kataomoi.stareDuration,
                () => {
                    _ = new StaticAchievementToken("kataomoi.common1");
                    Kataomoi.doStare();
                    kataomoiButton.Timer = Kataomoi.canLove() ? 0 : kataomoiButton.MaxTimer;
                },
                buttonText: ModTranslation.getString("KataomoiStareText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton,
                shakeOnEnd: false
            );

            // Kataomoi search button
            kataomoiSearchButton = new CustomButton(
                () => {
                    if (Kataomoi.kataomoi == null) return;
                    Kataomoi.doSearch();
                },
                () => { return Kataomoi.kataomoi != null && Kataomoi.kataomoi == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    return PlayerControl.LocalPlayer.CanMove && Kataomoi.target != null;
                },
                () => {
                    kataomoiSearchButton.Timer = kataomoiSearchButton.MaxTimer;
                    kataomoiSearchButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    kataomoiSearchButton.isEffectActive = false;
                    Kataomoi.resetSearch();
                },
                Kataomoi.getSearchSprite(),
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.G,
                true,
                Kataomoi.searchDuration,
                () => {
                    kataomoiSearchButton.Timer = kataomoiSearchButton.MaxTimer;
                    Kataomoi.resetSearch();
                },
                buttonText: ModTranslation.getString("KataomoiSearchText")
            );

            // Kataomoi stalking button
            kataomoiStalkingButton = new CustomButton(
                () => {
                    if (Kataomoi.kataomoi == null) return;

                    byte playerId = Kataomoi.kataomoi.Data.PlayerId;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KataomoiStalking, Hazel.SendOption.Reliable, -1);
                    writer.Write(playerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    RPCProcedure.kataomoiStalking(playerId);
                },
                () => { return Kataomoi.kataomoi != null && Kataomoi.kataomoi == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () => {
                    kataomoiStalkingButton.Timer = kataomoiStalkingButton.MaxTimer;
                    kataomoiStalkingButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    kataomoiStalkingButton.isEffectActive = false;
                },
                Kataomoi.getStalkingSprite(),
                CustomButton.ButtonPositions.upperRowCenter,
                __instance,
                KeyCode.F,
                true,
                Kataomoi.stalkingDuration,
                () => {
                    kataomoiStalkingButton.Timer = kataomoiStalkingButton.MaxTimer;
                },
                buttonText: ModTranslation.getString("KataomoiStalkText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            foxStealthButton = new CustomButton(
                () =>
                {
                    if (foxStealthButton.isEffectActive)
                    {
                        foxStealthButton.Timer = 0;
                        return;
                    }

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.FoxStealth, Hazel.SendOption.Reliable, -1);
                    writer.Write(true);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.foxStealth(true);
                },
                () => { return Fox.fox != null && PlayerControl.LocalPlayer == Fox.fox && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (foxStealthButton.isEffectActive)
                    {
                        foxStealthButton.buttonText = ModTranslation.getString("FoxUnstealthText");
                    }
                    else
                    {
                        foxStealthButton.buttonText = ModTranslation.getString("FoxStealthText");
                    }
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () =>
                {
                    foxStealthButton.Timer = foxStealthButton.MaxTimer = Fox.stealthCooldown;
                    foxStealthButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    foxStealthButton.isEffectActive = false;
                },
                Fox.getHideButtonSprite(),
                CustomButton.ButtonPositions.upperRowCenter,
                __instance,
                KeyCode.F,
                true,
                Fox.stealthDuration,
                () =>
                {
                    foxStealthButton.Timer = foxStealthButton.MaxTimer = Fox.stealthCooldown;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.FoxStealth, Hazel.SendOption.Reliable, -1);
                    writer.Write(false);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.foxStealth(false);
                },
                buttonText: ModTranslation.getString("FoxStealthText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            )
            {
                effectCancellable = true
            };

            foxRepairButton = new CustomButton(
                () =>
                {
                    bool sabotageActive = false;
                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                        if (task.TaskType is TaskTypes.FixLights or TaskTypes.RestoreOxy or TaskTypes.ResetReactor or TaskTypes.ResetSeismic or TaskTypes.FixComms or TaskTypes.StopCharles)
                            sabotageActive = true;
                    if (!sabotageActive) return;

                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                    {
                        if (task.TaskType == TaskTypes.FixLights)
                        {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.EngineerFixLights, Hazel.SendOption.Reliable, -1);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.engineerFixLights();
                        }
                        else if (task.TaskType == TaskTypes.RestoreOxy)
                        {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.LifeSupp, 0 | 64);
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.LifeSupp, 1 | 64);
                        }
                        else if (task.TaskType == TaskTypes.ResetReactor)
                        {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Reactor, 16);
                        }
                        else if (task.TaskType == TaskTypes.ResetSeismic)
                        {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Laboratory, 16);
                        }
                        else if (task.TaskType == TaskTypes.FixComms)
                        {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Comms, 16 | 0);
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Comms, 16 | 1);
                        }
                        else if (task.TaskType == TaskTypes.StopCharles)
                        {
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Reactor, 0 | 16);
                            MapUtilities.CachedShipStatus.RpcUpdateSystem(SystemTypes.Reactor, 1 | 16);
                        }
                    }
                    Fox.numRepair -= 1;
                },
                () => { return PlayerControl.LocalPlayer == Fox.fox && !PlayerControl.LocalPlayer.Data.IsDead && Fox.numRepair > 0; },
                () =>
                {
                    if (foxRepairText != null) foxRepairText.text = Fox.numRepair.ToString();
                    bool sabotageActive = false;
                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                        if (task.TaskType is TaskTypes.FixLights or TaskTypes.RestoreOxy or TaskTypes.ResetReactor or TaskTypes.ResetSeismic or TaskTypes.FixComms or TaskTypes.StopCharles)
                            sabotageActive = true;
                    return sabotageActive && Fox.numRepair > 0 && PlayerControl.LocalPlayer.CanMove;
                },
                () => { foxRepairButton.Timer = foxRepairButton.MaxTimer = 0f; },
                Fox.getRepairButtonSprite(),
                CustomButton.ButtonPositions.upperRowRight,
                __instance,
                KeyCode.G,
                abilityTexture: CustomButton.ButtonLabelType.UseButton,
                buttonText: ModTranslation.getString("RepairText")
            );
            foxRepairText = foxRepairButton.ShowUsesIcon(4);

            foxImmoralistButton = new CustomButton(
                () =>
                {
                    if (Helpers.checkSuspendAction(Fox.fox, Fox.currentTarget)) return;
                    _ = new StaticAchievementToken("fox.common1");
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.FoxCreatesImmoralist, Hazel.SendOption.Reliable, -1);
                    writer.Write(Fox.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.foxCreatesImmoralist(Fox.currentTarget.PlayerId);
                },
                () => { return Fox.canCreateImmoralist && PlayerControl.LocalPlayer == Fox.fox && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Fox.canCreateImmoralist && Fox.currentTarget != null && PlayerControl.LocalPlayer.CanMove; },
                () => { foxImmoralistButton.Timer = foxImmoralistButton.MaxTimer = 20f; },
                Fox.getImmoralistButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.I,
                abilityTexture: CustomButton.ButtonLabelType.UseButton,
                buttonText: ModTranslation.getString("FoxImmoralistText")
            );

            immoralistButton = new CustomButton(
                () =>
                {
                    _ = new StaticAchievementToken("immoralist.challenge");
                    byte targetId = PlayerControl.LocalPlayer.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SerialKillerSuicide, Hazel.SendOption.Reliable, -1); killWriter.Write(targetId);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.serialKillerSuicide(targetId);
                },
                () => { return PlayerControl.LocalPlayer == Immoralist.immoralist && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return true; },
                () =>
                {
                    immoralistButton.Timer = immoralistButton.MaxTimer = 20f;
                },
                Immoralist.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("ImmoralistSuicideText")
            );

            // Yoyo button
            yoyoButton = new CustomButton(
                () => {
                    var pos = PlayerControl.LocalPlayer.transform.position;
                    byte[] buff = new byte[sizeof(float) * 2];
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                    if (Yoyo.markedLocation == null)
                    {
                        TheOtherRolesPlugin.Logger.LogMessage($"marked location is null in button press");
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.YoyoMarkLocation, Hazel.SendOption.Reliable);
                        writer.WriteBytesAndSize(buff);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.yoyoMarkLocation(buff);
                        SoundEffectsManager.play("tricksterPlaceBox");
                        yoyoButton.Sprite = Yoyo.getBlinkButtonSprite();
                        yoyoButton.Timer = 10f;
                        yoyoButton.HasEffect = false;
                        yoyoButton.buttonText = ModTranslation.getString("BlinkText");
                    }
                    else
                    {
                        TheOtherRolesPlugin.Logger.LogMessage("in else for some reason");
                        // Jump to location
                        TheOtherRolesPlugin.Logger.LogMessage($"trying to blink!");
                        var exit = (Vector3)Yoyo.markedLocation;
                        if (SubmergedCompatibility.IsSubmerged)
                        {
                            SubmergedCompatibility.ChangeFloor(exit.y > -7);
                        }
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.YoyoBlink, Hazel.SendOption.Reliable);
                        writer.Write(Byte.MaxValue);
                        writer.WriteBytesAndSize(buff);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.yoyoBlink(true, buff);
                        yoyoButton.EffectDuration = Yoyo.blinkDuration;
                        yoyoButton.Timer = 10f;
                        yoyoButton.HasEffect = true;
                        yoyoButton.buttonText = ModTranslation.getString("ReturnText");
                        SoundEffectsManager.play("morphlingMorph");
                    }
                },
                () => { return Yoyo.yoyo != null && Yoyo.yoyo == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove; },
                () => {
                    if (Yoyo.markStaysOverMeeting)
                    {
                        yoyoButton.Timer = 10f;
                    }
                    else
                    {
                        Yoyo.markedLocation = null;
                        yoyoButton.Timer = yoyoButton.MaxTimer;
                        yoyoButton.Sprite = Yoyo.getMarkButtonSprite();
                        yoyoButton.buttonText = ModTranslation.getString("MarkText");
                    }
                },
                Yoyo.getMarkButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                false,
                Yoyo.blinkDuration,
                () => {
                    if (TransportationToolPatches.isUsingTransportation(Yoyo.yoyo))
                    {
                        yoyoButton.Timer = 0.5f;
                        yoyoButton.DeputyTimer = 0.5f;
                        yoyoButton.isEffectActive = true;
                        yoyoButton.actionButton.cooldownTimerText.color = new Color(0F, 0.8F, 0F);
                        return;
                    }
                    else if (Yoyo.yoyo.inVent)
                    {
                        __instance.ImpostorVentButton.DoClick();
                    }

                    // jump back!
                    var pos = PlayerControl.LocalPlayer.transform.position;
                    byte[] buff = new byte[sizeof(float) * 2];
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));
                    var exit = (Vector3)Yoyo.markedLocation;
                    if (SubmergedCompatibility.IsSubmerged)
                    {
                        SubmergedCompatibility.ChangeFloor(exit.y > -7);
                    }
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.YoyoBlink, Hazel.SendOption.Reliable);
                    writer.Write((byte)0);
                    writer.WriteBytesAndSize(buff);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.yoyoBlink(false, buff);

                    yoyoButton.Timer = yoyoButton.MaxTimer;
                    yoyoButton.isEffectActive = false;
                    yoyoButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    yoyoButton.HasEffect = false;
                    yoyoButton.Sprite = Yoyo.getMarkButtonSprite();
                    yoyoButton.buttonText = ModTranslation.getString("MarkText");
                    SoundEffectsManager.play("morphlingMorph");
                    if (Minigame.Instance)
                    {
                        Minigame.Instance.Close();
                    }
                    _ = new StaticAchievementToken("yoyo.challenge");
                },
                buttonText: ModTranslation.getString("MarkText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton,
                actionName: ModTranslation.getString("yoyoButton")
            );

            // Serial Killer Suicide Countdown
            serialKillerButton = new CustomButton(
                () => { },
                () => { return SerialKiller.serialKiller != null && PlayerControl.LocalPlayer == SerialKiller.serialKiller && !PlayerControl.LocalPlayer.Data.IsDead && SerialKiller.isCountDown; },
                () => { return true; },
                () =>
                {
                    if (PlayerControl.LocalPlayer == SerialKiller.serialKiller)
                    {
                        SerialKiller.serialKiller.SetKillTimerUnchecked(SerialKiller.killCooldown);
                        if (SerialKiller.resetTimer)
                        {
                            serialKillerButton.Timer = SerialKiller.suicideTimer;
                        }
                    }
                },
                SerialKiller.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.None,
                true,
                SerialKiller.suicideTimer,
                () =>
                {
                    _ = new StaticAchievementToken("serialKiller.another1");
                    byte targetId = SerialKiller.serialKiller.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SerialKillerSuicide, Hazel.SendOption.Reliable, -1); killWriter.Write(targetId);
                    killWriter.Write(targetId);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.serialKillerSuicide(targetId);
                },
                buttonText: ModTranslation.getString("serialKillerSuicideText"),
                isSuicide: true,
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            )
            {
                isEffectActive = true
            };
            ButtonEffect.SetMouseActionIcon(serialKillerButton.actionButtonGameObject, true, ModTranslation.getString("buttonsNormalSuicide"), false, ButtonEffect.ActionIconType.InfoAction);

            // Evil Tracker track
            evilTrackerButton = new CustomButton(
                () => {
                    if (Helpers.checkSuspendAction(EvilTracker.evilTracker, EvilTracker.evilTracker)) return;
                    _ = new StaticAchievementToken("evilTracker.common1");
                    EvilTracker.target = EvilTracker.currentTarget;
                },
                () => { return EvilTracker.target == null && PlayerControl.LocalPlayer == EvilTracker.evilTracker && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return EvilTracker.currentTarget != null && EvilTracker.target == null && PlayerControl.LocalPlayer.CanMove; },
                () =>
                {
                    // Reset here, else the task option would be useless
                    if (EvilTracker.resetTargetAfterMeeting) EvilTracker.target = EvilTracker.futureTarget;
                    EvilTracker.futureTarget = null;
                    evilTrackerButton.Timer = evilTrackerButton.MaxTimer;
                },
                EvilTracker.getEvilTrackerButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("TrackerText")
            );

            // Vulture Eat
            vultureEatButton = new CustomButton(
                () => {
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer.MaxReportDistance, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody") {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component && !component.Reported) {
                                Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                Vector2 truePosition2 = component.TruePosition;
                                if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false)) {
                                    NetworkedPlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);
                                    _ = new StaticAchievementToken("vulture.common1");
                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CleanBody, Hazel.SendOption.Reliable, -1);
                                    writer.Write(playerInfo.PlayerId);
                                    writer.Write(Vulture.vulture.PlayerId);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.cleanBody(playerInfo.PlayerId, Vulture.vulture.PlayerId);

                                    Vulture.cooldown = vultureEatButton.Timer = vultureEatButton.MaxTimer;
                                    SoundEffectsManager.play("vultureEat");
                                    break;
                                }
                            }
                        }
                    }
                },
                () => { return Vulture.vulture != null && Vulture.vulture == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (vultureRemainingText != null) vultureRemainingText.text = (Vulture.vultureNumberToWin - Vulture.eatenBodies).ToString();
                    return __instance.ReportButton.graphic.color == Palette.EnabledColor && PlayerControl.LocalPlayer.CanMove;
                },
                () => { vultureEatButton.Timer = vultureEatButton.MaxTimer; },
                Vulture.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowCenter,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("VultureText")
            );
            vultureRemainingText = vultureEatButton.ShowUsesIcon(2);

            // EvilHacker button
            evilHackerButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == EvilHacker.evilHacker)
                        EvilHacker.acTokenChallenge.Value.admin = true;
                    if (!MapBehaviour.Instance || !MapBehaviour.Instance.isActiveAndEnabled)
                    {
                        HudManager __instance = FastDestroyableSingleton<HudManager>.Instance;
                        __instance.InitMap();
                        MapBehaviour.Instance.ShowCountOverlay(allowedToMove: true, showLivePlayerPosition: true, includeDeadBodies: true);
                    }
                },
                () => {
                    return ((EvilHacker.evilHacker != null &&
                      EvilHacker.evilHacker == PlayerControl.LocalPlayer) || EvilHacker.isInherited()) &&
                      !PlayerControl.LocalPlayer.Data.IsDead && !mimicAAdminButton.HasButton();
                },
                () =>
                {
                    if (EvilHacker.isInherited())
                        evilHackerButton.PositionOffset = CustomButton.ButtonPositions.lowerRowCenter;
                    else
                        evilHackerButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () => { },
                EvilHacker.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.H,
                false,
                FastDestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Admin),
                abilityTexture: CustomButton.ButtonLabelType.AdminButton
            );

            // Medium button
            mediumButton = new CustomButton(
                () => {
                    if (Medium.target != null) {
                        Medium.soulTarget = Medium.target;
                        mediumButton.HasEffect = true;
                        SoundEffectsManager.play("mediumAsk");
                    }
                },
                () => { return Medium.medium != null && Medium.medium == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (mediumButton.isEffectActive && Medium.target != Medium.soulTarget) {
                        Medium.soulTarget = null;
                        mediumButton.Timer = 0f;
                        mediumButton.isEffectActive = false;
                    }
                    return Medium.target != null && PlayerControl.LocalPlayer.CanMove;
                },
                () => {
                    mediumButton.Timer = mediumButton.MaxTimer;
                    mediumButton.isEffectActive = false;
                    Medium.soulTarget = null;
                },
                Medium.getQuestionSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                true,
                Medium.duration,
                () => {
                    Medium.acTokenCommon.Value++;
                    mediumButton.Timer = mediumButton.MaxTimer;
                    if (Medium.target == null || Medium.target.player == null) return;
                    string msg = Medium.getInfo(Medium.target.player, Medium.target.killerIfExisting, Medium.target.deathReason);
                    FastDestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, msg, false);

                    // Ghost Info
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareGhostInfo, Hazel.SendOption.Reliable, -1);
                    writer.Write(Medium.target.player.PlayerId);
                    writer.Write((byte)RPCProcedure.GhostInfoTypes.MediumInfo);
                    writer.Write(msg);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    // Remove soul
                    if (Medium.oneTimeUse) {
                        float closestDistance = float.MaxValue;
                        SpriteRenderer target = null;

                        foreach ((DeadPlayer db, Vector3 ps) in Medium.deadBodies) {
                            if (db == Medium.target) {
                                Tuple<DeadPlayer, Vector3> deadBody = Tuple.Create(db, ps);
                                Medium.deadBodies.Remove(deadBody);
                                break;
                            }

                        }
                        foreach (SpriteRenderer rend in Medium.souls) {
                            float distance = Vector2.Distance(rend.transform.position, PlayerControl.LocalPlayer.GetTruePosition());
                            if (distance < closestDistance) {
                                closestDistance = distance;
                                target = rend;
                            }
                        }

                        FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(5f, new Action<float>((p) => {
                            if (target != null) {
                                var tmp = target.color;
                                tmp.a = Mathf.Clamp01(1 - p);
                                target.color = tmp;
                            }
                            if (p == 1f && target != null && target.gameObject != null) UnityEngine.Object.Destroy(target.gameObject);
                        })));

                        Medium.souls.Remove(target);
                    }
                    SoundEffectsManager.stop("mediumAsk");
                },
                buttonText: ModTranslation.getString("MediumText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton,
                shakeOnEnd: false
            );

            doomsayerButton = new(
                () =>
                {
                    if (Doomsayer.currentTarget != null)
                    {
                        if (Helpers.checkSuspendAction(Doomsayer.doomsayer, Doomsayer.currentTarget)) return;
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.DoomsayerObserve, Hazel.SendOption.Reliable, -1);
                        writer.Write(Doomsayer.currentTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.doomsayerObserve(Doomsayer.currentTarget.PlayerId);
                        Doomsayer.usesLeft--;
                        doomsayerButton.Timer = doomsayerButton.MaxTimer;
                        _ = new StaticAchievementToken("doomsayer.common1");
                    }
                },
                () => { return PlayerControl.LocalPlayer == Doomsayer.doomsayer && Doomsayer.doomsayer != null && !PlayerControl.LocalPlayer.Data.IsDead && Doomsayer.usesLeft > 0; },
                () =>
                {
                    if (doomsayerUsesText != null) doomsayerUsesText.text = Doomsayer.usesLeft.ToString();
                    return PlayerControl.LocalPlayer.CanMove && Doomsayer.currentTarget != null;
                },
                () =>
                {
                    Doomsayer.observed = null;
                    doomsayerButton.Timer = doomsayerButton.MaxTimer = Doomsayer.cooldown;
                },
                Doomsayer.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("DoomsayerText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            doomsayerUsesText = doomsayerButton.ShowUsesIcon(1);

            // Fortune Teller button
            fortuneTellerButtons = new List<CustomButton>();
            Vector3 fortuneTellerCalcPos(byte index)
            {
                //int adjIndex = index < PlayerControl.LocalPlayer.PlayerId ? index % 15 : (index > 15 ? index % 15 : index - 1);
                int adjIndex = 0;
                if (index >= PlayerControl.LocalPlayer.PlayerId) index--;
                adjIndex = index % 14;
                //return new Vector3(-0.25f, -0.15f, 0) + Vector3.right * adjIndex * 0.55f;
                return new Vector3(0.95f, -0.15f, -61f) + Vector3.right * adjIndex * 0.55f;
            }

            Action fortuneTellerButtonOnClick(byte index)
            {
                return () =>
                {
                    if (PlayerControl.LocalPlayer.CanMove && FortuneTeller.numUsed < 1 && FortuneTeller.canDivine(index))
                    {
                        PlayerControl p = Helpers.playerById(index);
                        FortuneTeller.divine(p);
                    }
                };
            };

            Func<bool> fortuneTellerHasButton(byte index)
            {
                return () =>
                {
                    return PlayerControl.LocalPlayer == FortuneTeller.fortuneTeller;
                };
            }

            void setButtonPos(byte index)
            {
                Vector3 pos = fortuneTellerCalcPos(index);
                Vector3 scale = new(0.4f, 0.5f, 1.0f);

                Vector3 iconBase = new Vector3(-0.82f, 0.19f, 0) + IntroCutsceneOnDestroyPatch.bottomLeft;

                fortuneTellerButtons[index].PositionOffset = pos;
                fortuneTellerButtons[index].actionButton.transform.localScale = scale;
                TORMapOptions.playerIcons[index].transform.localPosition = pos + iconBase;
            }

            void setIconPos(byte index, bool transparent)
            {
                TORMapOptions.playerIcons[index].transform.localScale = Vector3.one * 0.25f;
                TORMapOptions.playerIcons[index].gameObject.SetActive(PlayerControl.LocalPlayer.CanMove);
                TORMapOptions.playerIcons[index].setSemiTransparent(transparent);
            }

            Func<bool> fortuneTellerCouldUse(byte index)
            {
                return () =>
                {
                    int adjustedIndex = index < PlayerControl.LocalPlayer.PlayerId ? index : index - 1;
                    //　占い師以外の場合、リソースがない場合はボタンを表示しない
                    if (!TORMapOptions.playerIcons.ContainsKey(index) ||
                        PlayerControl.LocalPlayer != FortuneTeller.fortuneTeller ||
                        PlayerControl.LocalPlayer.Data.IsDead ||
                        PlayerControl.LocalPlayer.PlayerId == index ||
                        !FortuneTeller.isCompletedNumTasks(FortuneTeller.fortuneTeller) ||
                        FortuneTeller.numUsed >= 1 || (adjustedIndex < 14 && FortuneTeller.pageIndex == 2) ||
                        (adjustedIndex >= 14 && FortuneTeller.pageIndex == 1))
                    {
                        if (TORMapOptions.playerIcons.ContainsKey(index)) TORMapOptions.playerIcons[index].gameObject.SetActive(false);
                        if (fortuneTellerButtons.Count > index) fortuneTellerButtons[index].setActive(false);
                        return false;
                    }

                    // ボタンの位置を変更
                    setButtonPos(index);

                    // ボタンにテキストを設定
                    bool status = true;
                    if (FortuneTeller.playerStatus.ContainsKey(index))
                    {
                        status = FortuneTeller.playerStatus[index];
                    }

                    fortuneTellerButtons[index].showButtonText = true;
                    if (status)
                    {
                        var progress = FortuneTeller.progress.ContainsKey(index) ? FortuneTeller.progress[index] : 0f;
                        fortuneTellerButtons[index].buttonText = $"{progress:0.0}/{FortuneTeller.duration:0.0}";
                    }
                    else
                    {
                        fortuneTellerButtons[index].buttonText = ModTranslation.getString("fortuneTellerDead");
                    }

                    // アイコンの位置と透明度を変更
                    setIconPos(index, !FortuneTeller.canDivine(index));

                    TORMapOptions.playerIcons[index].gameObject.SetActive(!(MapBehaviour.Instance && MapBehaviour.Instance.IsOpen) &&
                      !MeetingHud.Instance &&
                      !ExileController.Instance && PlayerControl.LocalPlayer.CanMove);
                    fortuneTellerButtons[index].setActive(!(MapBehaviour.Instance && MapBehaviour.Instance.IsOpen) &&
                      !MeetingHud.Instance &&
                      !ExileController.Instance && PlayerControl.LocalPlayer.CanMove);
                    return PlayerControl.LocalPlayer.CanMove && FortuneTeller.numUsed < 1 && FortuneTeller.canDivine(index);
                };
            }

            for (byte i = 0; i < 24; i++)
            {
                CustomButton fortuneTellerButton = new(
                    // Action OnClick
                    fortuneTellerButtonOnClick(i),
                    // bool HasButton
                    fortuneTellerHasButton(i),
                    // bool CouldUse
                    fortuneTellerCouldUse(i),
                    // Action OnMeetingEnds
                    () => { },
                    // sprite
                    null,
                    // position
                    Vector3.zero,
                    __instance,
                    // keyboard shortcut
                    KeyCode.None,
                    true,
                    abilityTexture: CustomButton.ButtonLabelType.UseButton,
                    isSuicide: true
                )
                {
                    Timer = 0.0f,
                    MaxTimer = 0.0f
                };

                fortuneTellerButtons.Add(fortuneTellerButton);
            }

            fortuneTellerLeftButton = new CustomButton(
                () =>
                {
                    FortuneTeller.pageIndex = 1;
                },
                () => {
                    return PlayerControl.LocalPlayer == FortuneTeller.fortuneTeller && !PlayerControl.LocalPlayer.Data.IsDead &&
                    FortuneTeller.pageIndex == 2 && FortuneTeller.isCompletedNumTasks(PlayerControl.LocalPlayer) && FortuneTeller.numUsed < 1 &&
                    TORMapOptions.playerIcons.Count >= 16;
                },
                () =>
                {
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () => { },
                FortuneTeller.getLeftButtonSprite(),
                new Vector3(0.5f, -0.15f, -61f),
                __instance,
                KeyCode.None,
                true
            );

            fortuneTellerRightButton = new CustomButton(
                () =>
                {
                    FortuneTeller.pageIndex = 2;
                },
                () => {
                    return PlayerControl.LocalPlayer == FortuneTeller.fortuneTeller && !PlayerControl.LocalPlayer.Data.IsDead &&
                    FortuneTeller.pageIndex == 1 && FortuneTeller.isCompletedNumTasks(PlayerControl.LocalPlayer) && FortuneTeller.numUsed < 1 &&
                    TORMapOptions.playerIcons.Count >= 16;
                },
                () =>
                {
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () => { },
                FortuneTeller.getRightButtonSprite(),
                new Vector3(8.6f, -0.15f, -70f),
                __instance,
                KeyCode.None,
                true
            );

            // Pursuer button
            pursuerButton = new CustomButton(
                () => {
                    if (Pursuer.target != null) {
                        if (Helpers.checkSuspendAction(Pursuer.pursuer, Pursuer.target)) return;
                        if (Pursuer.target.Data.Role.IsImpostor) _ = new StaticAchievementToken("pursuer.common1");
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetBlanked, Hazel.SendOption.Reliable, -1);
                        writer.Write(Pursuer.target.PlayerId);
                        writer.Write(Byte.MaxValue);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.setBlanked(Pursuer.target.PlayerId, Byte.MaxValue);

                        Pursuer.target = null;

                        Pursuer.blanks++;
                        pursuerButton.Timer = pursuerButton.MaxTimer;
                        SoundEffectsManager.play("pursuerBlank");
                    }

                },
                () => { return Pursuer.pursuer != null && Pursuer.pursuer == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Pursuer.blanks < Pursuer.blanksNumber; },
                () => {
                    if (pursuerButtonBlanksText != null) pursuerButtonBlanksText.text = $"{Pursuer.blanksNumber - Pursuer.blanks}";

                    return Pursuer.blanksNumber > Pursuer.blanks && PlayerControl.LocalPlayer.CanMove && Pursuer.target != null;
                },
                () => { pursuerButton.Timer = pursuerButton.MaxTimer; },
                Pursuer.getTargetSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                buttonText: ModTranslation.getString("PursuerText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            // Pursuer button blanks left
            pursuerButtonBlanksText = pursuerButton.ShowUsesIcon(1);


            // Witch Spell button
            witchSpellButton = new CustomButton(
                () => {
                    if (Helpers.checkSuspendAction(Witch.witch, Witch.currentTarget)) return;
                    if (Witch.currentTarget != null) {
                        Witch.spellCastingTarget = Witch.currentTarget;
                        SoundEffectsManager.play("witchSpell");
                    }
                },
                () => { return Witch.witch != null && Witch.witch == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (witchSpellButton.isEffectActive && Witch.spellCastingTarget != Witch.currentTarget) {
                        Witch.spellCastingTarget = null;
                        witchSpellButton.Timer = 0f;
                        witchSpellButton.isEffectActive = false;
                    }
                    return PlayerControl.LocalPlayer.CanMove && Witch.currentTarget != null;
                },
                () => {
                    witchSpellButton.Timer = witchSpellButton.MaxTimer;
                    witchSpellButton.isEffectActive = false;
                    Witch.spellCastingTarget = null;
                },
                Witch.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                true,
                Witch.spellCastingDuration,
                () => {
                    if (Witch.spellCastingTarget == null) return;
                    MurderAttemptResult attempt = Helpers.checkMuderAttempt(Witch.witch, Witch.spellCastingTarget);
                    if (attempt == MurderAttemptResult.PerformKill) {
                        _ = new StaticAchievementToken("witch.common1");
                        Witch.acTokenChallenge.Value++;
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetFutureSpelled, Hazel.SendOption.Reliable, -1);
                        writer.Write(Witch.currentTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.setFutureSpelled(Witch.currentTarget.PlayerId);
                    }
                    if (attempt is MurderAttemptResult.BlankKill or MurderAttemptResult.PerformKill) {
                        Witch.currentCooldownAddition += Witch.cooldownAddition;
                        witchSpellButton.MaxTimer = Witch.cooldown + Witch.currentCooldownAddition;
                        Patches.PlayerControlFixedUpdatePatch.miniCooldownUpdate();  // Modifies the MaxTimer if the witch is the mini
                        witchSpellButton.Timer = witchSpellButton.MaxTimer;
                        if (Witch.triggerBothCooldowns) {
                            float multiplier = (Mini.mini != null && PlayerControl.LocalPlayer == Mini.mini) ? (Mini.isGrownUp() ? 0.66f : 2f) : 1f;
                            Witch.witch.killTimer = GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown * multiplier;
                        }
                    } else {
                        witchSpellButton.Timer = 0f;
                    }
                    Witch.spellCastingTarget = null;
                },
                buttonText: ModTranslation.getString("WitchText"),
                shakeOnEnd: false
            );

            sprintButton = new CustomButton(
                () =>
                {
                    if (sprintButton.isEffectActive)
                    {
                        sprintButton.Timer = 0;
                        return;
                    }

                    _ = new StaticAchievementToken("sprinter.common1");
                    if (Sprinter.acTokenMove != null) Sprinter.acTokenMove.Value.pos = PlayerControl.LocalPlayer.GetTruePosition();

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SprinterSprint, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(true);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.sprinterSprint(PlayerControl.LocalPlayer.PlayerId, true);
                    SoundEffectsManager.play("ninjaStealth");
                },
                () => { return PlayerControl.LocalPlayer == Sprinter.sprinter && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (sprintButton.isEffectActive)
                    {
                        sprintButton.buttonText = ModTranslation.getString("SprinterStopText");
                    }
                    else
                    {
                        sprintButton.buttonText = ModTranslation.getString("SprintText");
                    }
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () =>
                {
                    sprintButton.Timer = sprintButton.MaxTimer = Sprinter.sprintCooldown;
                    sprintButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    sprintButton.isEffectActive = false;
                },
                Sprinter.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                true,
                Sprinter.sprintDuration,
                () =>
                {
                    sprintButton.Timer = sprintButton.MaxTimer = Sprinter.sprintCooldown;
                    if (Sprinter.acTokenMove != null) Sprinter.acTokenMove.Value.cleared |= PlayerControl.LocalPlayer.GetTruePosition().Distance(Sprinter.acTokenMove.Value.pos) > 15f;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SprinterSprint, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(false);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.sprinterSprint(PlayerControl.LocalPlayer.PlayerId, false);
                },
                abilityTexture: CustomButton.ButtonLabelType.UseButton,
                buttonText: ModTranslation.getString("SprintText")
            )
            {
                effectCancellable = true
            };

            // Assassin mark and assassinate button 
            assassinButton = new CustomButton(
                () => {
                    MessageWriter writer;
                    if (Assassin.assassinMarked != null) {
                        // Murder attempt with teleport
                        MurderAttemptResult attempt = Helpers.checkMuderAttempt(Assassin.assassin, Assassin.assassinMarked);
                        if (attempt == MurderAttemptResult.ReverseKill)
                        {
                            Helpers.checkMurderAttemptAndKill(Veteran.veteran, Assassin.assassin);
                            return;
                        }
                        if (attempt == MurderAttemptResult.PerformKill) {
                            _ = new StaticAchievementToken("assassin.common1");
                            Assassin.acTokenChallenge.Value.markKill = true;
                            // Create first trace before killing
                            var pos = PlayerControl.LocalPlayer.transform.position;
                            byte[] buff = new byte[sizeof(float) * 2];
                            Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                            Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                            writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceAssassinTrace, Hazel.SendOption.Reliable);
                            writer.WriteBytesAndSize(buff);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.placeAssassinTrace(buff);

                            /*MessageWriter invisibleWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetInvisible, Hazel.SendOption.Reliable, -1);
                            invisibleWriter.Write(Assassin.assassin.PlayerId);
                            invisibleWriter.Write(byte.MinValue);
                            AmongUsClient.Instance.FinishRpcImmediately(invisibleWriter);
                            RPCProcedure.setInvisible(Assassin.assassin.PlayerId, byte.MinValue);*/

                            // Perform Kill
                            MessageWriter writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                            writer2.Write(PlayerControl.LocalPlayer.PlayerId);
                            writer2.Write(Assassin.assassinMarked.PlayerId);
                            writer2.Write(byte.MaxValue);
                            AmongUsClient.Instance.FinishRpcImmediately(writer2);
                            if (SubmergedCompatibility.IsSubmerged)
                            {
                                SubmergedCompatibility.ChangeFloor(Assassin.assassinMarked.transform.localPosition.y > -7);
                            }
                            RPCProcedure.uncheckedMurderPlayer(PlayerControl.LocalPlayer.PlayerId, Assassin.assassinMarked.PlayerId, byte.MaxValue);

                            // Create Second trace after killing
                            pos = Assassin.assassinMarked.transform.position;
                            buff = new byte[sizeof(float) * 2];
                            Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                            Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                            MessageWriter writer3 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceAssassinTrace, Hazel.SendOption.Reliable);
                            writer3.WriteBytesAndSize(buff);
                            writer3.EndMessage();
                            RPCProcedure.placeAssassinTrace(buff);
                        }

                        if (attempt is MurderAttemptResult.BlankKill or MurderAttemptResult.PerformKill) {
                            assassinButton.Timer = assassinButton.MaxTimer;
                            Assassin.assassin.killTimer = GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown;
                        } else if (attempt == MurderAttemptResult.SuppressKill) {
                            assassinButton.Timer = 0f;
                        }
                        Assassin.assassinMarked = null;
                        return;
                    } 
                    if (Assassin.currentTarget != null) {
                        if (Helpers.checkSuspendAction(Assassin.assassin, Assassin.currentTarget)) return;
                        Assassin.assassinMarked = Assassin.currentTarget;
                        assassinButton.Timer = 5f;
                        SoundEffectsManager.play("warlockCurse");

                        // Ghost Info
                        writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareGhostInfo, Hazel.SendOption.Reliable, -1);
                        writer.Write(PlayerControl.LocalPlayer.PlayerId);
                        writer.Write((byte)RPCProcedure.GhostInfoTypes.AssassinMarked);
                        writer.Write(Assassin.assassinMarked.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                    }
                },
                () => { return Assassin.assassin != null && Assassin.assassin == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {  // CouldUse
                    assassinButton.Sprite = Assassin.assassinMarked != null ? Assassin.getKillButtonSprite() : Assassin.getMarkButtonSprite(); 
                    return (Assassin.currentTarget != null || (Assassin.assassinMarked != null && !TransportationToolPatches.isUsingTransportation(Assassin.assassinMarked))) && PlayerControl.LocalPlayer.CanMove;
                },
                () => {  // on meeting ends
                    assassinButton.Timer = assassinButton.MaxTimer;
                    Assassin.assassinMarked = null;
                },
                Assassin.getMarkButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F                   
            );

            mayorMeetingButton = new CustomButton(
               () => {
                   PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement 
                   Mayor.remoteMeetingsLeft--;

                   if (Mathf.RoundToInt(CustomOptionHolder.mayorMaxRemoteMeetings.getFloat()) - Mayor.remoteMeetingsLeft >= 3)
                       _ = new StaticAchievementToken("mayor.another1");

	               Helpers.handleVampireBiteOnBodyReport(); // Manually call Vampire handling, since the CmdReportDeadBody Prefix won't be called
                   Helpers.HandleUndertakerDropOnBodyReport();
                   Helpers.handleTrapperTrapOnBodyReport();
                   RPCProcedure.uncheckedCmdReportDeadBody(PlayerControl.LocalPlayer.PlayerId, Byte.MaxValue);

                   MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedCmdReportDeadBody, Hazel.SendOption.Reliable, -1);
                   writer.Write(PlayerControl.LocalPlayer.PlayerId);
                   writer.Write(Byte.MaxValue);
                   AmongUsClient.Instance.FinishRpcImmediately(writer);
                   mayorMeetingButton.Timer = 1f;
               },
               () => { return Mayor.mayor != null && Mayor.mayor == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Mayor.meetingButton; },
               () => {
                   mayorMeetingButton.actionButton.OverrideText(ModTranslation.getString("mayorEmergencyLeftText") + " (" + Mayor.remoteMeetingsLeft + ")");
                   bool sabotageActive = false;
                   foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks.GetFastEnumerator())
                       if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles
                           || (SubmergedCompatibility.IsSubmerged && task.TaskType == SubmergedCompatibility.RetrieveOxygenMask))
                           sabotageActive = true;
                   return !sabotageActive && PlayerControl.LocalPlayer.CanMove && (Mayor.remoteMeetingsLeft > 0);
               },
               () => { mayorMeetingButton.Timer = mayorMeetingButton.MaxTimer; },
               Mayor.getMeetingSprite(),
               CustomButton.ButtonPositions.lowerRowRight,
               __instance,
               KeyCode.F,
               true,
               0f,
               () => {},
               false,
               ModTranslation.getString("mayorEmergencyMeetingText")
           );

            // Trapper button
            //trapperButton = new CustomButton(
            /*() => {


                var pos = PlayerControl.LocalPlayer.transform.position;
                byte[] buff = new byte[sizeof(float) * 2];
                Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetTrap, Hazel.SendOption.Reliable);
                writer.WriteBytesAndSize(buff);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.setTrap(buff);

                SoundEffectsManager.play("trapperTrap");
                trapperButton.Timer = trapperButton.MaxTimer;
            },
            () => { return Trapper.trapper != null && Trapper.trapper == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
            () => {
                if (trapperChargesText != null) trapperChargesText.text = $"{Trapper.charges} / {Trapper.maxCharges}";
                return PlayerControl.LocalPlayer.CanMove && Trapper.charges > 0;
            },
            () => { trapperButton.Timer = trapperButton.MaxTimer; },
            Trapper.getButtonSprite(),
            CustomButton.ButtonPositions.lowerRowRight,
            __instance,
            KeyCode.F
        );*/

            // Bomber button
            /*bomberButton = new CustomButton(
                () => {
                    if (Helpers.checkMuderAttempt(Bomber.bomber, Bomber.bomber) != MurderAttemptResult.BlankKill) {
                        var pos = PlayerControl.LocalPlayer.transform.position;
                        byte[] buff = new byte[sizeof(float) * 2];
                        Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                        Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceBomb, Hazel.SendOption.Reliable);
                        writer.WriteBytesAndSize(buff);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.placeBomb(buff);

                        SoundEffectsManager.play("trapperTrap");
                    }

                    bomberButton.Timer = bomberButton.MaxTimer;
                    Bomber.isPlanted = true;
                },
                () => { return Bomber.bomber != null && Bomber.bomber == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Bomber.isPlanted; },
                () => { bomberButton.Timer = bomberButton.MaxTimer; },
                Bomber.getButtonSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.F,
                true,
                Bomber.destructionTime,
                () => {
                    bomberButton.Timer = bomberButton.MaxTimer;
                    bomberButton.isEffectActive = false;
                    bomberButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                }
            );

            defuseButton = new CustomButton(
                () => {
                    defuseButton.HasEffect = true;
                },
                () => {
                    if (shifterShiftButton.HasButton())
                        defuseButton.PositionOffset = new Vector3(0f, 2f, 0f);
                    else
                        defuseButton.PositionOffset = new Vector3(0f, 1f, 0f);
                    return Bomber.bomb != null && Bomb.canDefuse && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (defuseButton.isEffectActive && !Bomb.canDefuse) {
                        defuseButton.Timer = 0f;
                        defuseButton.isEffectActive = false;
                    }
                    return PlayerControl.LocalPlayer.CanMove; 
                },
                () => {
                    defuseButton.Timer = 0f;
                    defuseButton.isEffectActive = false;
                },
                Bomb.getDefuseSprite(),
                new Vector3(0f, 1f, 0),
                __instance,
                null,
                true,
                Bomber.defuseDuration,
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.DefuseBomb, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.defuseBomb();

                    defuseButton.Timer = 0f;
                    Bomb.canDefuse = false;
                },
                true
            );*/

            operateButton = new CustomButton(
                () => { FreePlayGM.OpenRoleWindow(); },
                () => { return FreePlayGM.isFreePlayGM; },
                () => { return FreePlayGM.roleScreen == null; },
                () => { operateButton.Timer = operateButton.MaxTimer = 0f; },
                FreePlayGM.getOperateButtonSprite(),
                new Vector3(0f, 1f, 0f),
                __instance,
                KeyCode.Z,
                true,
                buttonText: ModTranslation.getString("FreePlayOperateText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            freePlayReviveButton = new CustomButton(
                () =>
                {
                    PlayerControl.LocalPlayer.Revive();
                    DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == PlayerControl.LocalPlayer.PlayerId)
                        {
                            UnityEngine.Object.Destroy(array[i].gameObject);
                        }
                    }
                },
                () => { return FreePlayGM.isFreePlayGM && PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    return true;
                },
                () => { freePlayReviveButton.Timer = freePlayReviveButton.MaxTimer = 0f; },
                FreePlayGM.getReviveButtonSprite(),
                new Vector3(1f, 1f, 0f),
                __instance,
                KeyCode.Y,
                true,
                buttonText: ModTranslation.getString("FreePlayReviveText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            freePlaySuicideButton = new CustomButton(
                () =>
                {
                    if (PlayerControl.LocalPlayer.AmOwner)
                    {
                        if (Constants.ShouldPlaySfx())
                        {
                            SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.8f);
                        }
                        PlayerControl.LocalPlayer.cosmetics.SetNameMask(false);
                        PlayerControl.LocalPlayer.RpcSetScanner(false);
                    }
                    PlayerControl.LocalPlayer.MyPhysics.StartCoroutine(PlayerControl.LocalPlayer.KillAnimations.First().CoPerformKill(PlayerControl.LocalPlayer, PlayerControl.LocalPlayer));
                },
                () => { return FreePlayGM.isFreePlayGM && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    return true;
                },
                () => { freePlaySuicideButton.Timer = freePlaySuicideButton.MaxTimer = 0f; },
                __instance.KillButton.graphic.sprite,
                new Vector3(1f, 1f, 0f),
                __instance,
                KeyCode.X,
                true,
                buttonText: ModTranslation.getString("FreePlaySuicideText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            thiefKillButton = new CustomButton(
                () => {
                    PlayerControl thief = Thief.thief;
                    PlayerControl target = Thief.currentTarget;
                    var result = Helpers.checkMuderAttempt(thief, target);
                    if (result == MurderAttemptResult.BlankKill) {
                        thiefKillButton.Timer = thiefKillButton.MaxTimer;
                        return;
                    }

                    if (Thief.suicideFlag) {
                        // Suicide
                        MessageWriter writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                        writer2.Write(thief.PlayerId);
                        writer2.Write(thief.PlayerId);
                        writer2.Write(0);
                        RPCProcedure.uncheckedMurderPlayer(thief.PlayerId, thief.PlayerId, 0);
                        AmongUsClient.Instance.FinishRpcImmediately(writer2);
                        if (!FreePlayGM.isFreePlayGM) Thief.thief.clearAllTasks();
                        _ = new StaticAchievementToken("thief.another1");
                    }

                    // Steal role if survived.
                    if (!Thief.thief.Data.IsDead && result == MurderAttemptResult.PerformKill) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ThiefStealsRole, Hazel.SendOption.Reliable, -1);
                        writer.Write(target.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.thiefStealsRole(target.PlayerId);
                        _ = new StaticAchievementToken("thief.challenge");
                    }
                    // Kill the victim (after becoming their role - so that no win is triggered for other teams)
                    if (result == MurderAttemptResult.PerformKill) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                        writer.Write(thief.PlayerId);
                        writer.Write(target.PlayerId);
                        writer.Write(byte.MaxValue);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.uncheckedMurderPlayer(thief.PlayerId, target.PlayerId, byte.MaxValue);
                    }
                },
               () => { return Thief.thief != null && PlayerControl.LocalPlayer == Thief.thief && !PlayerControl.LocalPlayer.Data.IsDead; },
               () => { return Thief.currentTarget != null && PlayerControl.LocalPlayer.CanMove; },
               () => { thiefKillButton.Timer = thiefKillButton.MaxTimer; },
               __instance.KillButton.graphic.sprite,
               CustomButton.ButtonPositions.upperRowRight,
               __instance,
               KeyCode.Q
               );

            // Trapper Charges
            //trapperChargesText = GameObject.Instantiate(trapperButton.actionButton.cooldownTimerText, trapperButton.actionButton.cooldownTimerText.transform.parent);
            //trapperChargesText.text = "";
            //trapperChargesText.enableWordWrapping = false;
            //trapperChargesText.transform.localScale = Vector3.one * 0.5f;
            //trapperChargesText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            zoomOutButton = new CustomButton(
                () =>
                {
                    Helpers.toggleZoom();
                },
                () =>
                {
                    if (PlayerControl.LocalPlayer == null || !PlayerControl.LocalPlayer.Data.IsDead || PlayerControl.LocalPlayer.Data.Role.IsImpostor || (PlayerControl.LocalPlayer == Busker.busker && Busker.pseudocideFlag)) return false;
                    var (playerCompleted, playerTotal) = TasksHandler.taskInfo(PlayerControl.LocalPlayer.Data);
                    int numberOfLeftTasks = playerTotal - playerCompleted;
                    return numberOfLeftTasks <= 0 || !CustomOptionHolder.finishTasksBeforeHauntingOrZoomingOut.getBool();
                },
                () => { return true; },
                () => { return; },
                null,  // Invisible button!
                new Vector3(0.4f, 2.8f, 0),
                __instance,
                KeyCode.KeypadPlus
                )
            {
                Timer = 0f
            };


            hunterLighterButton = new CustomButton(
                () => {
                    Hunter.lightActive.Add(PlayerControl.LocalPlayer.PlayerId);
                    SoundEffectsManager.play("lighterLight");

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareTimer, Hazel.SendOption.Reliable, -1);
                    writer.Write(Hunter.lightPunish);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.shareTimer(Hunter.lightPunish);
                },
                () => { return HideNSeek.isHunter() && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return true; },
                () => {
                    hunterLighterButton.Timer = 30f;
                    hunterLighterButton.isEffectActive = false;
                    hunterLighterButton.actionButton.graphic.color = Palette.EnabledColor;
                },
                Hunter.getLightSprite(),
                CustomButton.ButtonPositions.upperRowFarLeft,
                __instance,
                KeyCode.F,
                true,
                Hunter.lightDuration,
                () => {
                    Hunter.lightActive.Remove(PlayerControl.LocalPlayer.PlayerId);
                    hunterLighterButton.Timer = hunterLighterButton.MaxTimer;
                    SoundEffectsManager.play("lighterLight");
                },
                buttonText: ModTranslation.getString("LighterText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            hunterAdminTableButton = new CustomButton(
               () => {
                   if (!MapBehaviour.Instance || !MapBehaviour.Instance.isActiveAndEnabled) {
                       HudManager __instance = FastDestroyableSingleton<HudManager>.Instance;
                       __instance.InitMap();
                       MapBehaviour.Instance.ShowCountOverlay(allowedToMove: true, showLivePlayerPosition: true, includeDeadBodies: false);
                   }

                   PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement 

                   MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareTimer, Hazel.SendOption.Reliable, -1);
                   writer.Write(Hunter.AdminPunish); 
                   AmongUsClient.Instance.FinishRpcImmediately(writer);
                   RPCProcedure.shareTimer(Hunter.AdminPunish);
               },
               () => { return HideNSeek.isHunter() && !PlayerControl.LocalPlayer.Data.IsDead; },
               () => { return true; },
               () => {
                   hunterAdminTableButton.Timer = hunterAdminTableButton.MaxTimer;
                   hunterAdminTableButton.isEffectActive = false;
                   hunterAdminTableButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
               },
               Hacker.getAdminSprite(),
               CustomButton.ButtonPositions.lowerRowCenter,
               __instance,
               KeyCode.G,
               true,
               Hunter.AdminDuration,
               () => {
                   hunterAdminTableButton.Timer = hunterAdminTableButton.MaxTimer;
                   if (MapBehaviour.Instance && MapBehaviour.Instance.isActiveAndEnabled) MapBehaviour.Instance.Close();
               },
               false,
               FastDestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Admin)
            );

            hunterArrowButton = new CustomButton(
                () => {
                    Hunter.arrowActive = true;
                    SoundEffectsManager.play("trackerTrackPlayer");

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareTimer, Hazel.SendOption.Reliable, -1);
                    writer.Write(Hunter.ArrowPunish);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.shareTimer(Hunter.ArrowPunish);
                },
                () => { return HideNSeek.isHunter() && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return true; },
                () => {
                    hunterArrowButton.Timer = 30f;
                    hunterArrowButton.isEffectActive = false;
                    hunterArrowButton.actionButton.graphic.color = Palette.EnabledColor;
                },
                Hunter.getArrowSprite(),
                CustomButton.ButtonPositions.upperRowLeft,
                __instance,
                KeyCode.R,
                true,
                Hunter.ArrowDuration,
                () => {
                    Hunter.arrowActive = false;
                    hunterArrowButton.Timer = hunterArrowButton.MaxTimer;
                    SoundEffectsManager.play("trackerTrackPlayer");
                },
                buttonText: ModTranslation.getString("HunterArrowText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );

            huntedShieldButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.HuntedShield, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.huntedShield(PlayerControl.LocalPlayer.PlayerId);
                    SoundEffectsManager.play("timemasterShield");

                    Hunted.shieldCount--;
                },
                () => { return HideNSeek.isHunted() && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (huntedShieldCountText != null) huntedShieldCountText.text = $"{Hunted.shieldCount}";
                    return PlayerControl.LocalPlayer.CanMove && Hunted.shieldCount > 0;
                },
                () => {
                    huntedShieldButton.Timer = huntedShieldButton.MaxTimer;
                    huntedShieldButton.isEffectActive = false;
                    huntedShieldButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                TimeMaster.getButtonSprite(),
                CustomButton.ButtonPositions.lowerRowRight,
                __instance,
                KeyCode.F,
                true,
                Hunted.shieldDuration,
                () => {
                    huntedShieldButton.Timer = huntedShieldButton.MaxTimer;
                    SoundEffectsManager.stop("timemasterShield");

                },
                buttonText: ModTranslation.getString("TimeShieldText"),
                abilityTexture: CustomButton.ButtonLabelType.UseButton
            );
            huntedShieldCountText = huntedShieldButton.ShowUsesIcon(3);

            // Set the default (or settings from the previous game) timers / durations when spawning the buttons
            initialized = true;
            setCustomButtonCooldowns();
            deputyHandcuffedButtons = new Dictionary<byte, List<CustomButton>>();
            
        }
    }
}
