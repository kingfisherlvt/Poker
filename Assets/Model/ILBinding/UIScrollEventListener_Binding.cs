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
    unsafe class UIScrollEventListener_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::UIScrollEventListener);
            args = new Type[]{typeof(UnityEngine.GameObject)};
            method = type.GetMethod("Get", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Get_0);
            args = new Type[]{typeof(UnityEngine.GameObject)};
            method = type.GetMethod("RegisterButton", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, RegisterButton_1);

            field = type.GetField("onDrag", flag);
            app.RegisterCLRFieldGetter(field, get_onDrag_0);
            app.RegisterCLRFieldSetter(field, set_onDrag_0);
            field = type.GetField("onUp", flag);
            app.RegisterCLRFieldGetter(field, get_onUp_1);
            app.RegisterCLRFieldSetter(field, set_onUp_1);
            field = type.GetField("onBeginDrag", flag);
            app.RegisterCLRFieldGetter(field, get_onBeginDrag_2);
            app.RegisterCLRFieldSetter(field, set_onBeginDrag_2);
            field = type.GetField("onEndDrag", flag);
            app.RegisterCLRFieldGetter(field, get_onEndDrag_3);
            app.RegisterCLRFieldSetter(field, set_onEndDrag_3);


        }


        static StackObject* Get_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.GameObject @go = (UnityEngine.GameObject)typeof(UnityEngine.GameObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = global::UIScrollEventListener.Get(@go);

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* RegisterButton_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.GameObject @go = (UnityEngine.GameObject)typeof(UnityEngine.GameObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            global::UIScrollEventListener instance_of_this_method = (global::UIScrollEventListener)typeof(global::UIScrollEventListener).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.RegisterButton(@go);

            return __ret;
        }


        static object get_onDrag_0(ref object o)
        {
            return ((global::UIScrollEventListener)o).onDrag;
        }
        static void set_onDrag_0(ref object o, object v)
        {
            ((global::UIScrollEventListener)o).onDrag = (global::UIScrollEventListener.VoidDelegate)v;
        }
        static object get_onUp_1(ref object o)
        {
            return ((global::UIScrollEventListener)o).onUp;
        }
        static void set_onUp_1(ref object o, object v)
        {
            ((global::UIScrollEventListener)o).onUp = (global::UIScrollEventListener.VoidDelegate)v;
        }
        static object get_onBeginDrag_2(ref object o)
        {
            return ((global::UIScrollEventListener)o).onBeginDrag;
        }
        static void set_onBeginDrag_2(ref object o, object v)
        {
            ((global::UIScrollEventListener)o).onBeginDrag = (global::UIScrollEventListener.VoidDelegate)v;
        }
        static object get_onEndDrag_3(ref object o)
        {
            return ((global::UIScrollEventListener)o).onEndDrag;
        }
        static void set_onEndDrag_3(ref object o, object v)
        {
            ((global::UIScrollEventListener)o).onEndDrag = (global::UIScrollEventListener.VoidDelegate)v;
        }


    }
}
