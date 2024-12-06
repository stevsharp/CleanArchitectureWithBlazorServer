﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-06
//     Last Modified: 2024-12-06
//     Description: 
//       Represents a domain event that occurs when a new supplier is updated.
//       Used to signal other parts of the system that a new supplier has been updated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanArchitecture.Blazor.Domain.Events;


    public class SupplierUpdatedEvent : DomainEvent
    {
        public SupplierUpdatedEvent(Supplier item)
        {
            Item = item;
        }

        public Supplier Item { get; }
    }

