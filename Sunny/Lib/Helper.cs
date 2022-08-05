using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sunny.Lib
{
    class Tool
    {

        public static string BytesToStr(byte[] byteArray, string Encoding = "gb2312")
        {
            if (Encoding == "gb2312")
            {
                return System.Text.Encoding.Default.GetString(byteArray);
            }
            return System.Text.Encoding.UTF8.GetString(byteArray);
        }
        public static byte[] StrToBytes(string str, string Encoding = "gb2312")
        {
            return System.Text.Encoding.GetEncoding(Encoding).GetBytes(str);
        }
        /// <summary>   
        /// 指针到字节数组   
        /// </summary>   
        /// <param name="data">要操作的指针</param>   
        /// <param name="n">数据长度</param>   
        /// <param name="sikp">偏移</param>   
        /// <returns>指针到整数</returns>
        public static byte[] PtrToBytes(IntPtr data, int n, int sikp = 0)
        {
            byte[] datas = new byte[n];

            for (var i = 0; i < n; i++)
            {
                datas[i] = (byte)Marshal.ReadByte(data, i + sikp);
            }
            return datas;
        }
        /// <summary>   
        /// 指针到字符串
        /// </summary>   
        /// <param name="data">要操作的指针</param>    
        /// <returns>指针到整数</returns>
        public static string PtrToString(IntPtr data)
        {
            byte[] byteArray = new byte[0];
            byte[] t = new byte[1];
            int i = 0;
            while (true)
            {
                byte p = (byte)Marshal.ReadByte(data, i);
                if (p != 0)
                {
                    t[0] = p;
                    byteArray = byteArray.Concat(t).ToArray();
                    i++;
                    continue;
                }
                break;
            }
            return System.Text.Encoding.Default.GetString(byteArray);
        }
        /// <summary>   
        /// 指针到整数   
        /// </summary>   
        /// <param name="p">要操作的指针</param>   
        /// <returns>指针到整数</returns>
        public static long PtrTolong(IntPtr p)
        {
            if (Environment.Is64BitProcess)
            {
                return Marshal.ReadInt64(p, 0);
            }
            return Marshal.ReadInt32(p, 0);
        }
        /// <summary>   
        /// 释放指针，不是C#请求的内存 不需要释放  
        /// </summary>   
        /// <param name="p">要释放指针</param>   
        /// <returns>释放指针，不是C#请求的内存 不需要释放</returns>
        public static void PtrFree(IntPtr p)
        {
            Marshal.FreeHGlobal(p);
        }
        /// <summary>   
        /// 根据数据的长度申请非托管空间   
        /// </summary>   
        /// <param name="strData">要申请非托管空间的数据</param>   
        /// <returns>指向非拖管空间的指针</returns>
        public static IntPtr StringToIntptr(string strData)
        {
            //先将字符串转化成字节方式   
            Byte[] btData = System.Text.Encoding.Default.GetBytes(strData);

            //申请非拖管空间   
            IntPtr m_ptr = Marshal.AllocHGlobal(btData.Length);

            //给非拖管空间清０    
            Byte[] btZero = new Byte[btData.Length + 1]; //因为C字符串以0结尾
            Marshal.Copy(btZero, 0, m_ptr, btZero.Length);

            //给指针指向的空间赋值   
            Marshal.Copy(btData, 0, m_ptr, btData.Length);

            return m_ptr;
        }
        /// <summary>   
        /// 根据数据的长度申请非托管空间   
        /// </summary>   
        /// <param name="btData">要申请非托管空间的数据</param>   
        /// <returns>指向非拖管空间的指针</returns>
        public static IntPtr BytesToIntptr(byte[] btData)
        {

            //申请非拖管空间   
            IntPtr m_ptr = Marshal.AllocHGlobal(btData.Length);

            //给非拖管空间清０    
            Byte[] btZero = new Byte[btData.Length + 1]; //因为C字符串以0结尾
            Marshal.Copy(btZero, 0, m_ptr, btZero.Length);

            //给指针指向的空间赋值   
            Marshal.Copy(btData, 0, m_ptr, btData.Length);

            return m_ptr;
        }

        /// <summary>   
        /// 根据长度申请非托管空间   
        /// </summary>   
        /// <param name="length">要申请非托管空间的大小</param>   
        /// <returns>指向非拖管空间的指针</returns>   
        public static IntPtr mallocIntptr(int length)
        {
            //申请非拖管空间   
            IntPtr m_ptr = Marshal.AllocHGlobal(length);

            //给非拖管空间清０    
            Byte[] btZero = new Byte[length + 1]; //一定要加1,否则后面是乱码，原因未找到   
            Marshal.Copy(btZero, 0, m_ptr, btZero.Length);

            //给指针指向的空间赋值   
            Marshal.Copy(btZero, 0, m_ptr, length);

            return m_ptr;
        }
    }
}
