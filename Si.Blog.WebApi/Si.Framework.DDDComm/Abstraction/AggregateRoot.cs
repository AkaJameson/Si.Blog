using MediatR;

namespace Si.Framework.DDDComm.Abstraction
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : IEquatable<TId>
    {
        private readonly IMediator _mediator;
        public AggregateRoot(IMediator mediator)
        {
            _mediator = mediator;
        }
        public virtual void Publish(INotification notification)
        {
            _mediator.Publish(notification);
        }
    }
}
