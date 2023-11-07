using NMH_WebAPI.Models;

namespace NMH_WebAPI.Helpers
{
    public interface IProcessHelper
    {
        OutputModel ProcessKey(int key, double inputValue);
    }
}
