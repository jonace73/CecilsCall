using Android.Widget;
using CecilsCall.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CecilsCall.Droid
{
    public class Debugger
    {
        private static int numberCalls = 0;
        public static void Msg(string text)
        {
            if (App.isInDebug)
            {
                numberCalls++;
                string textToWrite = numberCalls.ToString() + ".) " + text;
                DebugPage.AppendLine(textToWrite);
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
                DebugPage.AppendLine("ERR: " + sf.GetMethod().ToString() + " " + sf.GetFileLineNumber().ToString());
            }
        }
        public async static Task<bool> Prompt(string header, string body, string OK)
        {
            if (App.isInDebug)
                await App.Current.MainPage.DisplayAlert(header, body, OK);

            return true;
        }
    } // CLASS ENDS
}
