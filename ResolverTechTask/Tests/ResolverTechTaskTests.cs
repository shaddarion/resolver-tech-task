using Allure.NUnit.Attributes;
using Allure.NUnit;
using ApplicationData.Pages;
using AutomationFramework.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;

namespace ResolverTechTask.Tests
{
    [AllureNUnit]
    [AllureSuite("UI Tests")]
    public class ResolverTechTaskTests : BaseTest
    {
        private TaskPage TaskPage => Pages.TaskPage.Value;

        [SetUp]
        public void Setup()
        {
            TaskPage.Open();
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var email = Fake.Person.Email;
            var password = Fake.Internet.Password();

            // Assert
            using (new AssertionScope("Make sure both the email address and password inputs are present as well as the login button"))
            {
                TaskPage.TaskOne.IsEmailInputPresent().Should().BeTrue("Email field does not exist on a page");
                TaskPage.TaskOne.IsPasswordInputPresent().Should().BeTrue("Password field does not exist on a page");
                TaskPage.TaskOne.IsSignInButtonPresent().Should().BeTrue("Sign in button does not exist on a page");
            }

            // Act
            TaskPage.TaskOne
                .InputEmail(email)
                .InputPassword(password);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            const string SecondListItemValue = "List Item 2";
            const string SecondListItemBadgeValue = "6";

            // Act
            var listElements = TaskPage.TaskTwo.TakeListElements();

            // Assert
            listElements.Count.Should().Be(3, "There are not three values in the listgroup");

            using (new AssertionScope("Make sure the second list item has correct text and badge value"))
            {
                listElements[1].Text.Should().Be(SecondListItemValue, "The second list item text is incorrect");
                listElements[1].BadgeText.Should().Be(SecondListItemBadgeValue, "The second list item badge value is incorrect");
            }
        }

        [Test]
        public void Test3()
        {
            // Arrange
            const string DefaultDropdownValue = "Option 1";
            const string OptionToSelect = "Option 3";

            // Assert
            TaskPage.TaskThree.GetDropDownValue().Should().Be(DefaultDropdownValue, "The default dropdown value is not correct");

            // Act
            TaskPage.TaskThree.SelectDropDownValue(OptionToSelect);

            // Assert
            TaskPage.TaskThree.GetDropDownValue().Should().Be(OptionToSelect, "It is not possible to select new option from dropdown");
        }

        [Test]
        public void Test4()
        {
            // Assert
            using (new AssertionScope("Make sure left and right buttons are in correct state"))
            {
                TaskPage.TaskFour.IsLeftButtonEnabled().Should().BeTrue("Left button is not enabled");
                TaskPage.TaskFour.IsRightButtonEnabled().Should().BeFalse("Right button is enabled");
            }
        }

        [Test]
        public void Test5()
        {
            // Arrange
            const string ExpectedMessageText = "You clicked a button!";
            const int ApproximateButtonWaitTime = 10;

            // Assert
            var result = WaitHelper.WaitUntilCondition(() => TaskPage.TaskFive.IsButtonDisplayed(), TimeSpan.FromSeconds(ApproximateButtonWaitTime));
            result.Should().BeTrue("Button is not displayed");

            // Act
            TaskPage.TaskFive.ClickButton();

            // Assert
            using (new AssertionScope("Make sure success message displayed and has correct text"))
            {
                TaskPage.TaskFive.IsSuccessMessageDisplayed().Should().BeTrue("Success message is not displayed");
                TaskPage.TaskFive.GetMessageText().Should().Be(ExpectedMessageText, "Success message text is incorrect");
            }
        }

        [Test]
        public void Test6()
        {
            // Arrange
            const string ExpectedCellText = "Ventosanzap";
            const int RowIndex = 2;
            const int ColumnIndex = 2;

            // Act
            var cellValue = TaskPage.TaskSix.Table.GetCellText(RowIndex, ColumnIndex);

            // Assert
            cellValue.Should().Be(ExpectedCellText, "Cell value is incorrect");
        }
    }
}