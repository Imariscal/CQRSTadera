using Catalog.API.Execution.Answers.Contracts;
using Catalog.API.Execution.Answers;
using Catalog.CrossCutting.Exceptions;

namespace Catalog.API.Execution;

public class SafeExecutor<TObjectResult> where TObjectResult : class
{
    public static IAnswerBase<TObjectResult> Exec(Func<TObjectResult> predicate)
    {
        try
        {
            var response = predicate();

            return response is null ? throw new NotFoundException() : new SuccessfullyAnswer<TObjectResult>(response);
        }
        catch (Exception ex)
        {
            GC.SuppressFinalize(predicate);
            return new ErrorAnswer<TObjectResult>(ex);
        }
    }

    public static async Task<IAnswerBase<TObjectResult>> ExecAsync(Func<Task<TObjectResult>> predicate)
    {
        try
        {
            var response = await predicate().ConfigureAwait(false);
            return response is null
                ? throw new NotFoundException("The item was not found in the database")
                : new SuccessfullyAnswer<TObjectResult>(response);
        }
        catch (Exception ex)
        {
            GC.SuppressFinalize(predicate);
            return new ErrorAnswer<TObjectResult>(ex);
        }
    }
}

public class SafeExecutor
{
    public static IAnswerBase<object> Exec(Func<object> predicate)
    {
        try
        {
            var response = predicate();
            return new SuccessfullyAnswer<object>(response);
        }
        catch (Exception ex)
        {
            GC.SuppressFinalize(predicate);
            return new ErrorAnswer<object>(ex);
        }
    }

    public static async Task<IAnswerBase<Task>> ExecAsync(Func<Task> predicate)
    {
        try
        {
            await predicate();
            return new SuccessfullyAnswer<Task>();
        }
        catch (Exception ex)
        {
            GC.SuppressFinalize(predicate);
            return new ErrorAnswer<Task>(ex);
        }
    }
}
