using System;
using System.Collections.Generic;
using System.Text;

namespace FD.DapperExtention
{
    public class DynamicBaseTableModel
    {
        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string AliseName { get; set; }
        public string Content { get; set; }
        public string ShowContent { get; set; }
        public IEnumerable<DynamicIndexModel> Indexs { get; set; }
        public IEnumerable<DynamicBaseColumnModel> Colummns { get; set; }
    }
}
