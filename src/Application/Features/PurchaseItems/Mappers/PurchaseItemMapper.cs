
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.PurchaseItems.Mappers;

#pragma warning disable RMG020
#pragma warning disable RMG012
[Mapper]
public static partial class PurchaseItemMapper
{
    public static partial PurchaseItemDto ToDto(PurchaseItem source);
    //public static partial PurchaseItem FromDto(PurchaseItemDto dto);

    public static partial AddEditPurchaseItemCommand ToCommand(PurchaseItemDto dto);

    public static partial PurchaseItem FromEditCommand(AddEditPurchaseItemCommand command);
    //public static partial PurchaseItem FromCreateCommand(CreatePurchaseItemCommand command);
    //public static partial UpdatePurchaseItemCommand ToUpdateCommand(PurchaseItemDto dto);
    //public static partial AddEditPurchaseItemCommand CloneFromDto(PurchaseItemDto dto);
    //public static partial void ApplyChangesFrom(UpdatePurchaseItemCommand source, PurchaseItem target);
    public static partial void ApplyChangesFrom(AddEditPurchaseItemCommand source, PurchaseItem target);
    public static partial IQueryable<PurchaseItemDto> ProjectTo(this IQueryable<PurchaseItem> q);
}

