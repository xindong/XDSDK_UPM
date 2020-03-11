using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xdsdk.Unity.Service
{
    public class PlayLog
    {

        private static PlayLog instance;

        private static GameObject uploaderObject;

        public static PlayLog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayLog();

                }
                if (uploaderObject == null)
                {
                    instance.Init();
                }
                return instance;
            }
        }

        private void Init()
        {
            uploaderObject = new GameObject
            {
                name = "XDSDKPlayLogUploader"
            };
            uploaderObject.AddComponent<Uploader>();
            UnityEngine.Object.DontDestroyOnLoad(uploaderObject);
        }

        public void StartTrack(string userId, string token)
        {
            Uploader uploader = uploaderObject.GetComponent<Uploader>();
            if (uploader == null)
            {
                uploader = uploaderObject.AddComponent<Uploader>();
            }
            uploader.StartAutoUpload(userId, token);
        }

        public void StopTrack()
        {
            Uploader uploader = uploaderObject.GetComponent<Uploader>();
            if (uploader == null)
            {
                uploader = uploaderObject.AddComponent<Uploader>();
            }
            uploader.StopAutoUpload();
        }

        private class Uploader : MonoBehaviour
        {
            private static readonly float INTERVAL = 10.0f;

            private long serverStartTimestamp = 0L;
            private long localStartTimestamp = 0L;

            private readonly List<long[]> stashedLocalTimes = new List<long[]>();
            private readonly List<long[]> stashedServerTimes = new List<long[]>();

            private string currentUserId;
            private string currentToken;

            public void StartAutoUpload(string userId, string token)
            {
                if(!userId.Equals(currentUserId))
                {
                    currentUserId = null;
                    currentToken = null;
                    stashedLocalTimes.Clear();
                    stashedServerTimes.Clear();
                }
                currentUserId = userId;
                currentToken = token;
                StartCoroutine("AutoUpload");
            }

            public void StopAutoUpload()
            {
                StopCoroutine("AutoUpload");
                currentUserId = null;
                currentToken = null;
                stashedLocalTimes.Clear();
                stashedServerTimes.Clear();
            }

            private IEnumerator AutoUpload()
            {
                bool waitGetServerTime = true;
                Api.Instance.GetServerTime(
                    (long timestamp) =>
                        {
                            serverStartTimestamp = timestamp;
                            waitGetServerTime = false;
                        },
                    (string error) =>
                        {
                            waitGetServerTime = false;
                        });
                while (waitGetServerTime)
                {
                    yield return new WaitForSeconds(1.0f);
                }
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                localStartTimestamp = Convert.ToInt64(ts.TotalSeconds);

                long localLastEndTimestamp = Convert.ToInt64(ts.TotalSeconds);
                while (true)
                {
                    yield return new WaitForSeconds(INTERVAL);
                    ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    long localCurrentEndTimestamp = Convert.ToInt64(ts.TotalSeconds);
                    long localCurrentStartTimestamp = localLastEndTimestamp;
                    long serverCurrentEndTimestamp = localCurrentEndTimestamp + serverStartTimestamp - localStartTimestamp;
                    long serverCurrentStartTimestamp = localLastEndTimestamp + serverStartTimestamp - localStartTimestamp;
                    stashedLocalTimes.Add(new long[] { localCurrentStartTimestamp, localCurrentEndTimestamp });
                    stashedServerTimes.Add(new long[] { serverCurrentStartTimestamp, serverCurrentEndTimestamp });
                    localLastEndTimestamp = localCurrentEndTimestamp;
                    bool waitSetPlayLog = true;
                    Api.Instance.SetPlayLog(
                        currentToken,
                        stashedLocalTimes.ToArray(),
                        stashedServerTimes.ToArray(),
                        (result) =>
                        {
                            stashedLocalTimes.Clear();
                            stashedServerTimes.Clear();
                            waitSetPlayLog = false;
                        },
                        (string error) =>
                        {
                            Debug.Log("SetPlayLog error " + error);
                            waitSetPlayLog = false;
                        }
                        );
                    while (waitSetPlayLog)
                    {
                        yield return new WaitForSeconds(1.0f);
                    }


                }
            }

        }
    }

}
