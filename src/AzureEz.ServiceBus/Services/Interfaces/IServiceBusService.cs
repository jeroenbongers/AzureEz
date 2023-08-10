namespace AzureEz.ServiceBus.Services.Interfaces;

public interface IServiceBusService
{
    /// <summary>
    /// <para>Sends a message to a specified queue or topic.</para>
    /// </summary>
    /// <param name="queueOrTopicName">The queue or topic name.</param>
    /// <param name="message">The message to be sent to the service bus.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public Task SendMessageAsync(string queueOrTopicName, ServiceBusMessage message, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// <para>Sends a message to a specified queue or topic with an optional delay.</para>
    /// </summary>
    /// <param name="queueOrTopicName">The queue or topic name.</param>
    /// <param name="message">The message to be sent to the service bus. Message will be serialized as json.</param>
    /// <param name="delay">The delay as a timespan added to the current UTC time.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public Task SendMessageAsync(string queueOrTopicName, object message, TimeSpan? delay = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// <para>Sends a message to a specified queue or topic with an optional delay.</para>
    /// </summary>
    /// <param name="queueOrTopicName">The queue or topic name.</param>
    /// <param name="message">The message to be sent to the service bus.</param>
    /// <param name="delay">The delay as a timespan added to the current UTC time.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public Task SendMessageAsync(string queueOrTopicName, byte[] message, TimeSpan? delay = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// <para>Sends a message to a specified queue or topic with an optional delay.</para>
    /// </summary>
    /// <param name="queueOrTopicName">The queue or topic name.</param>
    /// <param name="message">The message to be sent to the service bus. Message will be UTF-8 encoded.</param>
    /// <param name="delay">The delay as a timespan added to the current UTC time.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public Task SendMessageAsync(string queueOrTopicName, string message, TimeSpan? delay = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// <para>Sends a message to a specified queue or topic with an optional dateTimeOffset.</para>
    /// </summary>
    /// <param name="queueOrTopicName">The queue or topic name.</param>
    /// <param name="message">The message to be sent to the service bus. Message will be serialized as json.</param>
    /// <param name="dateTimeOffset">The dateTimeOffset when the message should be available for receiving by consumers.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public Task SendMessageAsync(string queueOrTopicName, object message, DateTimeOffset? dateTimeOffset = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// <para>Sends a message to a specified queue or topic with an optional dateTimeOffset.</para>
    /// </summary>
    /// <param name="queueOrTopicName">The queue or topic name.</param>
    /// <param name="message">The message to be sent to the service bus.</param>
    /// <param name="dateTimeOffset">The dateTimeOffset when the message should be available for receiving by consumers.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public Task SendMessageAsync(string queueOrTopicName, byte[] message, DateTimeOffset? dateTimeOffset = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// <para>Sends a message to a specified queue or topic with an optional dateTimeOffset.</para>
    /// </summary>
    /// <param name="queueOrTopicName">The queue or topic name.</param>
    /// <param name="message">The message to be sent to the service bus. Message will be UTF-8 encoded.</param>
    /// <param name="dateTimeOffset">The dateTimeOffset when the message should be available for receiving by consumers.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public Task SendMessageAsync(string queueOrTopicName, string message, DateTimeOffset? dateTimeOffset = null, CancellationToken cancellationToken = default);
}