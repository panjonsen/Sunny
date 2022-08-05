using System;
using System.Text.RegularExpressions;

namespace Sunny.Lib
{
    /// <summary>
    ///  Sunny HTTP 请求返回操作对象
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Sunny HTTP 请求操作对象
        /// </summary>
        public  SunnyRequest request = null;

        /// <summary>
        /// Sunny HTTP 返回操作对象
        /// </summary>
        public  SunnyResponse response = null;

        public Request(int MessageId)
        {
            request = new SunnyRequest(MessageId);
            response = new SunnyResponse(MessageId);
        }
    }

    public class SunnyRequest
    {
        private int MessageId = 0;

        public SunnyRequest(int _MessageId)
        {
            MessageId = _MessageId;
        }

        /// <summary>
        /// 请求协议头中去除Gzip 若不删除压缩标记，返回数据可能是压缩后的
        /// </summary>
        public void Del_Gzip()
        {
            IntPtr a = Tool.StringToIntptr("Accept-Encoding");
            SunnyLib.DelRequestHeader(MessageId, a);
            Tool.PtrFree(a);
        }

        /// <summary>
        /// 请求置代理
        /// </summary>
        /// <param name="代理类型">0=http 其他为s5代理</param>
        /// <param name="代理地址">例如:127.0.0.1:8888 </param>
        /// <param name="代理账号">可空</param>
        /// <param name="代理密码">可空</param>
        /// <param name="超时">单位秒 默认60</param>
        public void SetProxy(int proxyType, string proxyPath, string proxyUserName = "", string proxyPassWord = "", int timeOut = 60)
        {
            IntPtr a = (IntPtr)(0);
            if (proxyType == 0)
            {
                a = Tool.StringToIntptr(proxyPath);
            }
            else
            {
                a = Tool.StringToIntptr("s5|" + proxyPath);
            }
            IntPtr b = Tool.StringToIntptr(proxyUserName + ":" + proxyPassWord);
            SunnyLib.SetRequestProxy(MessageId, a, b, timeOut);
            Tool.PtrFree(a);
            Tool.PtrFree(b);
        }

        public bool Edit_Url(string 欲转向地址)
        {
            IntPtr A = Tool.StringToIntptr(欲转向地址);
            bool b = SunnyLib.SetRequestUrl(MessageId, A);
            Tool.PtrFree(A);
            return b;
        }

        /// <summary>
        /// 请求修改post内容_字节数组
        /// </summary>
        /// <param name="欲修改为的Body"></param>
        /// <returns></returns>
        public bool Edit_Data_Bytes(byte[] data)
        {
            IntPtr A = Tool.BytesToIntptr(data);
            int o = SunnyLib.SetRequestData(MessageId, A, data.Length);
            Tool.PtrFree(A);
            return o == 1;
        }

        /// <summary>
        /// 请求修改post内容_GBK
        /// </summary>
        /// <param name="欲修改为的Body"></param>
        /// <returns></returns>
        public bool Edit_Data_GBK(string data)
        {
            byte[] bs = Tool.StrToBytes(data);
            IntPtr A = Tool.BytesToIntptr(bs);
            int o = SunnyLib.SetRequestData(MessageId, A, bs.Length);
            Tool.PtrFree(A);
            return o == 1;
        }

        /// <summary>
        ///  请求修改post内容_UTF8
        /// </summary>
        /// <param name="欲修改为的Body"></param>
        /// <returns></returns>
        public bool Edit_Data_UTF8(string data)
        {
            byte[] bs = Tool.StrToBytes(data, "UTF-8");
            IntPtr A = Tool.BytesToIntptr(bs);
            int o = SunnyLib.SetRequestData(MessageId, A, bs.Length);
            Tool.PtrFree(A);
            return o == 1;
        }

        /// <summary>
        /// 编辑/新增协议头 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Edit_Insert_Header(string key,string value) {
         
            IntPtr a = Tool.StringToIntptr(key);
            IntPtr b = Tool.StringToIntptr(value);
            SunnyLib.SetRequestHeader(MessageId, a, b);
            Tool.PtrFree(a);
            Tool.PtrFree(b);
        }

        /// <summary>
        /// 批量编辑/新增协议头
        /// </summary>
        /// <param name="Heads">【可多条 一行一个 例如 Accept: image/gif】  【\r\n 分割】</param>
        public void Edit_Insert_Headers(string Heads)
        {
            IntPtr a = (IntPtr)(0);
            IntPtr b = (IntPtr)(0);
            string[] arr = Regex.Split(Heads, "\r\n", RegexOptions.IgnoreCase);
            foreach (string s in arr)
            {
                string[] arr1 = Regex.Split(s, ": ", RegexOptions.IgnoreCase);
                if (arr1.Length == 2)
                {
                    string name = arr1[0];
                    string value = arr1[1].Replace(name + ": ", "");
                    a = Tool.StringToIntptr(name);
                    b = Tool.StringToIntptr(value);
                    SunnyLib.SetRequestHeader(MessageId, a, b);
                    Tool.PtrFree(a);
                    Tool.PtrFree(b);
                }
            }
        }


        /// <summary>
        /// 获取整个协议头
        /// </summary>
        /// <returns></returns>
        public string Get_Headers()
        {
            IntPtr i1 = SunnyLib.GetRequestAllHeader(MessageId);
            if (i1.ToInt64() < 1)
            {
                return "";
            }
            return Tool.PtrToString(i1);
        }

        /// <summary>
        /// 获取指定协议头
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get_Header(string key)
        {
            IntPtr a = Tool.StringToIntptr(key);
            IntPtr i1 = SunnyLib.GetRequestHeader(MessageId, a);
            Tool.PtrFree(a);
            if (i1.ToInt64() < 1)
            {
                return "";
            }
            return Tool.PtrToString(i1);
        }

        /// <summary>
        /// 删除协议头
        /// </summary>
        /// <param name="key"></param>
        public void Del_Header(string key)
        {
            IntPtr a = Tool.StringToIntptr(key);
            SunnyLib.DelRequestHeader(MessageId, a);
            Tool.PtrFree(a);
        }

        /// <summary>
        /// 删除全部协议头
        /// </summary>
        public void Del_Headers()
        {
            IntPtr a = (IntPtr)(0);
            string[] arr = Regex.Split(Get_Headers(), "\r\n", RegexOptions.IgnoreCase);
            foreach (string s in arr)
            {
                string[] arr1 = Regex.Split(s, ": ", RegexOptions.IgnoreCase);
                if (arr1.Length >= 1)
                {
                    a = Tool.StringToIntptr(arr1[0]);
                    SunnyLib.DelRequestHeader(MessageId, a);
                    Tool.PtrFree(a);
                }
            }
        }

        /// <summary>
        /// 设置Cookies 
        /// </summary>
        /// <param name="cookie">例如:a=1;b=2;c=3   无需前缀（Cookie: ）</param>
        public void Edit_Cookies(string cookie)
        {
            IntPtr a = Tool.StringToIntptr(cookie);
            SunnyLib.SetRequestAllCookie(MessageId, a);
            Tool.PtrFree(a);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookie">例如:a=1;b=2;c=3   无需前缀（Cookie: ）</param>
        public void Edit_Cookie(string key, string value)
        {
            IntPtr a = Tool.StringToIntptr(key);
            IntPtr b = Tool.StringToIntptr(value);
            SunnyLib.SetRequestCookie(MessageId, a, b);
            Tool.PtrFree(a);
            Tool.PtrFree(b);
        }

        /// <summary>
        /// 获取Cookies
        /// </summary>
        /// <returns></returns>

        public string Get_Cookies()
        {
            IntPtr i1 = SunnyLib.GetRequestALLCookie(MessageId);
            if (i1.ToInt64() < 1)
            {
                return "";
            }
            return Tool.PtrToString(i1);
        }
        /// <summary>
        /// 获取指定cookie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get_Cookie(string key)
        {
            IntPtr a = Tool.StringToIntptr(key);
            IntPtr i1 = SunnyLib.GetRequestCookie(MessageId, a);
            Tool.PtrFree(a);
            if (i1.ToInt64() < 1)
            {
                return "";
            }
            return Tool.PtrToString(i1);
        }

        /// <summary>
        /// 获取指定cookie的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get_Cookie_Value(string key)
        {
            IntPtr a = Tool.StringToIntptr(key);
            IntPtr i1 = SunnyLib.GetRequestCookie(MessageId, a);
            Tool.PtrFree(a);
            if (i1.ToInt64() < 1)
            {
                return "";
            }
            string s = Tool.PtrToString(i1);
            string[] arr = Regex.Split(s, "=", RegexOptions.IgnoreCase);
            if (arr.Length < 2)
            {
                return "";
            }
            string value = arr[1].Replace(arr[0] + "=", "");
            return value;
        }


        /// <summary>
        /// 获取post内容长度
        /// </summary>
        /// <returns></returns>
        public int Get_Data_Lenth()
        {
            return SunnyLib.GetRequestBodyLen(MessageId);
        }

        /// <summary>
        /// 获取Post内容 
        /// </summary>
        /// <returns></returns>
        public string Get_Data_GBK()
        {
            byte[] s = Get_Data();
            return Tool.BytesToStr(s);
        }

        /// <summary>
        ///  获取Post内容
        /// </summary>
        /// <returns></returns>
        public string Get_Data_UTF8()
        {
            byte[] s = Get_Data();
            return Tool.BytesToStr(s, "UTF-8");
        }
        /// <summary>
        ///  获取Post内容
        /// </summary>
        /// <returns></returns>
        public byte[] Get_Data()
        {
            IntPtr p = SunnyLib.GetRequestBody(MessageId);
            if (p.ToInt64() < 1)
            {
                return new byte[0];
            }
            return Tool.PtrToBytes(p, Get_Data_Lenth());
        }
    }

    public class SunnyResponse
    {
        private int MessageId = 0;

        public SunnyResponse(int _MessageId)
        {
            MessageId = _MessageId;
        }

        /// <summary>
        /// 获取响应内容长度
        /// </summary>
        /// <returns></returns>
        public int Get_Data_Lenth()
        {
            return SunnyLib.GetResponseBodyLen(MessageId);
        }
        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <returns></returns>
        public string Get_Data_GBK()
        {
            byte[] s = Get_Data();
            return Tool.BytesToStr(s);
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <returns></returns>

        public string Get_Data_UFT8()
        {
            byte[] s = Get_Data();
            return Tool.BytesToStr(s, "UTF-8");
        }

        /// <summary>
        /// 获取响应内容字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] Get_Data()
        {
            IntPtr p = SunnyLib.GetResponseData(MessageId);
            if (p.ToInt64() < 1)
            {
                return new byte[0];
            }
            return Tool.PtrToBytes(p, Get_Data_Lenth());
        }

        /// <summary>
        /// 获取响应指定协议头
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get_Header(string key)
        {
            IntPtr a = Tool.StringToIntptr(key);
            IntPtr i1 = SunnyLib.GetResponseHeader(MessageId, a);
            Tool.PtrFree(a);
            if (i1.ToInt64() < 1)
            {
                return "";
            }
            return Tool.PtrToString(i1);
        }

        /// <summary>
        /// 获取响应协议头
        /// </summary>
        /// <returns></returns>
        public string Get_Headers()
        {
            IntPtr i1 = SunnyLib.GetResponseAllHeader(MessageId);
            if (i1.ToInt64() < 1)
            {
                return "";
            }
            return Tool.PtrToString(i1);
        }


        /// <summary>
        /// 获取响应状态码
        /// </summary>
        /// <returns></returns>
        public int Get_StatusCode()
        {
            return SunnyLib.GetResponseStatusCode(MessageId);
        }

        /// <summary>
        /// 获取响应状态码对应的文本说明
        /// </summary>
        /// <returns></returns>
        public string Get_StatusCode_Msg()
        {
            IntPtr o = SunnyLib.GetResponseStatus(MessageId);
            if (o.ToInt64() < 1)
            {
                return "";
            }
            string[] res_list = Regex.Split(Tool.PtrToString(o), " ", RegexOptions.IgnoreCase);
            string res = "";
            if (res_list.Length >= 2)
            {
                foreach (string s in res_list)
                {
                    if (res == "")
                    {
                        res = s;
                    }
                    else
                    {
                        res = res + " " + s;
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// 修改响应内容
        /// </summary>
        /// <param data="欲修改为的Body"></param>
        /// <returns></returns>
        public bool Edit_Data(byte[] data)
        {
            IntPtr A = Tool.BytesToIntptr(data);
            int o = SunnyLib.SetResponseData(MessageId, A, data.Length);
            Tool.PtrFree(A);
            return o == 1;
        }

        /// <summary>
        /// 修改响应内容
        /// </summary>
        /// <param data="欲修改为的Body"></param>
        /// <returns></returns>
        public bool Edit_Data_GBK(string data)
        {
            byte[] bs = Tool.StrToBytes(data);
            IntPtr A = Tool.BytesToIntptr(bs);
            int o = SunnyLib.SetResponseData(MessageId, A, bs.Length);
            Tool.PtrFree(A);
            return o == 1;
        }

        /// <summary>
        /// 修改响应内容
        /// </summary>
        /// <param data="欲修改为的Body"></param>
        /// <returns></returns>
        public bool Edit_Data_UTF8(string data)
        {
            byte[] bs = Tool.StrToBytes(data, "UTF-8");
            IntPtr A = Tool.BytesToIntptr(bs);
            int o = SunnyLib.SetResponseData(MessageId, A, bs.Length);
            Tool.PtrFree(A);
            return o == 1;
        }

        /// <summary>
        /// 删除响应指定协议头
        /// </summary>
        /// <param name="key"></param>
        public void Del_Header(string key)
        {
            IntPtr a = Tool.StringToIntptr(key);
            SunnyLib.DelResponseHeader(MessageId, a);
            Tool.PtrFree(a);
        }

      /// <summary>
      /// 删除响应协议头
      /// </summary>
        public void Del_Headers()
        {
            IntPtr a = (IntPtr)(0);
            string[] arr = Regex.Split(Get_Headers(), "\r\n", RegexOptions.IgnoreCase);
            foreach (string s in arr)
            {
                string[] arr1 = Regex.Split(s, ":", RegexOptions.IgnoreCase);
                if (arr1.Length >= 1)
                {
                    a = Tool.StringToIntptr(arr1[0]);
                    SunnyLib.DelResponseHeader(MessageId, a);
                    Tool.PtrFree(a);
                }
            }
        }

        public void Edit_Header(string key, string value)
        {
            IntPtr i1 = Tool.StringToIntptr(key);
            IntPtr i2 = Tool.StringToIntptr(value);
            SunnyLib.SetResponseHeader(MessageId, i1, i2);
            Tool.PtrFree(i1);
            Tool.PtrFree(i2);
        }

        /// <summary>
        /// 修改响应协议头  实际是替换你的上去 原来的就没有了
        /// </summary>
        /// <param name="Heads"></param>
        public void Edit_Headers(string Heads)
        {
            IntPtr a = Tool.StringToIntptr(Heads);
            SunnyLib.SetResponseAllHeader(MessageId, a);
            Tool.PtrFree(a);
        }

        /// <summary>
        /// 修改状态码
        /// </summary>
        /// <param name="状态码">默认200</param>
        public void Edit_StatusCode(int code = 200)
        {
            SunnyLib.SetResponseStatus(MessageId, code);
        }
    }
}