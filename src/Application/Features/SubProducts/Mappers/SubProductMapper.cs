
using CleanArchitecture.Blazor.Application.Features.SubProducts.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.SubProducts.Commands.Create;
using CleanArchitecture.Blazor.Application.Features.SubProducts.Commands.Update;
using CleanArchitecture.Blazor.Application.Features.SubProducts.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.SubProducts.Mappers;

#pragma warning disable RMG020
#pragma warning disable RMG012
[Mapper]
public static partial class SubProductMapper
{
    public static partial SubProductDto ToDto(SubProduct source);
    public static partial SubProduct FromDto(SubProductDto dto);
    public static partial SubProduct FromEditCommand(AddEditSubProductCommand command);
    public static partial SubProduct FromCreateCommand(CreateSubProductCommand command);
    
    public static partial UpdateSubProductCommand ToUpdateCommand(SubProductDto dto);
    public static partial AddEditSubProductCommand CloneFromDto(SubProductDto dto);
    public static partial void ApplyChangesFrom(UpdateSubProductCommand source, SubProduct target);
    public static partial void ApplyChangesFrom(AddEditSubProductCommand source, SubProduct target);
    public static partial IQueryable<SubProductDto> ProjectTo(this IQueryable<SubProduct> q);
}

