using System;
using System.Collections.Generic;
using System.Text;

namespace FD.DapperExtention
{
    public class DynamicBaseColumnModel
    {
        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string AliseName { get; set; }
        public string DBName { get; set; }
        public string DATA_TYPE { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsNull { get; set; }
        public object DefaultValue { get; set; }
        public string DBContent { get; set; }
        public string ShowContent { get; set; }
    }
}
