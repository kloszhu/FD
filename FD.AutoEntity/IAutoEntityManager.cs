using System;
using System.Collections.Generic;

namespace FD.AutoEntity
{
    public interface IAutoEntityManager
    {
        List<Type> AutoEntityList { get;  }
        void Create(AutoEntityManager.classDefineModel model);
    }
}