﻿using System;
using DryIoc;
using Prism.DryIoc;
using TodoDemo.i18n;
using TodoDemo.Droid.i18n;

namespace TodoDemo.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainer container)
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
            container.Register<ILocalize, Localize>(Reuse.Singleton);
        }
    }
}
