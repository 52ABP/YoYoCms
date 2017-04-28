﻿// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：CommunicationCryptor.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System;
using System.Security.Cryptography;
using System.Text;
using Galaxy.Common.Extensions;

#endregion

namespace Galaxy.Common.Secutiry
{
    /// <summary>
    ///     结合RSA，DES的通信加密解密操作类
    /// </summary>
    public class CommunicationCryptor
    {
        private static readonly string Separator = Convert.ToBase64String(Encoding.UTF8.GetBytes("#@|kingsshap|@#"));
        private readonly string _facePublicKey;
        private readonly string _hashType;
        private readonly string _ownPrivateKey;

        /// <summary>
        ///     初始化一个<see cref="CommunicationCryptor" />类型的新实例
        /// </summary>
        /// <param name="ownPrivateKey">己方私钥</param>
        /// <param name="facePublicKey">对方公钥</param>
        /// <param name="hashType">摘要哈希方式，值必须为MD5或SHA1</param>
        public CommunicationCryptor(string ownPrivateKey, string facePublicKey, string hashType)
        {
            ownPrivateKey.CheckNotNull(nameof(ownPrivateKey));
            facePublicKey.CheckNotNull(nameof(facePublicKey));
            hashType.CheckNotNull(nameof(hashType));
            hashType.Required(str => hashType == "MD5" || hashType == "SHA1", Resources.Security_RSA_Sign_HashType);

            _ownPrivateKey = ownPrivateKey;
            _facePublicKey = facePublicKey;
            _hashType = hashType;
        }

        /// <summary>
        ///     解密接收到的加密数据并验证完整性，如果验证通过返回明文
        /// </summary>
        /// <param name="data">接收到的加密数据</param>
        /// <returns>解密并验证成功后，返回明文</returns>
        public string DecryptAndVerifyData(string data)
        {
            data.CheckNotNullOrEmpty(nameof(data));

            string[] separators = {Separator};
            //0为DES密钥密文，1为 正文+摘要 的密文
            var datas = data.Split(separators, StringSplitOptions.None);
            //用接收端私钥RSA解密获取DES密钥
            var desKey = RsaHelper.Decrypt(Convert.FromBase64String(datas[0]), _ownPrivateKey);
            //DES解密获取 正文+摘要 的明文
            data = new DesHelper(desKey).Decrypt(datas[1]);
            //0为正文明文，1为摘要
            datas = data.Split(separators, StringSplitOptions.None);
            data = datas[0];
            if (RsaHelper.VerifyData(data, datas[1], _hashType, _facePublicKey))
                return data;
            throw new CryptographicException("加密数据在进行解密时校验失败");
        }

        /// <summary>
        ///     加密要发送的数据，包含签名，DES加密，RSA加密DES密钥等步骤
        /// </summary>
        /// <param name="data">要加密的正文明文数据</param>
        /// <returns>已加密待发送的密文</returns>
        public string EncryptData(string data)
        {
            data.CheckNotNull(nameof(data));

            //获取正文摘要
            var signData = RsaHelper.SignData(data, _hashType, _ownPrivateKey);
            data = new[] {data, signData}.ExpandAndToString(Separator);
            //使用DES加密 正文+摘要
            var des = new DesHelper();
            data = des.Encrypt(data);
            //RSA加密DES密钥
            var enDesKey = Convert.ToBase64String(RsaHelper.Encrypt(des.Key, _facePublicKey));
            return new[] {enDesKey, data}.ExpandAndToString(Separator);
        }
    }
}