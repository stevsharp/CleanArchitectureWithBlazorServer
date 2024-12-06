﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under one or more agreements.
//     The .NET Foundation licenses this file to you under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-06
//     Last Modified: 2024-12-06
//     Description: 
//       Handles the `SupplierDeletedEvent` which occurs when a new supplier is deleted.
//       This event handler can be extended to trigger additional actions, such as 
//       sending notifications or updating other systems.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.EventHandlers;

    public class SupplierDeletedEventHandler : INotificationHandler<SupplierDeletedEvent>
    {
        private readonly ILogger<SupplierDeletedEventHandler> _logger;

        public SupplierDeletedEventHandler(
            ILogger<SupplierDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(SupplierDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
            return Task.CompletedTask;
        }
    }
