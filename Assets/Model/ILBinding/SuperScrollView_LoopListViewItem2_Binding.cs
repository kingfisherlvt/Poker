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
    unsafe class SuperScrollView_LoopListViewItem2_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(SuperScrollView.LoopListViewItem2);
            args = new Type[]{};
            method = type.GetMethod("get_IsInitHandlerCalled", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_IsInitHandlerCalled_0);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("set_IsInitHandlerCalled", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_IsInitHandlerCalled_1);
            args = new Type[]{};
            method = type.GetMethod("get_CachedRectTransform", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_CachedRectTransform_2);
            args = new Type[]{};
            method = type.GetMethod("get_ItemIndex", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_ItemIndex_3);
            args = new Type[]{};
            method = type.GetMethod("get_DistanceWithViewPortSnapCenter", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_DistanceWithViewPortSnapCenter_4);
            args = new Type[]{typeof(System.Single)};
            method = type.GetMethod("set_Padding", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_Padding_5);


        }


        static StackObject* get_IsInitHandlerCalled_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            SuperScrollView.LoopListViewItem2 instance_of_this_method = (SuperScrollView.LoopListViewItem2)typeof(SuperScrollView.LoopListViewItem2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.IsInitHandlerCalled;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* set_IsInitHandlerCalled_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @value = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SuperScrollView.LoopListViewItem2 instance_of_this_method = (SuperScrollView.LoopListViewItem2)typeof(SuperScrollView.LoopListViewItem2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.IsInitHandlerCalled = value;

            return __ret;
        }

        static StackObject* get_CachedRectTransform_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            SuperScrollView.LoopListViewItem2 instance_of_this_method = (SuperScrollView.LoopListViewItem2)typeof(SuperScrollView.LoopListViewItem2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.CachedRectTransform;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_ItemIndex_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            SuperScrollView.LoopListViewItem2 instance_of_this_method = (SuperScrollView.LoopListViewItem2)typeof(SuperScrollView.LoopListViewItem2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.ItemIndex;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_DistanceWithViewPortSnapCenter_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            SuperScrollView.LoopListViewItem2 instance_of_this_method = (SuperScrollView.LoopListViewItem2)typeof(SuperScrollView.LoopListViewItem2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.DistanceWithViewPortSnapCenter;

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* set_Padding_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @value = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            SuperScrollView.LoopListViewItem2 instance_of_this_method = (SuperScrollView.LoopListViewItem2)typeof(SuperScrollView.LoopListViewItem2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Padding = value;

            return __ret;
        }



    }
}
