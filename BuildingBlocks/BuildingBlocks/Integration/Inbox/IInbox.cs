namespace BuildingBlocks.Integration.Inbox;

public interface IInbox
{
    Task<InboxMessage> GetById(Guid id);
    Task Add(InboxMessage inboxMessage);
    Task<IEnumerable<InboxMessage>> GetUnProcessedMessages();
    Task Update(InboxMessage inboxMessage);
    Task<bool> IdenticalMessageAlreadyExists(Guid integrationEventId, object toExecuteMessage);
}