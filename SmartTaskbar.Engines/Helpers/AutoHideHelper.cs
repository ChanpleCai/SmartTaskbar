﻿using System;
using System.Runtime.InteropServices;
using static SmartTaskbar.PlatformInvoke.SafeNativeMethods;

namespace SmartTaskbar.Engines.Helpers
{
    internal static class AutoHideHelper
    {
        private const int AbsAutohide = 1;
        private const int AbsAlwaysontop = 2;
        private const uint AbmSetstate = 10;
        private const uint AbmGetstate = 4;
        private static AppbarData _msgData = new() {cbSize = (uint) Marshal.SizeOf(typeof(AppbarData))};


        internal static void SetAutoHide()
        {
            _msgData.lParam = AbsAutohide;
            SHAppBarMessage(AbmSetstate, ref _msgData);
            PostMessageHelper.HideTaskbar();
        }


        internal static void SetAutoHide(bool isAutoHide)
        {
            if (isAutoHide)
                SetAutoHide();
            else
                CancelAutoHide();
        }


        internal static void CancelAutoHide()
        {
            _msgData.lParam = AbsAlwaysontop;
            SHAppBarMessage(AbmSetstate, ref _msgData);
        }


        internal static bool NotAutoHide()
            => SHAppBarMessage(AbmGetstate, ref _msgData) == IntPtr.Zero;
    }
}
