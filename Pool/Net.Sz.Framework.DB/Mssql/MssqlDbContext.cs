﻿using Net.Sz.Framework.Szlog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * 
 * @author 失足程序员
 * @Blog http://www.cnblogs.com/ty408/
 * @mail 492794628@qq.com
 * @phone 13882122019
 * 
 */
namespace Net.Sz.Framework.DB.Mssql
{
    /// <summary>
    ///
    /// <para>PS:</para>
    /// <para>@author 失足程序员</para>
    /// <para>@Blog http://www.cnblogs.com/ty408/</para>
    /// <para>@mail 492794628@qq.com</para>
    /// <para>@phone 13882122019</para>
    /// </summary>
    public class MssqlDbContext : DbContext
    {

        private static SzLogger log = SzLogger.getLogger();

        public MssqlDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MssqlDbContext, ReportingDbMigrationsConfiguration<MssqlDbContext>>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //表名为类名，不是上面带s的名字  //移除复数表名的契约
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();     //不创建EdmMetadata表  //防止黑幕交易 要不然每次都要访问 EdmMetadata这个表
        }

        public void InitializeDatabase()
        {
            try
            {
                log.Info("开始创建数据库");
                log.Info(this.Database.CreateIfNotExists() + "");
                log.Info("创建数据库完成");
            }
            catch (Exception ex)
            {
                log.Error("创建数据库失败" + ex);
            }
        }
    }
}
