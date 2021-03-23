// Decompiled with JetBrains decompiler
// Type: Interop
// Assembly: System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e
// MVID: 87DBB896-6B20-4A14-9B6A-7AB43642646D
// Assembly location: /usr/share/dotnet/shared/Microsoft.NETCore.App/3.1.8/System.Private.CoreLib.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Buffers;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

internal static class Interop
{
  private static void ThrowExceptionForIoErrno(
    Interop.ErrorInfo errorInfo,
    string path,
    bool isDirectory,
    Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter)
  {
    if (errorRewriter != null)
      errorInfo = errorRewriter(errorInfo);
    throw Interop.GetExceptionForIoErrno(errorInfo, path, isDirectory);
  }

  internal static void CheckIo(
    Interop.Error error,
    string path = null,
    bool isDirectory = false,
    Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
  {
    if (error == Interop.Error.SUCCESS)
      return;
    Interop.ThrowExceptionForIoErrno(error.Info(), path, isDirectory, errorRewriter);
  }

  internal static long CheckIo(
    long result,
    string path = null,
    bool isDirectory = false,
    Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
  {
    if (result < 0L)
      Interop.ThrowExceptionForIoErrno(Interop.Sys.GetLastErrorInfo(), path, isDirectory, errorRewriter);
    return result;
  }

  internal static int CheckIo(
    int result,
    string path = null,
    bool isDirectory = false,
    Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
  {
    Interop.CheckIo((long) result, path, isDirectory, errorRewriter);
    return result;
  }

  internal static IntPtr CheckIo(
    IntPtr result,
    string path = null,
    bool isDirectory = false,
    Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
  {
    Interop.CheckIo((long) result, path, isDirectory, errorRewriter);
    return result;
  }

  internal static TSafeHandle CheckIo<TSafeHandle>(
    TSafeHandle handle,
    string path = null,
    bool isDirectory = false,
    Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
    where TSafeHandle : SafeHandle
  {
    if (handle.IsInvalid)
      Interop.ThrowExceptionForIoErrno(Interop.Sys.GetLastErrorInfo(), path, isDirectory, errorRewriter);
    return handle;
  }

  internal static Exception GetExceptionForIoErrno(
    Interop.ErrorInfo errorInfo,
    string path = null,
    bool isDirectory = false)
  {
    switch (errorInfo.Error)
    {
      case Interop.Error.EACCES:
      case Interop.Error.EBADF:
      case Interop.Error.EPERM:
        Exception ioException = Interop.GetIOException(errorInfo);
        return string.IsNullOrEmpty(path) ? (Exception) new UnauthorizedAccessException(SR.UnauthorizedAccess_IODenied_NoPathName, ioException) : (Exception) new UnauthorizedAccessException(SR.Format(SR.UnauthorizedAccess_IODenied_Path, (object) path), ioException);
      case Interop.Error.EAGAIN:
        return string.IsNullOrEmpty(path) ? (Exception) new IOException(SR.IO_SharingViolation_NoFileName, errorInfo.RawErrno) : (Exception) new IOException(SR.Format(SR.IO_SharingViolation_File, (object) path), errorInfo.RawErrno);
      case Interop.Error.ECANCELED:
        return (Exception) new OperationCanceledException();
      case Interop.Error.EEXIST:
        if (!string.IsNullOrEmpty(path))
          return (Exception) new IOException(SR.Format(SR.IO_FileExists_Name, (object) path), errorInfo.RawErrno);
        break;
      case Interop.Error.EFBIG:
        return (Exception) new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_FileLengthTooBig);
      case Interop.Error.ENAMETOOLONG:
        return string.IsNullOrEmpty(path) ? (Exception) new PathTooLongException(SR.IO_PathTooLong) : (Exception) new PathTooLongException(SR.Format(SR.IO_PathTooLong_Path, (object) path));
      case Interop.Error.ENOENT:
        return isDirectory ? (string.IsNullOrEmpty(path) ? (Exception) new DirectoryNotFoundException(SR.IO_PathNotFound_NoPathName) : (Exception) new DirectoryNotFoundException(SR.Format(SR.IO_PathNotFound_Path, (object) path))) : (string.IsNullOrEmpty(path) ? (Exception) new FileNotFoundException(SR.IO_FileNotFound) : (Exception) new FileNotFoundException(SR.Format(SR.IO_FileNotFound_FileName, (object) path), path));
    }
    return Interop.GetIOException(errorInfo);
  }

  internal static Exception GetIOException(Interop.ErrorInfo errorInfo) => (Exception) new IOException(errorInfo.GetErrorMessage(), errorInfo.RawErrno);

  internal static unsafe bool CallStringMethod<TArg1, TArg2, TArg3>(
    SpanFunc<char, TArg1, TArg2, TArg3, Interop.Globalization.ResultCode> interopCall,
    TArg1 arg1,
    TArg2 arg2,
    TArg3 arg3,
    out string result)
  {
    // ISSUE: untyped stack allocation
    Span<char> span1 = new Span<char>((void*) __untypedstackalloc(new IntPtr(512)), 256);
    switch (interopCall(span1, arg1, arg2, arg3))
    {
      case Interop.Globalization.ResultCode.Success:
        result = span1.Slice(0, span1.IndexOf<char>(char.MinValue)).ToString();
        return true;
      case Interop.Globalization.ResultCode.InsufficentBuffer:
        Span<char> span2 = (Span<char>) new char[1280];
        if (interopCall(span2, arg1, arg2, arg3) == Interop.Globalization.ResultCode.Success)
        {
          result = span2.Slice(0, span2.IndexOf<char>(char.MinValue)).ToString();
          return true;
        }
        break;
    }
    result = (string) null;
    return false;
  }

  internal static uint GetCurrentProcessId() => (uint) Interop.Sys.GetPid();

  internal static unsafe void GetRandomBytes(byte* buffer, int length) => Interop.Sys.GetNonCryptographicallySecureRandomBytes(buffer, length);

  internal static class Kernel32
  {
    internal const uint LMEM_FIXED = 0;
    internal const uint LMEM_MOVEABLE = 2;
    internal const int MAXIMUM_ALLOWED = 33554432;
    internal const int SYNCHRONIZE = 1048576;
    internal const int MUTEX_MODIFY_STATE = 1;
    internal const int SEMAPHORE_MODIFY_STATE = 2;
    internal const int EVENT_MODIFY_STATE = 2;
    internal const uint CREATE_EVENT_INITIAL_SET = 2;
    internal const uint CREATE_EVENT_MANUAL_RESET = 1;
    private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
    private const int FORMAT_MESSAGE_FROM_HMODULE = 2048;
    private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
    private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
    private const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
    private const int ERROR_INSUFFICIENT_BUFFER = 122;
    internal const uint CREATE_MUTEX_INITIAL_OWNER = 1;

    [DllImport("QCall")]
    internal static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("QCall")]
    internal static extern IntPtr LocalAlloc(uint uFlags, UIntPtr uBytes);

    [DllImport("QCall")]
    internal static extern IntPtr LocalReAlloc(IntPtr hMem, IntPtr uBytes, uint uFlags);

    [DllImport("QCall", SetLastError = true)]
    internal static extern IntPtr LocalFree(IntPtr hMem);

    [DllImport("QCall", SetLastError = true)]
    internal static extern bool CloseHandle(IntPtr handle);

    [DllImport("QCall", SetLastError = true)]
    internal static extern bool SetEvent(SafeWaitHandle handle);

    [DllImport("QCall", SetLastError = true)]
    internal static extern bool ResetEvent(SafeWaitHandle handle);

    [DllImport("QCall", EntryPoint = "CreateEventExW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern SafeWaitHandle CreateEventEx(
      IntPtr lpSecurityAttributes,
      string name,
      uint flags,
      uint desiredAccess);

    [DllImport("QCall", EntryPoint = "OpenEventW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern SafeWaitHandle OpenEvent(
      uint desiredAccess,
      bool inheritHandle,
      string name);

    internal static unsafe int GetEnvironmentVariable(string lpName, Span<char> buffer)
    {
      fixed (char* lpBuffer = &MemoryMarshal.GetReference<char>(buffer))
        return Interop.Kernel32.GetEnvironmentVariable(lpName, lpBuffer, buffer.Length);
    }

    [DllImport("QCall", EntryPoint = "GetEnvironmentVariableW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern unsafe int GetEnvironmentVariable(
      string lpName,
      char* lpBuffer,
      int nSize);

    [DllImport("QCall", EntryPoint = "GetEnvironmentStringsW", CharSet = CharSet.Unicode, SetLastError = true, BestFitMapping = false)]
    internal static extern unsafe char* GetEnvironmentStrings();

    [DllImport("QCall", EntryPoint = "FreeEnvironmentStringsW", CharSet = CharSet.Unicode, SetLastError = true, BestFitMapping = false)]
    internal static extern unsafe bool FreeEnvironmentStrings(char* lpszEnvironmentBlock);

    [DllImport("QCall", EntryPoint = "FormatMessageW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern unsafe int FormatMessage(
      int dwFlags,
      IntPtr lpSource,
      uint dwMessageId,
      int dwLanguageId,
      void* lpBuffer,
      int nSize,
      IntPtr arguments);

    internal static string GetMessage(int errorCode) => Interop.Kernel32.GetMessage(errorCode, IntPtr.Zero);

    internal static unsafe string GetMessage(int errorCode, IntPtr moduleHandle)
    {
      int dwFlags = 12800;
      if (moduleHandle != IntPtr.Zero)
        dwFlags |= 2048;
      // ISSUE: untyped stack allocation
      Span<char> span = new Span<char>((void*) __untypedstackalloc(new IntPtr(512)), 256);
      fixed (char* chPtr = &span.GetPinnableReference())
      {
        int length = Interop.Kernel32.FormatMessage(dwFlags, moduleHandle, (uint) errorCode, 0, (void*) chPtr, span.Length, IntPtr.Zero);
        if (length > 0)
          return Interop.Kernel32.GetAndTrimString(span.Slice(0, length));
      }
      if (Marshal.GetLastWin32Error() == 122)
      {
        IntPtr hglobal = new IntPtr();
        try
        {
          int length = Interop.Kernel32.FormatMessage(dwFlags | 256, moduleHandle, (uint) errorCode, 0, (void*) &hglobal, 0, IntPtr.Zero);
          if (length > 0)
            return Interop.Kernel32.GetAndTrimString(new Span<char>((void*) hglobal, length));
        }
        finally
        {
          Marshal.FreeHGlobal(hglobal);
        }
      }
      return string.Format("Unknown error (0x{0:x})", (object) errorCode);
    }

    private static string GetAndTrimString(Span<char> buffer)
    {
      int length = buffer.Length;
      while (length > 0 && buffer[length - 1] <= ' ')
        --length;
      return buffer.Slice(0, length).ToString();
    }

    [DllImport("QCall", EntryPoint = "OpenMutexW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern SafeWaitHandle OpenMutex(
      uint desiredAccess,
      bool inheritHandle,
      string name);

    [DllImport("QCall", EntryPoint = "CreateMutexExW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern SafeWaitHandle CreateMutexEx(
      IntPtr lpMutexAttributes,
      string name,
      uint flags,
      uint desiredAccess);

    [DllImport("QCall", SetLastError = true)]
    internal static extern bool ReleaseMutex(SafeWaitHandle handle);

    [DllImport("QCall", EntryPoint = "OpenSemaphoreW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern SafeWaitHandle OpenSemaphore(
      uint desiredAccess,
      bool inheritHandle,
      string name);

    [DllImport("QCall", EntryPoint = "CreateSemaphoreExW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern SafeWaitHandle CreateSemaphoreEx(
      IntPtr lpSecurityAttributes,
      int initialCount,
      int maximumCount,
      string name,
      uint flags,
      uint desiredAccess);

    [DllImport("QCall", SetLastError = true)]
    internal static extern bool ReleaseSemaphore(
      SafeWaitHandle handle,
      int releaseCount,
      out int previousCount);

    [DllImport("QCall", EntryPoint = "SetEnvironmentVariableW", CharSet = CharSet.Unicode, SetLastError = true, BestFitMapping = false)]
    internal static extern bool SetEnvironmentVariable(string lpName, string lpValue);

    [DllImport("QCall", SetLastError = true)]
    internal static extern unsafe int WriteFile(
      SafeHandle handle,
      byte* bytes,
      int numBytesToWrite,
      out int numBytesWritten,
      IntPtr mustBeZero);

    internal class HandleTypes
    {
      internal const int STD_INPUT_HANDLE = -10;
      internal const int STD_OUTPUT_HANDLE = -11;
      internal const int STD_ERROR_HANDLE = -12;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct OSVERSIONINFOEX
    {
      public int dwOSVersionInfoSize;
      public int dwMajorVersion;
      public int dwMinorVersion;
      public int dwBuildNumber;
      public int dwPlatformId;
      public unsafe fixed char szCSDVersion[128];
      public ushort wServicePackMajor;
      public ushort wServicePackMinor;
      public ushort wSuiteMask;
      public byte wProductType;
      public byte wReserved;
    }
  }

  internal class Ole32
  {
    [DllImport("QCall")]
    internal static extern IntPtr CoTaskMemAlloc(UIntPtr cb);

    [DllImport("QCall")]
    internal static extern IntPtr CoTaskMemRealloc(IntPtr pv, UIntPtr cb);

    [DllImport("QCall")]
    internal static extern void CoTaskMemFree(IntPtr ptr);
  }

  internal class OleAut32
  {
    [DllImport("QCall")]
    internal static extern IntPtr SysAllocStringByteLen(byte[] str, uint len);

    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern SafeBSTRHandle SysAllocStringLen(IntPtr src, uint len);

    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern IntPtr SysAllocStringLen(string src, int len);

    [DllImport("QCall")]
    internal static extern void SysFreeString(IntPtr bstr);

    [DllImport("QCall")]
    internal static extern uint SysStringLen(SafeBSTRHandle bstr);

    [DllImport("QCall")]
    internal static extern uint SysStringLen(IntPtr bstr);
  }

  internal enum BOOL
  {
    FALSE,
    TRUE,
  }

  internal static class Libraries
  {
    internal const string Kernel32 = "QCall";
    internal const string User32 = "QCall";
    internal const string Ole32 = "QCall";
    internal const string OleAut32 = "QCall";
    internal const string Advapi32 = "QCall";
    internal const string GlobalizationNative = "System.Globalization.Native";
    internal const string SystemNative = "System.Native";
  }

  internal class Advapi32
  {
    internal const int EVENT_CONTROL_CODE_DISABLE_PROVIDER = 0;
    internal const int EVENT_CONTROL_CODE_ENABLE_PROVIDER = 1;
    internal const int EVENT_CONTROL_CODE_CAPTURE_STATE = 2;

    internal enum ActivityControl : uint
    {
      EVENT_ACTIVITY_CTRL_GET_ID = 1,
      EVENT_ACTIVITY_CTRL_SET_ID = 2,
      EVENT_ACTIVITY_CTRL_CREATE_ID = 3,
      EVENT_ACTIVITY_CTRL_GET_SET_ID = 4,
      EVENT_ACTIVITY_CTRL_CREATE_SET_ID = 5,
    }

    internal struct EVENT_FILTER_DESCRIPTOR
    {
      public long Ptr;
      public int Size;
      public int Type;
    }

    internal unsafe delegate void EtwEnableCallback(
      in Guid sourceId,
      int isEnabled,
      byte level,
      long matchAnyKeywords,
      long matchAllKeywords,
      Interop.Advapi32.EVENT_FILTER_DESCRIPTOR* filterData,
      void* callbackContext);

    internal enum EVENT_INFO_CLASS
    {
      BinaryTrackInfo,
      SetEnableAllKeywords,
      SetTraits,
    }
  }

  internal class Errors
  {
    internal const int ERROR_SUCCESS = 0;
    internal const int ERROR_FILE_NOT_FOUND = 2;
    internal const int ERROR_PATH_NOT_FOUND = 3;
    internal const int ERROR_ACCESS_DENIED = 5;
    internal const int ERROR_INVALID_HANDLE = 6;
    internal const int ERROR_NOT_ENOUGH_MEMORY = 8;
    internal const int ERROR_INVALID_DRIVE = 15;
    internal const int ERROR_NO_MORE_FILES = 18;
    internal const int ERROR_NOT_READY = 21;
    internal const int ERROR_SHARING_VIOLATION = 32;
    internal const int ERROR_HANDLE_EOF = 38;
    internal const int ERROR_NOT_SUPPORTED = 50;
    internal const int ERROR_FILE_EXISTS = 80;
    internal const int ERROR_INVALID_PARAMETER = 87;
    internal const int ERROR_BROKEN_PIPE = 109;
    internal const int ERROR_INSUFFICIENT_BUFFER = 122;
    internal const int ERROR_INVALID_NAME = 123;
    internal const int ERROR_BAD_PATHNAME = 161;
    internal const int ERROR_ALREADY_EXISTS = 183;
    internal const int ERROR_ENVVAR_NOT_FOUND = 203;
    internal const int ERROR_FILENAME_EXCED_RANGE = 206;
    internal const int ERROR_NO_DATA = 232;
    internal const int ERROR_MORE_DATA = 234;
    internal const int ERROR_NO_MORE_ITEMS = 259;
    internal const int ERROR_NOT_OWNER = 288;
    internal const int ERROR_TOO_MANY_POSTS = 298;
    internal const int ERROR_ARITHMETIC_OVERFLOW = 534;
    internal const int ERROR_MUTANT_LIMIT_EXCEEDED = 587;
    internal const int ERROR_OPERATION_ABORTED = 995;
    internal const int ERROR_IO_PENDING = 997;
    internal const int ERROR_NO_UNICODE_TRANSLATION = 1113;
    internal const int ERROR_NOT_FOUND = 1168;
    internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;
    internal const int ERROR_NO_SYSTEM_RESOURCES = 1450;
    internal const int ERROR_TIMEOUT = 1460;
  }

  internal enum Error
  {
    SUCCESS = 0,
    E2BIG = 65537, // 0x00010001
    EACCES = 65538, // 0x00010002
    EADDRINUSE = 65539, // 0x00010003
    EADDRNOTAVAIL = 65540, // 0x00010004
    EAFNOSUPPORT = 65541, // 0x00010005
    EAGAIN = 65542, // 0x00010006
    EWOULDBLOCK = 65542, // 0x00010006
    EALREADY = 65543, // 0x00010007
    EBADF = 65544, // 0x00010008
    EBADMSG = 65545, // 0x00010009
    EBUSY = 65546, // 0x0001000A
    ECANCELED = 65547, // 0x0001000B
    ECHILD = 65548, // 0x0001000C
    ECONNABORTED = 65549, // 0x0001000D
    ECONNREFUSED = 65550, // 0x0001000E
    ECONNRESET = 65551, // 0x0001000F
    EDEADLK = 65552, // 0x00010010
    EDESTADDRREQ = 65553, // 0x00010011
    EDOM = 65554, // 0x00010012
    EDQUOT = 65555, // 0x00010013
    EEXIST = 65556, // 0x00010014
    EFAULT = 65557, // 0x00010015
    EFBIG = 65558, // 0x00010016
    EHOSTUNREACH = 65559, // 0x00010017
    EIDRM = 65560, // 0x00010018
    EILSEQ = 65561, // 0x00010019
    EINPROGRESS = 65562, // 0x0001001A
    EINTR = 65563, // 0x0001001B
    EINVAL = 65564, // 0x0001001C
    EIO = 65565, // 0x0001001D
    EISCONN = 65566, // 0x0001001E
    EISDIR = 65567, // 0x0001001F
    ELOOP = 65568, // 0x00010020
    EMFILE = 65569, // 0x00010021
    EMLINK = 65570, // 0x00010022
    EMSGSIZE = 65571, // 0x00010023
    EMULTIHOP = 65572, // 0x00010024
    ENAMETOOLONG = 65573, // 0x00010025
    ENETDOWN = 65574, // 0x00010026
    ENETRESET = 65575, // 0x00010027
    ENETUNREACH = 65576, // 0x00010028
    ENFILE = 65577, // 0x00010029
    ENOBUFS = 65578, // 0x0001002A
    ENODEV = 65580, // 0x0001002C
    ENOENT = 65581, // 0x0001002D
    ENOEXEC = 65582, // 0x0001002E
    ENOLCK = 65583, // 0x0001002F
    ENOLINK = 65584, // 0x00010030
    ENOMEM = 65585, // 0x00010031
    ENOMSG = 65586, // 0x00010032
    ENOPROTOOPT = 65587, // 0x00010033
    ENOSPC = 65588, // 0x00010034
    ENOSYS = 65591, // 0x00010037
    ENOTCONN = 65592, // 0x00010038
    ENOTDIR = 65593, // 0x00010039
    ENOTEMPTY = 65594, // 0x0001003A
    ENOTRECOVERABLE = 65595, // 0x0001003B
    ENOTSOCK = 65596, // 0x0001003C
    ENOTSUP = 65597, // 0x0001003D
    EOPNOTSUPP = 65597, // 0x0001003D
    ENOTTY = 65598, // 0x0001003E
    ENXIO = 65599, // 0x0001003F
    EOVERFLOW = 65600, // 0x00010040
    EOWNERDEAD = 65601, // 0x00010041
    EPERM = 65602, // 0x00010042
    EPIPE = 65603, // 0x00010043
    EPROTO = 65604, // 0x00010044
    EPROTONOSUPPORT = 65605, // 0x00010045
    EPROTOTYPE = 65606, // 0x00010046
    ERANGE = 65607, // 0x00010047
    EROFS = 65608, // 0x00010048
    ESPIPE = 65609, // 0x00010049
    ESRCH = 65610, // 0x0001004A
    ESTALE = 65611, // 0x0001004B
    ETIMEDOUT = 65613, // 0x0001004D
    ETXTBSY = 65614, // 0x0001004E
    EXDEV = 65615, // 0x0001004F
    ESOCKTNOSUPPORT = 65630, // 0x0001005E
    EPFNOSUPPORT = 65632, // 0x00010060
    ESHUTDOWN = 65644, // 0x0001006C
    EHOSTDOWN = 65648, // 0x00010070
    ENODATA = 65649, // 0x00010071
    EHOSTNOTFOUND = 131073, // 0x00020001
  }

  internal struct ErrorInfo
  {
    private Interop.Error _error;
    private int _rawErrno;

    internal ErrorInfo(int errno)
    {
      this._error = Interop.Sys.ConvertErrorPlatformToPal(errno);
      this._rawErrno = errno;
    }

    internal ErrorInfo(Interop.Error error)
    {
      this._error = error;
      this._rawErrno = -1;
    }

    internal Interop.Error Error => this._error;

    internal int RawErrno => this._rawErrno != -1 ? this._rawErrno : (this._rawErrno = Interop.Sys.ConvertErrorPalToPlatform(this._error));

    internal string GetErrorMessage() => Interop.Sys.StrError(this.RawErrno);

    public override string ToString() => string.Format("RawErrno: {0} Error: {1} GetErrorMessage: {2}", (object) this.RawErrno, (object) this.Error, (object) this.GetErrorMessage());
  }

  internal static class Sys
  {
    internal static Interop.Error GetLastError() => Interop.Sys.ConvertErrorPlatformToPal(Marshal.GetLastWin32Error());

    internal static Interop.ErrorInfo GetLastErrorInfo() => new Interop.ErrorInfo(Marshal.GetLastWin32Error());

    internal static unsafe string StrError(int platformErrno)
    {
      int bufferSize = 1024;
      byte* buffer = stackalloc byte[bufferSize];
      byte* numPtr = Interop.Sys.StrErrorR(platformErrno, buffer, bufferSize);
      if ((IntPtr) numPtr == IntPtr.Zero)
        numPtr = buffer;
      return Marshal.PtrToStringAnsi((IntPtr) (void*) numPtr);
    }

    [DllImport("System.Native", EntryPoint = "SystemNative_ConvertErrorPlatformToPal")]
    internal static extern Interop.Error ConvertErrorPlatformToPal(int platformErrno);

    [DllImport("System.Native", EntryPoint = "SystemNative_ConvertErrorPalToPlatform")]
    internal static extern int ConvertErrorPalToPlatform(Interop.Error error);

    [DllImport("System.Native", EntryPoint = "SystemNative_StrErrorR")]
    private static extern unsafe byte* StrErrorR(int platformErrno, byte* buffer, int bufferSize);

    [DllImport("System.Native", EntryPoint = "SystemNative_Access", SetLastError = true)]
    internal static extern int Access(string path, Interop.Sys.AccessMode mode);

    [DllImport("System.Native", EntryPoint = "SystemNative_ChDir", SetLastError = true)]
    internal static extern int ChDir(string path);

    [DllImport("System.Native", EntryPoint = "SystemNative_Close", SetLastError = true)]
    internal static extern int Close(IntPtr fd);

    [DllImport("System.Native", EntryPoint = "SystemNative_FLock", SetLastError = true)]
    internal static extern int FLock(SafeFileHandle fd, Interop.Sys.LockOperations operation);

    [DllImport("System.Native", EntryPoint = "SystemNative_FLock", SetLastError = true)]
    internal static extern int FLock(IntPtr fd, Interop.Sys.LockOperations operation);

    [DllImport("System.Native", EntryPoint = "SystemNative_FSync", SetLastError = true)]
    internal static extern int FSync(SafeFileHandle fd);

    [DllImport("System.Native", EntryPoint = "SystemNative_FTruncate", SetLastError = true)]
    internal static extern int FTruncate(SafeFileHandle fd, long length);

    [DllImport("System.Native", EntryPoint = "SystemNative_GetCpuUtilization")]
    internal static extern int GetCpuUtilization(
      ref Interop.Sys.ProcessCpuInformation previousCpuInfo);

    [DllImport("System.Native", EntryPoint = "SystemNative_GetCwd", SetLastError = true)]
    private static extern unsafe byte* GetCwd(byte* buffer, int bufferLength);

    internal static unsafe string GetCwd()
    {
      byte* ptr1 = stackalloc byte[256];
      string cwdHelper1 = Interop.Sys.GetCwdHelper(ptr1, 256);
      if (cwdHelper1 != null)
        return cwdHelper1;
      int minimumLength = 256;
label_3:
      checked { minimumLength *= 2; }
      byte[] array = ArrayPool<byte>.Shared.Rent(minimumLength);
      try
      {
        fixed (byte* ptr2 = &array[0])
        {
          string cwdHelper2 = Interop.Sys.GetCwdHelper(ptr2, array.Length);
          if (cwdHelper2 != null)
            return cwdHelper2;
          goto label_3;
        }
      }
      finally
      {
        ArrayPool<byte>.Shared.Return(array);
      }
    }

    private static unsafe string GetCwdHelper(byte* ptr, int bufferSize)
    {
      if ((IntPtr) Interop.Sys.GetCwd(ptr, bufferSize) != IntPtr.Zero)
        return Marshal.PtrToStringAnsi((IntPtr) (void*) ptr);
      Interop.ErrorInfo lastErrorInfo = Interop.Sys.GetLastErrorInfo();
      if (lastErrorInfo.Error == Interop.Error.ERANGE)
        return (string) null;
      throw Interop.GetExceptionForIoErrno(lastErrorInfo);
    }

    [DllImport("System.Native", EntryPoint = "SystemNative_GetEUid")]
    internal static extern uint GetEUid();

    [DllImport("System.Native", EntryPoint = "SystemNative_GetHostName", SetLastError = true)]
    private static extern unsafe int GetHostName(byte* name, int nameLength);

    internal static unsafe string GetHostName()
    {
      byte* name = stackalloc byte[256];
      int hostName = Interop.Sys.GetHostName(name, 256);
      if (hostName != 0)
        throw new InvalidOperationException(string.Format("{0}: {1}", (object) nameof (GetHostName), (object) hostName));
      name[(int) byte.MaxValue] = (byte) 0;
      return Marshal.PtrToStringAnsi((IntPtr) (void*) name);
    }

    [DllImport("System.Native", EntryPoint = "SystemNative_GetPid")]
    internal static extern int GetPid();

    [DllImport("System.Native", EntryPoint = "SystemNative_GetPwUidR")]
    internal static extern unsafe int GetPwUidR(
      uint uid,
      out Interop.Sys.Passwd pwd,
      byte* buf,
      int bufLen);

    [DllImport("System.Native", EntryPoint = "SystemNative_GetPwNamR")]
    internal static extern unsafe int GetPwNamR(
      string name,
      out Interop.Sys.Passwd pwd,
      byte* buf,
      int bufLen);

    [DllImport("System.Native", EntryPoint = "SystemNative_GetNonCryptographicallySecureRandomBytes")]
    internal static extern unsafe void GetNonCryptographicallySecureRandomBytes(
      byte* buffer,
      int length);

    [DllImport("System.Native", EntryPoint = "SystemNative_GetSystemTimeAsTicks")]
    internal static extern long GetSystemTimeAsTicks();

    [DllImport("System.Native", EntryPoint = "SystemNative_GetUnixName")]
    private static extern IntPtr GetUnixNamePrivate();

    internal static string GetUnixName() => Marshal.PtrToStringAnsi(Interop.Sys.GetUnixNamePrivate());

    [DllImport("System.Native", EntryPoint = "SystemNative_GetUnixRelease", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern string GetUnixRelease();

    [DllImport("System.Native", EntryPoint = "SystemNative_LockFileRegion", SetLastError = true)]
    internal static extern int LockFileRegion(
      SafeHandle fd,
      long offset,
      long length,
      Interop.Sys.LockType lockType);

    [DllImport("System.Native", EntryPoint = "SystemNative_LSeek", SetLastError = true)]
    internal static extern long LSeek(
      SafeFileHandle fd,
      long offset,
      Interop.Sys.SeekWhence whence);

    [DllImport("System.Native", EntryPoint = "SystemNative_MksTemps", SetLastError = true)]
    internal static extern IntPtr MksTemps(byte[] template, int suffixlen);

    [DllImport("System.Native", EntryPoint = "SystemNative_GetAllMountPoints", SetLastError = true)]
    private static extern int GetAllMountPoints(Interop.Sys.MountPointFound mpf);

    internal static unsafe string[] GetAllMountPoints()
    {
      int count = 0;
      string[] found = new string[4];
      Interop.Sys.GetAllMountPoints((Interop.Sys.MountPointFound) (name =>
      {
        if (count == found.Length)
          Array.Resize<string>(ref found, count * 2);
        found[count++] = Marshal.PtrToStringAnsi((IntPtr) (void*) name);
      }));
      Array.Resize<string>(ref found, count);
      return found;
    }

    [DllImport("System.Native", EntryPoint = "SystemNative_Open", SetLastError = true)]
    internal static extern SafeFileHandle Open(
      string filename,
      Interop.Sys.OpenFlags flags,
      int mode);

    [DllImport("System.Native", EntryPoint = "SystemNative_PathConf", SetLastError = true)]
    private static extern int PathConf(string path, Interop.Sys.PathConfName name);

    [DllImport("System.Native", EntryPoint = "SystemNative_PosixFAdvise")]
    internal static extern int PosixFAdvise(
      SafeFileHandle fd,
      long offset,
      long length,
      Interop.Sys.FileAdvice advice);

    [DllImport("System.Native", EntryPoint = "SystemNative_Read", SetLastError = true)]
    internal static extern unsafe int Read(SafeHandle fd, byte* buffer, int count);

    [DllImport("System.Native", EntryPoint = "SystemNative_OpenDir", SetLastError = true)]
    internal static extern IntPtr OpenDir(string path);

    [DllImport("System.Native", EntryPoint = "SystemNative_GetReadDirRBufferSize")]
    internal static extern int GetReadDirRBufferSize();

    [DllImport("System.Native", EntryPoint = "SystemNative_ReadDirR")]
    internal static extern unsafe int ReadDirR(
      IntPtr dir,
      byte* buffer,
      int bufferSize,
      out Interop.Sys.DirectoryEntry outputEntry);

    [DllImport("System.Native", EntryPoint = "SystemNative_CloseDir", SetLastError = true)]
    internal static extern int CloseDir(IntPtr dir);

    [DllImport("System.Native", EntryPoint = "SystemNative_ReadLink", SetLastError = true)]
    private static extern int ReadLink(string path, byte[] buffer, int bufferSize);

    public static string ReadLink(string path)
    {
      int minimumLength = 256;
      while (true)
      {
        byte[] numArray = ArrayPool<byte>.Shared.Rent(minimumLength);
        try
        {
          int count = Interop.Sys.ReadLink(path, numArray, numArray.Length);
          if (count < 0)
            return (string) null;
          if (count < numArray.Length)
            return Encoding.UTF8.GetString(numArray, 0, count);
        }
        finally
        {
          ArrayPool<byte>.Shared.Return(numArray);
        }
        minimumLength *= 2;
      }
    }

    [DllImport("System.Native", EntryPoint = "SystemNative_FStat", SetLastError = true)]
    internal static extern int FStat(SafeFileHandle fd, out Interop.Sys.FileStatus output);

    [DllImport("System.Native", EntryPoint = "SystemNative_Stat", SetLastError = true)]
    internal static extern int Stat(string path, out Interop.Sys.FileStatus output);

    [DllImport("System.Native", EntryPoint = "SystemNative_LStat", SetLastError = true)]
    internal static extern int LStat(string path, out Interop.Sys.FileStatus output);

    [DllImport("System.Native", EntryPoint = "SystemNative_SysConf", SetLastError = true)]
    internal static extern long SysConf(Interop.Sys.SysConfName name);

    [DllImport("System.Native", EntryPoint = "SystemNative_SysLog")]
    internal static extern void SysLog(
      Interop.Sys.SysLogPriority priority,
      string message,
      string arg1);

    [DllImport("System.Native", EntryPoint = "SystemNative_Unlink", SetLastError = true)]
    internal static extern int Unlink(string pathname);

    [DllImport("System.Native", EntryPoint = "SystemNative_Write", SetLastError = true)]
    internal static extern unsafe int Write(SafeHandle fd, byte* buffer, int bufferSize);

    [DllImport("System.Native", EntryPoint = "SystemNative_Write", SetLastError = true)]
    internal static extern unsafe int Write(int fd, byte* buffer, int bufferSize);

    internal enum AccessMode
    {
      F_OK = 0,
      X_OK = 1,
      W_OK = 2,
      R_OK = 4,
    }

    internal enum LockOperations
    {
      LOCK_SH = 1,
      LOCK_EX = 2,
      LOCK_NB = 4,
      LOCK_UN = 8,
    }

    internal struct ProcessCpuInformation
    {
      internal ulong lastRecordedCurrentTime;
      internal ulong lastRecordedKernelTime;
      internal ulong lastRecordedUserTime;
    }

    internal struct Passwd
    {
      internal const int InitialBufferSize = 256;
      internal unsafe byte* Name;
      internal unsafe byte* Password;
      internal uint UserId;
      internal uint GroupId;
      internal unsafe byte* UserInfo;
      internal unsafe byte* HomeDirectory;
      internal unsafe byte* Shell;
    }

    internal enum LockType : short
    {
      F_WRLCK = 1,
      F_UNLCK = 2,
    }

    internal enum SeekWhence
    {
      SEEK_SET,
      SEEK_CUR,
      SEEK_END,
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private unsafe delegate void MountPointFound(byte* name);

    [Flags]
    internal enum OpenFlags
    {
      O_RDONLY = 0,
      O_WRONLY = 1,
      O_RDWR = 2,
      O_CLOEXEC = 16, // 0x00000010
      O_CREAT = 32, // 0x00000020
      O_EXCL = 64, // 0x00000040
      O_TRUNC = 128, // 0x00000080
      O_SYNC = 256, // 0x00000100
    }

    internal enum PathConfName
    {
      PC_LINK_MAX = 1,
      PC_MAX_CANON = 2,
      PC_MAX_INPUT = 3,
      PC_NAME_MAX = 4,
      PC_PATH_MAX = 5,
      PC_PIPE_BUF = 6,
      PC_CHOWN_RESTRICTED = 7,
      PC_NO_TRUNC = 8,
      PC_VDISABLE = 9,
    }

    [Flags]
    internal enum Permissions
    {
      Mask = 511, // 0x000001FF
      S_IRWXU = 448, // 0x000001C0
      S_IRUSR = 256, // 0x00000100
      S_IWUSR = 128, // 0x00000080
      S_IXUSR = 64, // 0x00000040
      S_IRWXG = 56, // 0x00000038
      S_IRGRP = 32, // 0x00000020
      S_IWGRP = 16, // 0x00000010
      S_IXGRP = 8,
      S_IRWXO = 7,
      S_IROTH = 4,
      S_IWOTH = 2,
      S_IXOTH = 1,
    }

    internal enum FileAdvice
    {
      POSIX_FADV_NORMAL,
      POSIX_FADV_RANDOM,
      POSIX_FADV_SEQUENTIAL,
      POSIX_FADV_WILLNEED,
      POSIX_FADV_DONTNEED,
      POSIX_FADV_NOREUSE,
    }

    internal enum NodeType
    {
      DT_UNKNOWN = 0,
      DT_FIFO = 1,
      DT_CHR = 2,
      DT_DIR = 4,
      DT_BLK = 6,
      DT_REG = 8,
      DT_LNK = 10, // 0x0000000A
      DT_SOCK = 12, // 0x0000000C
      DT_WHT = 14, // 0x0000000E
    }

    internal struct DirectoryEntry
    {
      internal unsafe byte* Name;
      internal int NameLength;
      internal Interop.Sys.NodeType InodeType;
      internal const int NameBufferSize = 256;

      internal unsafe ReadOnlySpan<char> GetName(Span<char> buffer)
      {
        int chars = Encoding.UTF8.GetChars(this.NameLength == -1 ? new ReadOnlySpan<byte>((void*) this.Name, new ReadOnlySpan<byte>((void*) this.Name, 256).IndexOf<byte>((byte) 0)) : new ReadOnlySpan<byte>((void*) this.Name, this.NameLength), buffer);
        return (ReadOnlySpan<char>) buffer.Slice(0, chars);
      }
    }

    internal struct FileStatus
    {
      internal Interop.Sys.FileStatusFlags Flags;
      internal int Mode;
      internal uint Uid;
      internal uint Gid;
      internal long Size;
      internal long ATime;
      internal long ATimeNsec;
      internal long MTime;
      internal long MTimeNsec;
      internal long CTime;
      internal long CTimeNsec;
      internal long BirthTime;
      internal long BirthTimeNsec;
      internal long Dev;
      internal long Ino;
      internal uint UserFlags;
    }

    internal static class FileTypes
    {
      internal const int S_IFMT = 61440;
      internal const int S_IFIFO = 4096;
      internal const int S_IFCHR = 8192;
      internal const int S_IFDIR = 16384;
      internal const int S_IFREG = 32768;
      internal const int S_IFLNK = 40960;
      internal const int S_IFSOCK = 49152;
    }

    [Flags]
    internal enum FileStatusFlags
    {
      None = 0,
      HasBirthTime = 1,
    }

    internal enum SysConfName
    {
      _SC_CLK_TCK = 1,
      _SC_PAGESIZE = 2,
    }

    internal enum SysLogPriority
    {
      LOG_EMERG = 0,
      LOG_KERN = 0,
      LOG_ALERT = 1,
      LOG_CRIT = 2,
      LOG_ERR = 3,
      LOG_WARNING = 4,
      LOG_NOTICE = 5,
      LOG_INFO = 6,
      LOG_DEBUG = 7,
      LOG_USER = 8,
      LOG_MAIL = 16, // 0x00000010
      LOG_DAEMON = 24, // 0x00000018
      LOG_AUTH = 32, // 0x00000020
      LOG_SYSLOG = 40, // 0x00000028
      LOG_LPR = 48, // 0x00000030
      LOG_NEWS = 56, // 0x00000038
      LOG_UUCP = 64, // 0x00000040
      LOG_CRON = 72, // 0x00000048
      LOG_AUTHPRIV = 80, // 0x00000050
      LOG_FTP = 88, // 0x00000058
      LOG_LOCAL0 = 128, // 0x00000080
      LOG_LOCAL1 = 136, // 0x00000088
      LOG_LOCAL2 = 144, // 0x00000090
      LOG_LOCAL3 = 152, // 0x00000098
      LOG_LOCAL4 = 160, // 0x000000A0
      LOG_LOCAL5 = 168, // 0x000000A8
      LOG_LOCAL6 = 176, // 0x000000B0
      LOG_LOCAL7 = 184, // 0x000000B8
    }
  }

  internal static class Globalization
  {
    internal const int AllowUnassigned = 1;
    internal const int UseStd3AsciiRules = 2;

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetCalendars", CharSet = CharSet.Unicode)]
    internal static extern int GetCalendars(
      string localeName,
      CalendarId[] calendars,
      int calendarsCapacity);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetCalendarInfo", CharSet = CharSet.Unicode)]
    internal static extern unsafe Interop.Globalization.ResultCode GetCalendarInfo(
      string localeName,
      CalendarId calendarId,
      CalendarDataType calendarDataType,
      char* result,
      int resultCapacity);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_EnumCalendarInfo", CharSet = CharSet.Unicode)]
    internal static extern bool EnumCalendarInfo(
      Interop.Globalization.EnumCalendarInfoCallback callback,
      string localeName,
      CalendarId calendarId,
      CalendarDataType calendarDataType,
      IntPtr context);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetLatestJapaneseEra")]
    internal static extern int GetLatestJapaneseEra();

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetJapaneseEraStartDate")]
    internal static extern bool GetJapaneseEraStartDate(
      int era,
      out int startYear,
      out int startMonth,
      out int startDay);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_ChangeCase", CharSet = CharSet.Unicode)]
    internal static extern unsafe void ChangeCase(
      char* src,
      int srcLen,
      char* dstBuffer,
      int dstBufferCapacity,
      bool bToUpper);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_ChangeCaseInvariant", CharSet = CharSet.Unicode)]
    internal static extern unsafe void ChangeCaseInvariant(
      char* src,
      int srcLen,
      char* dstBuffer,
      int dstBufferCapacity,
      bool bToUpper);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_ChangeCaseTurkish", CharSet = CharSet.Unicode)]
    internal static extern unsafe void ChangeCaseTurkish(
      char* src,
      int srcLen,
      char* dstBuffer,
      int dstBufferCapacity,
      bool bToUpper);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetSortHandle", CharSet = CharSet.Ansi)]
    internal static extern Interop.Globalization.ResultCode GetSortHandle(
      string localeName,
      out IntPtr sortHandle);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_CloseSortHandle")]
    internal static extern void CloseSortHandle(IntPtr handle);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_CompareString", CharSet = CharSet.Unicode)]
    internal static extern unsafe int CompareString(
      IntPtr sortHandle,
      char* lpStr1,
      int cwStr1Len,
      char* lpStr2,
      int cwStr2Len,
      CompareOptions options);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_IndexOf", CharSet = CharSet.Unicode)]
    internal static extern unsafe int IndexOf(
      IntPtr sortHandle,
      char* target,
      int cwTargetLength,
      char* pSource,
      int cwSourceLength,
      CompareOptions options,
      int* matchLengthPtr);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_LastIndexOf", CharSet = CharSet.Unicode)]
    internal static extern unsafe int LastIndexOf(
      IntPtr sortHandle,
      char* target,
      int cwTargetLength,
      char* pSource,
      int cwSourceLength,
      CompareOptions options);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_IndexOfOrdinalIgnoreCase", CharSet = CharSet.Unicode)]
    internal static extern unsafe int IndexOfOrdinalIgnoreCase(
      string target,
      int cwTargetLength,
      char* pSource,
      int cwSourceLength,
      bool findLast);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_IndexOfOrdinalIgnoreCase", CharSet = CharSet.Unicode)]
    internal static extern unsafe int IndexOfOrdinalIgnoreCase(
      char* target,
      int cwTargetLength,
      char* pSource,
      int cwSourceLength,
      bool findLast);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_StartsWith", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern unsafe bool StartsWith(
      IntPtr sortHandle,
      char* target,
      int cwTargetLength,
      char* source,
      int cwSourceLength,
      CompareOptions options);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_EndsWith", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern unsafe bool EndsWith(
      IntPtr sortHandle,
      char* target,
      int cwTargetLength,
      char* source,
      int cwSourceLength,
      CompareOptions options);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_StartsWith", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool StartsWith(
      IntPtr sortHandle,
      string target,
      int cwTargetLength,
      string source,
      int cwSourceLength,
      CompareOptions options);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_EndsWith", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool EndsWith(
      IntPtr sortHandle,
      string target,
      int cwTargetLength,
      string source,
      int cwSourceLength,
      CompareOptions options);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetSortKey", CharSet = CharSet.Unicode)]
    internal static extern unsafe int GetSortKey(
      IntPtr sortHandle,
      char* str,
      int strLength,
      byte* sortKey,
      int sortKeyLength,
      CompareOptions options);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_CompareStringOrdinalIgnoreCase", CharSet = CharSet.Unicode)]
    internal static extern unsafe int CompareStringOrdinalIgnoreCase(
      char* lpStr1,
      int cwStr1Len,
      char* lpStr2,
      int cwStr2Len);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetSortVersion")]
    internal static extern int GetSortVersion(IntPtr sortHandle);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_LoadICU")]
    internal static extern int LoadICU();

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_ToAscii", CharSet = CharSet.Unicode)]
    internal static extern unsafe int ToAscii(
      uint flags,
      char* src,
      int srcLen,
      char* dstBuffer,
      int dstBufferCapacity);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_ToUnicode", CharSet = CharSet.Unicode)]
    internal static extern unsafe int ToUnicode(
      uint flags,
      char* src,
      int srcLen,
      char* dstBuffer,
      int dstBufferCapacity);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetLocaleName", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern unsafe bool GetLocaleName(
      string localeName,
      char* value,
      int valueLength);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetLocaleInfoString", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern unsafe bool GetLocaleInfoString(
      string localeName,
      uint localeStringData,
      char* value,
      int valueLength);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetDefaultLocaleName", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern unsafe bool GetDefaultLocaleName(char* value, int valueLength);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetLocaleTimeFormat", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern unsafe bool GetLocaleTimeFormat(
      string localeName,
      bool shortFormat,
      char* value,
      int valueLength);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetLocaleInfoInt", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetLocaleInfoInt(
      string localeName,
      uint localeNumberData,
      ref int value);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetLocaleInfoGroupingSizes", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetLocaleInfoGroupingSizes(
      string localeName,
      uint localeGroupingData,
      ref int primaryGroupSize,
      ref int secondaryGroupSize);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetLocales", CharSet = CharSet.Unicode)]
    internal static extern int GetLocales([Out] char[] value, int valueLength);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_IsNormalized", CharSet = CharSet.Unicode)]
    internal static extern int IsNormalized(
      NormalizationForm normalizationForm,
      string src,
      int srcLen);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_NormalizeString", CharSet = CharSet.Unicode)]
    internal static extern int NormalizeString(
      NormalizationForm normalizationForm,
      string src,
      int srcLen,
      [Out] char[] dstBuffer,
      int dstBufferCapacity);

    [DllImport("System.Globalization.Native", EntryPoint = "GlobalizationNative_GetTimeZoneDisplayName", CharSet = CharSet.Unicode)]
    internal static extern unsafe Interop.Globalization.ResultCode GetTimeZoneDisplayName(
      string localeName,
      string timeZoneId,
      Interop.Globalization.TimeZoneDisplayNameType type,
      char* result,
      int resultLength);

    internal delegate void EnumCalendarInfoCallback([MarshalAs(UnmanagedType.LPWStr)] string calendarString, IntPtr context);

    internal enum ResultCode
    {
      Success,
      UnknownError,
      InsufficentBuffer,
      OutOfMemory,
    }

    internal enum TimeZoneDisplayNameType
    {
      Generic,
      Standard,
      DaylightSavings,
    }
  }
}
