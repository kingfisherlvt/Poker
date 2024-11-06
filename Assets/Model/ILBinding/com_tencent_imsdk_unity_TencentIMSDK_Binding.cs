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
    unsafe class com_tencent_imsdk_unity_TencentIMSDK_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(com.tencent.imsdk.unity.TencentIMSDK);
            args = new Type[]{typeof(com.tencent.imsdk.unity.callback.RecvNewMsgCallback)};
            method = type.GetMethod("AddRecvNewMsgCallback", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AddRecvNewMsgCallback_0);
            args = new Type[]{typeof(System.String), typeof(System.String), typeof(com.tencent.imsdk.unity.callback.ValueCallback)};
            method = type.GetMethod("Login", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Login_1);
            args = new Type[]{typeof(com.tencent.imsdk.unity.callback.ValueCallback)};
            method = type.GetMethod("Logout", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Logout_2);
            args = new Type[]{typeof(System.String), typeof(com.tencent.imsdk.unity.callback.ValueCallback)};
            method = type.GetMethod("GroupQuit", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GroupQuit_3);
            args = new Type[]{typeof(System.String), typeof(com.tencent.imsdk.unity.enums.TIMConvType), typeof(com.tencent.imsdk.unity.types.Message), typeof(System.Text.StringBuilder), typeof(com.tencent.imsdk.unity.callback.ValueCallback)};
            method = type.GetMethod("MsgSendMessage", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, MsgSendMessage_4);
            args = new Type[]{typeof(System.String), typeof(System.String), typeof(com.tencent.imsdk.unity.callback.ValueCallback)};
            method = type.GetMethod("GroupJoin", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GroupJoin_5);
            args = new Type[]{typeof(System.Int64), typeof(com.tencent.imsdk.unity.types.SdkConfig)};
            method = type.GetMethod("Init", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Init_6);


        }


        static StackObject* AddRecvNewMsgCallback_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            com.tencent.imsdk.unity.callback.RecvNewMsgCallback @callback = (com.tencent.imsdk.unity.callback.RecvNewMsgCallback)typeof(com.tencent.imsdk.unity.callback.RecvNewMsgCallback).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            com.tencent.imsdk.unity.TencentIMSDK.AddRecvNewMsgCallback(@callback);

            return __ret;
        }

        static StackObject* Login_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            com.tencent.imsdk.unity.callback.ValueCallback @callback = (com.tencent.imsdk.unity.callback.ValueCallback)typeof(com.tencent.imsdk.unity.callback.ValueCallback).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @user_sig = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.String @user_id = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = com.tencent.imsdk.unity.TencentIMSDK.Login(@user_id, @user_sig, @callback);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Logout_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            com.tencent.imsdk.unity.callback.ValueCallback @callback = (com.tencent.imsdk.unity.callback.ValueCallback)typeof(com.tencent.imsdk.unity.callback.ValueCallback).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = com.tencent.imsdk.unity.TencentIMSDK.Logout(@callback);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GroupQuit_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            com.tencent.imsdk.unity.callback.ValueCallback @callback = (com.tencent.imsdk.unity.callback.ValueCallback)typeof(com.tencent.imsdk.unity.callback.ValueCallback).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @group_id = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = com.tencent.imsdk.unity.TencentIMSDK.GroupQuit(@group_id, @callback);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* MsgSendMessage_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 5);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            com.tencent.imsdk.unity.callback.ValueCallback @callback = (com.tencent.imsdk.unity.callback.ValueCallback)typeof(com.tencent.imsdk.unity.callback.ValueCallback).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Text.StringBuilder @message_id = (System.Text.StringBuilder)typeof(System.Text.StringBuilder).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            com.tencent.imsdk.unity.types.Message @message = (com.tencent.imsdk.unity.types.Message)typeof(com.tencent.imsdk.unity.types.Message).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            com.tencent.imsdk.unity.enums.TIMConvType @conv_type = (com.tencent.imsdk.unity.enums.TIMConvType)typeof(com.tencent.imsdk.unity.enums.TIMConvType).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 5);
            System.String @conv_id = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = com.tencent.imsdk.unity.TencentIMSDK.MsgSendMessage(@conv_id, @conv_type, @message, @message_id, @callback);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GroupJoin_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            com.tencent.imsdk.unity.callback.ValueCallback @callback = (com.tencent.imsdk.unity.callback.ValueCallback)typeof(com.tencent.imsdk.unity.callback.ValueCallback).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @hello_message = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.String @group_id = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = com.tencent.imsdk.unity.TencentIMSDK.GroupJoin(@group_id, @hello_message, @callback);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Init_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            com.tencent.imsdk.unity.types.SdkConfig @json_sdk_config = (com.tencent.imsdk.unity.types.SdkConfig)typeof(com.tencent.imsdk.unity.types.SdkConfig).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int64 @sdk_app_id = *(long*)&ptr_of_this_method->Value;


            var result_of_this_method = com.tencent.imsdk.unity.TencentIMSDK.Init(@sdk_app_id, @json_sdk_config);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
