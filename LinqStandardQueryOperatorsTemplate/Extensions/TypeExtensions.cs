using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAtomic(this Type t)
        {
            return
                t == typeof(string) ||
                t == typeof(int) ||
                t == typeof(long) ||
                t == typeof(short) ||
                t == typeof(byte) ||
                t == typeof(uint) ||
                t == typeof(ulong) ||
                t == typeof(ushort) ||
                t == typeof(sbyte) ||
                t == typeof(double) ||
                t == typeof(float) ||
                t == typeof(decimal) ||
                t == typeof(char) ||
                t == typeof(bool) ||
                t.IsEnum;                
        }
        public static bool HasToString(this Type t)
        {
            return t.GetMethod("ToString", Type.EmptyTypes).DeclaringType == t;
        }
    }
}
