namespace BuildingBlocks.Integration.Inbox;

public interface IInbox
{
    Task Add(InboxMessage inboxMessage);

    Task<IEnumerable<InboxMessage>> GetUnProcessedMessages();

    Task Processed(IEnumerable<InboxMessage> messages);
    Task Update(InboxMessage message);
}