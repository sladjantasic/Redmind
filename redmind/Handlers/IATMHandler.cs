namespace RedmindATM
{
    public interface IATMHandler
    {
        bool CheckForSufficientBalance(int transaction);
        int GetATMBalance();
        WithdrawalResult WithdrawCash(int amount);
    }
}