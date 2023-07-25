using System.Diagnostics;
using System.Security;

namespace InlineASM
{
    internal class Program
    {
        [SuppressUnmanagedCodeSecurity]
        private delegate long Muladd_long(long a, long b, long c);

        [SuppressUnmanagedCodeSecurity]
        private delegate float Muladd_float(float a, float b, float c);

        [SuppressUnmanagedCodeSecurity]
        private delegate double Muladd_double(double a, double b, double c);

        [SuppressUnmanagedCodeSecurity]
        private delegate float Sqrt_float(float a);

        [SuppressUnmanagedCodeSecurity]
        private delegate double Sqrt_double(double a);

        private static void Main(string[] args)
        {
            ASMWrapper<Muladd_long> muladd_long = new ASMWrapper<Muladd_long>(ref AssemblyBytes.MulAdd_int64);
            ASMWrapper<Muladd_float> muladd_float = new ASMWrapper<Muladd_float>(ref AssemblyBytes.MulAdd_float);
            ASMWrapper<Muladd_double> muladd_double = new ASMWrapper<Muladd_double>(ref AssemblyBytes.MulAdd_double);
            ASMWrapper<Sqrt_float> sqrt_float = new ASMWrapper<Sqrt_float>(ref AssemblyBytes.Sqrt_Float);
            ASMWrapper<Sqrt_double> sqrt_double = new ASMWrapper<Sqrt_double>(ref AssemblyBytes.Sqrt_Double);

            Debug.Assert(muladd_long.DelegateFunc(100, 200, 300) == 100 * 200 + 300);
            Debug.Assert(muladd_float.DelegateFunc(1.1f, 2.2f, 3.3f) == 1.1f * 2.2f + 3.3f);
            Debug.Assert(muladd_double.DelegateFunc(1.1d, 2.2d, 3.3d) == 1.1d * 2.2d + 3.3d);
            Debug.Assert(sqrt_float.DelegateFunc(MathF.Tau) == MathF.Sqrt(MathF.Tau));
            Debug.Assert(sqrt_double.DelegateFunc(Math.Tau) == Math.Sqrt(Math.Tau));

            Console.Write("Test OK. Press Enter to Exit...");
            Console.ReadLine();
        }
    }
}