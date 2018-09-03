using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xdsdk.Unity
{
    public class LoginEntry
    {
        public enum Type
        {
            WX,
            Tap,
            QQ,
            Guest,
            XD,
            None,
        };

        private Type first = Type.WX;
        private Type second = Type.Tap;
        private Type third = Type.QQ;
        private Type fourth = Type.None;

        public Type First
        {
            get
            {
                return first;
            }
        }

        public Type Second
        {
            get
            {
                return second;
            }
        }

        public Type Third
        {
            get
            {
                return third;
            }

        }

        public Type Fourth
        {
            get
            {
                return fourth;
            }
        }

        public LoginEntry()
        {

        }

        public LoginEntry(string[] entries) : this()
        {
            if (entries != null)
            {
                SetStringArrayToCurrentEntries(entries);
            }
            else
            {
                SetDefaultEntries();
            }
        }

        public void HideGuest()
        {
            SetStringArrayToCurrentEntries(RemoveStringFromArray("GUEST_LOGIN",
                                                                 CurrentEntriesToStringArray()));
        }

        public void HideWX()
        {
            SetStringArrayToCurrentEntries(RemoveStringFromArray("WX_LOGIN",
                                                     CurrentEntriesToStringArray()));
        }

        public void HideQQ()
        {
            SetStringArrayToCurrentEntries(RemoveStringFromArray("QQ_LOGIN",
                                                     CurrentEntriesToStringArray()));
        }

        public void HideTapTap()
        {
            SetStringArrayToCurrentEntries(RemoveStringFromArray("TAPTAP_LOGIN",
                                                     CurrentEntriesToStringArray()));
        }

        public void SetEntries(string[] entries)
        {
            SetStringArrayToCurrentEntries(entries);
        }

        private void SetDefaultEntries()
        {
            first = Type.WX;
            second = Type.Tap;
            third = Type.QQ;
            fourth = Type.None;
        }

        private void SetStringArrayToCurrentEntries(string[] entries)
        {
            first = entries.Length > 0 ? ParseEntryToType(entries[0]) : Type.None;
            second = entries.Length > 1 ? ParseEntryToType(entries[1]) : Type.None;
            third = entries.Length > 2 ? ParseEntryToType(entries[2]) : Type.None;
            fourth = entries.Length > 3 ? ParseEntryToType(entries[3]) : Type.None;
        }

        private string[] CurrentEntriesToStringArray()
        {
            string[] entries = new string[4];
            entries[0] = EntryToString(first);
            entries[1] = EntryToString(second);
            entries[2] = EntryToString(third);
            entries[3] = EntryToString(fourth);
            return entries;
        }


        private static string[] RemoveStringFromArray(String s, String[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrEmpty(array[i]) && array[i].Equals(s))
                {
                    array[i] = null;
                }
            }
            return TrimStringArray(array);

        }



        private static string EntryToString(Type entry)
        {
            switch (entry)
            {
                case Type.WX:
                    return "WX_LOGIN";
                case Type.Tap:
                    return "TAPTAP_LOGIN";
                case Type.QQ:
                    return "QQ_LOGIN";
                case Type.Guest:
                    return "GUEST_LOGIN";
                case Type.XD:
                    return "XD_LOGIN";
                default:
                    return null;
            }
        }


        private static Type ParseEntryToType(string entry)
        {
            return string.IsNullOrEmpty(entry)
                         ? Type.None
                             : entry.Equals("WX_LOGIN")
                         ? Type.WX
                             : entry.Equals("TAPTAP_LOGIN")
                         ? Type.Tap
                             : entry.Equals("QQ_LOGIN")
                         ? Type.QQ
                             : entry.Equals("GUEST_LOGIN")
                         ? Type.Guest
                             : entry.Equals("XD_LOGIN")
                         ? Type.XD
                             : Type.None;
        }


        private static string[] TrimStringArray(string[] array)
        {
            List<string> temp = new List<string>();
            foreach (string s in array)
            {
                if (!string.IsNullOrEmpty(s))
                    temp.Add(s);
            }
            array = temp.ToArray();
            return array;
        }
    }
}
