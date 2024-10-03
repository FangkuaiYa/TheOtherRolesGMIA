using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmongUs.GameOptions;
using HarmonyLib;
using MonoMod.Cil;
using TheOtherRoles.MetaContext;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using TheOtherRoles.Players;
using UnityEngine;

namespace TheOtherRoles.CustomGameModes
{
    public static class FreePlayGM
    {
        public static bool isFreePlayGM = false;
        public static Sprite operateButtonSprite;
        public static Sprite reviveSprite;

        public static Sprite getOperateButtonSprite()
        {
            if (operateButtonSprite) return operateButtonSprite;
            operateButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.OperateButton.png", 115f);
            return operateButtonSprite;
        }

        public static Sprite getReviveButtonSprite()
        {
            if (reviveSprite) return reviveSprite;
            reviveSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.ReviveButton.png", 115f);
            return reviveSprite;
        }

        public static void OpenRoleWindow()
        {
            var window = MetaScreen.GenerateWindow(new Vector2(7.5f, 4.5f), HudManager.Instance.transform, new Vector3(0, 0, -400f), true, false);

            var gui = TORGUIContextEngine.Instance;
            var roleMaskedTittleAttr = gui.GetAttribute(AttributeAsset.MetaRoleButton);
            var roleTittleAttr = new TextAttributes(roleMaskedTittleAttr) { Font = gui.GetFont(FontAsset.Gothic) };

            void SetWidget(int tab)
            {
                List<GUIContext> guis = new()
                { gui.LocalizedButton(GUIAlignment.Center, roleTittleAttr, "freePlayRoles", () => SetWidget(0), color: tab == 0 ? Color.yellow : null),
                    gui.LocalizedButton(GUIAlignment.Center, roleTittleAttr, "freePlayModifiers", () => SetWidget(1), color: tab == 1 ? Color.yellow : null)};
                var holder = gui.HorizontalHolder(GUIAlignment.Center,
                    guis
                    );

                GUIContext inner = GUIEmptyWidget.Default;

                if (tab == 0)
                {
                    inner = gui.Arrange(GUIAlignment.Center, RoleInfo.allRoleInfos.Where(x => x != RoleInfo.bomberB && x != RoleInfo.bomberA && x != RoleInfo.jackal
                    && x != RoleInfo.sidekick && x != RoleInfo.mimicA && x != RoleInfo.mimicK && x != RoleInfo.arsonist && x != RoleInfo.bountyHunter && !x.isModifier).Select(r => gui.RawButton(GUIAlignment.Center, roleMaskedTittleAttr, Helpers.cs(r.color, r.name), () =>
                    {
                        var formerRole = RoleInfo.getRoleInfoForPlayer(PlayerControl.LocalPlayer, false).FirstOrDefault();
                        if (formerRole == r) return; // Do nothing if the same role was given
                        RPCProcedure.erasePlayerRoles(PlayerControl.LocalPlayer.PlayerId);
                        if (r.isImpostor() && !formerRole.isImpostor()) PlayerControl.LocalPlayer.FastSetRole(RoleTypes.Impostor);
                        else if (!r.isImpostor() && formerRole.isImpostor()) PlayerControl.LocalPlayer.FastSetRole(RoleTypes.Crewmate);

                        if (r == RoleInfo.chainshifter) Shifter.isNeutral = true;
                        else if (r == RoleInfo.niceshifter) Shifter.isNeutral = false;
                        RPCProcedure.setRole((byte)r.roleId, PlayerControl.LocalPlayer.PlayerId);

                        if (r.roleId == RoleId.Fox) {
                            if (Shrine.allShrine?.FirstOrDefault() == null){
                                Shrine.activateShrines(GameOptionsManager.Instance.currentNormalGameOptions.MapId);System.Console.WriteLine("1");
                            }
                            List<Byte> taskIdList = new();
                            Shrine.allShrine.ForEach(shrine => taskIdList.Add((byte)shrine.console.ConsoleId));
                            taskIdList.Shuffle();
                            var cpt = new CustomNormalPlayerTask("foxTaskStay", Il2CppType.Of<FoxTask>(), Fox.numTasks, taskIdList.ToArray(), Shrine.allShrine.Find(x => x.console.ConsoleId == taskIdList.ToArray()[0]).console.Room, true);
                            cpt.addTaskToPlayer(CachedPlayer.LocalPlayer.PlayerId);
                        } else if (r.roleId == RoleId.JekyllAndHyde) {
                            CachedPlayer.LocalPlayer.PlayerControl.generateAndAssignTasks(JekyllAndHyde.numCommonTasks, JekyllAndHyde.numShortTasks, JekyllAndHyde.numLongTasks);
                        } else if (formerRole.roleId == RoleId.Fox || formerRole.roleId == RoleId.JekyllAndHyde || formerRole.roleId == RoleId.TaskMaster) {
                            var options = GameOptionsManager.Instance.currentNormalGameOptions;
                            PlayerControl.LocalPlayer.generateAndAssignTasks(options.NumCommonTasks, options.NumShortTasks, options.NumLongTasks);
                        }
                        RPCProcedure.resetAchievement();
                        window.CloseScreen();
                    })), 4);
                }
                else if (tab == 1)
                {
                    inner = gui.VerticalHolder(GUIAlignment.Center,
                        new List<GUIContext>() { gui.LocalizedText(GUIAlignment.Center, roleMaskedTittleAttr, "freePlayModifiersEquipped"),
                        gui.Arrange(GUIAlignment.Center, RoleInfo.allRoleInfos.Where(r => r.isModifier && RoleInfo.getRoleInfoForPlayer(PlayerControl.LocalPlayer).Contains(r)).Select(r => gui.RawButton(GUIAlignment.Center, roleMaskedTittleAttr, Helpers.cs(r.color, r.name), () =>
                        {
                            removeModifier(r.roleId);
                            SetWidget(1);
                        })), 4),
                        gui.LocalizedText(GUIAlignment.Center, roleMaskedTittleAttr, "freePlayModifiersUnequipped"),
                        gui.Arrange(GUIAlignment.Center, RoleInfo.allRoleInfos.Where(r => r.isModifier && r != RoleInfo.cupidLover && r != RoleInfo.lover && r != RoleInfo.mini && !RoleInfo.getRoleInfoForPlayer(PlayerControl.LocalPlayer).Contains(r)).Select(r => gui.RawButton(GUIAlignment.Center, roleMaskedTittleAttr, Helpers.cs(r.color, r.name), () =>
                        {
                            RPCProcedure.setModifier((byte)r.roleId, PlayerControl.LocalPlayer.PlayerId, 0);
                            SetWidget(1);
                        })), 4)}
                        );
                }
                window.SetContext(gui.VerticalHolder(GUIAlignment.Center, new List<GUIContext>() { holder, gui.ScrollView(GUIAlignment.Center, new(7.4f, 3.5f), null, inner, out _) }), out _);
            }

            SetWidget(0);
        }

        private static void FastSetRole(this PlayerControl targetPlayer, RoleTypes roleType)
        {
            NetworkedPlayerInfo data = targetPlayer.Data;
            RoleBehaviour roleBehaviour = UnityEngine.Object.Instantiate(RoleManager.Instance.AllRoles.First(r => r.Role == roleType), data.gameObject.transform);
            roleBehaviour.Initialize(targetPlayer);
            targetPlayer.Data.Role = roleBehaviour;
            targetPlayer.Data.RoleType = roleType;
            if (roleType != RoleTypes.ImpostorGhost && roleType != RoleTypes.CrewmateGhost)
                targetPlayer.Data.RoleWhenAlive = new Il2CppSystem.Nullable<RoleTypes>(roleType);
            roleBehaviour.AdjustTasks(targetPlayer);
        }

        private static bool isImpostor(this RoleInfo roleInfo) => roleInfo.color == Palette.ImpostorRed && roleInfo.roleId != RoleId.Spy;

        public static void removeModifier(RoleId modifierId)
        {
            var player = PlayerControl.LocalPlayer;
            var playerId = player.PlayerId;
            switch (modifierId)
            {
                case RoleId.AntiTeleport:
                    AntiTeleport.antiTeleport.RemoveAll(x => x.PlayerId == playerId);
                    break;
                case RoleId.Chameleon:
                    Chameleon.chameleon.RemoveAll(x => x.PlayerId == playerId);
                    break;
                case RoleId.Mini:
                    Mini.mini = null;
                    break;
                case RoleId.Invert:
                    Invert.invert.RemoveAll(x => x.PlayerId == playerId);
                    break;
                case RoleId.Bloody:
                    Bloody.bloody.RemoveAll(x => x.PlayerId == playerId);
                    break;
                case RoleId.Sunglasses:
                    Sunglasses.sunglasses.RemoveAll(x => x.PlayerId == playerId);
                    break;
                case RoleId.Tiebreaker:
                    Tiebreaker.tiebreaker = null;
                    break;
                case RoleId.Vip:
                    Vip.vip.RemoveAll(x => x.PlayerId == playerId);
                    break;
            }
        }

        public static PlayerControl SpawnDummy()
        {
            var playerControl = UnityEngine.Object.Instantiate(AmongUsClient.Instance.PlayerPrefab);
            var i = playerControl.PlayerId = (byte)GameData.Instance.GetAvailableId();

            playerControl.isDummy = true;

            var playerInfo = GameData.Instance.AddDummy(playerControl);

            playerControl.transform.position = PlayerControl.LocalPlayer.transform.position;
            playerControl.GetComponent<DummyBehaviour>().enabled = true;
            playerControl.isDummy = true;
            playerControl.SetName(AccountManager.Instance.GetRandomName());
            playerControl.SetColor(i);
            playerControl.SetHat(CosmeticsLayer.EMPTY_HAT_ID, i);
            playerControl.SetVisor(CosmeticsLayer.EMPTY_VISOR_ID, i);
            playerControl.SetSkin(CosmeticsLayer.EMPTY_SKIN_ID, i);
            playerControl.SetPet(CosmeticsLayer.EMPTY_PET_ID, i);

            AmongUsClient.Instance.Spawn(playerControl, -2, InnerNet.SpawnFlags.None);
            playerInfo.RpcSetTasks(new byte[0]);

            return playerControl;
        }

        public static void clearAndReload()
        {
            isFreePlayGM = TORMapOptions.gameMode == CustomGamemodes.FreePlay;
        }

        [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.BeginGame))]
        public class GameStartManagerBeginGame
        {
            public static bool Prefix(GameStartManager __instance)
            {
                if (TORMapOptions.gameMode == CustomGamemodes.FreePlay)
                {
                    if (AmongUsClient.Instance.AmHost && PlayerControl.AllPlayerControls.Count == 1)
                    {
                        var numDummies = CustomOptionHolder.freePlayGameModeNumDummies != null ? (int)CustomOptionHolder.freePlayGameModeNumDummies.getFloat() : 0;
                        for (int i = 0; i < numDummies; i++) SpawnDummy();
                    }
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(GameManager), nameof(GameManager.CheckTaskCompletion))]
        static class CheckTaskCompletionPatch
        {
            static bool Prefix(GameManager __instance, ref bool __result)
            {
                if (!isFreePlayGM) return true;
                __result = false;
                return false;
            }
        }

        [HarmonyPatch(typeof(GameManager), nameof(GameManager.CheckEndGameViaTasks))]
        static class CheckEndGameViaTasksPatch
        {
            static bool Prefix(GameManager __instance, ref bool __result)
            {
                if (!isFreePlayGM) return true;
                __result = false;
                return false;
            }
        }

        [HarmonyPatch(typeof(LogicGameFlowNormal), nameof(LogicGameFlowNormal.IsGameOverDueToDeath))]
        public static class BlockGameOverPatch
        {
            public static bool Prefix(LogicGameFlowNormal __instance, ref bool __result)
            {
                if (!isFreePlayGM) return true;
                __result = false;
                return false;
            }
        }
    }
}