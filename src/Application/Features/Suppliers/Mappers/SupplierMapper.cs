
using CleanArchitecture.Blazor.Application.Features.Suppliers.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Suppliers.Commands.Create;
using CleanArchitecture.Blazor.Application.Features.Suppliers.Commands.Update;
using CleanArchitecture.Blazor.Application.Features.Suppliers.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.Mappers;

#pragma warning disable RMG020
#pragma warning disable RMG012
[Mapper]
public static partial class SupplierMapper
{
    public static partial SupplierDto ToDto(Supplier source);
    public static partial Supplier FromDto(SupplierDto dto);
    public static partial Supplier FromEditCommand(AddEditSupplierCommand command);

    public static partial AddEditSupplierCommand MapToEditCommand(SupplierDto command);

    public static partial Supplier FromCreateCommand(CreateSupplierCommand command);
    public static partial UpdateSupplierCommand ToUpdateCommand(SupplierDto dto);
    public static partial AddEditSupplierCommand CloneFromDto(SupplierDto dto);
    public static partial void ApplyChangesFrom(UpdateSupplierCommand source, Supplier target);
    public static partial void ApplyChangesFrom(AddEditSupplierCommand source, Supplier target);
    public static partial IQueryable<SupplierDto> ProjectTo(this IQueryable<Supplier> q);
}

