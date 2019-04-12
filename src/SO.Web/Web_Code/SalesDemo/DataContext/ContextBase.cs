using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DevExpress.Internal;

public partial class ContextBase : DbContext {

    public ContextBase(string connectionString) : base(GetPathedConnectionString(connectionString)) { }

    static string GetPathedConnectionString(string connectionString) {
        string sqlExpressString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        return DbEngineDetector.PatchConnectionString(sqlExpressString);
    }
}
