using FD.Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace FD.DapperExtention
{
    public interface IDynamicProvider
    {

    }

    public enum SchemaSavedEnum { 
      Redis,
      File,
      DB
    }
    public class DynamicProvider : IDynamicProvider
    {

        public IDapperRepository dapperRepository { get; set; }
        public SchemaSavedEnum schemaSavedEnum { get; set; } = SectionConf.Config["db:SchemaSave"] == null ? SchemaSavedEnum.File : (SchemaSavedEnum)Enum.Parse(typeof(SchemaSavedEnum), SectionConf.Config["db:SchemaSave"]);
        public DynamicProvider() {
            switch (schemaSavedEnum)
            {
                case SchemaSavedEnum.Redis:
                    break;
                case SchemaSavedEnum.File:
                    SchemaFile.Save();
                    break;
                case SchemaSavedEnum.DB:
                    break;
                default:
                    break;
            }
        }
    }
}
