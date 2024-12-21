using SqlSugar;

namespace Si.Framework.SqlSugar
{
    public class Repository<T> : SimpleClient<T> where T : class, new()
    {
        public Repository(ISqlSugarClient db)
        {
            base.Context = db;
        }
        public ISqlSugarClient Db
        {
            get { return base.Context; }
        }

        public async Task UseTran(Action<Repository<T>> action)
        {
            try
            {
                await base.AsTenant().BeginTranAsync();

                action.Invoke(this);

                await base.AsTenant().CommitTranAsync();

            }
            catch (Exception)
            {
                await base.AsTenant().RollbackTranAsync();
                throw;
            }
        }
    }
}
