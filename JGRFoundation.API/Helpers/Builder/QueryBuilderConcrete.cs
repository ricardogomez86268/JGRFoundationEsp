using JGRFoundation.Shared.DTOs;
using JGRFoundation.Shared.Entities;
using System.Xml.Linq;

namespace JGRFoundation.API.Helpers.Builder
{
    public class QueryBuilderConcrete : IQueryBuilerContract
    {
        private string tableName;
        private List<string> columns;
        private string condition;

        public QueryBuilderConcrete()
        {
            columns = new List<string>();
        }

        public QueryBuilderConcrete Update(string tableName)
        {
            this.tableName = tableName;
            return this;
        }
        public QueryBuilderConcrete Set(string columns)
        {
            this.columns.Add(columns);
            return this;
        }

        public QueryBuilderConcrete Where(string condition)
        {
            this.condition = condition;
            return this;
        }

        public QueryDTO Build()
        {
            var QueryDTO = new QueryDTO();
            QueryDTO.Query = $"UPDATE {tableName} SET {string.Join(", ", columns)} WHERE {condition}";
            return QueryDTO;
        }
    }
}
