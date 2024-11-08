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
    unsafe class DragonBones_DragonBoneEventDispatcher_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(DragonBones.DragonBoneEventDispatcher);
            args = new Type[]{typeof(System.String), typeof(DragonBones.ListenerDelegate<DragonBones.EventObject>)};
            method = type.GetMethod("AddDBEventListener", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AddDBEventListener_0);
            args = new Type[]{typeof(System.String), typeof(DragonBones.ListenerDelegate<DragonBones.EventObject>)};
            method = type.GetMethod("RemoveDBEventListener", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, RemoveDBEventListener_1);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("HasDBEventListener", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, HasDBEventListener_2);


        }


        static StackObject* AddDBEventListener_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            DragonBones.ListenerDelegate<DragonBones.EventObject> @listener = (DragonBones.ListenerDelegate<DragonBones.EventObject>)typeof(DragonBones.ListenerDelegate<DragonBones.EventObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @type = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            DragonBones.DragonBoneEventDispatcher instance_of_this_method = (DragonBones.DragonBoneEventDispatcher)typeof(DragonBones.DragonBoneEventDispatcher).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.AddDBEventListener(@type, @listener);

            return __ret;
        }

        static StackObject* RemoveDBEventListener_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            DragonBones.ListenerDelegate<DragonBones.EventObject> @listener = (DragonBones.ListenerDelegate<DragonBones.EventObject>)typeof(DragonBones.ListenerDelegate<DragonBones.EventObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @type = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            DragonBones.DragonBoneEventDispatcher instance_of_this_method = (DragonBones.DragonBoneEventDispatcher)typeof(DragonBones.DragonBoneEventDispatcher).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.RemoveDBEventListener(@type, @listener);

            return __ret;
        }

        static StackObject* HasDBEventListener_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @type = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            DragonBones.DragonBoneEventDispatcher instance_of_this_method = (DragonBones.DragonBoneEventDispatcher)typeof(DragonBones.DragonBoneEventDispatcher).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.HasDBEventListener(@type);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }



    }
}
