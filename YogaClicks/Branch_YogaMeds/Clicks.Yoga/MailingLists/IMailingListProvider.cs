namespace Clicks.Yoga.MailingLists
{
    public interface IMailingListProvider
    {
        void Subsribe(MailingList list, string address, string forename, string surname);
        void Unsubscribe(MailingList list, string address);
        void ChangeEmailAddress(string currentAddress, string newAddress);
        void AddToGroup(MailingList list, string address, string option, string group);
    }
}