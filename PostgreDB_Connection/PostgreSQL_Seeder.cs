using System;
using System.Collections.Generic;
using System.Text;

namespace PostgreDB_Connection
{
    /// <summary>
    /// Seed de base de datos de PostgreSQL
    /// </summary>
    public class PostgreSQL_Seeder
    {
        public static void Seed(PostgreSQL_Context context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}