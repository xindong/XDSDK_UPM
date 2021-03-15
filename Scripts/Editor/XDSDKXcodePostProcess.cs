#if UNITY_EDITOR && UNITY_IOS
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif
using UnityEngine;
using TDSEditor;

 public class XDSDKXcodePostProcess
    {
#if UNITY_IOS
        // 添加标签，unity导出工程后自动执行该函数
        [PostProcessBuildAttribute(99)]
        /* 
            2020-11-20 Jiang Jiahao
            该脚本中参数为DEMO参数，项目组根据实际参数修改
            导出工程后核对配置或依赖是否正确，根据需要修改脚本
        */
        public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)
        {
            if (BuildTarget == BuildTarget.iOS)
            {   
                // 获得工程路径
                string projPath = PBXProject.GetPBXProjectPath(path);
                UnityEditor.iOS.Xcode.PBXProject proj = new PBXProject();
                proj.ReadFromString(File.ReadAllText(projPath));

                // 2019.3以上有多个target
#if UNITY_2019_3_OR_NEWER
                string unityFrameworkTarget = proj.GetUnityFrameworkTargetGuid();
                string target = proj.GetUnityMainTargetGuid();
#else
                string unityFrameworkTarget = proj.TargetGuidByName("Unity-iPhone");
                string target = proj.TargetGuidByName("Unity-iPhone");
#endif
                if (target == null)
                {
                    Debug.Log("target is null ?");
                    return;
                }

                proj.SetBuildProperty(target,"RUNPATH_SEARCH_PATHS","@executable_path/Frameworks"); 
                proj.SetBuildProperty(unityFrameworkTarget,"RUNPATH_SEARCH_PATHS","@executable_path/Frameworks"); 
                proj.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
                proj.SetBuildProperty(unityFrameworkTarget, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");

                proj.SetBuildProperty(unityFrameworkTarget, "CODE_SIGN_STYLE", "Manual");
                
                // 添加资源文件，注意文件路径
                var resourcePath = Path.Combine(path, "XDSDKResource");

                string parentFolder = Directory.GetParent(Application.dataPath).FullName;

                if (Directory.Exists(resourcePath))
                {
                    Directory.Delete(resourcePath,true);
                }

                Directory.CreateDirectory(resourcePath);

                string remotePackagePath = TDSFileHelper.FilterFile(parentFolder + "/Library/PackageCache/","com.xd.sdk@");

                string localPacckagePath = TDSFileHelper.FilterFile(parentFolder,"XDSDK");

                string tdsResourcePath = remotePackagePath !=null ? remotePackagePath + "/Plugins/iOS/Resource" : localPacckagePath + "/Plugins/iOS/Resource";
                
                if(Directory.Exists(tdsResourcePath)){
                    Debug.Log($"Copy XDSDKResource to XCode Project:{tdsResourcePath}");
                    TDSFileHelper.CopyAndReplaceDirectory(tdsResourcePath, resourcePath);
                    List<string> names = new List<string>();    
                    names.Add("XDSDKResouse.bundle");
                    foreach (var name in names)
                    {
                        proj.AddFileToBuild(target, proj.AddFile(Path.Combine(resourcePath,name), Path.Combine(resourcePath,name), PBXSourceTree.Source));
                    }
                }
                // 复制资源文件夹到工程目录
                if(File.Exists(parentFolder + "/Assets/Plugins/iOS/Resource/XDSDK-Info.plist")){
                    File.Copy(parentFolder + "/Assets/Plugins/iOS/Resource/XDSDK-Info.plist", resourcePath + "/XDSDK-Info.plist");
                }

                // capabilities 
                string fileName = "Unity-iPhone" + ".entitlements";
                string entitleFilePath = path + "/" + fileName;
                PlistDocument tempEntitlements = new PlistDocument();
                
                string key_associatedDomains = "com.apple.developer.associated-domains";
                string key_signinWithApple = "com.apple.developer.applesignin";

                string isNeedAppleSignIn = GetValueFromPlist(resourcePath + "/XDSDK-Info.plist","Apple_SignIn_Enable");
                string domain = GetValueFromPlist(resourcePath + "/XDSDK-Info.plist","Game_Domain");
                if(isNeedAppleSignIn!=null && isNeedAppleSignIn.Equals("true"))
                {
                    var arr_signinWithApple = (tempEntitlements.root[key_signinWithApple] = new PlistElementArray()) as PlistElementArray;
                    arr_signinWithApple.values.Add(new PlistElementString("Default"));
                    // Sign In With Apple
                    proj.AddCapability (target, PBXCapabilityType.SignInWithApple,entitleFilePath);
                }
                if(domain!=null)
                {
                    var arr_associateDomains = (tempEntitlements.root[key_associatedDomains] = new PlistElementArray()) as PlistElementArray;
                    arr_associateDomains.values.Add(new PlistElementString("applinks:"+domain));
                    proj.AddCapability(target, PBXCapabilityType.AssociatedDomains, entitleFilePath);
                }

                tempEntitlements.WriteToFile(entitleFilePath);

                File.WriteAllText(projPath, proj.WriteToString());
                SetPlist(path,resourcePath + "/XDSDK-Info.plist");
                SetScriptClass(path);
                Debug.Log("测试打包成功");
            }
        }

        // 添加动态库 注意路径
        public static void AddFramework(string coreFrameworkName, UnityEditor.iOS.Xcode.PBXProject proj, string target)
        {
            const string defaultLocationInProj = "Library/";
            string framework = Path.Combine(defaultLocationInProj, coreFrameworkName);
            string fileGuid = proj.AddFile(framework, "Frameworks/" + framework, PBXSourceTree.Sdk);
            PBXProjectExtensions.AddFileToEmbedFrameworks(proj, target, fileGuid);
            proj.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
        }

        // 修改Build版本号，使用时间 『年月日时分秒』
        public static string AddBuildNumber()
        {
            string buildNumber = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Hour.ToString() +
            DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            return buildNumber;
        }

        public static void CopyAndReplaceDirectory(string srcPath, string dstPath)
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

        public static void DeleteDirectory(string path)
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

        private static string GetValueFromPlist(string infoPlistPath,string key)
        {
            if(infoPlistPath==null)
            {
                return null;
            }
            Dictionary<string, object> dic = (Dictionary<string, object>)Plist.readPlist(infoPlistPath);
            foreach (var item in dic)
            {
                if(item.Key.Equals(key))
                {
                    return (string)item.Value;
                }
            }
            return null;
        }

        // 修改pilist
        private static void SetPlist(string pathToBuildProject,string infoPlistPath)
        {
           //添加info
            string _plistPath = pathToBuildProject + "/Info.plist";
            PlistDocument _plist = new PlistDocument();
            _plist.ReadFromString(File.ReadAllText(_plistPath));
            PlistElementDict _rootDic = _plist.root;

            List<string> items = new List<string>()
            {
                "tapsdk",
                "mqq",
                "mqqapi",
                "wtloginmqq2",
                "mqqopensdkapiV4",
                "mqqopensdkapiV3",
                "mqqopensdkapiV2",
                "mqqwpa",
                "mqqOpensdkSSoLogin",
                "mqqgamebindinggroup",
                "mqqopensdkfriend",
                "mqzone",
                "weixin",
                "wechat",
                "weixinULAPI"
            };
            PlistElementArray _list = _rootDic.CreateArray("LSApplicationQueriesSchemes");
            for (int i = 0; i < items.Count; i++)
            {
                _list.AddString(items[i]);
            }

            PlistElementDict _dict = _rootDic.CreateDict("NSAppTransportSecurity");
            _dict.SetBoolean("NSAllowsArbitraryLoads", true); // HTTP
            _rootDic.SetString("CFBundleVersion", AddBuildNumber());

            PlistElementDict dict = _plist.root.AsDict();
            
            if(!string.IsNullOrEmpty(infoPlistPath))
            {   
                Dictionary<string, object> dic = (Dictionary<string, object>)Plist.readPlist(infoPlistPath);
                string xdId = null;
                string tencentId = null;
                string wechatId = null;

                foreach (var item in dic)
                {
                    if(item.Key.Equals("XD"))
                    {
                        Dictionary<string,object> xdDic = (Dictionary<string,object>) item.Value;
                        foreach (var xdItem in xdDic)
                        {
                            if(xdItem.Key.Equals("client_id")){
                                xdId = (string) xdItem.Value;
                                break;
                            }
                        }
                    } 
                    else if(item.Key.Equals("tencent"))
                    {
                        Dictionary<string,object> tencentDic = (Dictionary<string,object>) item.Value;
                        foreach (var tencentItem in tencentDic)
                        {
                            if(tencentItem.Key.Equals("client_id")){
                                tencentId = (string) tencentItem.Value;
                                break;
                            }
                        }
                    }
                    else if(item.Key.Equals("wechat"))
                    {
                        Dictionary<string,object> wechatDic = (Dictionary<string,object>) item.Value;
                        foreach (var wechatItem in wechatDic)
                        {
                            if(wechatItem.Key.Equals("client_id")){
                                wechatId = (string) wechatItem.Value;
                                break;
                            }
                        }
                    }
                    else {
                        //Copy XDSDK-Info.plist中的数据
                        _rootDic.SetString(item.Key.ToString(),item.Value.ToString());
                    }
                }
                PlistElementArray array = dict.CreateArray("CFBundleURLTypes");

                PlistElementDict dict2 = array.AddDict();

                if(xdId!=null)
                {
                    dict2 = array.AddDict();
                    dict2.SetString("CFBundleURLName", "XD");
                    PlistElementArray array2 = dict2.CreateArray("CFBundleURLSchemes");
                    array2 = dict2.CreateArray("CFBundleURLSchemes");
                    array2.AddString(xdId);             
                }

                if(tencentId!=null)
                {
                    dict2 = array.AddDict();
                    dict2.SetString("CFBundleURLName", "tencent");
                    PlistElementArray array2 = dict2.CreateArray("CFBundleURLSchemes");
                    array2 = dict2.CreateArray("CFBundleURLSchemes");
                    array2.AddString(tencentId);   
                }

                if(wechatId!=null)
                {
                    dict2 = array.AddDict();
                    dict2.SetString("CFBundleURLName", "wechat");
                    PlistElementArray array2 = dict2.CreateArray("CFBundleURLSchemes");
                    array2 = dict2.CreateArray("CFBundleURLSchemes");
                    array2.AddString(wechatId);   
                }

            }

            string exitsOnSuspendKey = "UIApplicationExitsOnSuspend";
            if(_rootDic.values.ContainsKey(exitsOnSuspendKey))
            {
                _rootDic.values.Remove(exitsOnSuspendKey);
            }

            File.WriteAllText(_plistPath, _plist.WriteToString());
            Debug.Log("修改添加info文件成功");
        }

        // 添加appdelegate处理
        private static void SetScriptClass(string pathToBuildProject)
        {
            //插入代码
            //读取UnityAppController.mm文件
            string unityAppControllerPath = pathToBuildProject + "/Classes/UnityAppController.mm";
            XClass UnityAppController = new XClass(unityAppControllerPath);
            //在指定代码后面增加一行代码
            UnityAppController.WriteBelow(@"#import <OpenGLES/ES2/glext.h>", @"#import <XdComPlatform/XDCore.h>");
            UnityAppController.WriteBelow(@"[KeyboardDelegate Initialize];",@"[XDCore setupXDStore];");
            UnityAppController.WriteBelow(@"AppController_SendNotificationWithArg(kUnityOnOpenURL, notifData);",@"return [XDCore HandleXDOpenURL:url];");
            UnityAppController.WriteBelow(@"NSURL* url = userActivity.webpageURL;",@"[XDCore handleOpenUniversalLink:userActivity];");
            Debug.Log("修改代码成功");
        }
    }

    internal partial class XClass : System.IDisposable
    {
        private string filePath;

        public XClass(string fPath)
        {
            filePath = fPath;
            if (!System.IO.File.Exists(filePath))
            {
                Debug.LogError(filePath + "路径下文件不存在");
                return;
            }
        }

        public void WriteBelow(string below, string text)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string all = streamReader.ReadToEnd();
            streamReader.Close();
            int beginIndex = all.IndexOf(below, StringComparison.Ordinal);
            if (beginIndex == -1)
            {
                Debug.LogError(filePath + "中没有找到字符串" + below);
                return;
            }

            int endIndex = all.LastIndexOf("\n", beginIndex + below.Length, StringComparison.Ordinal);
            all = all.Substring(0, endIndex) + "\n" + text + "\n" + all.Substring(endIndex);
            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(all);
            streamWriter.Close();
        }

        public void Replace(string below, string newText)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string all = streamReader.ReadToEnd();
            streamReader.Close();
            int beginIndex = all.IndexOf(below, StringComparison.Ordinal);
            if (beginIndex == -1)
            {
                Debug.LogError(filePath + "中没有找到字符串" + below);
                return;
            }

            all = all.Replace(below, newText);
            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(all);
            streamWriter.Close();
        }

        public void Dispose()
        {
        }
#endif
}
#endif
