﻿using System;
using System.Diagnostics;

namespace SmartTaskbar.Hook
{
    public class ServerInterface : MarshalByRefObject
    {
        public void Ping()
            => Debug.WriteLine("ping");
    }
}
