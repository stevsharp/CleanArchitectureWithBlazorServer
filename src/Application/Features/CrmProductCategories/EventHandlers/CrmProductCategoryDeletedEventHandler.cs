﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under one or more agreements.
//     The .NET Foundation licenses this file to you under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-02
//     Last Modified: 2024-12-02
//     Description: 
//       Handles the `CrmProductCategoryDeletedEvent` which occurs when a new crmproductcategory is deleted.
//       This event handler can be extended to trigger additional actions, such as 
//       sending notifications or updating other systems.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanArchitecture.Blazor.Application.Features.CrmProductCategories.EventHandlers;

    public class CrmProductCategoryDeletedEventHandler : INotificationHandler<CrmProductCategoryDeletedEvent>
    {
        private readonly ILogger<CrmProductCategoryDeletedEventHandler> _logger;

        public CrmProductCategoryDeletedEventHandler(
            ILogger<CrmProductCategoryDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(CrmProductCategoryDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
            return Task.CompletedTask;
        }
    }
