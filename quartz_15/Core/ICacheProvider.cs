using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Core
{
    public interface ICacheProvider
    {
        void Store<T>(string key, T data);
        void Destroy(string key);
        T Get<T>(string key);
    }
}
