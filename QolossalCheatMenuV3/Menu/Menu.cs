using ExitGames.Client.Photon;
using GorillaNetworking;
using Il2CppSystem.Net;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using PlayFab;
using POpusCodec.Enums;
using Qolossal.Notifacation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Qolossal.Menu
{
    public class MenuOption
    {
        public string DisplayName;
        public string _type;
        public bool AssociatedBool;
        public string AssociatedString;
        public float AssociatedFloat;
        public int AssociatedInt;
        public string[] StringArray;
        public int stringsliderind;
        public string AssociatedBind;
        public string extra;
    }

    public class Menu
    {
        public static bool GUIToggled = true;

        public static GameObject MenuHub;
        public static Text MenuHubText;

        public static GameObject AgreementHub;
        public static Text AgreementHubText;

        public static string MenuColour = "magenta";
        public static float menurgb = 0;

        private static GameObject pointerObj;
        private static PanelElement activePanel; // Track the currently active panel
        private static PanelElement grabbedPanel = null; // Ensure this is a field
        private static Vector3 grabOffset;

        public static string MenuState = "MainMenu";
        public static int SelectedOptionIndex = 0;
        public static MenuOption[] CurrentViewingMenu = null;
        public static MenuOption[] MainMenu;
        public static MenuOption[] Movement;
        public static MenuOption[] Movement2;
        public static MenuOption[] Visual;
        public static MenuOption[] Visual2;
        public static MenuOption[] Player;
        public static MenuOption[] Player2;
        public static MenuOption[] Computer;
        public static MenuOption[] Exploits;
        public static MenuOption[] Exploits2;
        public static MenuOption[] Exploits3;
        public static MenuOption[] Safety;
        public static MenuOption[] Settings;

        public static MenuOption[] Speed;
        public static MenuOption[] Tracers;
        public static MenuOption[] NameTags;
        public static MenuOption[] Strafe;
        public static MenuOption[] CosmeticsSpoofer;
        public static MenuOption[] Gamemodes;

        public static MenuOption[] MusicPlayer;
        public static MenuOption[] ColourSettings;
        public static MenuOption[] Info;
        public static MenuOption[] Macro;

        public static MenuOption[] Dev;
        public static MenuOption[] ConsoleGuns;
        public static MenuOption[] ConsoleAll;

        private static bool isGrabbing = false;
        public static bool inputcooldown = false;
        public static bool menutogglecooldown = false;
        public static bool agreement = false;

        public static void LoadOnce()
        {
            // Security stuff dont touch it
            if (typeof(GorillaTagger).GetMethod("L3THASFKAdsfds4tewEAa3THASFKAdsfds4tewEAt3THASFKAdsfds4tewEAe3THASFKAdsfds4tewEAU3THASFKAdsfds4tewEAp3THASFKAdsfds4tewEAd3THASFKAdsfds4tewEAa3THASFKAdsfds4tewEAt3THASFKAdsfds4tewEAe3THASFKAdsfds4tewEA".Replace("3THASFKAdsfds4tewEA", ""), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) == null)
            {
                Plugin.QG();
                Application.Quit();
                Application.ForceCrash(1);
                Application.CallLowMemory();
                Environment.Exit(0);
            }
            // Security stuff dont touch it
            Plugin.CheckIntegrity(Plugin.DecodeString(Plugin.anti1));

            // If your adding something ask me first and make sure the bool name is the same as the displayname
            try
            {
                if (!agreement)
                    (AgreementHub, AgreementHubText) = GUICreator.CreateTextGUI("<color=magenta><VR CONTROLS></color>\nLeft Joystick Click (Hold):\nRight Grip: Select\nRight Trigger: Scroll\nLeft Trigger: Custom Bind\nLeft Grip: Remove Custom Bind\nBoth Joysticks: Toggle UI\n\n<color=magenta><PC CONTROLS></color>\nEnterKey: Select\nArrowKey (Up): Move Up\nArrowKey (Down): Move Down\n\n<color=cyan>Press Both Joysticks Or Enter...</color>", "AgreementHub", TextAnchor.MiddleCenter, new Vector3(0, 0f, 2));
                else
                {
                    if (PluginConfig.legacyui)
                    {
                        if (Plugin.holder.GetComponent<Overlay>() == null)
                            Plugin.holder.AddComponent<Overlay>();

                        if (Plugin.holder.GetComponent<Notifacations>() == null)
                            Plugin.holder.AddComponent<Notifacations>();
                    }

                    if (Plugin.holder.GetComponent<ToolTips>() == null)
                        Plugin.holder.AddComponent<ToolTips>();

                    if (Plugin.holder.GetComponent<Boards>() == null)
                        Plugin.holder.AddComponent<Boards>();

                    if (!string.IsNullOrEmpty(Plugin.buttonthingy) || !string.IsNullOrEmpty(Plugin.backthingy) || !string.IsNullOrEmpty(Plugin.submenuthingy) || !string.IsNullOrEmpty(Plugin.sliderthingy) || !string.IsNullOrEmpty(Plugin.togglethingy))
                    {
                        MelonLoader.MelonCoroutines.Start(LoadMenu());
                    }
                    else
                    {
                        MenuHubText.text = "<color=red>Error Loading Menu Types (Code: 2)\nPlease Show This To Nova\nRestart Your Game</color>";
                        return;
                    }
                }

                UpdateMenuState(new MenuOption(), null, null);

                CustomConsole.LogToConsole("[QOLOSSAL] Updated Menu State");
            }
            catch (Exception ex)
            {
                CustomConsole.LogToConsole("[QOLOSSAL] " + ex.ToString());
            }
        }

        public static int loadingNumber = 0;
        private static System.Collections.IEnumerator LoadMenu()
        {
            void UpdateLoadingText()
            {
                string loadingText = $"<color=magenta>Loading {loadingNumber}/22</color>";
                if (AgreementHubText != null)
                {
                    AgreementHubText.text = loadingText;
                }
                else
                {
                    MelonLoader.MelonLogger.Msg("AgreementHubText is null");
                }
            }

            UpdateLoadingText();

            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;
            loadingNumber += 1; UpdateLoadingText(); yield return null;

            if (loadingNumber == 22)
            {
                MenuLoader.LoadMenu();
                if (AgreementHub != null)
                    UnityEngine.Object.Destroy(AgreementHub);
            }

            // Final update
            loadingNumber = 22;
            UpdateLoadingText();

            (MenuHub, MenuHubText) = GUICreator.CreateTextGUI("", "MenuHub", TextAnchor.UpperLeft, new Vector3(0, 0.4f, 3.6f));
            if (MenuHub == null || MenuHubText == null)
            {
                MelonLoader.MelonLogger.Msg("Failed to create MenuHub or MenuHubText");
                UnityEngine.Object.Destroy(AgreementHub);
                yield break;
            }

            MenuState = "MainMenu";
            CurrentViewingMenu = MainMenu;
            MelonLoader.MelonLogger.Msg("Build Menu");

            if (!PluginConfig.legacyui)
            {
                if (PointerLine.Instance == null)
                {
                    GameObject pointerObj = new GameObject("PointerLineObj");
                    pointerObj.AddComponent<PointerLine>();
                    CustomConsole.LogToConsole("Spawned PointerLineObj in LoadOnce");
                }
            }
        }

        public static void Load()
        {
            if (!agreement)
            {
                if (AgreementHub == null) //watch as this breaks the whole menu
                    LoadOnce();
                if (Controls.LeftJoystick() && Controls.RightJoystick() && !menutogglecooldown || Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    menutogglecooldown = true;
                    Plugin.CheckIntegrity2(Plugin.DecodeString(Plugin.anti2));
                    agreement = true;
                    Console.ConsoleQolossal.LoadConsole();
                    LoadOnce();
                }
            }
            else
            {
                if (!Plugin.shouldbeallowed)
                {
                    Environment.Exit(0);
                    Application.Quit();
                    Plugin.QG();
                    return;
                }

                if (MenuHub == null || MenuHubText == null)
                    return;
                if (Controls.LeftJoystick() && Controls.RightJoystick() && !menutogglecooldown)
                {
                    menutogglecooldown = true;
                    if (PluginConfig.legacyui)
                    {
                        MenuHub.active = !MenuHub.active;
                    }
                    else
                    {
                        foreach (PanelElement panel in GUICreator.openPanels)
                        {
                            Animator panelAnimator = panel.RootObject.GetComponent<Animator>();

                            if (GUIToggled)
                            {
                                panel.RootObject.SetActive(true);

                                panel.RootObject.transform.LookAt(GorillaLocomotion.Player.Instance.headCollider.transform.position);
                                panel.RootObject.transform.Rotate(0, 180f, 0f, Space.Self);

                                panelAnimator.Play(AssetBundleLoader.Menu_In);
                            }
                            else
                            {
                                panelAnimator.Play(AssetBundleLoader.Menu_Out);
                                GUICreator.panelsToDisable.Add(panel);
                            }
                        }

                        AssetBundleLoader.hud.transform.position = Camera.main.transform.position;
                    }
                    GUIToggled = !GUIToggled;

                    UpdateMenuState(new MenuOption(), null, null);
                }
                if (!Controls.LeftJoystick() && !Controls.RightJoystick() && menutogglecooldown)
                    menutogglecooldown = false;

                if (GUICreator.panelsToDisable.Count > 0)
                {
                    for (int j = GUICreator.panelsToDisable.Count - 1; j >= 0; j--)
                    {
                        PanelElement panel = GUICreator.panelsToDisable[j];
                        Animator panelAnimator = panel.RootObject.GetComponent<Animator>();
                        AnimatorStateInfo stateInfo = panelAnimator.GetCurrentAnimatorStateInfo(0);

                        if (stateInfo.IsName(AssetBundleLoader.Menu_Out) && stateInfo.normalizedTime >= 1.0f)
                        {
                            panel.RootObject.SetActive(false);
                            GUICreator.panelsToDisable.RemoveAt(j);
                        }
                    }
                }

                if (GUIToggled)
                {
                    if (Plugin.update || Plugin.locked) return;

                    if (!PluginConfig.legacyui)
                    {
                        PanelElement currentPanel = null;
                        foreach (var panel in GUICreator.openPanels)
                        {
                            if (panel.RootObject.activeSelf)
                            {
                                currentPanel = panel;
                                break;
                            }
                        }
                        if (currentPanel == null)
                        {
                            if (PointerLine.Instance != null) PointerLine.Instance.DisableLine();
                            return;
                        }


                        RaycastHit hit;
                        bool worked = false;
                        GameObject hitObject = null;
                        string hitName = "";
                        Ray rayUsed = new Ray();


                        Vector3 rayOrigin = GorillaLocomotion.Player.Instance.rightHandTransform.position - GorillaLocomotion.Player.Instance.rightHandTransform.up * 0.05f;
                        Vector3 rayDirection = -GorillaLocomotion.Player.Instance.rightHandTransform.up;
                        rayUsed = new Ray(rayOrigin, rayDirection);
                        worked = Physics.Raycast(rayOrigin, rayDirection, out hit, float.PositiveInfinity, GUICreator.UILayerMask);

                        if (worked && hit.collider != null)
                        {
                            hitObject = hit.collider.gameObject;
                            if (hitObject.layer != 14)
                            {
                                hitObject = null;
                                worked = false;
                            }
                            else
                            {
                                hitName = hitObject.transform.parent != null ? hitObject.transform.parent.name : hitObject.name;
                                foreach (var panel in GUICreator.openPanels)
                                {
                                    if (panel.RootObject.activeSelf && IsObjectInPanel(hitObject, panel))
                                    {
                                        currentPanel = panel;
                                        break;
                                    }
                                }
                            }
                        }


                        if (PointerLine.Instance != null)
                        {
                            PointerLine.Instance.UpdateLine(rayUsed, worked, hit, currentPanel);
                        }

                        if ( worked && hitObject != null && hitObject.name.Contains("grab") && !isGrabbing)
                        {
                            grabbedPanel = currentPanel;
                            isGrabbing = true;
                            PointerLine.ShortRangeMode = true;
                        }
                        if (isGrabbing)
                        {
                            isGrabbing = false;
                            grabbedPanel = null;
                            PointerLine.ShortRangeMode = false;
                        }
                        if (isGrabbing && grabbedPanel != null)
                        {
                            Vector3 targetPosition = PointerLine.lastPointerPos;
                            targetPosition.y = Mathf.Max(targetPosition.y, 0.1f);
                            grabbedPanel.RootObject.transform.position = targetPosition;

                            Vector3 directionToPlayer = GorillaLocomotion.Player.Instance.headCollider.transform.position - grabbedPanel.RootObject.transform.position;
                            Quaternion targetRotation = Quaternion.LookRotation(-directionToPlayer);
                            grabbedPanel.RootObject.transform.rotation = targetRotation;
                        }


                        if (worked && hitObject != null && hitObject.name.Contains("x") && !isGrabbing)
                        {
                            Animator panelAnimator = currentPanel.RootObject.GetComponent<Animator>();
                            panelAnimator.Play(AssetBundleLoader.Menu_Out);
                            GUICreator.panelsToDisable.Add(currentPanel);
                        }


                        bool interactionTriggered = Controls.RightTrigger();
                        if (worked && interactionTriggered && !menutogglecooldown)
                        {
                            menutogglecooldown = true;

                            if (hitName.Contains("_"))
                            {
                                string[] parts = hitName.Split('_');
                                if (parts.Length > 1 && int.TryParse(parts[1], out int index) && index >= 0 && index < currentPanel.CurrentViewingMenu.Length)
                                {
                                    MenuOption option = currentPanel.CurrentViewingMenu[index];
                                    SelectedOptionIndex = index;

                                    if (hitName.StartsWith("bind_"))
                                    {
                                        CustomBinding.StartListeningForBind(option.DisplayName);
                                        return;
                                    }
                                    else if (hitName.StartsWith("Toggle_"))
                                    {
                                        option.AssociatedBool = !option.AssociatedBool;

                                        UpdateMenuState(option, null, "optionhit");
                                        PanelElement.UpdatePanel(currentPanel, currentPanel.CurrentViewingMenu);
                                    }
                                    else if (hitName.StartsWith("Button_") || hitName.StartsWith("Slider_"))
                                    {
                                        UpdateMenuState(option, null, "optionhit");
                                        PanelElement.UpdatePanel(currentPanel, currentPanel.CurrentViewingMenu);
                                    }
                                    else if (hitName.StartsWith("Submenu_"))
                                    {
                                        string newMenuState = option.DisplayName;
                                        GUICreator.NewUI(newMenuState);
                                    }
                                    else if (hitName.StartsWith("SliderLArrow_") && option.stringsliderind > 0)
                                    {
                                        option.stringsliderind--;
                                        UpdateMenuState(option, null, "optionhit");
                                        PanelElement.UpdatePanel(currentPanel, currentPanel.CurrentViewingMenu);
                                    }
                                    else if (hitName.StartsWith("SliderRArrow_") && option.stringsliderind < option.StringArray.Length - 1)
                                    {
                                        option.stringsliderind++;
                                        UpdateMenuState(option, null, "optionhit");
                                        PanelElement.UpdatePanel(currentPanel, currentPanel.CurrentViewingMenu);
                                    }
                                }
                            }
                        }

                        bool IsObjectInPanel(GameObject meow, PanelElement panel)
                        {
                            Transform currentTransform = meow.transform;
                            while (currentTransform != null)
                            {
                                if (currentTransform.gameObject == panel.RootObject)
                                    return true;
                                currentTransform = currentTransform.parent;
                            }
                            return false;
                        }
                    }
                    else
                    {
                        //KEYBOARD CONTROLS
                        Keyboard current = Keyboard.current;
                        if (current.upArrowKey.wasPressedThisFrame)
                        {
                            inputcooldown = true;
                            if (SelectedOptionIndex == 0)
                                SelectedOptionIndex = CurrentViewingMenu.Count<MenuOption>() - 1;
                            else
                                SelectedOptionIndex--;
                            UpdateMenuState(new MenuOption(), null, null);
                        }
                        if (current.downArrowKey.wasPressedThisFrame)
                        {
                            inputcooldown = true;
                            if (SelectedOptionIndex + 1 == CurrentViewingMenu.Count<MenuOption>())
                                SelectedOptionIndex = 0;
                            else
                                SelectedOptionIndex++;
                            UpdateMenuState(new MenuOption(), null, null);
                        }
                        if (current.enterKey.wasPressedThisFrame)
                        {
                            inputcooldown = true;
                            UpdateMenuState(CurrentViewingMenu[SelectedOptionIndex], null, "optionhit");
                        }
                        if (CurrentViewingMenu[SelectedOptionIndex]._type == Plugin.sliderthingy)
                        {
                            if (current.rightArrowKey.wasPressedThisFrame)
                            {
                                if (CurrentViewingMenu[SelectedOptionIndex].DisplayName == Settings[2].DisplayName || CurrentViewingMenu[SelectedOptionIndex].DisplayName == MusicPlayer[0].DisplayName)
                                {
                                    int arrayLength = CurrentViewingMenu[SelectedOptionIndex].StringArray.Count();
                                    if (CurrentViewingMenu[SelectedOptionIndex].stringsliderind < arrayLength - 1)
                                        CurrentViewingMenu[SelectedOptionIndex].stringsliderind++;
                                    else
                                        CurrentViewingMenu[SelectedOptionIndex].stringsliderind = 0;
                                    inputcooldown = true;
                                }
                                else
                                {
                                    foreach (var prop in typeof(PluginConfig).GetFields(BindingFlags.Public | BindingFlags.Static))
                                    {
                                        if (prop.Name.Replace(" ", "").Replace("(", "").Replace(")", "").ToLower() == CurrentViewingMenu[SelectedOptionIndex].DisplayName.Replace(" ", "").Replace("(", "").Replace(")", "").ToLower())
                                        {
                                            object currentValue = prop.GetValue(null);
                                            int? currentIntValue = currentValue as int?;
                                            if (currentIntValue.HasValue)
                                            {
                                                int newValue = currentIntValue.Value + 1;
                                                int stringArrayCount = CurrentViewingMenu[SelectedOptionIndex].StringArray.Length;
                                                if (newValue >= stringArrayCount)
                                                    newValue = 0;

                                                prop.SetValue(null, newValue);
                                            }
                                            break;
                                        }
                                    }
                                }
                                inputcooldown = true;
                            }
                            UpdateMenuState(new MenuOption(), null, null);
                        }

                        bool isJoystickPressed = PluginConfig.invertedControls ? Controls.RightJoystick() : Controls.LeftJoystick();
                        bool isBothJoystickPressed = Controls.RightJoystick() && Controls.LeftJoystick();
                        bool isTriggerPressed = PluginConfig.invertedControls ? Controls.LeftTrigger() : Controls.RightTrigger();
                        bool isGripPressed = PluginConfig.invertedControls ? ControlsV2.LeftGrip() : ControlsV2.RightGrip();

                        //VR CONTROLS
                        if (isJoystickPressed)
                        {
                            bool isBindTriggerPressed = PluginConfig.invertedControls ? ControlsV2.RightTrigger() : ControlsV2.LeftTrigger();
                            bool isBindGripPressed = PluginConfig.invertedControls ? ControlsV2.RightGrip() : ControlsV2.LeftGrip();
                            if (isBindTriggerPressed && !inputcooldown)
                            {
                                inputcooldown = true;
                                CustomBinding.StartListeningForBind(CurrentViewingMenu[SelectedOptionIndex].DisplayName);
                            }
                            if (isBindGripPressed && !inputcooldown)
                            {
                                inputcooldown = true;
                                CustomBinding.ClearBinds(CurrentViewingMenu[SelectedOptionIndex].DisplayName);
                            }

                            if (isTriggerPressed && !inputcooldown)
                            {
                                inputcooldown = true;

                                if (SelectedOptionIndex + 1 == CurrentViewingMenu.Count<MenuOption>())
                                    SelectedOptionIndex = 0;
                                else
                                    SelectedOptionIndex++;
                                UpdateMenuState(new MenuOption(), null, null);
                            }
                            if (!isGripPressed && !isTriggerPressed && inputcooldown)
                            {
                                inputcooldown = false;
                            }
                            if (CurrentViewingMenu[SelectedOptionIndex]._type == Plugin.sliderthingy)
                            {
                                if (isGripPressed && !inputcooldown)
                                {
                                    if (CurrentViewingMenu[SelectedOptionIndex].DisplayName == Settings[2].DisplayName || CurrentViewingMenu[SelectedOptionIndex].DisplayName == MusicPlayer[0].DisplayName || CurrentViewingMenu[SelectedOptionIndex].DisplayName == Macro[0].DisplayName)
                                    {
                                        int arrayLength = CurrentViewingMenu[SelectedOptionIndex].StringArray.Count();
                                        if (CurrentViewingMenu[SelectedOptionIndex].stringsliderind < arrayLength - 1)
                                            CurrentViewingMenu[SelectedOptionIndex].stringsliderind++;
                                        else
                                            CurrentViewingMenu[SelectedOptionIndex].stringsliderind = 0;
                                        inputcooldown = true;
                                    }
                                    else
                                    {
                                        foreach (var prop in typeof(PluginConfig).GetFields(BindingFlags.Public | BindingFlags.Static))
                                        {
                                            if (prop.Name.Replace(" ", "").Replace("(", "").Replace(")", "").ToLower() == CurrentViewingMenu[SelectedOptionIndex].DisplayName.Replace(" ", "").Replace("(", "").Replace(")", "").ToLower())
                                            {
                                                object currentValue = prop.GetValue(null);
                                                int? currentIntValue = currentValue as int?;
                                                if (currentIntValue.HasValue)
                                                {
                                                    int newValue = currentIntValue.Value + 1;
                                                    int stringArrayCount = CurrentViewingMenu[SelectedOptionIndex].StringArray.Length;
                                                    if (newValue >= stringArrayCount)
                                                        newValue = 0;
                                                    prop.SetValue(null, newValue);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    inputcooldown = true;
                                }
                                UpdateMenuState(new MenuOption(), null, null);
                            }
                            if (isGripPressed && !inputcooldown)
                            {
                                inputcooldown = true;
                                UpdateMenuState(CurrentViewingMenu[SelectedOptionIndex], null, "optionhit");
                            }
                        }
                    }
                }
                //PluginConfig.anticrash = MainMenu[8].AssociatedBool;
                MainMenu[10].AssociatedBool = PluginConfig.Notifications;
                MainMenu[11].AssociatedBool = PluginConfig.overlay;
                MainMenu[12].AssociatedBool = PluginConfig.tooltips;

                //Movement
                Movement[0].stringsliderind = PluginConfig.excelfly;
                Movement[1].AssociatedBool = PluginConfig.tfly;
                Movement[2].stringsliderind = PluginConfig.wallwalk;
                Speed[0].stringsliderind = PluginConfig.speed;
                Speed[1].stringsliderind = PluginConfig.speedtoggle;
                Speed[2].stringsliderind = PluginConfig.nearspeed;
                Speed[3].stringsliderind = PluginConfig.nearspeeddistance;
                Strafe[0].stringsliderind = PluginConfig.strafe;
                Strafe[1].stringsliderind = PluginConfig.strafespeed;
                Strafe[2].stringsliderind = PluginConfig.strafejumpamount;
                Movement[4].AssociatedBool = PluginConfig.platforms;
                Movement[5].AssociatedBool = PluginConfig.upsidedownmonkey;
                Movement[6].AssociatedBool = PluginConfig.wateryair;
                Movement[7].AssociatedBool = PluginConfig.longarms;
                Movement[8].AssociatedBool = PluginConfig.SpinBot;
                Movement[9].AssociatedBool = PluginConfig.JoystickFly;

                //Movement2
                Movement2[0].stringsliderind = PluginConfig.Timer;
                Movement2[1].stringsliderind = PluginConfig.FloatyMonkey;
                Movement2[2].AssociatedBool = PluginConfig.ClimbableGorillas;
                Movement2[3].stringsliderind = PluginConfig.NearPulse;
                Movement2[4].stringsliderind = PluginConfig.NearPulseDistance;
                Movement2[5].AssociatedBool = PluginConfig.PlayerScale;
                Movement2[6].AssociatedBool = PluginConfig.NoClip;
                Movement2[7].AssociatedBool = PluginConfig.forcetagfreeze;
                Movement2[9].stringsliderind = PluginConfig.hzhands;
                Movement2[10].AssociatedBool = PluginConfig.Throw;
                Movement2[12].stringsliderind = PluginConfig.pullmod;

                //Visual
                Visual[0].AssociatedBool = PluginConfig.chams;
                Visual[1].AssociatedBool = PluginConfig.boxesp;
                Visual[2].AssociatedBool = PluginConfig.hollowboxesp;
                Visual[3].AssociatedBool = PluginConfig.boneesp;
                Visual[6].AssociatedBool = PluginConfig.ProximityAlert;
                Visual[7].AssociatedBool = PluginConfig.fullbright;
                Visual[8].stringsliderind = PluginConfig.skycolour;
                Visual[9].AssociatedBool = PluginConfig.whyiseveryonelookingatme;
                //Visual2
                Visual2[0].AssociatedBool = PluginConfig.NoLeaves;
                Visual2[1].AssociatedBool = PluginConfig.showboards;

                //Tracers
                Tracers[0].stringsliderind = PluginConfig.tracers;
                Tracers[1].stringsliderind = PluginConfig.tracersize;

                //Nametags
                NameTags[0].AssociatedBool = PluginConfig.NameTags;
                NameTags[1].AssociatedBool = PluginConfig.ShowCreationDate;
                NameTags[2].AssociatedBool = PluginConfig.ShowColourCode;
                NameTags[3].AssociatedBool = PluginConfig.ShowDistance;
                NameTags[4].stringsliderind = PluginConfig.nametagheight;
                NameTags[5].stringsliderind = PluginConfig.nametagsize;
                NameTags[6].stringsliderind = PluginConfig.nametagcolour;

                //Player
                Player[0].AssociatedBool = PluginConfig.nofinger;
                Player[1].AssociatedBool = PluginConfig.taggun;
                Player[2].AssociatedBool = PluginConfig.creepermonkey;
                Player[3].AssociatedBool = PluginConfig.ghostmonkey;
                Player[4].AssociatedBool = PluginConfig.invismonkey;
                Player[5].stringsliderind = PluginConfig.tagaura;
                Player[6].AssociatedBool = PluginConfig.tagall;
                Player[7].AssociatedBool = PluginConfig.desync;
                Player[8].stringsliderind = PluginConfig.hitboxes;
                Player[9].AssociatedBool = PluginConfig.fakelag;
                Player[10].AssociatedBool = PluginConfig.rainbowmonkey;
                Player[11].AssociatedBool = PluginConfig.namechanger;

                Player2[0].AssociatedBool = PluginConfig.decapitation;
                Player2[1].AssociatedBool = PluginConfig.antitag;
                Player2[2].AssociatedBool = PluginConfig.Bees;

                //Exploit
                Exploits[0].AssociatedBool = PluginConfig.breaknametags;
                Exploits[1].AssociatedBool = PluginConfig.ChangeNameAll;
                Exploits[2].AssociatedBool = PluginConfig.audiocrash;
                Exploits[4].AssociatedBool = PluginConfig.lagall;
                Exploits[7].AssociatedBool = PluginConfig.CrashAll;

                Exploits2[1].AssociatedBool = PluginConfig.snowballgun;
                Exploits2[2].stringsliderind = PluginConfig.projectiletype;
                Exploits2[6].AssociatedBool = PluginConfig.SpazInfection;
                Exploits2[7].AssociatedBool = PluginConfig.bangun;

                Exploits3[0].AssociatedBool = PluginConfig.kickgun;
                Exploits3[1].AssociatedBool = PluginConfig.laggun;
                Exploits3[2].AssociatedBool = PluginConfig.rigspam;
                Exploits3[3].AssociatedBool = PluginConfig.ChangeNameGun;
                Exploits3[4].AssociatedBool = PluginConfig.MaterialSpamAll;
                Exploits3[5].AssociatedBool = PluginConfig.MaterialSpamGun;
                Exploits3[6].AssociatedBool = PluginConfig.ChangeNameGun;

                CosmeticsSpoofer[0].AssociatedBool = PluginConfig.spazallcosmeticstryon;
                CosmeticsSpoofer[1].AssociatedBool = PluginConfig.spazallcosmetics;
                // Safety
                Safety[0].AssociatedBool = PluginConfig.Panic;
                Safety[1].stringsliderind = PluginConfig.antireport;
                Safety[3].AssociatedBool = PluginConfig.pccheckbypass;
                Safety[4].AssociatedBool = PluginConfig.fakequestmenu;
                Safety[5].AssociatedBool = PluginConfig.anticrash;
                Safety[6].stringsliderind = PluginConfig.anticrashtype;

                //Settings
                Settings[1].stringsliderind = PluginConfig.MenuPosition;
                Settings[5].AssociatedBool = PluginConfig.PlayerLogging;
                Settings[6].AssociatedBool = PluginConfig.invertedControls;
                //Settings[7].AssociatedBool = PluginConfig.legacyui;
                Settings[7].stringsliderind = PluginConfig.menufont;

                Computer[8].AssociatedBool = PluginConfig.Turning;
                Gamemodes[0].AssociatedBool = PluginConfig.moddedgamemode;
                Gamemodes[1].AssociatedBool = PluginConfig.competitivegamemode;

                //MusicPlayer
                MusicPlayer[3].AssociatedBool = PluginConfig.loopmusic;
                MusicPlayer[4].AssociatedBool = PluginConfig.soundboard;
                MusicPlayer[5].stringsliderind = PluginConfig.volume;

                Macro[3].AssociatedBool = PluginConfig.recordmacro;
                Macro[5].AssociatedBool = PluginConfig.autoplayproximity;
                Macro[6].stringsliderind = PluginConfig.autoplaydistance;
                Macro[7].stringsliderind = PluginConfig.macrolerpspeed;

                //Colour Settings
                ColourSettings[0].stringsliderind = PluginConfig.MenuColour;
                ColourSettings[1].stringsliderind = PluginConfig.GhostColour;
                ColourSettings[2].stringsliderind = PluginConfig.BeamColour;
                ColourSettings[3].stringsliderind = PluginConfig.ESPColour;
                ColourSettings[4].stringsliderind = PluginConfig.GhostOpacity;
                ColourSettings[5].stringsliderind = PluginConfig.HitBoxesOpacity;
                ColourSettings[6].stringsliderind = PluginConfig.HitBoxesColour;

                // Console
                if (Console.ConsoleQolossal.instance.IsAdmin(PhotonNetwork.LocalPlayer.UserId))
                {
                    Dev[3].AssociatedBool = PluginConfig.consoleusersnametags;

                    ConsoleGuns[0].AssociatedBool = PluginConfig.consolequitgun;
                    ConsoleGuns[1].AssociatedBool = PluginConfig.consolebringgun;
                    ConsoleGuns[2].AssociatedBool = PluginConfig.consolekickgun;
                    ConsoleGuns[3].AssociatedBool = PluginConfig.consolechangenamegun;
                    ConsoleGuns[4].AssociatedBool = PluginConfig.consolerestartmicgun;
                    ConsoleGuns[5].AssociatedBool = PluginConfig.consoleghostgun;
                    ConsoleGuns[6].AssociatedBool = PluginConfig.consoleunghostgun;
                    ConsoleGuns[7].AssociatedBool = PluginConfig.consolemutegun;
                    ConsoleGuns[8].AssociatedBool = PluginConfig.consoleunmutegun;
                    ConsoleGuns[9].AssociatedBool = PluginConfig.consoledisablemovementgun;
                    ConsoleGuns[10].AssociatedBool = PluginConfig.consoleenablemovementgun;
                    ConsoleGuns[11].AssociatedBool = PluginConfig.consoletargetplayergun;
                    ConsoleGuns[12].AssociatedBool = PluginConfig.consoleflinggun;
                }

                if (Plugin.update)
                {
                    MenuHubText.text = "<color=red>QOLOSSAL NEEDS TO BE UPDATED</color>";
                    return;
                }
                if (Plugin.locked || Plugin.serverLocked)
                {
                    MenuHubText.text = "<color=red>QOLOSSAL HAS BEEN LOCKED\nKILLSWITCHED TOGGLED: EITHER LEAKED OR CRACKED</color>";
                    return;
                }
                if (!Plugin.hasvalidkey)
                {
                    MenuHubText.text = "<color=yellow>DONT TRY TO CRACK QOLOSSAL\nYOUR IP AND HWID\nHAS BEEN COLLECTED\nDUE TO SAFETY CONCERNS</color>";
                    return;
                }

                string ToDraw = Plugin.sussy ? $"<color={MenuColour}>SUSSY : {MenuState}</color>\n" : $"<color={MenuColour}>QOLOSSAL : {MenuState}</color>\n";
                int i = 0;
                if (CurrentViewingMenu != null)
                {
                    foreach (MenuOption opt in CurrentViewingMenu)
                    {
                        if (SelectedOptionIndex == i)
                            ToDraw = ToDraw + "> ";
                        ToDraw = ToDraw + opt.DisplayName + " " + opt.extra;

                        if (opt._type == Plugin.togglethingy)
                        {
                            if (opt.AssociatedBool == true)
                                ToDraw = ToDraw + $" <color={MenuColour}>[ON]</color>";
                            else
                                ToDraw = ToDraw + " <color=red>[OFF]</color>";
                        }
                        if (opt._type == Plugin.sliderthingy)
                        {
                            string sliderText = opt.StringArray[opt.stringsliderind];
                            string color = sliderText == "[OFF]" ? "red" : $"{MenuColour}";
                            ToDraw = ToDraw + ": <color=" + color + ">" + sliderText + "</color> [" + (opt.stringsliderind + 1).ToString() + "/" + opt.StringArray.Length.ToString() + "]";
                        }
                        string bindsDisplay = CustomBinding.GetBinds(opt.DisplayName.Replace(" ", "").Replace("(", "").Replace(")", "").ToLower());
                        if (!string.IsNullOrEmpty(bindsDisplay))
                        {
                            ToDraw += $" <color={MenuColour}>[{bindsDisplay}]</color>";
                        }
                        ToDraw = ToDraw + "\n";
                        i++;
                    }
                    //Testtext.text = ToDraw;
                    MenuHubText.text = ToDraw;
                }
                else
                    Debug.Log("Null for some reason");
            }
        }
        public static void UpdateMenuState(MenuOption option, string _MenuState, string OperationType)
        {
            try
            {
                ToolTips.HandToolTips(MenuState, SelectedOptionIndex);
                Settings[2].StringArray = Configs.GetConfigFileNames();
                MusicPlayer[0].StringArray = Music.GetSongFileNames();
                Macro[0].StringArray = Qolossal.Macro.GetMacros();

                if (OperationType == "optionhit")
                {
                    if (option._type == Plugin.submenuthingy)
                    {
                        string newMenuState = option.AssociatedString == Plugin.backthingy ? "MainMenu" : option.AssociatedString;

                        if (!PluginConfig.legacyui)
                        {
                            // Create a new panel without reusing from panelMap
                            GameObject newPanel = new GameObject();
                            newPanel.name = $"{newMenuState}_{Guid.NewGuid().ToString()}"; // Unique name to avoid conflicts
                            newPanel.transform.SetParent(AssetBundleLoader.hud.transform, false);

                            // Position logic (similar to GUICreator.NewUI)
                            Vector3 basePosition = Camera.main.transform.position + Vector3.forward * 0.3f;
                            Vector3 spawnPosition = basePosition;
                            int offsetCount = 0;
                            bool positionValid = false;
                            while (!positionValid)
                            {
                                positionValid = true;
                                foreach (var panel in GUICreator.openPanels)
                                {
                                    if (panel.RootObject.activeSelf && Vector3.Distance(panel.RootObject.transform.localPosition, spawnPosition) < GUICreator.panelOffset * 0.5f)
                                    {
                                        offsetCount++;
                                        spawnPosition = basePosition + new Vector3(GUICreator.panelOffset * offsetCount, 0, 0);
                                        positionValid = false;
                                        break;
                                    }
                                }
                            }
                            newPanel.transform.localPosition = spawnPosition;
                            newPanel.transform.localRotation = Quaternion.identity;

                            // Add animator (optional)
                            Animator animator = newPanel.AddComponent<Animator>();
                            if (AssetBundleLoader.Menu_Controller != null)
                            {
                                animator.runtimeAnimatorController = AssetBundleLoader.Menu_Controller;
                                animator.Play("Menu_In");
                            }

                            // Create and initialize the new panel
                            PanelElement newPanelElement = new PanelElement(newPanel);
                            GUICreator.openPanels.Add(newPanelElement);
                            GUICreator.panelMap[newPanel.name] = newPanelElement; // Store with unique name

                            // Update the new panel with the submenu options
                            MenuOption[] options = GUICreator.GetMenuOptions(newMenuState);
                            PanelElement.UpdatePanel(newPanelElement, options);

                            // Optional: Keep the old panel open instead of hiding it
                            // if (activePanel != null) activePanel.RootObject.SetActive(false); // Remove this if you want multiple panels open

                            activePanel = newPanelElement; // Set the new panel as active (optional, depending on your needs)
                        }

                        MenuState = newMenuState;
                        CurrentViewingMenu = GUICreator.GetMenuOptions(newMenuState);
                        SelectedOptionIndex = 0;
                    }
                    if (!PluginConfig.legacyui && activePanel != null)
                    {
                        PanelElement.UpdatePanel(activePanel, CurrentViewingMenu); // Update the active panel
                    }

                    if (option._type == Plugin.togglethingy)
                    {
                        var values = new Dictionary<string, object>();
                        foreach (var prop in typeof(PluginConfig).GetFields(BindingFlags.Public | BindingFlags.Static))
                        {
                            values[prop.Name] = prop.GetValue(null);
                            object parsedValue = values[prop.Name];
                            if (parsedValue is bool parsedBoolValue)
                            {
                                if (string.Equals(prop.Name.Replace(" ", "").Replace("(", "").Replace(")", ""), option.DisplayName.Replace(" ", "").Replace("(", "").Replace(")", ""), StringComparison.OrdinalIgnoreCase))
                                {
                                    prop.SetValue(null, !parsedBoolValue);
                                    Notifacations.SendNotification($"<color={MenuColour}>[TOGGLED]</color> {option.DisplayName} : {!parsedBoolValue}");
                                    break;
                                }
                            }
                        }
                    }
                    if (option._type == Plugin.buttonthingy)
                    {
                        //Movement
                        if (option.AssociatedString == "teleporttorandom" && PhotonNetwork.InRoom)
                        {
                            List<MeshCollider> colliders = new List<MeshCollider>();
                            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                                colliders.Add(collider);
                            VRRig[] vrrigList = GorillaParent.instance.vrrigs.ToArray();
                            System.Random random = new System.Random();
                            int randomIndex = random.Next(0, vrrigList.Count());
                            VRRig randomVRRig = vrrigList[randomIndex];
                            foreach (MeshCollider c in colliders)
                                c.enabled = false;
                            GorillaTagger.Instance.transform.position = randomVRRig.transform.position - GorillaTagger.Instance.bodyCollider.transform.position + GorillaTagger.Instance.transform.position;
                            foreach (MeshCollider c in colliders)
                                c.enabled = true;
                        }

                        //Exploits
                        if (option.AssociatedString == "Unlock All Cosmetics")
                        {
                            foreach (Plugin.CosmeticItem item in Plugin.GetAllCosmetics())
                            {
                                Plugin.UnlockItem(item.displayName);
                                Plugin.UpdateWardrobeModelsAndButtons();
                                GorillaTagger.Instance.offlineVRRig.UpdateAllowedCosmetics();
                                GorillaTagger.Instance.offlineVRRig.SetCosmeticsActive();
                            }
                            //Plugin.RigRPC("UpdateCosmetics", RpcTarget.All, new object[] { "", "", "" });
                        }

                        if (option.AssociatedString == "Clear Prefabs")
                        {
                            if (PhotonNetwork.InRoom)
                            {
                                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                                PhotonNetwork.DestroyAll();
                                PhotonNetwork.SendAllOutgoingCommands();
                            }
                        }
                        if (option.AssociatedString == "Clear Infection")
                        {
                            if (PhotonNetwork.InRoom && GorillaComputer.instance.currentGameMode == "INFECTION")
                            {
                                foreach (GorillaTagManager tag in GameObject.FindObjectsOfType<GorillaTagManager>())
                                {
                                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                                    tag.EndInfectionGame();
                                    tag.ClearInfectionState();
                                    PhotonNetwork.SendAllOutgoingCommands();
                                }  
                            }
                        }
                        if (option.AssociatedString == "Anti Ban")
                        {
                            PhotonNetwork.Disconnect();
                            PlayFabClientAPI.ForgetAllCredentials();
                            Notifacations.SendNotification("<color=yellow>[ANTIBAN]</color> Starting.");
                            string[] newIds = new string[] 
                            {
                                "QOLOSSAL", "ON",  "TOP", "QOLOSSALCHEATMENUV3", "QCMV3", "NOVA IS SILLY", "ANTIBAN BRO", "ANTI", "BAN",
                                "CANT BAN ME LMAO", "SHITTY ASS GAME", "DK WHY YOUR TRING TO BAN ME", "LIKE BRO", "YOU CANT BAN US",
                                "QCMV3", "COLOSSAL", "COLOSSALCHEATMENUV2", "234234", "353245245WFSGF", "HDSF974G FPWFW", "G8WDFWEPFWEBF",
                                "DSGIYFDSPFI", "DS8FG4-9", "DSF89YTGEWPGFWE", "GDSF8PS"
                            };
                            int random = UnityEngine.Random.Range(0, newIds.Length);
                            string actualNewId = newIds[random];
                            AuthenticationValues authValues = new AuthenticationValues(actualNewId);
                            authValues.UserId = actualNewId;
                            authValues.AuthType = CustomAuthenticationType.Custom;
                            PhotonNetwork.AuthValues = authValues;
                            PlayFabAuthenticator.instance.loginFailed = false;
                            PhotonNetwork.ConnectUsingSettings();
                            Notifacations.SendNotification("<color=yellow>[ANTIBAN]</color> Finished anti ban.");
                        }

                        //Computer
                        if (option.AssociatedString == "disconnect")
                        {
                            PhotonNetwork.Disconnect();
                        }
                        if (option.AssociatedString == "randomidentity")
                        {
                            string[] names =
                            {
                                "NOVA",
                                "COLOSSUS",
                                "123",
                                "PP",
                                "PBBV",
                                "SKILLISSUE",
                                "IMAGINE",
                                "SREN17",
                                "YOURMOM",
                                "GUMMIES",
                                "WATCH",
                                "MOUSE",
                                "BOZO",
                                "KEYS",
                                "PINE",
                                "LEMMING",
                                "ELECTRONIC",
                                "BODA",
                                "TTTPIG",
                                "TTTPIGFAN",
                                "555999",
                                "83459230",
                                "923059439",
                                "IJ48FNSF",
                                "MF4J8T9J",
                                "J3VU",
                                "3993NF39",
                                "FEMBOY",
                                "RAWR",
                                "MEOW",
                            };
                            System.Random rand = new System.Random();
                            int index = rand.Next(names.Length);
                            PhotonNetwork.LocalPlayer.NickName = names[index];
                            GorillaComputer.instance.currentName = names[index];
                            GorillaComputer.instance.savedName = names[index];
                            PlayerPrefs.SetString("GorillaTaggerName", names[index]);
                        }
                        if (option.AssociatedString == "join GTC")
                        {
                            Plugin.networkController.AttemptToJoinSpecificRoom("GTC");
                        }
                        if (option.AssociatedString == "join TTT")
                        {
                            Plugin.networkController.AttemptToJoinSpecificRoom("TTT");
                        }
                        if (option.AssociatedString == "join YTTV")
                        {
                            Plugin.networkController.AttemptToJoinSpecificRoom("YTTV");
                        }
                        if (option.AssociatedString == "join MODS")
                        {
                            Plugin.networkController.AttemptToJoinSpecificRoom("MODS");
                        }
                        if (option.AssociatedString == "join MOD")
                        {
                            Plugin.networkController.AttemptToJoinSpecificRoom("MOD");
                        }
                        if (option.AssociatedString == "join 1")
                        {
                            Plugin.networkController.AttemptToJoinSpecificRoom("1");
                        }
                        if (option.AssociatedString == "join PUBLIC")
                        {
                            PhotonNetwork.JoinRandomRoom();
                        }
                        if (option.AssociatedString == "join QCMV3 Only")
                        {
                            Plugin.networkController.AttemptToJoinSpecificRoom("@QCMV3@");
                        }
                        if (option.AssociatedString.Contains("cgamemode"))
                        {
                            string gamemode = option.AssociatedString.Substring(9);
                            string formatted = gamemode.Replace(" ", "");
                            GorillaComputer.instance.currentGameMode = PluginConfig.moddedgamemode ? $"MODDED_{formatted}" : formatted;
                            GorillaComputer.instance.currentQueue = PluginConfig.competitivegamemode ? "COMPETITIVE" : "DEFAULT";
                        }

                        // Configs
                        if (option.AssociatedString == "loadconfig")
                        {
                            string text = (Settings.Length > 2 && Settings[2]?.StringArray != null && Settings[2].StringArray.Length > Settings[2].stringsliderind) ? Settings[2].StringArray[Settings[2].stringsliderind] + ".json" : null;
                            string text2 = text != null ? Path.Combine(Configs.configPath, text) : null;
                            if (text2 != null && File.Exists(text2))
                                Configs.LoadConfig(text2);
                            else
                                Notifacations.SendNotification(text2 != null ? "Config file not found: " + text2 : "Invalid configuration or index out of range.");
                        }

                        if (option.AssociatedString == "saveconfig")
                            Configs.SaveConfig();

                        if (option.AssociatedString == "playmusic")
                        {
                            Music.PlayMusic();
                        }
                        if (option.AssociatedString == "stopmusic")
                        {
                            Music.StopAllSounds();
                        }

                        // Macro
                        if (option.AssociatedString == "loadmacro")
                        {
                            Qolossal.Macro.Instance.StartPlayback(Macro[0].StringArray[Macro[0].stringsliderind]);
                        }
                        if (option.AssociatedString == "stopmacro")
                        {
                            Qolossal.Macro.Instance.StopPlayback();
                        }
                        if (option.AssociatedString == "deletemacro")
                        {
                            Qolossal.Macro.Instance.DeleteMacro(Macro[0].StringArray[Macro[0].stringsliderind]);
                        }

                        // Exploits
                        if (option.AssociatedString == "setmaster" && PhotonNetwork.InRoom)
                        {
                            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                        } 
                        if (option.AssociatedString == "gamemodecasual" && PhotonNetwork.InRoom)
                        {
                            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                            hash.Add("gameMode", "forestDEFAULTCASUAL");
                            PhotonNetwork.CurrentRoom.LoadBalancingClient.OpSetCustomPropertiesOfRoom(hash);
                        }
                        if (option.AssociatedString == "gamemodeinfection" && PhotonNetwork.InRoom)
                        {
                            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                            hash.Add("gameMode", "forestDEFAULTINFECTION");
                            PhotonNetwork.CurrentRoom.LoadBalancingClient.OpSetCustomPropertiesOfRoom(hash);
                        }
                        if (option.AssociatedString == "gamemodehunt" && PhotonNetwork.InRoom)
                        {
                            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                            hash.Add("gameMode", "forestDEFAULTHUNT");
                            PhotonNetwork.CurrentRoom.LoadBalancingClient.OpSetCustomPropertiesOfRoom(hash);
                        }
                        if (option.AssociatedString == "Break Gamemode" && PhotonNetwork.InRoom)
                        {
                            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                            hash.Add("gameMode", "forestDEFAULTERROR");
                            PhotonNetwork.CurrentRoom.LoadBalancingClient.OpSetCustomPropertiesOfRoom(hash);
                            WhatAmI.infectionmanager().ClearInfectionState();
                            WhatAmI.infectionmanager().EndInfectionGame();
                        }
                        if (option.AssociatedString == "Break Room" && PhotonNetwork.InRoom)
                        {
                            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                            hash.Add("gameMode", "forestDEFAULTERROR");
                            PhotonNetwork.CurrentRoom.LoadBalancingClient.OpSetCustomPropertiesOfRoom(hash);
                            WhatAmI.infectionmanager().ClearInfectionState();
                            WhatAmI.infectionmanager().EndInfectionGame();
                            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
                            {
                                PhotonNetwork.CurrentRoom.StorePlayer(plr);
                                PhotonNetwork.CurrentRoom.RemovePlayer(plr);
                                PhotonNetwork.DestroyPlayerObjects(plr);
                                PhotonNetwork.CurrentRoom.AddPlayer(plr);
                                PhotonNetwork.DestroyPlayerObjects(plr);
                                PhotonNetwork.SetMasterClient(plr);
                                PhotonNetwork.DestroyAll();
                            }
                        }
                        if (option.AssociatedString == "Spawn Network Player")
                        {
                            if (PhotonNetwork.InRoom)
                                PhotonNetwork.Instantiate("Network Player", GorillaTagger.Instance.myVRRig.headMesh.transform.position, GorillaTagger.Instance.myVRRig.headMesh.transform.rotation);
                        }
                        if (option.AssociatedString == "Spawn Stickable Target")
                        {
                            if (PhotonNetwork.InRoom)
                                PhotonNetwork.Instantiate("STICKABLE TARGET", GorillaTagger.Instance.myVRRig.headMesh.transform.position, GorillaTagger.Instance.myVRRig.headMesh.transform.rotation);
                        }
                        if (option.AssociatedString == "Give All Custom Properties")
                        {
                            if (PhotonNetwork.InRoom)
                            {
                                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                                foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
                                {
                                    Hashtable therenewprops = new Hashtable();
                                    therenewprops.Add("GET FUCKED BY QOLOSSAL CHEAT MENU V3", "GET FUCKED BY QOLOSSAL CHEAT MENU V3");
                                    plr.SetCustomProperties(therenewprops);
                                }
                            }
                        }
                        if (option.AssociatedString == "Kick All")
                        {
                            if (PhotonNetwork.InRoom)
                            {
                                foreach (VRRig plr in GorillaParent.instance.vrrigs)
                                {
                                    if (plr != GorillaTagger.Instance.myVRRig)
                                    {
                                        object meorwmosmd = new object { };
                                        if (PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion.ToLower() != "omnytag")
                                            meorwmosmd = "Gorilla Player Networked";
                                        else
                                            meorwmosmd = "Gorilla Player Actual";
                                        int[] hi = { plr.photonView.Owner.ActorNumber };
                                        List<int> list = new List<int>();
                                        LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
                                        byte eventCode = 202;
                                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                                        for (int i = 0; i < 500; i++)
                                        {
                                            Hashtable data = new Hashtable();
                                            object viewIds = new object[2]
                                            {
                                                plr.photonView.ViewID,
                                                plr.photonView.ViewID
                                            };
                                            data.Add(0, Plugin.BoxAny(meorwmosmd));
                                            data.Add(6, Plugin.BoxAny(PhotonNetwork.ServerTimestamp));
                                            data.Add(4, Plugin.BoxAny(viewIds));
                                            data.Add(7, Plugin.BoxAny(plr.photonView.ViewID));
                                            networkingClient.OpRaiseEvent(eventCode, Plugin.BoxAny(data), new RaiseEventOptions { TargetActors = hi }, SendOptions.SendReliable);
                                        }
                                        PhotonNetwork.DestroyPlayerObjects(plr.photonView.Owner);
                                        PhotonNetwork.CurrentRoom.Players.Remove(plr.photonView.Owner.ActorNumber);
                                        PhotonNetwork.SendAllOutgoingCommands();
                                    }
                                }
                            }
                        }
                        if (option.AssociatedString == "banall" && PhotonNetwork.InRoom)
                        {
                            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
                            {
                                WebClient webClient = new WebClient();
                                webClient.Headers.Add("Content-Type", "application/json");
                                webClient.Headers.Add("User-Agent", "banneratqolossallol");
                                var content = new
                                {
                                    titleId = PlayFabSettings.TitleId,
                                    playerId = plr.UserId
                                };
                                string json;
                                using (var stringWriter = new StringWriter())
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    serializer.Serialize(stringWriter, content);
                                    json = stringWriter.ToString();
                                }
                                webClient.UploadString("https://api-nova-two.vercel.app/banusingcloudscript", "POST", json);
                            }
                        }

                        if (option.AssociatedString == "Clone Self")
                        {
                            if (PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion.ToLower() != "omnytag")
                                PhotonNetwork.Instantiate("gorillaprefabs/Gorilla Player Networked", new Vector3(0, 0, 0), Quaternion.identity);
                            else
                                PhotonNetwork.Instantiate("gorillaprefabs/Gorilla Player Actual", new Vector3(0, 0, 0), Quaternion.identity);
                        }
                        if (option.AssociatedString == "High Quality Mic")
                        {
                            Recorder rec = null;
                            if (GameObject.Find("Network Voice") == null)
                                rec = GameObject.Find("Photon Manager").GetComponent<Recorder>();
                            else
                                rec = GameObject.Find("Network Voice").GetComponent<Recorder>();
                            if (rec == null)
                                Notifacations.SendNotification("<color=yellow>[WARNING]</color> Recorder is null, unable to use mic mods.");
                            PropertyInfo samplingRateProperty = typeof(Photon.Voice.Unity.Recorder).GetProperty("SamplingRate");
                            if (samplingRateProperty != null && samplingRateProperty.CanWrite)
                                samplingRateProperty.SetValue(rec, SamplingRate.Sampling48000);
                            PropertyInfo bitrateProperty = typeof(Photon.Voice.Unity.Recorder).GetProperty("Bitrate");
                            if (bitrateProperty != null && bitrateProperty.CanWrite)
                                bitrateProperty.SetValue(rec, 500000);
                            MethodInfo restartMethod = typeof(Photon.Voice.Unity.Recorder).GetMethod("RestartRecording");
                            if (restartMethod != null)
                                restartMethod.Invoke(rec, new object[] { true });
                        }
                        if (option.AssociatedString == "Low Quality Mic")
                        {
                            Recorder rec = null;
                            if (GameObject.Find("Network Voice") == null)
                                rec = GameObject.Find("Photon Manager").GetComponent<Recorder>();
                            else
                                rec = GameObject.Find("Network Voice").GetComponent<Recorder>();
                            if (rec == null)
                                Notifacations.SendNotification("<color=yellow>[WARNING]</color> Recorder is null, unable to use mic mods.");
                            PropertyInfo samplingRateProperty = typeof(Photon.Voice.Unity.Recorder).GetProperty("SamplingRate");
                            if (samplingRateProperty != null && samplingRateProperty.CanWrite)
                                samplingRateProperty.SetValue(rec, SamplingRate.Sampling48000);
                            PropertyInfo bitrateProperty = typeof(Photon.Voice.Unity.Recorder).GetProperty("Bitrate");
                            if (bitrateProperty != null && bitrateProperty.CanWrite)
                                bitrateProperty.SetValue(rec, 6000);
                            MethodInfo restartMethod = typeof(Photon.Voice.Unity.Recorder).GetMethod("RestartRecording");
                            if (restartMethod != null)
                                restartMethod.Invoke(rec, new object[] { true });
                        }
                        if (option.AssociatedString == "Fix Mic")
                        {
                            Music.StopAllSounds();
                        }

                        // Console
                        if (option.AssociatedString == "Console Quit All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleQuitAll();
                        }
                        if (option.AssociatedString == "Console Bring All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleBringAll();
                        }
                        if (option.AssociatedString == "Console Kick All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleKickAll();
                        }
                        if (option.AssociatedString == "Console Change Name All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleChangeNameAll();
                        }
                        if (option.AssociatedString == "Console Restart Mic All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleRestartMicAll();
                        }
                        if (option.AssociatedString == "Console Ghost All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleGhostAll();
                        }
                        if (option.AssociatedString == "Console Unghost All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleUnGhostAll();
                        }
                        if (option.AssociatedString == "Console Mute All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleMuteAll();
                        }
                        if (option.AssociatedString == "Console Unmute All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleUnMuteAll();
                        }
                        if (option.AssociatedString == "Console Disable Movement All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleDisableMovementAll();
                        }
                        if (option.AssociatedString == "Console Enable Movement All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleEnableMovementAll();
                        }
                        if (option.AssociatedString == "Console Target All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleTargetPlayerAll();
                        }
                        if (option.AssociatedString == "Console Fling All")
                        {
                            Console.Mods.ConsoleGuns.ConsoleFlingAll();
                        }
                        if (option.AssociatedString == "Comfirm Using")
                        {
                            Console.ConsoleQolossal.ConsoleBeacon();
                        }
                    }

                    if (PluginConfig.MenuColour != 6)
                    {
                        if (menurgb != 0)
                            menurgb = 0;
                    }
                    switch (PluginConfig.MenuColour)
                    {
                        case 0:
                            MenuColour = "magenta";
                            break;
                        case 1:
                            MenuColour = "red";
                            break;
                        case 2:
                            MenuColour = "yellow";
                            break;
                        case 3:
                            MenuColour = "green";
                            break;
                        case 4:
                            MenuColour = "blue";
                            break;
                    }
                    switch (PluginConfig.MenuPosition)
                    {
                        case 0:
                            MenuHubText.alignment = TextAnchor.UpperLeft;
                            Notifacations.NotiHubText.alignment = TextAnchor.UpperRight;
                            break;
                        case 1:
                            MenuHubText.alignment = TextAnchor.MiddleCenter;
                            Notifacations.NotiHubText.alignment = TextAnchor.UpperLeft;
                            break;
                        case 2:
                            MenuHubText.alignment = TextAnchor.UpperRight;
                            Notifacations.NotiHubText.alignment = TextAnchor.UpperLeft;
                            break;
                    }
                    switch (PluginConfig.menufont)
                    {
                        case 0: Plugin.gtagfont = GameObject.Find("COC Text").GetComponent<Text>().font; MenuHubText.font = Plugin.gtagfont; Overlay.OverlayHubText.font = Plugin.gtagfont; Overlay.OverlayHubTextRoom.font = Plugin.gtagfont; Notifacations.NotiHubText.font = Plugin.gtagfont; ToolTips.Testtext.font = Plugin.gtagfont; ; break;
                        case 1: Plugin.gtagfont = Resources.GetBuiltinResource<Font>("Arial.ttf"); MenuHubText.font = Plugin.gtagfont; Overlay.OverlayHubText.font = Plugin.gtagfont; Overlay.OverlayHubTextRoom.font = Plugin.gtagfont; Notifacations.NotiHubText.font = Plugin.gtagfont; ToolTips.Testtext.font = Plugin.gtagfont; break;
                    }
                }
            }
            catch { }
        }
    }
}