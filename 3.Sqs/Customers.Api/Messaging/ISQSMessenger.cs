﻿using Amazon.SQS.Model;

namespace Customers.Api.Messaging;

public interface ISQSMessenger
{
    Task<SendMessageResponse> SendMessageAsync<T> (T message);
}