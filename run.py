import hashlib
import binascii

def sha512(s):
    return hashlib.sha512(s).digest()

# Base field Z_p
p = 2**255 - 19

print("Base Field: "+str(p))
print()

def modp_inv(x):
    return pow(x, p-2, p)

# Curve constant
d = -121665 * modp_inv(121666) % p
print("Curve Constant: "+ str(d))
print()

# Group order
q = 2**252 + 27742317777372353535851937790883648493
print("Group order: "+str(q))
print()

def sha512_modq(s):

    print()
    print()
    print("In sha512 function")

    print("s")
    print(binascii.hexlify(s).decode('utf-8'))

    print("hash")
    print(binascii.hexlify(sha512(s)).decode('utf-8'))

    print("int_from_bytes_little");
    print(int.from_bytes(sha512(s), "little"))

    print("modulo")
    print(int.from_bytes(sha512(s), "little") % q)
    print()
    print()


    return int.from_bytes(sha512(s), "little") % q

## Then follows functions to perform point operations.

# Points are represented as tuples (X, Y, Z, T) of extended
# coordinates, with x = X/Z, y = Y/Z, x*y = T/Z

def point_add(P, Q):
    A, B = (P[1]-P[0]) * (Q[1]-Q[0]) % p, (P[1]+P[0]) * (Q[1]+Q[0]) % p;
    C, D = 2 * P[3] * Q[3] * d % p, 2 * P[2] * Q[2] % p;
    E, F, G, H = B-A, D-C, D+C, B+A;
    return (E*F, G*H, F*G, E*H);

# Computes Q = s * Q
def point_mul(s, P):

    print("PoitMul")
    print()
    
    Q = (0, 1, 1, 0)  # Neutral element

    while s > 0:

        #check LSB for 1
        if s & 1:
            Q = point_add(Q, P)
        P = point_add(P, P)
        s >>= 1

    return Q

def point_equal(P, Q):
    # x1 / z1 == x2 / z2  <==>  x1 * z2 == x2 * z1
    if (P[0] * Q[2] - Q[0] * P[2]) % p != 0:
        return False
    if (P[1] * Q[2] - Q[1] * P[2]) % p != 0:
        return False
    return True

## Now follows functions for point compression.

# Square root of -1
modp_sqrt_m1 = pow(2, (p-1) // 4, p)

# Compute corresponding x-coordinate, with low bit corresponding to
# sign, or return None on failure
def recover_x(y, sign):
    if y >= p:
        return None
    x2 = (y*y-1) * modp_inv(d*y*y+1)
    if x2 == 0:
        if sign:
            return None
        else:
            return 0

    # Compute square root of x2
    x = pow(x2, (p+3) // 8, p)
    if (x*x - x2) % p != 0:
        x = x * modp_sqrt_m1 % p
    if (x*x - x2) % p != 0:
        return None

    if (x & 1) != sign:
        x = p - x
    return x

# Base point
g_y = 4 * modp_inv(5) % p
g_x = recover_x(g_y, 0)
G = (g_x, g_y, 1, g_x * g_y % p)

def point_compress(P):
    zinv = modp_inv(P[2])
    x = P[0] * zinv % p
    y = P[1] * zinv % p
    return int.to_bytes(y | ((x & 1) << 255), 32, "little")

def point_decompress(s):
    if len(s) != 32:
        raise Exception("Invalid input length for decompression")
    y = int.from_bytes(s, "little")
    sign = y >> 255
    y &= (1 << 255) - 1

    x = recover_x(y, sign)
    if x is None:
        return None
    else:
        return (x, y, 1, x*y % p)

## These are functions for manipulating the private key.

def secret_expand(secret):
    print("expand secret")
    if len(secret) != 32:
        raise Exception("Bad size of private key")


    #secret
    byteString = ""
    for x in range(32):
        #print(len(hex(h[x])))

        if(len(hex(secret[x]))==3):
               #print("0"+ str(hex(h[x]))[-1])
            byteString=byteString + "0"+ str(hex(secret[x]))[-1]
        else:
            byteString=byteString + str(hex(secret[x]))[2:]

    print("Secret: "+byteString)
    print()



    #create sha
    h = sha512(secret)
    print("THE PREFIX")
    print(binascii.hexlify(h[32:]).decode('utf-8'))


    byteString = ""
    for x in range(64):
        #print(len(hex(h[x])))

        if(len(hex(h[x]))==3):
               #print("0"+ str(hex(h[x]))[-1])
            byteString=byteString + "0"+ str(hex(h[x]))[-1]
        else:
            byteString=byteString + str(hex(h[x]))[2:]

    print("Secret 512-Hash: "+byteString)
    print()

    #grab first 32 bytes 
    byteString = ""
    for x in range(32):
        #print(len(hex(h[x])))

        if(len(hex(h[:32][x]))==3):
               #print("0"+ str(hex(h[x]))[-1])
            byteString=byteString + "0"+str(hex(h[:32][x]))[-1]
        else:
            byteString=byteString + str(hex(h[:32][x]))[2:]

    print("First 32 of 512-Hash: "+byteString)
    print()
    
    
    a = int.from_bytes(h[:32], "little")
    print("int from byte: "+ str(a))

    print()
    print("Binary: "+bin(a))
    
    a &= (1 << 254) - 8


    print()
    print("AND with: "+str(bin((1 << 254)-8)))
    
    a |= (1 << 254)

    print()
    print("OR with: "+str(bin((1 << 254))))
    print()
    print("Secret Scalar s[B]: "+str(a))
    
    return (a, h[32:])

def secret_to_public(secret):
    (a, dummy) = secret_expand(secret)
    print()
    print("G constant: " + str(G))
    print()
    return point_compress(point_mul(a, G))

## The signature function works as below.

def sign(secret, msg):

    print()
    print("In Sign")
    print(binascii.hexlify(sK).decode('utf-8'))
    print(binascii.hexlify(msg).decode('utf-8'))
    print()


    a, prefix = secret_expand(secret)
    A = point_compress(point_mul(a, G))


    print("In Sign Prefix")
    print(binascii.hexlify(secret).decode('utf-8'))
    print(binascii.hexlify(prefix).decode('utf-8'))
    print(a)
    print(binascii.hexlify(prefix + msg).decode('utf-8'))

    r = sha512_modq(prefix + msg)

    print()
    print()
    print("sign r")
    print(r)
    print()



    R = point_mul(r, G)
    Rs = point_compress(R)
    h = sha512_modq(Rs + A + msg)
    s = (r + h * a) % q



    print()
    print()
    print("THE SIGN CONCAT RETURN")
    print("Rs")
    print(binascii.hexlify(Rs).decode('utf-8'))
    print("Rs+A+Msg")
    print(binascii.hexlify(Rs+A+msg).decode('utf-8'))
    print("r")
    print(r)
    print("h")
    print(h)
    print("a")
    print(a)
    print("addition")
    print(r+h*a);
    print((r + h * a) % q)
    print("s")
    print(s)
    print(binascii.hexlify(int.to_bytes(s, 32, "little")).decode('utf-8'))

    return Rs + int.to_bytes(s, 32, "little")

## And finally the verification function.

def verify(public, msg, signature):
    if len(public) != 32:
        raise Exception("Bad public key length")
    if len(signature) != 64:
        Exception("Bad signature length")
    A = point_decompress(public)
    if not A:
        return False
    Rs = signature[:32]
    R = point_decompress(Rs)
    if not R:
        return False
    s = int.from_bytes(signature[32:], "little")
    if s >= q: return False
    h = sha512_modq(Rs + public + msg)
    sB = point_mul(s, G)
    hA = point_mul(h, A)
    return point_equal(sB, point_add(R, hA))


#https://www.rfc-editor.org/rfc/rfc8032#section-7.1
sK =    bytes.fromhex("c5aa8df43f9f837bedb7442f31dcb7b166d38535076f094b85ce3a2e0b4458f7")
msg=    bytes.fromhex("af82");


res = secret_to_public(sK)

#grab first 32 bytes 
byteString = ""
for x in range(32):
     #print(len(hex(h[x])))

      if(len(hex(res[:32][x]))==3):
            #print("0"+ str(hex(h[x]))[-1])
            byteString=byteString + "0"+str(hex(res[:32][x]))[-1]
      else:
            byteString=byteString + str(hex(res[:32][x]))[2:]



#Signature Test Vector
signature = sign(sK,msg)
print("Signed Message: "+binascii.hexlify(signature).decode('utf-8'))
print()
print("PublicKey: "+byteString)