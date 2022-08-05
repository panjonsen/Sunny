using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunny.Lib
{
    public class Const
    {
        /// <summary>
        /// 表示在握手过程中不应该请求客户端证书，并且如果发送了任何证书，它们将不会被验证。
        /// </summary>
        public const int SSL_ClientAuth_NoClientCert = 0;
        /// <summary>
        /// 表示应该在握手过程中请求客户端证书，但不要求客户端发送任何证书。
        /// </summary>
        public const int SSL_ClientAuth_RequestClientCert = 1;
        /// <summary>
        /// 表示在握手过程中应该请求客户端证书，并且客户端至少需要发送一个证书，但该证书不需要有效。
        /// </summary>
        public const int SSL_ClientAuth_RequireAnyClientCert = 2;
        /// <summary>
        /// 表示应该在握手过程中请求客户端证书，但不要求客户端发送证书。如果客户端发送了一个证书，它就需要是有效的。
        /// </summary>
        public const int SSL_ClientAuth_VerifyClientCertIfGiven = 2;
        /// <summary>
        /// 表示握手时需要请求客户端证书，客户端至少需要发送一个有效的证书。
        /// </summary>
        public const int SSL_ClientAuth_RequireAndVerifyClientCert = 2;



        //WS 客户端的一些常量
        public const int WSClient_TextMessage = 1;
        public const int WSClient_BinaryMessage = 2;
        public const int WSClient_CloseMessage = 8;
        public const int WSClient_PingMessage = 9;
        public const int WSClient_PongMessage = 10;
        public const int WSClient_invalid = 255;


        /// <summary>
        /// 收到请求
        /// </summary>
        public const int Net_Http_Request = 1;

        /// <summary>
        /// 请求返回
        /// </summary>
        public const int Net_Http_Response = 2;

        /// <summary>
        /// WSS连接成功
        /// </summary>
        public const int Net_Http_Wss_Connection = 3;

        /// <summary>
        /// wss 发送数据
        /// </summary>
        public const int Net_Http_Wss_send = 4;

        /// <summary>
        /// wss 收到数据
        /// </summary>
        public const int Net_Http_Wss_received = 5;

        /// <summary>
        /// wss 断开连接
        /// </summary>
        public const int Net_Http_Wss_Disconnect = 6;

        /// <summary>
        /// ws连接成功
        /// </summary>
        public const int Net_Http_Ws_Connection = 7;

        /// <summary>
        /// ws  发送数据
        /// </summary>
        public const int Net_Http_Ws_send = 8;

        /// <summary>
        /// ws 收到数据
        /// </summary>
        public const int Net_Http_Ws_received = 9;

        /// <summary>
        /// ws 断开连接
        /// </summary>
        public const int Net_Http_Ws_Disconnect = 10;

        /// <summary>
        /// Http请求失败
        /// </summary>
        public const int Net_Http_Request_Fail = 10;

        /// <summary>
        /// TCP进入连接成功
        /// </summary>
        public const int Net_TCP_Enter = 0;
        /// <summary>
        /// TCP发送数据
        /// </summary>
        public const int Net_TCP_Send = 1;
        /// <summary>
        /// TCP收到数据
        /// </summary>
        public const int Net_TCP_Received = 2;
        /// <summary>
        /// TCP断开连接
        /// </summary>
        public const int Net_TCP_Disconnect = 3;
        /// <summary>
        /// TCP即将连接
        /// </summary>
        public const int Net_TCP_Waiting = 4;
    }
}
