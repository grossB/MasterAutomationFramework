namespace MasterAutomationFramework.SeleniumAPI.Kendo
{
    public class GridFilter
    {
        public GridFilter(string columnName, FilterableOperators filterOperator, string filterValue)
        {
            ColumnName = columnName;
            FilterOperator = filterOperator;
            FilterValue = filterValue;
        }

        public string ColumnName { get; set; }

        public FilterableOperators FilterOperator { get; set; }

        public string FilterValue { get; set; }
    }
}
