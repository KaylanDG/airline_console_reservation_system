public class AccountsAccess : JsonHandler<AccountModel>, IJsonHandler<AccountModel>
{
    public AccountsAccess()
    {
        path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
    }
}