namespace App.Application.Core;

/// <summary>
/// Represents the result of an operation with an associated value or error message.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class OperationResult<T>
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the value associated with the result.
    /// </summary>
    public T ResultValue { get; set; }

    /// <summary>
    /// Gets or sets the error message associated with the result.
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the operation is unauthorized.
    /// </summary>
    public bool IsUnauthorized { get; set; }

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <returns>A new instance of <see cref="OperationResult{T}"/> representing a successful result.</returns>
    public static OperationResult<T> Success(T value) =>
        new OperationResult<T> { IsSuccessful = true, ResultValue = value };

    /// <summary>
    /// Creates a failed result with the specified error message.
    /// </summary>
    /// <param name="error">The error message associated with the result.</param>
    /// <returns>A new instance of <see cref="OperationResult{T}"/> representing a failed result.</returns>
    public static OperationResult<T> Failure(string error) =>
        new OperationResult<T> { IsSuccessful = false, ErrorMessage = error };

    /// <summary>
    /// Creates an unauthorized result with the specified error message.
    /// </summary>
    /// <param name="error">The error message associated with the result.</param>
    /// <returns>A new instance of <see cref="OperationResult{T}"/> representing an unauthorized result.</returns>
    public static OperationResult<T> Unauthorized(string error) =>
        new OperationResult<T> { IsUnauthorized = true, ErrorMessage = error };
}
