namespace JGRFoundation.API.Helpers.Builder
{
    public interface IQueryBuilerContract
    {
        QueryBuilderConcrete Update(string tableName);
        QueryBuilderConcrete Set(string columns);
        QueryBuilderConcrete Where(string condition);
    }
}
