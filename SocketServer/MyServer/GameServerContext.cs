using MyServer.Model;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GameServerContext : DbContext
    {
        //Enable-Migrations  Add-Migration Init    Update-Database 
        public GameServerContext()
            : base("name=GameServerContext")
        {
        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Result> Results { get; set; }

        /// <summary>
        /// OnModelCreating方法中的modelBuilder.Conventions.Remove语句禁止表名称正在多元化。如果你不这样做，所生成的表将命名为Students、Courses和Enrollments。相反，表名称将是Student、Course和Enrollment。开发商不同意关于表名称应该多数。本教程使用的是单数形式，但重要的一点是，您可以选择哪个你更喜欢通过包括或省略这行代码的形式。
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
