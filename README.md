
Web3JS Reference

Test Only
``` stomach knife cry deal hour diesel cream unlock battle surface skirt popular ```
```
{
    "publicKey": {
        "0": 140,
        "1": 80,
        "2": 18,
        "3": 212,
        "4": 195,
        "5": 92,
        "6": 253,
        "7": 60,
        "8": 35,
        "9": 196,
        "10": 233,
        "11": 168,
        "12": 13,
        "13": 163,
        "14": 126,
        "15": 247,
        "16": 248,
        "17": 191,
        "18": 189,
        "19": 209,
        "20": 84,
        "21": 25,
        "22": 206,
        "23": 158,
        "24": 115,
        "25": 46,
        "26": 76,
        "27": 112,
        "28": 58,
        "29": 128,
        "30": 220,
        "31": 155
    },
    "secretKey": {
        "0": 246,
        "1": 12,
        "2": 159,
        "3": 200,
        "4": 66,
        "5": 8,
        "6": 152,
        "7": 191,
        "8": 30,
        "9": 123,
        "10": 10,
        "11": 38,
        "12": 251,
        "13": 84,
        "14": 141,
        "15": 68,
        "16": 229,
        "17": 128,
        "18": 235,
        "19": 103,
        "20": 33,
        "21": 217,
        "22": 2,
        "23": 223,
        "24": 87,
        "25": 4,
        "26": 157,
        "27": 41,
        "28": 220,
        "29": 178,
        "30": 164,
        "31": 206,
        "32": 140,
        "33": 80,
        "34": 18,
        "35": 212,
        "36": 195,
        "37": 92,
        "38": 253,
        "39": 60,
        "40": 35,
        "41": 196,
        "42": 233,
        "43": 168,
        "44": 13,
        "45": 163,
        "46": 126,
        "47": 247,
        "48": 248,
        "49": 191,
        "50": 189,
        "51": 209,
        "52": 84,
        "53": 25,
        "54": 206,
        "55": 158,
        "56": 115,
        "57": 46,
        "58": 76,
        "59": 112,
        "60": 58,
        "61": 128,
        "62": 220,
        "63": 155
    }
}
```
```
  // https://yihau.github.io/solana-web3-demo/tour/create-keypair.html
  const mnemonic = "pill tomorrow foster begin walnut borrow virtual kick shift mutual shoe scatter";

  const seed = bip39.mnemonicToSeedSync(mnemonic, ""); // (mnemonic, password)
  const keypair = Keypair.fromSeed(seed.slice(0, 32));
  console.log(`${keypair.publicKey.toBase58()}`); // 5ZWj7a1f8tWkjBESHKgrLmXshuXxqeY9SYcfbshpAqPG

```
Sha512 Hex Input

https://emn178.github.io/online-tools/sha512.html

```
        string secret = "4ccd089b28ff96da9db6c346ec114e0f5b8a319f35aba624da8cf6ed4fb8a6fb";
        int NumberChars = secret.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(secret.Substring(i, 2), 16);
        }
 ```


Base Field: 57896044618658097711785492504343953926634992332820282019728792003956564819949

Curve Constant: 37095705934669439343138083508754565189542113879843219016388785533085940283555

Group order: 7237005577332262213973186563042994240857116359379907606001950938285454250989

expand secret

Secret: 4ccd089b28ff96da9db6c346ec114e0f5b8a319f35aba624da8cf6ed4fb8a6fb

Secret 512-Hash: 6ebd9ed75882d52815a97585caf4790a7f6c6b3b7f821c5e259a24b02e502e114566848291dacaf225cc63deb348da318e2c2e17b00b8160f9ce6bfa0472911d

First 32 of 512-Hash: 6ebd9ed75882d52815a97585caf4790a7f6c6b3b7f821c5e259a24b02e502e11

int from byte: 7771146789310644793240907535824857665122308212013791326779304059184915332462

Binary: 0b1000100101110010100000010111010110000001001001001101000100101010111100001110010000010011111110011101101101011011011000111111100001010011110011111010011001010100001010111010110101001000101010010100011010101100000100101100011010111100111101011110101101110

AND with: 0b11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111000

OR with: 0b100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000

Secret Scalar s[B]: 36719169098639693649133653787996834628439804378423932336643700061163197742440

G constant: (15112221349535400772501151409588531511454012693041857206046113283949847762202, 46316835694926478169428394003475163141307993866256225615783033603165251855960, 1, 46827403850823179245072216630277197565144205554125654976674165829533817101731)

PoitMul

PublicKey: 3d4017c3e843895a92b70aa74d1b7ebc9c982ccf2ec4968cc0cd55f12af4660c
