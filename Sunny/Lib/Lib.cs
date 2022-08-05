using Lib64And32.Properties;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Sunny.Lib
{
    public class Lib64And32
    {
        static Lib64And32()
        {

            var myPath = new Uri(typeof(Lib64And32).Assembly.CodeBase).LocalPath;
            var myFolder = Path.GetDirectoryName(myPath);

            var is64 = Environment.Is64BitProcess;
            var subfolder = is64 ? "\\win64\\" : "\\win32\\";

           

            LoadLibrary(myFolder + subfolder + "Sunny.dll");
            Console.WriteLine(myPath);
            Console.WriteLine(myFolder);
            Console.WriteLine(subfolder);
            Console.WriteLine(myFolder + subfolder + "Sunny.dll");
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        private const string DLLName = "Sunny.dll";

        [DllImport(DLLName, EntryPoint = "CreateCertificate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_CreateCertificate();

        [DllImport(DLLName, EntryPoint = "RemoveCertificate", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_RemoveCertificate(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "LoadP12Certificate", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_LoadP12Certificate(System.Int32 Context, IntPtr Name, IntPtr Password);

        [DllImport(DLLName, EntryPoint = "LoadX509KeyPair", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_LoadX509KeyPair(System.Int32 Context, IntPtr Ca_Path, IntPtr KEY_Path);

        [DllImport(DLLName, EntryPoint = "LoadX509Certificate", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_LoadX509Certificate(System.Int32 Context, IntPtr Host, IntPtr CA, IntPtr KEY);

        [DllImport(DLLName, EntryPoint = "SetInsecureSkipVerify", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SetInsecureSkipVerify(System.Int32 Context, System.Boolean b);

        [DllImport(DLLName, EntryPoint = "SetServerName", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SetServerName(System.Int32 Context, IntPtr name);

        [DllImport(DLLName, EntryPoint = "GetServerName", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetServerName(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "AddCertPoolPath", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_AddCertPoolPath(System.Int32 Context, IntPtr cer);

        [DllImport(DLLName, EntryPoint = "AddCertPoolText", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_AddCertPoolText(System.Int32 Context, IntPtr cer);

        [DllImport(DLLName, EntryPoint = "AddClientAuth", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_AddClientAuth(System.Int32 Context, System.Int32 val);

        [DllImport(DLLName, EntryPoint = "CreateCA", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_CreateCA(System.Int32 Context, IntPtr Country, IntPtr Organization, IntPtr OrganizationalUnit, IntPtr Province, IntPtr CommonName, IntPtr Locality, System.Int32 bits, System.Int32 NotAfter);

        [DllImport(DLLName, EntryPoint = "ExportCA", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_ExportCA(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "ExportKEY", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_ExportKEY(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "ExportPub", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_ExportPub(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "ExportP12", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_ExportP12(System.Int32 Context, IntPtr path, IntPtr pass);

        [DllImport(DLLName, EntryPoint = "CreateHTTPClient", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_CreateHTTPClient();

        [DllImport(DLLName, EntryPoint = "RemoveHTTPClient", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_RemoveHTTPClient(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "HTTPClientGetErr", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_HTTPClientGetErr(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "HTTPOpen", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_HTTPOpen(System.Int32 Context, IntPtr mod, IntPtr u);

        [DllImport(DLLName, EntryPoint = "HTTPSetHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_HTTPSetHeader(System.Int32 Context, IntPtr name, IntPtr value);

        [DllImport(DLLName, EntryPoint = "HTTPSetProxyIP", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_HTTPSetProxyIP(System.Int32 Context, IntPtr ip);

        [DllImport(DLLName, EntryPoint = "HTTPSetProxyBasicAuth", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_HTTPSetProxyBasicAuth(System.Int32 Context, IntPtr u, IntPtr p);

        [DllImport(DLLName, EntryPoint = "HTTPSetTimeouts", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_HTTPSetTimeouts(System.Int32 Context, System.Int32 t1, System.Int32 t2, System.Int32 t3);

        [DllImport(DLLName, EntryPoint = "HTTPSendBin", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_HTTPSendBin(System.Int32 Context, IntPtr b, System.Int32 l);

        [DllImport(DLLName, EntryPoint = "HTTPGetBodyLen", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_HTTPGetBodyLen(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "HTTPGetHeads", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_HTTPGetHeads(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "HTTPGetBody", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_HTTPGetBody(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "HTTPGetCode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_HTTPGetCode(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "CreateQueue", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_CreateQueue(IntPtr name);

        [DllImport(DLLName, EntryPoint = "QueueIsEmpty", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_QueueIsEmpty(IntPtr name);

        [DllImport(DLLName, EntryPoint = "QueueRelease", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_QueueRelease(IntPtr name);

        [DllImport(DLLName, EntryPoint = "QueueLength", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_QueueLength(IntPtr name);

        [DllImport(DLLName, EntryPoint = "QueuePush", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_QueuePush(IntPtr name, IntPtr val, System.Int32 vallen);

        [DllImport(DLLName, EntryPoint = "QueuePull", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_QueuePull(IntPtr name, IntPtr rlen);

        [DllImport(DLLName, EntryPoint = "CreateKeys", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_CreateKeys();

        [DllImport(DLLName, EntryPoint = "RemoveKeys", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_RemoveKeys(System.Int32 KeysHandle);

        [DllImport(DLLName, EntryPoint = "KeysDelete", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_KeysDelete(System.Int32 KeysHandle, IntPtr name);

        [DllImport(DLLName, EntryPoint = "KeysRead", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_KeysRead(System.Int32 KeysHandle, IntPtr name, IntPtr p);

        [DllImport(DLLName, EntryPoint = "KeysWrite", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_KeysWrite(System.Int32 KeysHandle, IntPtr name, IntPtr val, System.Int32 len);

        [DllImport(DLLName, EntryPoint = "KeysWriteFloat", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_KeysWriteFloat(System.Int32 KeysHandle, IntPtr name, System.Double val);

        [DllImport(DLLName, EntryPoint = "KeysReadFloat", CallingConvention = CallingConvention.Cdecl)]
        public static extern Double Sunny_KeysReadFloat(System.Int32 KeysHandle, IntPtr name);

        [DllImport(DLLName, EntryPoint = "KeysWriteLong", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_KeysWriteLong(System.Int32 KeysHandle, IntPtr name, System.Int64 val);

        [DllImport(DLLName, EntryPoint = "KeysReadLong", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 Sunny_KeysReadLong(System.Int32 KeysHandle, IntPtr name);

        [DllImport(DLLName, EntryPoint = "KeysWriteInt", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_KeysWriteInt(System.Int32 KeysHandle, IntPtr name, System.Int32 val);

        [DllImport(DLLName, EntryPoint = "KeysReadInt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_KeysReadInt(System.Int32 KeysHandle, IntPtr name);

        [DllImport(DLLName, EntryPoint = "KeysEmpty", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_KeysEmpty(System.Int32 KeysHandle);

        [DllImport(DLLName, EntryPoint = "KeysGetCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_KeysGetCount(System.Int32 KeysHandle);

        [DllImport(DLLName, EntryPoint = "KeysGetJson", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_KeysGetJson(System.Int32 KeysHandle);

        [DllImport(DLLName, EntryPoint = "KeysWriteStr", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_KeysWriteStr(System.Int32 KeysHandle, IntPtr name, IntPtr val, System.Int32 len);

        [DllImport(DLLName, EntryPoint = "CreateSocketClient", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_CreateSocketClient();

        [DllImport(DLLName, EntryPoint = "RemoveSocketClient", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_RemoveSocketClient(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "SocketClientGetErr", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_SocketClientGetErr(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "SocketClientSetBufferSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SocketClientSetBufferSize(System.Int32 Context, System.Int32 BufferSize);

        [DllImport(DLLName, EntryPoint = "SocketClientDial", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SocketClientDial(System.Int32 Context, IntPtr addr, IntPtr call, System.Boolean istls, System.Boolean synchronous, IntPtr ProxyIP, IntPtr ProxyUser, IntPtr ProxyPass, System.Int32 CertificateConText);

        [DllImport(DLLName, EntryPoint = "SocketClientReceive", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_SocketClientReceive(System.Int32 Context, System.Int32 OutTimes, IntPtr len);

        [DllImport(DLLName, EntryPoint = "SocketClientClose", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SocketClientClose(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "SocketClientWrite", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_SocketClientWrite(System.Int32 Context, System.Int32 OutTimes, IntPtr val, System.Int32 vallen);

        [DllImport(DLLName, EntryPoint = "CreateWebsocket", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_CreateWebsocket();

        [DllImport(DLLName, EntryPoint = "RemoveWebsocket", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_RemoveWebsocket(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "WebsocketGetErr", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_WebsocketGetErr(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "WebsocketDial", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_WebsocketDial(System.Int32 Context, IntPtr url_, IntPtr head_, IntPtr call, System.Boolean synchronous, IntPtr ProxyType, IntPtr ProxyAddr, IntPtr ProxyUser, IntPtr ProxyPass, System.Int32 CertificateConText);

        [DllImport(DLLName, EntryPoint = "WebsocketClose", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_WebsocketClose(System.Int32 Context);

        [DllImport(DLLName, EntryPoint = "WebsocketReadWrite", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_WebsocketReadWrite(System.Int32 Context, IntPtr val, System.Int32 vallen, System.Int32 types);

        [DllImport(DLLName, EntryPoint = "WebsocketClientReceive", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_WebsocketClientReceive(System.Int32 Context, System.Int32 OutTimes, IntPtr messageType, IntPtr lenx);

        [DllImport(DLLName, EntryPoint = "SetRequestHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetRequestHeader(System.Int32 MessageId, IntPtr name, IntPtr val);

        [DllImport(DLLName, EntryPoint = "SetRequestProxy", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetRequestProxy(System.Int32 MessageId, IntPtr val, IntPtr val1, System.Int32 out1);

        [DllImport(DLLName, EntryPoint = "GetResponseStatusCode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_GetResponseStatusCode(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetRequestClientIp", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetRequestClientIp(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetResponseStatus", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetResponseStatus(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "SetResponseStatus", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetResponseStatus(System.Int32 MessageId, System.Int32 code);

        [DllImport(DLLName, EntryPoint = "DelResponseHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_DelResponseHeader(System.Int32 MessageId, IntPtr name);

        [DllImport(DLLName, EntryPoint = "DelRequestHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_DelRequestHeader(System.Int32 MessageId, IntPtr name);

        [DllImport(DLLName, EntryPoint = "SetRequestUrl", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SetRequestUrl(System.Int32 MessageId, IntPtr URI);

        [DllImport(DLLName, EntryPoint = "SetRequestCookie", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetRequestCookie(System.Int32 MessageId, IntPtr name, IntPtr val);

        [DllImport(DLLName, EntryPoint = "SetRequestAllCookie", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetRequestAllCookie(System.Int32 MessageId, IntPtr val);

        [DllImport(DLLName, EntryPoint = "GetRequestHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetRequestHeader(System.Int32 MessageId, IntPtr name);

        [DllImport(DLLName, EntryPoint = "SetResponseHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetResponseHeader(System.Int32 MessageId, IntPtr name, IntPtr val);

        [DllImport(DLLName, EntryPoint = "SetResponseAllHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetResponseAllHeader(System.Int32 MessageId, IntPtr value);

        [DllImport(DLLName, EntryPoint = "GetRequestCookie", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetRequestCookie(System.Int32 MessageId, IntPtr name);

        [DllImport(DLLName, EntryPoint = "GetRequestData", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetRequestData(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "SetResponseData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_SetResponseData(System.Int32 MessageId, IntPtr data, System.Int32 datalen);

        [DllImport(DLLName, EntryPoint = "SetIpProxy", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetIpProxy(IntPtr ip);

        [DllImport(DLLName, EntryPoint = "SetIpAuth", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetIpAuth(IntPtr Auth);

        [DllImport(DLLName, EntryPoint = "SetProxyTimeout", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetProxyTimeout(System.Int32 Timeout);

        [DllImport(DLLName, EntryPoint = "GetResponseData", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetResponseData(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetRequestBodyLen", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_GetRequestBodyLen(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetResponseBodyLen", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_GetResponseBodyLen(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "SetRequestData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_SetRequestData(System.Int32 MessageId, IntPtr data, System.Int32 datalen);

        [DllImport(DLLName, EntryPoint = "GetRequestBody", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetRequestBody(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetResponseBody", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetResponseBody(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetWebsocketBodyLen", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_GetWebsocketBodyLen(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetWebsocketBody", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetWebsocketBody(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "SetWebsocketBody", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SetWebsocketBody(System.Int32 MessageId, IntPtr data, System.Int32 datalen);

        [DllImport(DLLName, EntryPoint = "SendWebsocketBody", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SendWebsocketBody(System.Int32 MessageId, System.Int32 MessageTpye, IntPtr data, System.Int32 datalen);

        [DllImport(DLLName, EntryPoint = "GetRequestALLCookie", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetRequestALLCookie(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetResponseAllHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetResponseAllHeader(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "GetResponseHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetResponseHeader(System.Int32 MessageId, IntPtr name);

        [DllImport(DLLName, EntryPoint = "GetRequestAllHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GetRequestAllHeader(System.Int32 MessageId);

        [DllImport(DLLName, EntryPoint = "SetTcpBody", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SetTcpBody(System.Int32 MessageId, System.Int32 MsgType, IntPtr data, System.Int32 datalen);

        [DllImport(DLLName, EntryPoint = "SetTcpAgent", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SetTcpAgent(System.Int32 MessageId, IntPtr ip, IntPtr user, IntPtr pass);

        [DllImport(DLLName, EntryPoint = "TcpCloseClient", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_TcpCloseClient(System.Int32 Theonly);

        [DllImport(DLLName, EntryPoint = "SetTcpConnectionIP", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SetTcpConnectionIP(System.Int32 MessageId, IntPtr data);

        [DllImport(DLLName, EntryPoint = "TcpSendMsg", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_TcpSendMsg(System.Int32 MessageId, IntPtr data, System.Int32 datalen);

        [DllImport(DLLName, EntryPoint = "TcpSendMsgClient", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_TcpSendMsgClient(System.Int32 MessageId, IntPtr data, System.Int32 datalen);

        [DllImport(DLLName, EntryPoint = "SetCa", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetCa(System.Int32 ca);

        [DllImport(DLLName, EntryPoint = "HexDump", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_HexDump(IntPtr data, System.Int32 datalen);

        [DllImport(DLLName, EntryPoint = "Geterr", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_Geterr();

        [DllImport(DLLName, EntryPoint = "SetIeProxy", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_SetIeProxy(System.Boolean del);

        [DllImport(DLLName, EntryPoint = "PInit", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_PInit(System.Int32 port, IntPtr _callback, bool IsInstall, bool SetIEproxy);

        [DllImport(DLLName, EntryPoint = "OpenTcp", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_OpenTcp(IntPtr s);

        [DllImport(DLLName, EntryPoint = "Stop", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_Stop();

        [DllImport(DLLName, EntryPoint = "BrUnCompress", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_BrUnCompress(IntPtr val, System.Int32 vallen);

        [DllImport(DLLName, EntryPoint = "GzipUnCompress", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_GzipUnCompress(IntPtr val, System.Int32 vallen);

        [DllImport(DLLName, EntryPoint = "DeflateUnCompress", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_DeflateUnCompress(IntPtr val, System.Int32 vallen);

        [DllImport(DLLName, EntryPoint = "BytesToInt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sunny_BytesToInt(IntPtr val, System.Int32 vallen);

        [DllImport(DLLName, EntryPoint = "Cfree", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_Cfree(IntPtr s);

        [DllImport(DLLName, EntryPoint = "StartProcess", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_StartProcess();

        [DllImport(DLLName, EntryPoint = "ProcessAddPID", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_ProcessAddPID(System.Int32 s);

        [DllImport(DLLName, EntryPoint = "ProcessStart", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_ProcessStart();

        [DllImport(DLLName, EntryPoint = "ProcessCancelAll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_ProcessCancelAll();

        [DllImport(DLLName, EntryPoint = "ProcessDelPID", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_ProcessDelPID(System.Int32 s);

        [DllImport(DLLName, EntryPoint = "DelP12Certificate", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_DelP12Certificate(IntPtr HostName);

        [DllImport(DLLName, EntryPoint = "AddP12Certificate", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_AddP12Certificate(IntPtr HostName, IntPtr privateKeyName, IntPtr privatePassword, System.Int32 cerType);

        [DllImport(DLLName, EntryPoint = "WebpToPng", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_WebpToPng(IntPtr webpPath, IntPtr savePath);

        [DllImport(DLLName, EntryPoint = "WebpToJpeg", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Sunny_WebpToJpeg(IntPtr webpPath, IntPtr savePath, System.Int32 SaveQuality);

        [DllImport(DLLName, EntryPoint = "WebpToPngBytes", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_WebpToPngBytes(IntPtr val, System.Int32 vallen);

        [DllImport(DLLName, EntryPoint = "WebpToJpegBytes", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_WebpToJpegBytes(IntPtr val, System.Int32 vallen, System.Int32 SaveQuality);

        [DllImport(DLLName, EntryPoint = "MustTcp", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_MustTcp(System.Boolean i);

        [DllImport(DLLName, EntryPoint = "ScriptCall", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Sunny_ScriptCall(System.Int32 i, IntPtr Request);

        [DllImport(DLLName, EntryPoint = "SetScript", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetScript(IntPtr Request);

        [DllImport(DLLName, EntryPoint = "SetScriptLogCallAddress", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sunny_SetScriptLogCallAddress(IntPtr i);
    }
}