using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CecilsCall.Services
{
    public interface IAudioDuration
    {
        Task<int> GetAudioDuration();
    }
}
