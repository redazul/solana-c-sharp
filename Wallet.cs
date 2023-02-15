using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Numerics;
using System.Globalization;
using System.Linq;

public class Wallet : MonoBehaviour
{


    BigInteger q = BigInteger.Pow(2, 252) + BigInteger.Parse("27742317777372353535851937790883648493");

    BigInteger p = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819949");
    BigInteger d = BigInteger.Parse("37095705934669439343138083508754565189542113879843219016388785533085940283555");

    BigInteger[] G = {
        BigInteger.Parse("15112221349535400772501151409588531511454012693041857206046113283949847762202"),
        BigInteger.Parse("46316835694926478169428394003475163141307993866256225615783033603165251855960"),
        BigInteger.Parse("1"),
        BigInteger.Parse("46827403850823179245072216630277197565144205554125654976674165829533817101731")
    };

    BigInteger[] point_add(BigInteger[] P, BigInteger[] Q)
    {
        BigInteger A = (((P[1] - P[0]) * (Q[1] - Q[0]) % p) + p) % p;
        BigInteger B = (((P[1] + P[0]) * (Q[1] + Q[0]) % p) + p) % p;

        BigInteger C = ((2 * P[3] * Q[3] * d % p) + p) % p;
        BigInteger D = ((2 * P[2] * Q[2] % p) + p) % p;

        BigInteger E = B - A;
        BigInteger F = D - C;
        BigInteger G = D + C;
        BigInteger H = B + A;


        BigInteger[] bByte = { E * F, G * H, F * G, E * H };


        return bByte;
    }

    BigInteger[] point_mul(BigInteger s, BigInteger[] P)
    {

        BigInteger[] Q = { BigInteger.Parse("0"), BigInteger.Parse("1"), BigInteger.Parse("1"), BigInteger.Parse("0") };


        while (s > 0)
        {

            if ((s & 1) == 1)
            {
                Q = point_add(Q, P);
            }

            P = point_add(P, P);

            s >>= 1;

        }

        return Q;

    }

    byte[] point_compress(BigInteger[] P)
    {

        BigInteger zinv = BigInteger.ModPow(P[2], p - 2, p);
        BigInteger x = ((P[0] * zinv % p) + p) % p;
        BigInteger y = ((P[1] * zinv % p) + p) % p;

        byte[] publickKey = (y | ((x & 1) << 255)).ToByteArray();

        return publickKey;
       
    }

    string expand_sk_a(byte[] sK)
    {

        //ed25519 sequence
        byte[] hash;
        SHA512 shaM = new SHA512Managed();
        hash = shaM.ComputeHash(sK);


        Debug.Log("SHA-512 of Secret is: " + BitConverter.ToString(hash).Replace("-", ""));


        hash[0] = (byte)(hash[0] & 0xF8);
        hash[31] = (byte)(hash[31] & 0x7F);
        hash[31] = (byte)(hash[31] | 0x40);
        byte[] tempRes = new byte[32];

        for (int i = 0; i < 32; i++)
        {
            tempRes[i] = hash[i];
        }

        hash = tempRes;


        //little endian 
        Array.Reverse(hash);

        string hexString = BitConverter.ToString(hash).Replace("-", "");

        //a part of sK expand
        Debug.Log("Secret Scalar: " + BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier));


        return hexString;



    }

    byte[] expand_sk_prefix(byte[] sK)
    {
        //ed25519 sequence
        byte[] hash;
        SHA512 shaM = new SHA512Managed();
        hash = shaM.ComputeHash(sK);

        byte[] subarray = hash.Skip(32).ToArray();
        return subarray;
    }

    //base58 encoding
    string base_58_encoding(byte[] publicKey)
    {
        byte[] truncArray = new byte[32];
        Array.Copy(publicKey, truncArray, truncArray.Length);

        BigInteger Base58Divisor = 58;

        char[] Base58Map = new char[] {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F',
            'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
            'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        var input = new BigInteger(truncArray, isUnsigned: true, isBigEndian: true);
        var remainders = new List<int>();
        while (input > 0)
        {
            input = BigInteger.DivRem(input, Base58Divisor, out var remainder);
            remainders.Add((int)remainder);
        }

        //preserve leading zeros
        foreach (var b in truncArray)
        {
            if (b == 0) { remainders.Add(0); } else { break; }
        }

        remainders.Reverse();

        var sbOutput = new StringBuilder();
        foreach (var remainder in remainders)
        {
            sbOutput.Append(Base58Map[remainder]);
        }
        string base58_pubkey = sbOutput.ToString();

        return base58_pubkey;

    }

    BigInteger Sha512Modq(byte[] s)
    {

        SHA512 shaM = new SHA512Managed();
        byte[] hash = shaM.ComputeHash(s);

        Debug.Log("********SHA TIME*************");
        Debug.Log("Secret");
        Debug.Log(BitConverter.ToString(s).Replace("-", ""));
        Debug.Log("HASH");
        Debug.Log(BitConverter.ToString(hash).Replace("-", ""));


        byte[] unsignedBytes = new byte[hash.Length + 1]; // add an extra byte
        hash.CopyTo(unsignedBytes, 0); // copy the original bytes
        BigInteger unsignedBigInt = new BigInteger(unsignedBytes);

        Debug.Log(unsignedBigInt);
        Debug.Log(unsignedBigInt % q);

        return unsignedBigInt % q;
    }



    byte[] sign_msg(byte[] sK, byte[] msg)
    {

        string hexString = expand_sk_a(sK);
        BigInteger sNum = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);
        BigInteger[] Q = point_mul(sNum, G);
        byte[] A = point_compress(point_mul(sNum, G));

        byte[] prefix = expand_sk_prefix(sK);

        BigInteger r = Sha512Modq(prefix.Concat(msg).ToArray());

        BigInteger[] R = point_mul(r, G);
        byte[] Rs = point_compress(R);
        
        BigInteger h = Sha512Modq(Rs.Take(32).Concat(A).Concat(msg).ToArray());
        BigInteger s = (r + h * sNum) % q;

        byte[] bytes = s.ToByteArray();

        return Rs.Take(32).Concat(bytes).ToArray();

    }



    public string GetPublicKey(string mnemonic_string)
    {

        //rfc test vector
        //string secret = "c5aa8df43f9f837bedb7442f31dcb7b166d38535076f094b85ce3a2e0b4458f7";

        string secret = "EA1622C08C673538021A4CD547033162A0C52C2168FCABC3D1BE0914B0F6DB35";

        byte[] sK = Enumerable.Range(0, secret.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(secret.Substring(x, 2), 16))
                         .ToArray();

        string hexString = expand_sk_a(sK);


        BigInteger sNum = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);


        BigInteger[] Q = point_mul(sNum, G);


        byte[] publicKey = point_compress(point_mul(sNum, G));


        hexString = BitConverter.ToString(publicKey).Replace("-", "");


        Debug.Log("ed25519 PubKey: "+hexString);


        Debug.Log("Solana PubKey: "+base_58_encoding(publicKey));



        byte[] msg1 = { 0x01, 0x00, 0x01, 0x03 };

        //from pubkey
        byte[] msg2 = { 0x82, 0x09, 0x69, 0x6e, 0x38, 0x5a, 0x2a, 0x50, 0x80, 0x65, 0xfa, 0xe4, 0x6b,
                        0xd2, 0x7a, 0xc8, 0xbd, 0x7c, 0xfa, 0xae, 0x6c, 0xe0, 0x94, 0xe3, 0x66, 0xec,
                        0x05, 0x11, 0x13, 0x2b, 0x00, 0xfb };
        //to pubkey
        byte[] msg3 = { 0x9b, 0x88, 0xa9, 0x04, 0xc8, 0x5b, 0xa6, 0xaa, 0x4c, 0x07, 0xa1, 0x79, 0xfb,
                        0xe9, 0x11, 0xc7, 0xcf, 0x68, 0x8e, 0x21, 0x62, 0x8c, 0xbf, 0xea, 0x24, 0x0e,
                        0x12, 0x43, 0x27, 0xcb, 0x65, 0x50 };

        //solana system programID
        byte[] msg4 = new byte[32];

        //recent block hash
        byte[] msg5 = { 0x3e, 0xd8, 0x93, 0x98, 0x01, 0x2f, 0x67, 0x06, 0x5f, 0xd5, 0xe6, 0x7f, 0xcc,
                        0x77, 0x6f, 0x19, 0x5a, 0xfd, 0x52, 0x12, 0x07, 0x53, 0x47, 0x07, 0xfa, 0x4f,
                        0xcd, 0x09, 0x3c, 0x29, 0x29, 0x06};

        byte[] msg6 = { 0x01, 0x02, 0x02, 0x00, 0x01, 0x0c };

        byte[] msg7 = { 0x02, 0x00, 0x00, 0x00, 0xe8, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };



        byte[] msg = msg1
            .Concat(msg2)
            .Concat(msg3)
            .Concat(msg4)
            .Concat(msg5)
            .Concat(msg6)
            .Concat(msg7)
            .ToArray();

        Debug.Log("msg: " + BitConverter.ToString(msg).Replace("-", ""));

        byte[] signature = sign_msg(sK, msg);
        Debug.Log("Signature: " + BitConverter.ToString(signature).Replace("-", ""));

        byte[] msgPreFix = { 0x01 };

        string rawTransaction = Convert.ToBase64String(msgPreFix.Concat(signature).Concat(msg).ToArray());


        Debug.Log(rawTransaction);



        return "";


    }



    // Start is called before the first frame update
    void Start()
    {

        GetPublicKey("test");

    }

    // Update is called once per frame
    void Update()
    {

    }
}