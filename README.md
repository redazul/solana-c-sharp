
Web3JS Reference
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
