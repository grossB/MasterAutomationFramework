using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace MasterAutomationFramework.SeleniumAPI.Kendo
{
    // https://demos.telerik.com/aspnet-mvc/grid
    public partial class KendoGrid
    {
        //GetGridReference
        public string GridElement => $"var grid = $('#{this.gridId}').data('kendoGrid');";
        private readonly string gridId;
        private readonly IJavaScriptExecutor jsExecutor;

        public KendoGrid(IWebDriver driver, string gridDiv)
        {
            this.gridId = gridDiv;
            this.jsExecutor = (IJavaScriptExecutor)driver;
        }

        public void RemoveFilters()
        {
            this.jsExecutor.ExecuteScript($"{GridElement} grid.dataSource.filter([]);");
        }

        public int TotalNumberRows()
        {
            return Convert.ToInt32(this.jsExecutor.ExecuteScript($"{GridElement} return grid.dataSource.total();").ToString());
        }

        public void Reload()
        {
            this.jsExecutor.ExecuteScript($"{GridElement} grid.dataSource.read();");
        }

        public int GetPageSize()
        {
            return Convert.ToInt32(this.jsExecutor.ExecuteScript($"{GridElement} return grid.dataSource.pageSize();").ToString());
        }

        public void ChangePageSize(int newSize)
        {
            this.jsExecutor.ExecuteScript($"{GridElement} grid.dataSource.pageSize({newSize});");
        }

        public void NavigateToPage(int pageNumber)
        {
            this.jsExecutor.ExecuteScript($"{GridElement} grid.dataSource.page({pageNumber});");
        }

        public void Sort(string columnName, FilterOrderType sortType)
        {
            this.jsExecutor.ExecuteScript($"{GridElement} grid.dataSource.sort({{field: '{columnName}', dir: '{sortType.ToString()}'}});");
        }

        public List<T> GetItems<T>() where T : class
        {
            var jsResults = this.jsExecutor.ExecuteScript($"{GridElement} return JSON.stringify(grid.dataSource.data());");
            var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        //public void Filter(string columnName, FilterableOperators filterOperator, string filterValue)
        //{
        //    this.Filter(new GridFilter(columnName, filterOperator, filterValue));
        //}
        //public void Filter(params GridFilter[] gridFilters)
        //{
        //}

        public int GetCurrentPageNumber()
        {
            int pageNumber = Convert.ToInt32(this.jsExecutor.ExecuteScript($"{GridElement} return grid.dataSource.page();").ToString());
            return pageNumber;
        }
        
        private string GetKendoFilter(FilterableOperators filterType)
        {
            string operatorValue;
            switch (filterType)
            {
                case FilterableOperators.EqualTo:
                    operatorValue = "eq";
                    break;
                case FilterableOperators.NotEqualTo:
                    operatorValue = "neq";
                    break;
                case FilterableOperators.IsNull:
                    operatorValue = "Null";
                    break;
                case FilterableOperators.IsNotNull:
                    operatorValue = "Not null";
                    break;
                case FilterableOperators.IsEmpty:
                    operatorValue = "Empty";
                    break;
                case FilterableOperators.IsNotEmpty:
                    operatorValue = "Not empty";
                    break;
                case FilterableOperators.StartsWith:
                    operatorValue = "startswith";
                    break;
                case FilterableOperators.DoesNotStartsWith:
                    operatorValue = "Does not start";
                    break;
                case FilterableOperators.Contains:
                    operatorValue = "contains";
                    break;
                case FilterableOperators.NotContains:
                    operatorValue = "doesnotcontain";
                    break;
                case FilterableOperators.EndsWith:
                    operatorValue = "endswith";
                    break;
                case FilterableOperators.DoesNotEndsWith:
                    operatorValue = "Does not end";
                    break;
                case FilterableOperators.GreaterThan:
                    operatorValue = "gt";
                    break;
                case FilterableOperators.GreaterThanOrEqualTo:
                    operatorValue = "gte";
                    break;
                case FilterableOperators.LessThan:
                    operatorValue = "lt";
                    break;
                case FilterableOperators.LessThanOrEqualTo:
                    operatorValue = "lte";
                    break;
                default:
                    throw new Exception("Incorrect Filter Operator");
            }

            return operatorValue;
        }

        //var filters = new List<Tuple<string, string, string>>
        //                  {
        //                      new Tuple<string, string, string>(
        //                          "Name",
        //                          "contains",
        //                          "value")
        //                  };
        //this.FilterGrid("configurationFilesGridTestSetupConfiguration", filters);

        public void FilterGrid(string Item1, FilterableOperators Item2, string Item3)
        {
            var prefix = $"return $('#{gridId}').data('kendoGrid').dataSource.filter([";
            var scriptFilters = string.Empty;
            var suffix = "])";

                scriptFilters +=
                 "{ " +
                        $"'field':'{Item1}', " +
                        $"'operator':'{GetKendoFilter(Item2)}', " +
                        $"'value':'{Item3}'" +
                 "},";

            jsExecutor.ExecuteScript(prefix + scriptFilters + suffix);
        }

        public void FilterGrid(List<Tuple<string, FilterableOperators, string>> filters)
        {
            var prefix = $"return $('#{gridId}').data('kendoGrid').dataSource.filter([";
            var scriptFilters = string.Empty;
            var suffix = "])";

            foreach (var filter in filters)
            {
                scriptFilters += 
                 "{ " +
                    "{ " +
                        $"'field':'{filter.Item1}', " +
                        $"'operator':'{GetKendoFilter(filter.Item2)}', " +
                        $"'value':'{filter.Item3}'" +
                    "} " +
                 "},";
            }

            jsExecutor.ExecuteScript(prefix + scriptFilters + suffix);
        }
    }
}