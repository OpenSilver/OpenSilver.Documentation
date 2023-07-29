using ServiceReference1;
using System;
using System.Threading.Tasks;

namespace OpenSilverServiceProxy
{
    public class ServiceProxy
    {
        private readonly Service1Client _serviceClient;
        public ServiceProxy()
        {
            _serviceClient = new Service1Client();
        }

        public async Task<string> GetDataAsync(Int32 id)
        {
            return await _serviceClient.GetDataAsync(id);
        }
    }
}
