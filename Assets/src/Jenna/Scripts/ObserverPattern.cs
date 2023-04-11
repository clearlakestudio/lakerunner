public interface IPublisher
{
    void Subscribe(ISubscriber subscriber);
    void Unsubscribe(ISubscriber subscriber);
    void Notify();
}

public interface ISubscriber
{
    void Update(IPublisher publisher);
}
