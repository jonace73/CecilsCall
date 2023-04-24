using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Websockets.DroidBridge {

	// Metadata.xml XPath class reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeController']"
	[global::Android.Runtime.Register ("websockets/DroidBridge/BridgeController", DoNotGenerateAcw=true)]
	public partial class BridgeController : global::Java.Lang.Object {

		// Metadata.xml XPath field reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeController']/field[@name='proxy']"
		[Register ("proxy")]
		public global::Websockets.DroidBridge.BridgeProxy Proxy {
			get {
				const string __id = "proxy.Lwebsockets/DroidBridge/BridgeProxy;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeProxy> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "proxy.Lwebsockets/DroidBridge/BridgeProxy;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}

		static readonly JniPeerMembers _members = new XAPeerMembers ("websockets/DroidBridge/BridgeController", typeof (BridgeController));

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

		protected BridgeController (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
		{
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeController']/constructor[@name='BridgeController' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe BridgeController () : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
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

		static Delegate cb_Close;
#pragma warning disable 0169
		static Delegate GetCloseHandler ()
		{
			if (cb_Close == null)
				cb_Close = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_V) n_Close);
			return cb_Close;
		}

		static void n_Close (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeController> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Close ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeController']/method[@name='Close' and count(parameter)=0]"
		[Register ("Close", "()V", "GetCloseHandler")]
		public virtual unsafe void Close ()
		{
			const string __id = "Close.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_Open_Ljava_lang_String_Ljava_lang_String_Ljava_util_Map_;
#pragma warning disable 0169
		static Delegate GetOpen_Ljava_lang_String_Ljava_lang_String_Ljava_util_Map_Handler ()
		{
			if (cb_Open_Ljava_lang_String_Ljava_lang_String_Ljava_util_Map_ == null)
				cb_Open_Ljava_lang_String_Ljava_lang_String_Ljava_util_Map_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPLLL_V) n_Open_Ljava_lang_String_Ljava_lang_String_Ljava_util_Map_);
			return cb_Open_Ljava_lang_String_Ljava_lang_String_Ljava_util_Map_;
		}

		static void n_Open_Ljava_lang_String_Ljava_lang_String_Ljava_util_Map_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, IntPtr native_p2)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeController> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = JNIEnv.GetString (native_p1, JniHandleOwnership.DoNotTransfer);
			var p2 = global::Android.Runtime.JavaDictionary<string, string>.FromJniHandle (native_p2, JniHandleOwnership.DoNotTransfer);
			__this.Open (p0, p1, p2);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeController']/method[@name='Open' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.util.Map&lt;java.lang.String, java.lang.String&gt;']]"
		[Register ("Open", "(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V", "GetOpen_Ljava_lang_String_Ljava_lang_String_Ljava_util_Map_Handler")]
		public virtual unsafe void Open (string p0, string p1, global::System.Collections.Generic.IDictionary<string, string> p2)
		{
			const string __id = "Open.(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V";
			IntPtr native_p0 = JNIEnv.NewString ((string)p0);
			IntPtr native_p1 = JNIEnv.NewString ((string)p1);
			IntPtr native_p2 = global::Android.Runtime.JavaDictionary<string, string>.ToLocalJniHandle (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
				global::System.GC.KeepAlive (p2);
			}
		}

		static Delegate cb_Send_arrayB;
#pragma warning disable 0169
		static Delegate GetSend_arrayBHandler ()
		{
			if (cb_Send_arrayB == null)
				cb_Send_arrayB = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_V) n_Send_arrayB);
			return cb_Send_arrayB;
		}

		static void n_Send_arrayB (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeController> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = (byte[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (byte));
			__this.Send (p0);
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeController']/method[@name='Send' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("Send", "([B)V", "GetSend_arrayBHandler")]
		public virtual unsafe void Send (byte[] p0)
		{
			const string __id = "Send.([B)V";
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

		static Delegate cb_Send_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetSend_Ljava_lang_String_Handler ()
		{
			if (cb_Send_Ljava_lang_String_ == null)
				cb_Send_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((_JniMarshal_PPL_V) n_Send_Ljava_lang_String_);
			return cb_Send_Ljava_lang_String_;
		}

		static void n_Send_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeController> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.Send (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeController']/method[@name='Send' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("Send", "(Ljava/lang/String;)V", "GetSend_Ljava_lang_String_Handler")]
		public virtual unsafe void Send (string p0)
		{
			const string __id = "Send.(Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString ((string)p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_SetIsAllTrusted;
#pragma warning disable 0169
		static Delegate GetSetIsAllTrustedHandler ()
		{
			if (cb_SetIsAllTrusted == null)
				cb_SetIsAllTrusted = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_V) n_SetIsAllTrusted);
			return cb_SetIsAllTrusted;
		}

		static void n_SetIsAllTrusted (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Websockets.DroidBridge.BridgeController> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.SetIsAllTrusted ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='websockets.DroidBridge']/class[@name='BridgeController']/method[@name='SetIsAllTrusted' and count(parameter)=0]"
		[Register ("SetIsAllTrusted", "()V", "GetSetIsAllTrustedHandler")]
		public virtual unsafe void SetIsAllTrusted ()
		{
			const string __id = "SetIsAllTrusted.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

	}
}
