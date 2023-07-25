using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace InlineASM
{
    public class ASMWrapper<T> : IDisposable
    {
        private readonly byte[] data;

        private IntPtr ptr;

        public T DelegateFunc;

        public ASMWrapper(ref byte[] asm_code)
        {
            data = asm_code;
            ptr = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(asm_code, 0, ptr, data.Length);

            if (!Win32Interop.VirtualProtectEx(Process.GetCurrentProcess().Handle, ptr,
                        (UIntPtr)data.Length, 0x40 /* PAGE_EXECUTE_READWRITE */, out uint _))
            {
                throw new Win32Exception();
            }

            DelegateFunc = Marshal.GetDelegateForFunctionPointer<T>(ptr);
        }

        public void Dispose()
        {
            // Restore memory address to readwrite for safety.
            if (!Win32Interop.VirtualProtectEx(Process.GetCurrentProcess().Handle, ptr,
                        (UIntPtr)data.Length, 0x04 /* PAGE_READWRITE */, out uint _))
            {
                throw new Win32Exception();
            }
            Marshal.FreeHGlobal(ptr);
        }
    }
}