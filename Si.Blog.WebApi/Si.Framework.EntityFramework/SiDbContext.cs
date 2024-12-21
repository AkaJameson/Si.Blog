using Microsoft.EntityFrameworkCore;

namespace Si.Framework.EntityFramework
{
    public class SiDbContext : DbContext
    {
        public SiDbContext(DbContextOptions<SiDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 异步执行事务操作
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task ExecuteTransactionAsync(Func<Task> action)
        {
            using var transaction = await Database.BeginTransactionAsync();
            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        /// <summary>
        /// 同步执行事务操作
        /// </summary>
        /// <param name="action">操作委托</param>
        public void ExecuteTransaction(Action action)
        {
            using var transaction = Database.BeginTransaction();
            try
            {
                action();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 开启一个事务（带重试机制）
        /// </summary>
        public async Task ExecuteWithRetryTransactionAsync(Func<Task> action, int retryCount = 3)
        {
            var retries = 0;
            while (true)
            {
                using var transaction = await Database.BeginTransactionAsync();
                try
                {
                    await action();
                    await transaction.CommitAsync();
                    break;
                }
                catch (Exception ex)
                {

                    await transaction.RollbackAsync();
                    if (++retries >= retryCount)
                    {
                        throw; // 超过重试次数，抛出异常
                    }
                }
            }
        }

    }
}
