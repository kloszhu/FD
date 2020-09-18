using System;
using System.Collections.Generic;
using System.Text;

namespace FD.AutoEntity.Model
{
    public class BaseModel<TKey>: IBaseModel<TKey>
    {
        public TKey id { get; set; }
        public DateTime crdDate { get; set; }
        public DateTime? updateDate { get; set; }
        public int version { get; set; }

        public void SetUpdateDate() {
            this.updateDate = DateTime.Now;
        }
        public string ModuleName { get; set; }

        public void SetModule(string moduleName)
        {
            this.ModuleName = moduleName;
        }

        public void SetVersion()
        {
            version++;
        }
    }
}
