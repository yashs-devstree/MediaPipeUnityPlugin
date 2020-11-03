using System;

namespace Mediapipe {
  public class SidePacket : MpResourceHandle {
    public SidePacket() : base() {
      UnsafeNativeMethods.mp_SidePacket__(out var ptr).Assert();
      this.ptr = ptr;
    }

    protected override void DisposeUnmanaged() {
      if (isOwner) {
        UnsafeNativeMethods.mp_SidePacket__delete(ptr);
      }
      base.DisposeUnmanaged();
    }

    public int size {
      get { return SafeNativeMethods.mp_SidePacket__size(mpPtr); }
    }

    /// TODO: force T to be Packet
    /// <remarks>Make sure that the type of the returned packet value is correct</remarks>
    public T At<T>(string key) {
      UnsafeNativeMethods.mp_SidePacket__at__PKc(mpPtr, key, out var packetPtr).Assert();

      if (packetPtr == IntPtr.Zero) {
        return default(T); // null
      }

      GC.KeepAlive(this);
      return (T)Activator.CreateInstance(typeof(T), packetPtr, true);
    }

    public void Emplace<T>(string key, Packet<T> packet) {
      UnsafeNativeMethods.mp_SidePacket__emplace__PKc_Rpacket(mpPtr, key, packet.mpPtr).Assert();
      packet.Dispose(); // respect move semantics
      GC.KeepAlive(this);
    }

    public int Erase(string key) {
      UnsafeNativeMethods.mp_SidePacket__erase__PKc(mpPtr, key, out var count).Assert();

      GC.KeepAlive(this);
      return count;
    }

    public void Clear() {
      SafeNativeMethods.mp_SidePacket__clear(mpPtr);
    }
  }
}
