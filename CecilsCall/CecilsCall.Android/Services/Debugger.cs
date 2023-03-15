using Android.Widget;
using CecilsCall.Views;
using System;
using System.Diagnostics;

namespace CecilsCall.Droid
{
    public class Debugger
    {
        private static int numberCalls = 0;
        public static void Msg(string text)
        {
            numberCalls++;
            string textToWrite = numberCalls.ToString() + ".) " + text;
            if (App.isInDebug)
            {
                DebugPage.AppendLine(textToWrite);
            } else
            {
                //Toast.MakeText(Android.App.Application.Context, textToWrite, ToastLength.Long).Show();
            }
        }
        [STAThread]
        public static void StackTrace()
        {
            StackTrace st = new StackTrace(true);
            for (int i = 0; i < st.FrameCount; i++)
            {
                // Note that high up the call stack, there is only
                // one stack frame.
                StackFrame sf = st.GetFrame(i);
                DebugPage.AppendLine("ERR: " +sf.GetMethod().ToString() + " " + sf.GetFileLineNumber().ToString());
            }
        }
    } // CLASS ENDS
}
