using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace XDSDK_Editor
{

    class ProjectBuild : Editor
    {

        static void ExportUnityPackage()
        {
            string ExportPath = "";

            string Version = "";

            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("-EXPORT_PATH"))
                {
                    ExportPath = arg.Split('=')[1].Trim('"');
                    Debug.Log("ExportPath:" + ExportPath);
                }
                else if (arg.StartsWith("-SDK_VERSION"))
                {
                    Version = arg.Split('=')[1].Trim('"');
                    Debug.Log("SDK_VERSION:" + Version);
                }
            }

            string CreatePath = "UnityPackage";

            if (Directory.Exists(CreatePath))
            {
                DeleteDirectory(CreatePath);
            }
            Directory.CreateDirectory(CreatePath);

            ExportUnitySDKPackage(CreatePath, Version);

            ExportUnityDemoPackdge(CreatePath, Version);

            CopyAndReplaceDirectory(CreatePath, ExportPath);

            if (Directory.Exists(CreatePath))
            {
                DeleteDirectory(CreatePath);
            }
        }

        static void ExportUnitySDKPackage(string createPath, string version)
        {
            var path = createPath + "/XDSDK_Unity_v" + version + ".unitypackage";
            string[] resPaths = { "Assets/Library/", "Assets/Plugins/" };
            var assetPathNames = AssetDatabase.GetDependencies(resPaths);
            AssetDatabase.ExportPackage(assetPathNames, path, ExportPackageOptions.Recurse);
        }

        static void ExportUnityDemoPackdge(string createPath, string version)
        {
            var path = createPath + "/XDSDK_Unity_v" + version + "_demo.unitypackage";
            var assetPathNames = AssetDatabase.GetDependencies("Assets/");
            AssetDatabase.ExportPackage(assetPathNames, path, ExportPackageOptions.Recurse);
        }

        //在这里找出你当前工程所有的场景文件，假设你只想把部分的scene文件打包 那么这里可以写你的条件判断 总之返回一个字符串数组。
        static string[] GetBuildScenes()
        {
            List<string> names = new List<string>();
            foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
            {
                if (e == null)
                    continue;
                if (e.enabled)
                    names.Add(e.path);
            }
            return names.ToArray();
        }

        private static void UpdateSetting(string key, string environmentKey, string defaultMacOSPath)
        {
            string defaultPath = defaultMacOSPath;
            if (!string.IsNullOrEmpty(defaultPath) && Directory.Exists(defaultPath))
            {
                Debug.Log("set:" + key + " path:" + defaultPath);
                EditorPrefs.SetString(key, defaultPath);
                return;
            }

            throw new DirectoryNotFoundException(string.Format("{0} {1} {2}", key, environmentKey, defaultPath));
        }

        static void DeleteDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                DirectoryInfo[] childs = dir.GetDirectories();
                foreach (DirectoryInfo child in childs)
                {
                    child.Delete(true);
                }
                dir.Delete(true);
            }
        }

        static void CopyAndReplaceDirectory(string srcPath, string dstPath)
        {
            if (Directory.Exists(dstPath))
                DeleteDirectory(dstPath);
            if (File.Exists(dstPath))
                File.Delete(dstPath);

            Directory.CreateDirectory(dstPath);

            foreach (var file in Directory.GetFiles(srcPath))
                File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));

            foreach (var dir in Directory.GetDirectories(srcPath))
                CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
        }

        static void BuildForAndroid()
        {

            string ExportPath = "";

            string Version = "";

            string UnityVersion = "";

            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("-EXPORT_PATH"))
                {
                    ExportPath = arg.Split('=')[1].Trim('"');
                    Debug.Log("ExportPath:" + ExportPath);
                }
                else if (arg.StartsWith("-SDK_VERSION"))
                {
                    Version = arg.Split('=')[1].Trim('"');
                    Debug.Log("SDK_VERSION:" + Version);
                }
                else if (arg.StartsWith("-UNITY_VERSION"))
                {
                    UnityVersion = arg.Split('=')[1].Trim('"');
                    Debug.Log("unityVersion:" + UnityVersion);
                }
            }
            // 签名文件配置，若不配置，则使用Unity默认签名
            PlayerSettings.Android.keyaliasName = "wxlogin";
            PlayerSettings.Android.keyaliasPass = "111111";
            PlayerSettings.Android.keystoreName = Application.dataPath.Replace("/Assets", "") + "/sign.keystore";
            PlayerSettings.Android.keystorePass = "111111";

            UpdateSetting("AndroidSdkRoot", "ANDROID_SDK", "/Applications/Unity/Hub/Editor/" + UnityVersion + "/PlaybackEngines/AndroidPlayer/SDK");

            UpdateSetting("AndroidNdkRoot", "ANDROID_NDK", "/Applications/Unity/Hub/Editor/" + UnityVersion + "/PlaybackEngines/AndroidPlayer/NDK");

            string path = (ExportPath + "/" + "XDSDKUnity_" + Version + ".apk").Replace("//", "/");

            Debug.Log("path:" + path);
            try
            {
                BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.Android, BuildOptions.None);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        static void BuildForIOS()
        {

            string path = Application.dataPath.Replace("/Assets", "") + "/XDSDKUnityDemo";

            AssetDatabase.Refresh();
            try
            {
                BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.iOS, BuildOptions.None);
            }
            catch (System.Exception m)
            {
                Debug.LogError(m.Message);
            }
        }


    }

}
