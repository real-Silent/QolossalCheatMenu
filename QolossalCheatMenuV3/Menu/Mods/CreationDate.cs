using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using MelonLoader;
using System.Collections;

namespace Qolossal
{
    public static class CreationDate
    {
        private static float RequestTimeoutSeconds = 10f;

        public static void GetCreationDate(VRRig vrrig, Action<string> callback)
        {
            MelonCoroutines.Start(GetCreationDateCoroutine(vrrig, callback));
        }

        private static IEnumerator GetCreationDateCoroutine(VRRig vrrig, Action<string> callback)
        {
            var request = new GetAccountInfoRequest
            {
                PlayFabId = vrrig.photonView.Owner.UserId
            };
            GetAccountInfoResult result = null;
            bool completed = false;
            PlayFabClientAPI.GetAccountInfo(
                request,
                new System.Action<GetAccountInfoResult>(response =>
                {
                    result = response;
                    completed = true;
                }),
                new System.Action<PlayFabError>(error =>
                {
                    OnPlayFabError(error, vrrig);
                    completed = true;
                })
            );
            float startTime = Time.time;
            while (!completed && Time.time - startTime < RequestTimeoutSeconds)
                yield return null;
            if (!completed)
            {
                CustomConsole.LogToConsole($"[QOLOSSAL] PlayFab request timed out for user {vrrig.photonView.Owner.UserId}");
                callback?.Invoke(null);
                yield break;
            }
            callback?.Invoke(OnAccountInfoReceived(result));
        }

        private static string OnAccountInfoReceived(GetAccountInfoResult result)
        {
            if (result?.AccountInfo != null)
            {
                Il2CppSystem.DateTime creationDateTime = result.AccountInfo.Created;
                return creationDateTime.ToString("yyyy-MM-dd");
            }
            return null;
        }

        private static void OnPlayFabError(PlayFabError error, VRRig vrrig)
        {
            CustomConsole.LogToConsole($"[QOLOSSAL] PlayFab error: {error.ErrorMessage}");
        }
    }
}