using System;
using System.IO;
using System.Threading.Tasks;

namespace DeviceTester.Interfaces
{
    public interface IPhotoPicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
