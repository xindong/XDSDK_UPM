using System;
using System.Collections.Generic;

namespace xdsdk.Unity
{

    public class AppInfo
    {
        private string id;

        private string name;

        private string pingppAppID;

        private string wxAppID;

        private string qqAppID;

        private string bundleID;

        private string alipayEname;

        private string ename;

        private string wxQRCodeAppID;

        private string kfApp;

        private string relationConfirmed;

        private string taptapClientID;

        private string tapdbAppID;

        private string kefuLink;

        private long needLoginRealName;

        private long needChargeRealName;

        private long needRegisterRealName;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string PingppAppID
        {
            get
            {
                return pingppAppID;
            }

            set
            {
                pingppAppID = value;
            }
        }

        public string WxAppID
        {
            get
            {
                return wxAppID;
            }

            set
            {
                wxAppID = value;
            }
        }

        public string QqAppID
        {
            get
            {
                return qqAppID;
            }

            set
            {
                qqAppID = value;
            }
        }

        public string BundleID
        {
            get
            {
                return bundleID;
            }

            set
            {
                bundleID = value;
            }
        }

        public string AlipayEname
        {
            get
            {
                return alipayEname;
            }

            set
            {
                alipayEname = value;
            }
        }

        public string Ename
        {
            get
            {
                return ename;
            }

            set
            {
                ename = value;
            }
        }

        public string WxQRCodeAppID
        {
            get
            {
                return wxQRCodeAppID;
            }

            set
            {
                wxQRCodeAppID = value;
            }
        }

        public string KfApp
        {
            get
            {
                return kfApp;
            }

            set
            {
                kfApp = value;
            }
        }

        public string RelationConfirmed
        {
            get
            {
                return relationConfirmed;
            }

            set
            {
                relationConfirmed = value;
            }
        }

        public string TaptapClientID
        {
            get
            {
                return taptapClientID;
            }

            set
            {
                taptapClientID = value;
            }
        }

        public string TapdbAppID
        {
            get
            {
                return tapdbAppID;
            }

            set
            {
                tapdbAppID = value;
            }
        }

        public string KefuLink
        {
            get
            {
                return kefuLink;
            }

            set
            {
                kefuLink = value;
            }
        }

        public long NeedLoginRealName
        {
            get
            {
                return needLoginRealName;
            }

            set
            {
                needLoginRealName = value;
            }
        }

        public long NeedChargeRealName
        {
            get
            {
                return needChargeRealName;
            }

            set
            {
                needChargeRealName = value;
            }
        }

        public long NeedRegisterRealName
        {
            get
            {
                return needRegisterRealName;
            }

            set
            {
                needRegisterRealName = value;
            }
        }

        public static AppInfo InitWithDict(Dictionary<string, object> appinfoDict)
        {
            AppInfo info = new AppInfo();
            if (appinfoDict.ContainsKey("id"))
            {
                info.Id = appinfoDict["id"] as string;
            }
            if (appinfoDict.ContainsKey("qq_appid"))
            {
                info.QqAppID = appinfoDict["qq_appid"] as string;
            }
            if (appinfoDict.ContainsKey("wx_qrcode_appid"))
            {
                info.WxQRCodeAppID = appinfoDict["wx_qrcode_appid"] as string;
            }
            if (appinfoDict.ContainsKey("taptap_client_id"))
            {
                info.TaptapClientID = appinfoDict["taptap_client_id"] as string;
            }
            if (appinfoDict.ContainsKey("need_login_shiming"))
            {
                info.NeedLoginRealName = (Int64)appinfoDict["need_login_shiming"];
            }
            if (appinfoDict.ContainsKey("need_charge_shiming"))
            {
                info.NeedChargeRealName = (Int64)appinfoDict["need_charge_shiming"];
            }
            if (appinfoDict.ContainsKey("need_register_shiming"))
            {
                info.NeedRegisterRealName = (Int64)appinfoDict["need_register_shiming"];
            }
            return info;
        }
    }

}
