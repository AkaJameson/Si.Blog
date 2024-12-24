using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Si.Framework.EntityFramework.Abstraction
{
    public class BaseEntity : IDomainEvents
    {
        [NotMapped]
        private List<INotification> DoaminEventList;
        /// <summary>
        /// 获取所有领域事件
        /// </summary>
        /// <returns></returns>
        public List<INotification> GetAllDomainEvents()
        {
            return DoaminEventList;
        }
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="item">实现了INotification接口的record类</param>
        public void AddDomainEvent(INotification item)
        {
            DoaminEventList.Add(item);
        }
        /// <summary>
        /// 添加事件(前提是不存在)
        /// </summary>
        /// <param name="item">实现了INotification接口的record类</param>
        public void AddDomainEventIfNoExist(INotification item)
        {
            if (!DoaminEventList.Contains(item))
            {
                DoaminEventList.Add(item);
            }
        }
        /// <summary>
        /// 清空所有事件
        /// </summary>
        public void ClearAllDomainEvents()
        {
            DoaminEventList.Clear();
        }
    }
}
