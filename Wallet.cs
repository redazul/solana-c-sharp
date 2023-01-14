using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Numerics;
using System.Globalization;


public class Wallet : MonoBehaviour
{
    BigInteger p = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819949");
    BigInteger d = BigInteger.Parse("37095705934669439343138083508754565189542113879843219016388785533085940283555");

    BigInteger[] G = {
        BigInteger.Parse("15112221349535400772501151409588531511454012693041857206046113283949847762202"),
        BigInteger.Parse("46316835694926478169428394003475163141307993866256225615783033603165251855960"),
        BigInteger.Parse("1"),
        BigInteger.Parse("46827403850823179245072216630277197565144205554125654976674165829533817101731")
    };

    BigInteger[] point_add(BigInteger[] P,BigInteger[] Q)
    {
        BigInteger A = (P[1] - P[0]) * (Q[1] - Q[0]) % p;
        BigInteger B = (P[1] + P[0]) * (Q[1] + Q[0]) % p;

        BigInteger C = 2 * P[3] * Q[3] * d % p;
        BigInteger D = 2 * P[2] * Q[2] % p;

        BigInteger E = B - A;
        BigInteger F = D - C;
        BigInteger G = D + C;
        BigInteger H = B + A;

        BigInteger[] bByte = {E*F,G*H,F*G,E*H};
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

    string point_compress(BigInteger[] P)
    {

        BigInteger zinv = BigInteger.ModPow(P[2], p - 2, p);
        BigInteger x = P[0] * zinv % p;
        BigInteger y = P[1] * zinv % p;

        Debug.Log( (y | ((x & 1) << 255)).ToByteArray());


        string byte_row4 = "";
        Debug.Log("PubKey");
        foreach (byte b in ((y | ((x & 1) << 255)).ToByteArray()))
        {
            byte_row4 += "," + (b).ToString("X");
        }

        Debug.Log(byte_row4);

        return "";

    }



    // Start is called before the first frame update
    void Start()
    {
        string secret = "4ccd089b28ff96da9db6c346ec114e0f5b8a319f35aba624da8cf6ed4fb8a6fb";
        

        int NumberChars = secret.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(secret.Substring(i, 2), 16);
        }




        //ed25519 sequence
        byte[] result;
        SHA512 shaM = new SHA512Managed();
        result = shaM.ComputeHash(bytes);
        Debug.Log(result);
        string byte_row4 = "";
        Debug.Log("SHA-512 of Secret is:");
        foreach (byte b in result)
        {
            byte_row4 += "," + (b).ToString("X");
        }

        Debug.Log(byte_row4);

        result[0] = (byte)(result[0] & 0xF8);
        result[31] = (byte)(result[31] & 0x7F);
        result[31] = (byte)(result[31] | 0x40);
        byte[] tempRes = new byte[32];

        for (int i = 0; i < 32; i++)
        {
            tempRes[i] = result[i];
        }

        result = tempRes;

        byte_row4 = "";
        foreach (byte b in result)
        {
            byte_row4 += "," + (b).ToString();
        }

        Debug.Log("After Setting Bits");
        Debug.Log(byte_row4);
        byte_row4 = "";

        //little endian 
        Array.Reverse(result);

        foreach (byte b in result)
        {
            byte_row4 += "," + (b).ToString();
        }

        Debug.Log("After Reversing");
        Debug.Log(byte_row4);

        string hexString = BitConverter.ToString(result).Replace("-", "");
        Debug.Log("hex result");
        Debug.Log(hexString);

        Debug.Log("BigNumber Version");
        Debug.Log(BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier));

        BigInteger sNum = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);


        point_compress(point_mul(sNum, G));
      


        //string sBin = "";
        //int first = 0;
        //foreach (char c in hexString)
        //{
        //    //Debug.Log(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'));

        //    if (first == 0){
        //        sBin = sBin + Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2);
        //        first++;
        //    }
        //    else
        //    {
        //        sBin = sBin + Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
        //    }


        //}

        //Debug.Log(sBin);











    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
