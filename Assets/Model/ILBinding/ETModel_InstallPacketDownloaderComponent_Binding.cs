using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class ETModel_InstallPacketDownloaderComponent_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ETModel.InstallPacketDownloaderComponent);

            field = type.GetField("remoteInstallPacketConfig", flag);
            app.RegisterCLRFieldGetter(field, get_remoteInstallPacketConfig_0);
            app.RegisterCLRFieldSetter(field, set_remoteInstallPacketConfig_0);


        }



        static object get_remoteInstallPacketConfig_0(ref object o)
        {
            return ((ETModel.InstallPacketDownloaderComponent)o).remoteInstallPacketConfig;
        }
        static void set_remoteInstallPacketConfig_0(ref object o, object v)
        {
            ((ETModel.InstallPacketDownloaderComponent)o).remoteInstallPacketConfig = (ETModel.InstallPacketConfig)v;
        }


    }
}
