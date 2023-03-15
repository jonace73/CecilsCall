
using System;
using CecilsCall.iOS.Services;
using CecilsCall.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSLockScreenAudio))]
namespace CecilsCall.iOS.Services
{
    public class IOSLockScreenAudio : ILockScreenAudio
    {
        public void FireIntent(string action)
        {

        }
    }
}