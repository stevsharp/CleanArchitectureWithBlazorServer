﻿////------------------------------------------------------------------------------
//// <auto-generated>
////     This file is part of the CleanArchitecture.Blazor project.
////     Licensed to the .NET Foundation under one or more agreements.
////     The .NET Foundation licenses this file to you under the MIT license.
////     See the LICENSE file in the project root for more information.
////
////     Author: neozhu
////     Created Date: 2024-12-06
////     Last Modified: 2024-12-06
////     Description: 
////       Handles the `OfferUpdatedEvent` which occurs when a new offer is updated.
////       This event handler can be extended to trigger additional actions, such as 
////       sending notifications or updating other systems.
//// </auto-generated>
////------------------------------------------------------------------------------

//namespace CleanArchitecture.Blazor.Application.Features.Offers.EventHandlers;

//    public class OfferUpdatedEventHandler : INotificationHandler<OfferUpdatedEvent>
//    {
//        private readonly ILogger<OfferUpdatedEventHandler> _logger;

//        public OfferUpdatedEventHandler(
//            ILogger<OfferUpdatedEventHandler> logger
//            )
//        {
//            _logger = logger;
//        }
//        public Task Handle(OfferUpdatedEvent notification, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
//            return Task.CompletedTask;
//        }
//    }
