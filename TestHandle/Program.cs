using System;
using System.Runtime.InteropServices;

namespace TestHandle
{
	class SafeHandleTest : SafeHandle
	{
		public SafeHandleTest () : base(IntPtr.Zero, true)
		{
		}

		protected override void Dispose (bool disposing)
		{
			bool leaked = !IsInvalid && !disposing;
			if (leaked)
				throw new NotImplementedException ("We dun goofed");

			base.Dispose (disposing);
		}

		public override bool IsInvalid {
			get {
				return handle == IntPtr.Zero;
			}
		}

		protected override bool ReleaseHandle ()
		{
			return true;
		}
	}

	class MainClass
	{
		[DllImport("mynativefunction")]
		static extern void alloc_test(out SafeHandleTest sht);

		static void TryCreateSafeHandle()
		{
			SafeHandleTest sht;
			alloc_test (out sht);

			// Comment this out if you want to get rid of on purpose leak.
			// sht.Dispose ();
		}

		public static void Main (string[] args)
		{
			TryCreateSafeHandle ();
			GC.Collect ();
			GC.WaitForPendingFinalizers ();
		}
	}
}
