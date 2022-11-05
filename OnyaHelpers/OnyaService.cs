using System;
using System.Collections.Generic;
using System.Text;

namespace OnyaServices
{
   public class OnyaService
    {
        public static string ConnectionString { get; set; }
        public static DatabaseType DatabaseType { get; set; }
    }

    public enum DatabaseType
    {
        SQL,
        PostgreSQL,
        MySQL
    }
}
