using System;
using System.Collections.Generic;
using System.Text;

namespace FD.DapperExtention
{
    public interface IDynamicRepository
    {
        IEnumerable<DynamicBaseTableModel> DbSchemas { get; set; }
    }

    public class DynamicRepository : IDynamicRepository
    {
        public IEnumerable<DynamicBaseTableModel> DbSchemas { get; set; }

        public IDynamicProvider DynamicProviders { get; set; }

    }
}
