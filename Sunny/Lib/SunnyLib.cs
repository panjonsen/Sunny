using System;
using System.Runtime.InteropServices;

namespace Sunny.Lib
{
    /// <summary>
    /// 基于Dll二次封装
    /// </summary>
    public static class SunnyLib

    {
        static System.Func<int, int, int, string, string, string, int, bool> httpFunc = null;
        static System.Func<int, int, int, string, string, int, int, bool> wsFunc = null;
        static System.Func<string, string, int, int, IntPtr, int, int, int, bool> tcpFunc = null;
        static NetHttpCallback iDelegate;
        public static Request MessageIdToSunny(int MessageId)
        {
            Request request = new Request(MessageId);
        
            return request;
        }
        public static string Get_Error()
        {
            IntPtr a =Geterr();
            if (a.ToInt64() < 1)
            {
                return "";
            }
            string s = Tool.PtrToString(a);
            Tool.PtrFree(a);
            return s;
        }

        private static IntPtr Geterr()
        {
            return Lib64And32.Sunny_Geterr();
        }

        /// <summary>
        /// 创建证书
        /// </summary>
        /// <returns>返回证书句柄</returns>
        public static int CreateCertificate()
        {
            return Lib64And32.Sunny_CreateCertificate();
        }
        /// <summary>
        /// 移除证书
        /// </summary>
        /// <param name="handle">证书句柄</param>
        public static void RemoveCertificate(int handle)
        {
            Lib64And32.Sunny_RemoveCertificate(handle);
        }
        /// <summary>
        /// 停止代理
        /// </summary>
        public static void Stop()
        {

            Lib64And32.Sunny_Stop();
            Lib64And32.Sunny_SetIeProxy(true);
        }


        public static bool Start(int port, System.Func<int, int, int, string, string, string, int, bool> CallBack_Http = null, System.Func<int, int, int, string, string, int, int, bool> CallBack_Wss = null, System.Func<string, string, int, int, IntPtr, int, int, int, bool> CallBack_Tcp = null, bool AutoSetIeProxy = false)
        {
            OpenTcp((IntPtr)(0));
            httpFunc = CallBack_Http;
            wsFunc = CallBack_Wss;
          iDelegate = new NetHttpCallback(DefaultNetHttpCallback);

            return PInit(port, Marshal.GetFunctionPointerForDelegate(iDelegate), true, AutoSetIeProxy);
        }

        public static void OpenTcp(IntPtr s)
        {
            Lib64And32.Sunny_OpenTcp(s);
        }
        public static bool PInit(System.Int32 port, IntPtr _callback, bool IsInstall, bool SetIEproxy)
        {
            return Lib64And32.Sunny_PInit(port, _callback, IsInstall, SetIEproxy);
        }


        //HTTP回调委托
        public delegate void NetHttpCallback(int _MessageId, int _Type, IntPtr _ptr_mod, IntPtr _ptr_Url, int _唯一ID, int 已弃用_允许过滤_已弃用, IntPtr _err, int pid, int WsMsgType);
        public static void DefaultNetHttpCallback(int _MessageId, int _Type, IntPtr _ptr_mod, IntPtr _ptr_Url, int _唯一ID, int 已弃用_允许过滤_已弃用, IntPtr _err, int pid, int WsMsgType)
        {
            if (_Type == Const.Net_Http_Request || _Type == Const.Net_Http_Response)
            {
                if (httpFunc != null)
                {
                    if (已弃用_允许过滤_已弃用 == 1)
                    {
                        return;
                    }
                    httpFunc(_唯一ID, _MessageId, _Type, Tool.PtrToString(_ptr_mod), Tool.PtrToString(_ptr_Url), "", pid);
                }
            }
            else if (_Type == Const.Net_Http_Request_Fail)
            {
                if (httpFunc != null)
                {
                    httpFunc(_唯一ID, _MessageId, _Type, Tool.PtrToString(_ptr_mod), Tool.PtrToString(_ptr_Url), Tool.PtrToString(_err), pid);
                }
            }
            else if (wsFunc != null)
            {
                wsFunc(_唯一ID, _MessageId, _Type, Tool.PtrToString(_ptr_mod), Tool.PtrToString(_ptr_Url), pid, WsMsgType);
            }
        }

        /// <summary>
        /// 删除协议头
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="name">键值</param>
        public static void DelRequestHeader(System.Int32 MessageId, IntPtr name)
        {
            Lib64And32.Sunny_DelRequestHeader(MessageId, name);
        }
        /// <summary>
        /// 请求置代理
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="val"></param>
        /// <param name="val1"></param>
        /// <param name="out1"></param>
        public static void SetRequestProxy(System.Int32 MessageId, IntPtr val, IntPtr val1, System.Int32 out1)
        {
            Lib64And32.Sunny_SetRequestProxy(MessageId, val, val1, out1);
        }
        /// <summary>
        /// 请求修改Url 
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="URI"></param>
        /// <returns></returns>
        public static bool SetRequestUrl(System.Int32 MessageId, IntPtr URI)
        {
            return Lib64And32.Sunny_SetRequestUrl(MessageId, URI);
        }
        /// <summary>
        /// 请求修改post内容
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="data"></param>
        /// <param name="datalen"></param>
        /// <returns></returns>
        public static int SetRequestData(System.Int32 MessageId, IntPtr data, System.Int32 datalen)
        {
            return Lib64And32.Sunny_SetRequestData(MessageId, data, datalen);
        }

        /// <summary>
        /// 请求新增/编辑协议头
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public static void SetRequestHeader(System.Int32 MessageId, IntPtr name, IntPtr val)
        {
            Lib64And32.Sunny_SetRequestHeader(MessageId, name, val);
        }
        /// <summary>
        /// 获取协议头
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static IntPtr GetRequestAllHeader(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetRequestAllHeader(MessageId);
        }
        /// <summary>
        /// 获取协议头_单个
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IntPtr GetRequestHeader(System.Int32 MessageId, IntPtr name)
        {
            return Lib64And32.Sunny_GetRequestHeader(MessageId, name);
        }
        /// <summary>
        /// 修改整个Cookies
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="val"></param>
        public static void SetRequestAllCookie(System.Int32 MessageId, IntPtr val)
        {
            Lib64And32.Sunny_SetRequestAllCookie(MessageId, val);
        }
        /// <summary>
        /// 修改单行cookies
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public static void SetRequestCookie(System.Int32 MessageId, IntPtr name, IntPtr val)
        {
            Lib64And32.Sunny_SetRequestCookie(MessageId, name, val);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static IntPtr GetRequestALLCookie(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetRequestALLCookie(MessageId);
        }
        /// <summary>
        /// 获取指定cookie
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IntPtr GetRequestCookie(System.Int32 MessageId, IntPtr name)
        {
            return Lib64And32.Sunny_GetRequestCookie(MessageId, name);
        }

        /// <summary>
        /// 获取post内容长度
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static int GetRequestBodyLen(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetRequestBodyLen(MessageId);
        }

        /// <summary>
        /// 获取post内容
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static IntPtr GetRequestBody(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetRequestBody(MessageId);
        }

        /// <summary>
        /// 获取响应内容长度
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static int GetResponseBodyLen(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetResponseBodyLen(MessageId);
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static IntPtr GetResponseData(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetResponseData(MessageId);
        }
        /// <summary>
        /// 获取指定响应协议头
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IntPtr GetResponseHeader(System.Int32 MessageId, IntPtr name)
        {
            return Lib64And32.Sunny_GetResponseHeader(MessageId, name);
        }
        /// <summary>
        /// 获取响应协议头
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static IntPtr GetResponseAllHeader(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetResponseAllHeader(MessageId);
        }

        /// <summary>
        /// 获取响应状态码
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>

        public static int GetResponseStatusCode(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetResponseStatusCode(MessageId);
        }

        /// <summary>
        /// 获取响应状态码对应的文本内容
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static IntPtr GetResponseStatus(System.Int32 MessageId)
        {
            return Lib64And32.Sunny_GetResponseStatus(MessageId);
        }

        /// <summary>
        /// 修改响应内容
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="data"></param>
        /// <param name="datalen"></param>
        /// <returns></returns>
        public static int SetResponseData(System.Int32 MessageId, IntPtr data, System.Int32 datalen)
        {
            return Lib64And32.Sunny_SetResponseData(MessageId, data, datalen);
        }
        /// <summary>
        /// 删除响应指定协议头
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="name"></param>
        public static void DelResponseHeader(System.Int32 MessageId, IntPtr name)
        {
            Lib64And32.Sunny_DelResponseHeader(MessageId, name);
        }

        /// <summary>
        /// 修改响应指定协议头
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public static void SetResponseHeader(System.Int32 MessageId, IntPtr name, IntPtr val)
        {
            Lib64And32.Sunny_SetResponseHeader(MessageId, name, val);
        }

        /// <summary>
        /// 修改响应协议头
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="value"></param>
        public static void SetResponseAllHeader(System.Int32 MessageId, IntPtr value)
        {
            Lib64And32.Sunny_SetResponseAllHeader(MessageId, value);
        }

        /// <summary>
        /// 修改响应状态码
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="code"></param>
        public static void SetResponseStatus(System.Int32 MessageId, System.Int32 code)
        {
            Lib64And32.Sunny_SetResponseStatus(MessageId, code);
        }

    }

}