using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Websockets.DroidBridge {

	// Metadata.xml XPath class reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']"
	[global::Android.Runtime.Register ("websockets/DroidBridge/BridgeProxy", DoNotGenerateAcw=true)]
	public partial class BridgeProxy : global::Java.Lang.Object {
		static readonly JniPeerMembers _members = new XAPeerMembers ("websockets/DroidBridge/BridgeProxy", typeof (BridgeProxy));

		internal static IntPtr class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override IntPtr ThresholdClass {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		protected BridgeProxy (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
		{
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']/constructor[@name='BridgeProxy' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe BridgeProxy () : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "()V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), null);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_RaiseClosed;
#pragma warning disable 0169
		static Delegate GetRaiseClosedHandler ()
		{
			if (cb_RaiseClosed == null)
				cb_RaiseClosed = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_V) n_RaiseClosed);
			return cb_RaiseClosed;
		}

		static void n_RaiseClosed (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeProxy> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.RaiseClosed ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']/method[@name='RaiseClosed' and count(parameter)=0]"
		[Register ("RaiseClosed", "()V", "GetRaiseClosedHandler")]
		public virtual unsafe void RaiseClosed ()
		{
			const string __id = "RaiseClosed.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_RaiseData_arrayB;
#pragma warning disable 0169
		static Delegate GetRaiseData_arrayBHandler ()
		{
			if (cb_RaiseData_arrayB == null)
				cb_RaiseData_arrayB = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_V) n_RaiseData_arrayB);
			return cb_RaiseData_arrayB;
		}

		static void n_RaiseData_arrayB (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeProxy> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = (byte[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (byte));
			__this.RaiseData (p0);
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']/method[@name='RaiseData' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("RaiseData", "([B)V", "GetRaiseData_arrayBHandler")]
		public virtual unsafe void RaiseData (byte[] p0)
		{
			const string __id = "RaiseData.([B)V";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
				global::System.GC.KeepAlive (p0);
			}
		}

		static Delegate cb_RaiseError_Ljava_lang_Exception_;
#pragma warning disable 0169
		static Delegate GetRaiseError_Ljava_lang_Exception_Handler ()
		{
			if (cb_RaiseError_Ljava_lang_Exception_ == null)
				cb_RaiseError_Ljava_lang_Exception_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_V) n_RaiseError_Ljava_lang_Exception_);
			return cb_RaiseError_Ljava_lang_Exception_;
		}

		static void n_RaiseError_Ljava_lang_Exception_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeProxy> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Java.Lang.Exception> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.RaiseError (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']/method[@name='RaiseError' and count(parameter)=1 and parameter[1][@type='java.lang.Exception']]"
		[Register ("RaiseError", "(Ljava/lang/Exception;)V", "GetRaiseError_Ljava_lang_Exception_Handler")]
		public virtual unsafe void RaiseError (global::Java.Lang.Exception p0)
		{
			const string __id = "RaiseError.(Ljava/lang/Exception;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Throwable) p0).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				global::System.GC.KeepAlive (p0);
			}
		}

		static Delegate cb_RaiseError_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetRaiseError_Ljava_lang_String_Handler ()
		{
			if (cb_RaiseError_Ljava_lang_String_ == null)
				cb_RaiseError_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_V) n_RaiseError_Ljava_lang_String_);
			return cb_RaiseError_Ljava_lang_String_;
		}

		static void n_RaiseError_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeProxy> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.RaiseError (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']/method[@name='RaiseError' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("RaiseError", "(Ljava/lang/String;)V", "GetRaiseError_Ljava_lang_String_Handler")]
		public virtual unsafe void RaiseError (string p0)
		{
			const string __id = "RaiseError.(Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString ((string)p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_RaiseLog_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetRaiseLog_Ljava_lang_String_Handler ()
		{
			if (cb_RaiseLog_Ljava_lang_String_ == null)
				cb_RaiseLog_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_V) n_RaiseLog_Ljava_lang_String_);
			return cb_RaiseLog_Ljava_lang_String_;
		}

		static void n_RaiseLog_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeProxy> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.RaiseLog (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']/method[@name='RaiseLog' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("RaiseLog", "(Ljava/lang/String;)V", "GetRaiseLog_Ljava_lang_String_Handler")]
		public virtual unsafe void RaiseLog (string p0)
		{
			const string __id = "RaiseLog.(Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString ((string)p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_RaiseMessage_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetRaiseMessage_Ljava_lang_String_Handler ()
		{
			if (cb_RaiseMessage_Ljava_lang_String_ == null)
				cb_RaiseMessage_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_V) n_RaiseMessage_Ljava_lang_String_);
			return cb_RaiseMessage_Ljava_lang_String_;
		}

		static void n_RaiseMessage_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeProxy> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.RaiseMessage (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']/method[@name='RaiseMessage' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("RaiseMessage", "(Ljava/lang/String;)V", "GetRaiseMessage_Ljava_lang_String_Handler")]
		public virtual unsafe void RaiseMessage (string p0)
		{
			const string __id = "RaiseMessage.(Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString ((string)p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_RaiseOpened;
#pragma warning disable 0169
		static Delegate GetRaiseOpenedHandler ()
		{
			if (cb_RaiseOpened == null)
				cb_RaiseOpened = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_V) n_RaiseOpened);
			return cb_RaiseOpened;
		}

		static void n_RaiseOpened (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeProxy> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.RaiseOpened ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeProxy']/method[@name='RaiseOpened' and count(parameter)=0]"
		[Register ("RaiseOpened", "()V", "GetRaiseOpenedHandler")]
		public virtual unsafe void RaiseOpened ()
		{
			const string __id = "RaiseOpened.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

	}
}
