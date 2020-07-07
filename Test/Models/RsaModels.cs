using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Kane.Extension.Test
{
    [XmlType("RSAKeyValue")]
    public class RsaModels
    {
        /// <summary>
        /// 表示 D 算法的 System.Security.Cryptography.RSA 参数。
        /// </summary>
        public string D{get;set;}
        /// <summary>
        /// 表示 DP 算法的 System.Security.Cryptography.RSA 参数。
        /// </summary>
        public string DP{get;set;}
        /// <summary>
        /// 表示 DQ 算法的 System.Security.Cryptography.RSA 参数。
        /// </summary>
        public string DQ{get;set;}
        /// <summary>
        /// 表示 Exponent 算法的 System.Security.Cryptography.RSA 参数。
        /// </summary>
        public string Exponent{get;set;}
        /// <summary>
        /// 表示 InverseQ 算法的 System.Security.Cryptography.RSA 参数。
        /// </summary>
        public string InverseQ{get;set;}
        /// <summary>
        /// 表示 Modulus 算法的 System.Security.Cryptography.RSA 参数。
        /// </summary>
        public string Modulus{get;set;}
        /// <summary>
        /// 表示 P 算法的 System.Security.Cryptography.RSA 参数。
        /// </summary>
        public string P{get;set;}
        /// <summary>
        /// 表示 Q 算法的 System.Security.Cryptography.RSA 参数。
        /// </summary>
        public string Q{get;set;}
    }
}