namespace BuildingBlocks.Integration.Inbox;

public interface IInbox
{
    Task Add(InboxMessage inboxMessage);

    Task<IEnumerable<InboxMessage>> GetUnProcessedMessages();

    Task Update(InboxMessage inboxMessage);
}