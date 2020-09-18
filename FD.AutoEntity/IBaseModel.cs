using System;
using System.Collections.Generic;
using System.Text;

namespace FD.AutoEntity
{
    public interface IBaseModel<TKey> : ModuleManager
    {
        TKey id { get; }
    }

    public interface ModuleManager
    {
        int version { get; }
        DateTime? updateDate{get;}
        string ModuleName { get; }
        void SetModule(string moduleName);
        void SetVersion();
        void SetUpdateDate();

    }


}
