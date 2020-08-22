using System;
using System.Collections.Generic;
using System.Text;

namespace FD.DapperExtention
{
    public interface IDynamicRepository
    {
        IEnumerable<DynamicBaseTableModel> GetSchema();
    }

    public class DynamicRepository : IDynamicRepository
    {
        public IEnumerable<DynamicBaseTableModel> DbSchemas { get; set; }

        public IDynamicProvider provider { get; set; }

        public IEnumerable<DynamicBaseTableModel> GetSchema() {
            return provider.GetSchema();
        }

    }
}
