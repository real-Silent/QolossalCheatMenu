using GorillaNetworking;
using Photon.Pun;
using UnityEngine;

namespace Qolossal
{
    public class WhatAmI
    {
        public static float GetDefaultSpeeds() // Could prob be done better -nova
        {
            if (PhotonNetwork.InRoom)
            {
                return 6.5f;
            }
            return 6.5f;
        }
        public static bool IsInfected(Photon.Realtime.Player who)
        {
            if (infectionmanager() != null)
            {
                if (infectionmanager().currentInfected.Contains(who))
                        return true;
            }
            return false;
        }
        public static bool LocalRig()
        {
            if (PhotonNetwork.InRoom)
            {
                return GorillaTagger.Instance.myVRRig;
            }
            return false;
        }
        public static string CurrentGamemode()
        {
            if (GorillaGameManager.instance == null)
                return "CASUAL";
            if (GorillaHuntManager.instance != null)
                return "HUNT";
            if (GorillaTagManager.instance != null)
                return "INFECTION";
            if (GorillaComputer.instance != null && !string.IsNullOrEmpty(GorillaComputer.instance.currentGameMode))
            {
                string mode = GorillaComputer.instance.currentGameMode;
                if (mode.Contains("BATTLE") || mode.Contains("PAINTBRAWL"))
                    return "BATTLE";
            }
            return "CASUAL";
        }
        public static bool IsPlayerSomethingWithTag(VRRig who)
        {
            return IsPlayerIt(who) || IsPlayerTagged(who);
        }
        public static bool IsPlayerIt(VRRig who)
        {
            if (infectionmanager() == null)
                return false;
            if (infectionmanager().currentIt == who.photonView.Owner)
                return true;
            return false;
        }
        public static bool IsPlayerTagged(VRRig who)
        {
            if (infectionmanager() == null)
                return false;
            if (infectionmanager().currentInfected.Contains(who.photonView.Owner))
                return true;
            return false;
        }
        public static GorillaTagManager infectionmanager()
        {
            foreach (GorillaTagManager tag in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                return tag;
            }
            return null;
        }
        public static void ImABetterMaster() // mars the other stuff errors -nova
        {
            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            }
        }
    }
}