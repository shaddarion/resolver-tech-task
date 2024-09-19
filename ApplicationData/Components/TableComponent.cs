using AutomationFramework.Controls;
using AutomationFramework.Controls.Interfaces;
using OpenQA.Selenium;

namespace ApplicationData.Components
{
    public class TableComponent(IWebDriver driver, By locator)
    {
        private IWebDriver _driver = driver;
        private Control Table => new(_driver, locator);

        private int TableColumnsCount => Table.FindElements(By.XPath("./thead/tr/th"), Table.Element).Count;
        private int TableRowsCount => Table.FindElements(By.XPath("./tbody/tr"), Table.Element).Count;

        public string GetCellText(int row, int column)
        {
            row++;
            column++;

            if (row > TableRowsCount)
            {
                throw new Exception($"Table has only {TableRowsCount} rows. Please provide a correct row number starting from 0.");
            }

            if (column > TableColumnsCount)
            {
                throw new Exception($"Table has only {TableColumnsCount} columns. Please provide a correct column number starting from 0.");
            }

            ILabel cell = new Label(_driver, By.XPath($"//table[contains(@class, 'table')]/tbody/tr[{row}]/td[{column}]"));
            return cell.Text;
        }
    }
}
