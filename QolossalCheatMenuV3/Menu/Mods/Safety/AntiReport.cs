using Qolossal.Menu;
using Qolossal.Notifacation;
using Photon.Pun;
using UnityEngine;
using System;

namespace Qolossal.Mods
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class AntiReport : MonoBehaviour
    {
        public AntiReport(IntPtr e) : base(e) { }
        public virtual void Update()
        {
            if (PluginConfig.antireport == 0)
            {
                GameObject.Destroy(Plugin.holder.GetComponent<AntiReport>());
                return;
            }

            if(PluginConfig.antireport != 0 && PhotonNetwork.InRoom)
            {
                foreach (GorillaPlayerScoreboardLine gorillaPlayerScoreboardLine in GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>())
                {
                    if (gorillaPlayerScoreboardLine.linePlayer.UserId == PhotonNetwork.LocalPlayer.UserId)
                    {
                        Transform transform = gorillaPlayerScoreboardLine.reportButton.gameObject.transform;
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                float distance = 0.45f;
                                if (Vector3.Distance(vrrig.rightHandTransform.position, transform.position) < distance || Vector3.Distance(vrrig.leftHandTransform.position, transform.position) < distance)
                                {
                                    Notifacations.SendNotification($"<color=red>[ANTIREPORT]</color> {vrrig.photonView.Owner.NickName} Attempted");

                                    switch (PluginConfig.antireport)
                                    {
                                        case 1:
                                            PhotonNetwork.Disconnect();
                                            break;
                                        case 2:
                                            string currentroom = PhotonNetwork.CurrentRoom.Name;
                                             PhotonNetwork.Disconnect();
                                             Plugin.networkController.AttemptToJoinSpecificRoom(currentroom);
                                            break;
                                        case 3:
                                            PhotonNetwork.Disconnect();
                                            PhotonNetwork.JoinRandomRoom();
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}