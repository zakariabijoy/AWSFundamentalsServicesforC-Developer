using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Customers.Api.Messaging;

public class SQSMessenger : ISQSMessenger
{
    private readonly IAmazonSQS _sqs;
    private readonly QueueSettings _queueSettings;
    private string? _queueUrl;
    public SQSMessenger(IAmazonSQS sqs, IOptions<QueueSettings> queueSettings)
    {
        _sqs = sqs;
        _queueSettings = queueSettings.Value;
    }
    public async Task<SendMessageResponse> SendMessageAsync<T>(T message)
    {
        var queueUrl = await GetQueueUrlAsync();

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        return await _sqs.SendMessageAsync(sendMessageRequest);
    }

    private async Task<string> GetQueueUrlAsync()
    {
        if (_queueUrl is not null) 
        {
            return _queueUrl;
        }

        var queueUrlResponse =  await _sqs.GetQueueUrlAsync(_queueSettings.QueueName);
        _queueUrl = queueUrlResponse.QueueUrl;
        return _queueUrl;
    }
}
