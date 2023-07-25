namespace InlineASM
{
    public static class AssemblyBytes
    {
        //f(a,b,c) = a*b+c
        public static byte[] MulAdd_int64 =
            {
            0x55,               //push  rbp
            0x0f,0xaf,0xca,     //imul  rcx,rdx
            0x49,0x01,0xc8,     //add   r8,rcx
            0x49,0x8b,0xc0,     //mov   rax,r8
            0x5D,               //pop   rbp
            0xC3                //ret
            };

        public static byte[] MulAdd_float =
            {
            0x55,                  //push    rbp
            0xf3,0x0f,0x59,0xc1,   //mulss   xmm0,xmm1
            0xf3,0x0f,0x58,0xc2,   //addss   xmm0,xmm2
            0x5D,                  //pop     rbp
            0xC3                   //ret
            };

        public static byte[] MulAdd_double =
            {
            0x55,                  //push    rbp
            0xf2,0x0f,0x59,0xc1,   //mulsd   xmm0,xmm1
            0xf2,0x0f,0x58,0xc2,   //addsd   xmm0,xmm2
            0x5D,                  //pop     rbp
            0xC3                   //ret
            };

        public static byte[] Sqrt_Float =
            {
            0x55,                  // push    rbp
            0xf3,0x0f,0x51,0xc0,   // sqrtss  xmm0,xmm0
            0x5D,                  // pop     rbp
            0xC3                   // ret
            };

        public static byte[] Sqrt_Double =
            {
            0x55,                  // push    rbp
            0xf2,0x0f,0x51,0xc0,   // sqrtsd  xmm0,xmm0
            0x5D,                  // pop     rbp
            0xC3                   // ret
            };
    }
}