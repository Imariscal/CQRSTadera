namespace Catalog.API.Execution.Answers.Contracts;

public interface ISuccessfullyAnswer<T> : IAnswerBase<T> where T : class
{
}