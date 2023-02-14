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

    bool testRFC = false;

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
        BigInteger A = (((P[1] - P[0]) * (Q[1] - Q[0]) % p)+p)%p;
        BigInteger B = (((P[1] + P[0]) * (Q[1] + Q[0]) % p)+p)%p;

        BigInteger C = ((2 * P[3] * Q[3] * d % p)+p)%p;
        BigInteger D = ((2 * P[2] * Q[2] % p)+p)%p;

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
        BigInteger x = ((P[0] * zinv % p)+p)%p;
        BigInteger y = ((P[1] * zinv % p)+p)%p;

        Debug.Log( (y | ((x & 1) << 255)).ToByteArray());

        Debug.Log((x & 1));
        Debug.Log((x & 1) << 255);


        string byte_rowX = "";
        string byte_rowY = "";
        foreach (byte b in x.ToByteArray())
        {
                byte_rowX += "," + (b).ToString();
        }

        foreach (byte b in y.ToByteArray())
        {
            byte_rowY += "," + (b).ToString();
        }


        Debug.Log(byte_rowX);
        Debug.Log(byte_rowY);


        string byte_row4 = "";
        Debug.Log("PubKey");


        byte[] publickKey = (y | ((x & 1) << 255)).ToByteArray();


        byte[] truncArray = new byte[32];

        Array.Copy(publickKey, truncArray, truncArray.Length);

        foreach (byte b in truncArray)
        {
            //RFC test vector
            byte_row4 += "," + (b).ToString();

            //if(firstByte)
            //{
            //    //solana set
            //    byte_row4 += "," + (b).ToString();
            //    //byte_row4 += ((b & 0xF8)).ToString();
            //    firstByte = false;
            //}
            //else
            //{
            //    byte_row4 += "," + (b).ToString();
            //}
            
        }

        Debug.Log(byte_row4);

        //base58 encoding

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


    public string GetPublicKey(string mnemonic_string)
    {

        //string secret = "9d61b19deffd5a60ba844af492ec2cc44449c5697b326919703bac031cae7f60";

        string secret = "4ccd089b28ff96da9db6c346ec114e0f5b8a319f35aba624da8cf6ed4fb8a6fb";


        // Start web3j example
        //mnemonic_string = "dutch steel mandate learn witness grab library achieve base mother resource scissors";
        



        string byte_row = "";

        byte[] bytesw = Encoding.ASCII.GetBytes(mnemonic_string);
        foreach (byte b in bytesw)
        {
            byte_row += "," + (b).ToString();
        }

        //Debug.Log(byte_row);

        string password = "";
        string salt = "mnemonic" + password;


        string byte_row2 = "";
        byte[] bytes2 = Encoding.ASCII.GetBytes(salt);
        foreach (byte b in bytes2)
        {
            byte_row2 += "," + (b).ToString();
        }

        //Debug.Log(byte_row2);


        Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(bytesw, bytes2, 2048, HashAlgorithmName.SHA512);


        byte[] bytes3 = k1.GetBytes(32);
        string byte_row3 = "";
        foreach (byte b in bytes3)
        {
            byte_row3 += "," + (b).ToString();
        }

        //Debug.Log("SecretKey");
        //Debug.Log(byte_row3);

        //end web3js example



        int NumberChars = secret.Length;
        byte[] bytes = new byte[NumberChars / 2];

        if (testRFC)
        {

            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(secret.Substring(i, 2), 16);
            }


        }
        else
        {
            bytes = bytes3;
        }






        //ed25519 sequence
        byte[] result;
        SHA512 shaM = new SHA512Managed();
        result = shaM.ComputeHash(bytes);
        //Debug.Log(result);
        string byte_row4 = "";

        foreach (byte b in result)
        {
            byte_row4 += "," + (b).ToString("X");
        }

        Debug.Log("SHA-512 of Secret is: " + byte_row4);


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

        //Debug.Log("After Setting Bits");
        //Debug.Log(byte_row4);
        byte_row4 = "";

        //little endian 
        Array.Reverse(result);

        foreach (byte b in result)
        {
            byte_row4 += "," + (b).ToString();
        }

        //Debug.Log("After Reversing");
        //Debug.Log(byte_row4);

        string hexString = BitConverter.ToString(result).Replace("-", "");
        //Debug.Log("hex result");
        //Debug.Log(hexString);

        Debug.Log("Secret Scalar: " + BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier));


        BigInteger sNum = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);


        BigInteger[] Q = point_mul(sNum, G);

        Debug.Log(Q[0]);
        Debug.Log(Q[1]);
        Debug.Log(Q[2]);
        Debug.Log(Q[3]);

        Debug.Log("PublicKey");
        Debug.Log(point_compress(point_mul(sNum, G)));



        return point_compress(point_mul(sNum, G));


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



    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
