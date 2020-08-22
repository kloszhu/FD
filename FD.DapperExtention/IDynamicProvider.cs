using FD.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using System.Linq;

namespace FD.DapperExtention
{
    public interface IDynamicProvider
    {
        IEnumerable<DynamicBaseTableModel> GetSchema();
    }

    public enum SchemaSavedEnum
    {
        Redis,
        File,
        DB
    }
    public class DynamicProvider : IDynamicProvider
    {


        public IFileShemaRepository fileShema { get; set; }
        public SchemaSavedEnum schemaSavedEnum { get; set; } = SectionConf.Config["db:SchemaSave"] == null ? SchemaSavedEnum.File : (SchemaSavedEnum)Enum.Parse(typeof(SchemaSavedEnum), SectionConf.Config["db:SchemaSave"]);
        public IEnumerable<DynamicBaseTableModel> GetSchema()
        {
            IEnumerable<DynamicBaseTableModel> result = null;
            switch (schemaSavedEnum)
            {
                case SchemaSavedEnum.Redis:
                    break;
                case SchemaSavedEnum.File:
                    result= fileShema.GetList();
                    break;
                case SchemaSavedEnum.DB:
                    break;
                default:
                    break;
            }
            return result;
        }
    }

    public interface IFileShemaRepository
    {
        IEnumerable<DynamicBaseTableModel> GetList();
    }

    public class FileShemaRepository:IFileShemaRepository
    {
        string BathPath = Path.Combine(Directory.GetCurrentDirectory(), "SaveSchema.json");
        public IDapperRepository dapperRepository { get; set; }


        public IEnumerable<DynamicBaseTableModel> GetList()
        {
            if (File.Exists(BathPath))
            {
                string jsonFile = File.ReadAllText(BathPath);
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<DynamicBaseColumnModel>>(jsonFile);
                List<DynamicBaseTableModel> dynamics = new List<DynamicBaseTableModel>();

                return list.GroupBy(a => new { a.TABLE_SCHEMA, a.TABLE_CATALOG, a.TABLE_NAME })
                    .Select(a => new DynamicBaseTableModel
                    {
                        TABLE_CATALOG = a.Key.TABLE_CATALOG,
                        TABLE_NAME = a.Key.TABLE_NAME,
                        TABLE_SCHEMA = a.Key.TABLE_SCHEMA,
                        Colummns = list.Where(b => b.TABLE_NAME == a.Key.TABLE_NAME)
                    });
            }
            else
            {
                string sql = "SELECT TABLE_CATALOG,TABLE_SCHEMA,TABLE_NAME,COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS";
                var list = dapperRepository.DbAccess.Query<DynamicBaseColumnModel>(sql);
               var jsonFile = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                File.WriteAllText(BathPath, jsonFile);
                return list.GroupBy(a => new { a.TABLE_SCHEMA, a.TABLE_CATALOG, a.TABLE_NAME })
                    .Select(a => new DynamicBaseTableModel
                    {
                        TABLE_CATALOG = a.Key.TABLE_CATALOG,
                        TABLE_NAME = a.Key.TABLE_NAME,
                        TABLE_SCHEMA = a.Key.TABLE_SCHEMA,
                        Colummns = list.Where(b => b.TABLE_NAME == a.Key.TABLE_NAME)
                    });

            }
           
        }

        

    }

}
